import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CartPage.scss";

const CartPage = () => {
  const navigate = useNavigate();
  const [cart, setCart] = useState(null);

  useEffect(() => {
    const storedCart = localStorage.getItem("gozbaCart");

    if (storedCart) {
      setCart(JSON.parse(storedCart));
    }
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
          onClick={() => navigate("/checkout")}
        >
          Nastavi sa poručivanjem
        </button>
      </div>
    </div>
  );
};

export default CartPage;
