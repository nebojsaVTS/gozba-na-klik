import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { createOrder } from "../services/orderService";
import "./CheckoutPage.scss";

const API_BASE_URL = "http://localhost:5171/api";

const CheckoutPage = () => {
  const navigate = useNavigate();

  const [user, setUser] = useState(null);
  const [cart, setCart] = useState(null);
  const [restaurant, setRestaurant] = useState(null);

  const [addresses, setAddresses] = useState([]);
  const [selectedAddressId, setSelectedAddressId] = useState(null);

  const [fulfillmentType, setFulfillmentType] = useState("");
  const [pickupPhoneNumber, setPickupPhoneNumber] = useState("");

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  const [orderError, setOrderError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  useEffect(() => {
    const loadCheckoutData = async () => {
      const storedUser = localStorage.getItem("gozbaUser");
      const storedCart = localStorage.getItem("gozbaCart");

      if (!storedUser) {
        navigate("/login");
        return;
      }

      if (!storedCart) {
        navigate("/cart");
        return;
      }

      const parsedUser = JSON.parse(storedUser);
      const parsedCart = JSON.parse(storedCart);

      if (!parsedCart.items || parsedCart.items.length === 0) {
        navigate("/cart");
        return;
      }

      setUser(parsedUser);
      setCart(parsedCart);

      try {
        const [restaurantResponse, addressesResponse] = await Promise.all([
          axios.get(`${API_BASE_URL}/restaurants/${parsedCart.restaurantId}`),
          axios.get(`${API_BASE_URL}/addresses?userId=${parsedUser.id}`),
        ]);

        const restaurantData = restaurantResponse.data;
        const addressesData = addressesResponse.data;

        setRestaurant(restaurantData);
        setAddresses(addressesData);

        if (addressesData.length > 0) {
          setSelectedAddressId(addressesData[0].id);

          if (addressesData[0].phoneNumber) {
            let phoneNumber = addressesData[0].phoneNumber;

            if (phoneNumber.startsWith("+381")) {
              phoneNumber = phoneNumber.slice(4);
            }

            setPickupPhoneNumber(phoneNumber.replace(/\D/g, "").slice(0, 9));
          }
        }

        if (restaurantData.supportsDelivery) {
          setFulfillmentType("Delivery");
        } else if (restaurantData.supportsPickup) {
          setFulfillmentType("Pickup");
        }
      } catch (err) {
        console.error(err);
        setError("Greška prilikom učitavanja podataka za porudžbinu.");
      } finally {
        setLoading(false);
      }
    };

    loadCheckoutData();
  }, [navigate]);

  const handleFulfillmentChange = (type) => {
    setFulfillmentType(type);
    setOrderError("");
  };

  const handleConfirmOrder = async () => {
    if (!user || !cart || !restaurant || !fulfillmentType) {
      setOrderError("Nedostaju podaci za kreiranje porudžbine.");
      return;
    }

    if (fulfillmentType === "Delivery" && !selectedAddressId) {
      setOrderError("Izaberite adresu za dostavu.");
      return;
    }

    if (fulfillmentType === "Pickup") {
      if (!pickupPhoneNumber) {
        setOrderError("Broj telefona je obavezan za lično preuzimanje.");
        return;
      }

      if (pickupPhoneNumber.length !== 8 && pickupPhoneNumber.length !== 9) {
        setOrderError("Broj telefona mora sadržati 8 ili 9 cifara nakon +381.");
        return;
      }
    }

    try {
      setIsSubmitting(true);
      setOrderError("");

      const orderData = {
        userId: user.id,
        restaurantId: cart.restaurantId,
        fulfillmentType,
        addressId: fulfillmentType === "Delivery" ? selectedAddressId : null,
        phoneNumber:
          fulfillmentType === "Pickup" ? `+381${pickupPhoneNumber}` : "",
        items: cart.items.map((item) => ({
          menuItemId: item.menuItemId,
          quantity: item.quantity,
          selectedSides: item.selectedSides || [],
          note: item.note || null,
        })),
      };

      const createdOrder = await createOrder(orderData);

      localStorage.removeItem("gozbaCart");

      alert(`Porudžbina #${createdOrder.id} je uspešno poslata.`);

      navigate("/kupac");
    } catch (err) {
      console.error(err);

      const responseData = err.response?.data;

      if (typeof responseData === "string") {
        setOrderError(responseData);
      } else if (responseData?.errors) {
        const validationMessages = Object.values(responseData.errors).flat();

        setOrderError(
          validationMessages[0] || "Podaci porudžbine nisu ispravni.",
        );
      } else {
        setOrderError("Došlo je do greške prilikom slanja porudžbine.");
      }
    } finally {
      setIsSubmitting(false);
    }
  };

  const totalPrice =
    cart?.items?.reduce(
      (total, item) => total + item.price * item.quantity,
      0,
    ) || 0;

  const canSubmitOrder = () => {
    if (isSubmitting || !fulfillmentType) {
      return false;
    }

    if (fulfillmentType === "Delivery" && !selectedAddressId) {
      return false;
    }

    return true;
  };

  if (loading) {
    return (
      <div className="checkout-container">
        <div className="checkout-content">
          <p className="checkout-message">Učitavanje...</p>
        </div>
      </div>
    );
  }

  if (!cart || !restaurant) {
    return null;
  }

  return (
    <div className="checkout-container">
      <div className="checkout-content">
        <div className="checkout-header">
          <button
            type="button"
            className="checkout-back-button"
            onClick={() => navigate("/cart")}
          >
            ← Nazad na korpu
          </button>

          <h2>Potvrda porudžbine</h2>

          <div className="checkout-header-placeholder" />
        </div>

        {error && <p className="checkout-message error">{error}</p>}

        <section className="checkout-section">
          <h3>Način preuzimanja</h3>

          <div className="checkout-fulfillment-options">
            {restaurant.supportsDelivery && (
              <label className="checkout-fulfillment-option">
                <input
                  type="radio"
                  name="fulfillmentType"
                  value="Delivery"
                  checked={fulfillmentType === "Delivery"}
                  onChange={() => handleFulfillmentChange("Delivery")}
                />
                <span>Dostava</span>
              </label>
            )}

            {restaurant.supportsPickup && (
              <label className="checkout-fulfillment-option">
                <input
                  type="radio"
                  name="fulfillmentType"
                  value="Pickup"
                  checked={fulfillmentType === "Pickup"}
                  onChange={() => handleFulfillmentChange("Pickup")}
                />
                <span>Lično preuzimanje</span>
              </label>
            )}
          </div>
        </section>

        {fulfillmentType === "Delivery" && (
          <section className="checkout-section">
            <h3>Adresa za dostavu</h3>

            {addresses.length === 0 ? (
              <div className="checkout-no-address">
                <p>Nemate sačuvanu adresu za dostavu.</p>

                <button
                  type="button"
                  className="add-address-button"
                  onClick={() =>
                    navigate("/moje-adrese", {
                      state: { returnTo: "/checkout" },
                    })
                  }
                >
                  Dodaj adresu
                </button>
              </div>
            ) : (
              <>
                <div className="checkout-addresses">
                  {addresses.map((address) => (
                    <label
                      key={address.id}
                      className={`checkout-address-card ${
                        selectedAddressId === address.id ? "selected" : ""
                      }`}
                    >
                      <input
                        type="radio"
                        name="deliveryAddress"
                        value={address.id}
                        checked={selectedAddressId === address.id}
                        onChange={() => setSelectedAddressId(address.id)}
                      />

                      <div>
                        <strong>{address.street}</strong>
                        <p>{address.city}</p>
                        <p>{address.phoneNumber}</p>
                      </div>
                    </label>
                  ))}
                </div>

                <button
                  type="button"
                  className="add-address-button"
                  onClick={() =>
                    navigate("/moje-adrese", {
                      state: { returnTo: "/checkout" },
                    })
                  }
                >
                  + Dodaj novu adresu
                </button>
              </>
            )}
          </section>
        )}

        {fulfillmentType === "Pickup" && (
          <section className="checkout-section">
            <h3>Lično preuzimanje</h3>

            <div className="checkout-pickup-card">
              <div className="checkout-pickup-info">
                <strong>{restaurant.name}</strong>
                <p>{restaurant.address}</p>
              </div>

              <div className="checkout-pickup-phone">
                <label>Broj telefona</label>

                <div className="phone-input-wrapper">
                  <span className="phone-prefix">+381</span>

                  <input
                    type="text"
                    inputMode="numeric"
                    maxLength={9}
                    value={pickupPhoneNumber}
                    onChange={(e) => {
                      const value = e.target.value
                        .replace(/\D/g, "")
                        .slice(0, 9);

                      setPickupPhoneNumber(value);
                      setOrderError("");
                    }}
                    placeholder="615648484"
                  />
                </div>
              </div>
            </div>
          </section>
        )}

        <section className="checkout-section">
          <h3>Pregled porudžbine</h3>

          <div className="checkout-restaurant-name">{cart.restaurantName}</div>

          <div className="checkout-items">
            {cart.items.map((item) => (
              <div className="checkout-item" key={item.cartItemId}>
                <div className="checkout-item-info">
                  <h4>
                    {item.quantity} × {item.name}
                  </h4>

                  {item.selectedSides?.length > 0 && (
                    <p>
                      <strong>Prilozi:</strong> {item.selectedSides.join(", ")}
                    </p>
                  )}

                  {item.note && (
                    <p>
                      <strong>Napomena:</strong> {item.note}
                    </p>
                  )}
                </div>

                <div className="checkout-item-price">
                  {item.price * item.quantity} RSD
                </div>
              </div>
            ))}
          </div>

          <div className="checkout-total">
            <strong>Ukupno: {totalPrice} RSD</strong>
          </div>
        </section>

        {orderError && <p className="checkout-message error">{orderError}</p>}

        <button
          type="button"
          className="confirm-order-button"
          onClick={handleConfirmOrder}
          disabled={!canSubmitOrder()}
        >
          {isSubmitting ? "Slanje porudžbine..." : "Potvrdi porudžbinu"}
        </button>
      </div>
    </div>
  );
};

export default CheckoutPage;
