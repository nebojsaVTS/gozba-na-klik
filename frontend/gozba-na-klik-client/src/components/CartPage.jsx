import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getRestaurantAvailability } from "../services/restaurantService";
import "./CartPage.scss";

const CartPage = () => {
  const navigate = useNavigate();

  const [cart, setCart] = useState(null);
  const [availability, setAvailability] = useState(null);
  const [loadingAvailability, setLoadingAvailability] = useState(false);

  useEffect(() => {
    const loadCart = async () => {
      const storedCart = localStorage.getItem("gozbaCart");

      if (!storedCart) {
        return;
      }

      const parsedCart = JSON.parse(storedCart);
      setCart(parsedCart);

      if (!parsedCart.restaurantId) {
        return;
      }

      try {
        setLoadingAvailability(true);

        const availabilityData = await getRestaurantAvailability(
          parsedCart.restaurantId,
        );

        setAvailability(availabilityData);
      } catch (err) {
        console.error("Greška prilikom učitavanja statusa restorana:", err);
      } finally {
        setLoadingAvailability(false);
      }
    };

    loadCart();
  }, []);

  const saveCart = (updatedCart) => {
    setCart(updatedCart);
    localStorage.setItem("gozbaCart", JSON.stringify(updatedCart));
  };

  const increaseQuantity = (cartItemId) => {
    const updatedCart = {
      ...cart,
      items: cart.items.map((item) =>
        item.cartItemId === cartItemId
          ? { ...item, quantity: item.quantity + 1 }
          : item,
      ),
    };

    saveCart(updatedCart);
  };

  const decreaseQuantity = (cartItemId) => {
    const updatedItems = cart.items
      .map((item) =>
        item.cartItemId === cartItemId
          ? { ...item, quantity: item.quantity - 1 }
          : item,
      )
      .filter((item) => item.quantity > 0);

    const updatedCart = {
      ...cart,
      items: updatedItems,
    };

    saveCart(updatedCart);
  };

  const removeItem = (cartItemId) => {
    const updatedCart = {
      ...cart,
      items: cart.items.filter((item) => item.cartItemId !== cartItemId),
    };

    saveCart(updatedCart);
  };

  const handleCloseCart = () => {
    if (cart?.restaurantId) {
      navigate(`/restaurants/${cart.restaurantId}`);
    } else {
      navigate("/restaurants");
    }
  };

  const formatTime = (time) => {
    if (!time) {
      return "";
    }

    return time.slice(0, 5);
  };

  const totalPrice =
    cart?.items.reduce(
      (total, item) => total + item.price * item.quantity,
      0,
    ) || 0;

  if (!cart || cart.items.length === 0) {
    return (
      <div className="cart-container">
        <div className="cart-content">
          <button
            type="button"
            className="cart-close-button"
            onClick={handleCloseCart}
          >
            ×
          </button>

          <div className="empty-cart">
            <h2>Korpa</h2>
            <p>Vaša korpa je prazna.</p>
          </div>
        </div>
      </div>
    );
  }

  const restaurantIsClosed = availability && !availability.isOpen;

  return (
    <div className="cart-container">
      <div className="cart-content">
        <button
          type="button"
          className="cart-close-button"
          onClick={handleCloseCart}
        >
          ×
        </button>

        <h2>Korpa</h2>

        <h3>{cart.restaurantName}</h3>

        {!loadingAvailability && availability && !availability.isOpen && (
          <div className="cart-availability closed">
            <strong>
              Restoran je trenutno zatvoren i ne prima porudžbine.
            </strong>

            {availability.openTime && availability.closeTime && (
              <span>
                Radno vreme danas: {formatTime(availability.openTime)}–
                {formatTime(availability.closeTime)}
              </span>
            )}
          </div>
        )}

        {!loadingAvailability &&
          availability?.isOpen &&
          availability.isClosingSoon && (
            <div className="cart-availability closing-soon">
              <strong>Restoran se uskoro zatvara.</strong>

              {availability.closeTime && (
                <span>
                  Radno vreme danas je do {formatTime(availability.closeTime)}.
                </span>
              )}
            </div>
          )}

        <div className="cart-items">
          {cart.items.map((item) => (
            <div className="cart-item" key={item.cartItemId}>
              <div className="cart-item-info">
                <h4>{item.name}</h4>

                <p className="cart-item-price">{item.price} RSD</p>

                {item.selectedSides?.length > 0 && (
                  <p className="cart-item-options">
                    <strong>Prilozi:</strong> {item.selectedSides.join(", ")}
                  </p>
                )}

                {item.note && (
                  <p className="cart-item-note">
                    <strong>Napomena:</strong> {item.note}
                  </p>
                )}
              </div>

              <div className="cart-item-actions">
                <button
                  type="button"
                  onClick={() => decreaseQuantity(item.cartItemId)}
                >
                  -
                </button>

                <span>{item.quantity}</span>

                <button
                  type="button"
                  onClick={() => increaseQuantity(item.cartItemId)}
                >
                  +
                </button>

                <button
                  type="button"
                  onClick={() => removeItem(item.cartItemId)}
                >
                  Ukloni
                </button>
              </div>
            </div>
          ))}
        </div>

        <div className="cart-total">
          <strong>Ukupno: {totalPrice} RSD</strong>
        </div>

        <button
          type="button"
          className="checkout-button"
          disabled={restaurantIsClosed || loadingAvailability}
          onClick={() => navigate("/checkout")}
        >
          {loadingAvailability
            ? "Provera restorana..."
            : restaurantIsClosed
              ? "Restoran je zatvoren"
              : "Nastavi sa poručivanjem"}
        </button>
      </div>
    </div>
  );
};

export default CartPage;
