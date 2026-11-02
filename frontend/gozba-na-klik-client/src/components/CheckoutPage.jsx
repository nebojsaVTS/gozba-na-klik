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
  const [addresses, setAddresses] = useState([]);
  const [selectedAddressId, setSelectedAddressId] = useState(null);

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
        const response = await axios.get(
          `${API_BASE_URL}/addresses?userId=${parsedUser.id}`,
        );

        setAddresses(response.data);

        if (response.data.length > 0) {
          setSelectedAddressId(response.data[0].id);
        }
      } catch (err) {
        console.error(err);
        setError("Greška prilikom učitavanja adresa.");
      } finally {
        setLoading(false);
      }
    };

    loadCheckoutData();
  }, [navigate]);

  const handleConfirmOrder = async () => {
    if (!user || !cart || !selectedAddressId) {
      setOrderError("Nedostaju podaci za kreiranje porudžbine.");
      return;
    }

    try {
      setIsSubmitting(true);
      setOrderError("");

      const orderData = {
        userId: user.id,
        restaurantId: cart.restaurantId,
        addressId: selectedAddressId,
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

      setOrderError(
        typeof err.response?.data === "string"
          ? err.response.data
          : "Došlo je do greške prilikom slanja porudžbine.",
      );
    } finally {
      setIsSubmitting(false);
    }
  };

  const totalPrice =
    cart?.items?.reduce(
      (total, item) => total + item.price * item.quantity,
      0,
    ) || 0;

  if (loading) {
    return (
      <div className="checkout-container">
        <div className="checkout-content">
          <p className="checkout-message">Učitavanje...</p>
        </div>
      </div>
    );
  }

  if (!cart) {
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
          )}
        </section>

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
          disabled={!selectedAddressId || isSubmitting}
        >
          {isSubmitting ? "Slanje porudžbine..." : "Potvrdi porudžbinu"}
        </button>
      </div>
    </div>
  );
};

export default CheckoutPage;
