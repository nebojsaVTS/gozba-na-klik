import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./CartPage.scss";

const CartPage = () => {
  const [cart, setCart] = useState(null);
  const navigate = useNavigate();

  const handleCloseCart = () => {
  if (cart?.restaurantId) {
    navigate(`/restaurants/${cart.restaurantId}`);
  } else {
    navigate("/restaurants");
  }
};

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

  const increaseQuantity = (itemId) => {
    const updatedCart = {
      ...cart,
      items: cart.items.map((item) =>
        item.id === itemId
          ? { ...item, quantity: item.quantity + 1 }
          : item
      ),
    };

    saveCart(updatedCart);
  };

  const decreaseQuantity = (itemId) => {
    const updatedItems = cart.items
      .map((item) =>
        item.id === itemId
          ? { ...item, quantity: item.quantity - 1 }
          : item
      )
      .filter((item) => item.quantity > 0);

    const updatedCart = {
      ...cart,
      items: updatedItems,
    };

    saveCart(updatedCart);
  };

  const removeItem = (itemId) => {
    const updatedCart = {
      ...cart,
      items: cart.items.filter((item) => item.id !== itemId),
    };

    saveCart(updatedCart);
  };

  const totalPrice =
    cart?.items.reduce(
      (total, item) => total + item.price * item.quantity,
      0
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
            <div className="cart-item" key={item.id}>
              <div>
                <h4>{item.name}</h4>
                <p>{item.price} RSD</p>
              </div>

              <div className="cart-item-actions">
                <button onClick={() => decreaseQuantity(item.id)}>-</button>

                <span>{item.quantity}</span>

                <button onClick={() => increaseQuantity(item.id)}>+</button>

                <button onClick={() => removeItem(item.id)}>
                  Ukloni
                </button>
              </div>
            </div>
          ))}
        </div>

        <div className="cart-total">
          <strong>Ukupno: {totalPrice} RSD</strong>
        </div>

        <button className="checkout-button">
          Nastavi sa poručivanje
        </button>
      </div>
    </div>
  );
};

export default CartPage;