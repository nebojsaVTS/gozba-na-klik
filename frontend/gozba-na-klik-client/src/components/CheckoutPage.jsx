import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../api";
import "./CheckoutPage.scss";

const CheckoutPage = () => {
  const navigate = useNavigate();

  const [user, setUser] = useState(null);
  const [cart, setCart] = useState(null);
  const [addresses, setAddresses] = useState([]);
  const [selectedAddressId, setSelectedAddressId] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
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
  }, [navigate]);

  useEffect(() => {
    if (!user) {
      return;
    }

    const loadAddresses = async () => {
      try {
        const response = await fetch(
          `${API_BASE_URL}/addresses?userId=${user.id}`,
        );

        if (!response.ok) {
          throw new Error();
        }

        const data = await response.json();

        setAddresses(data);

        if (data.length > 0) {
          setSelectedAddressId(data[0].id);
        }
      } catch {
        setError("Greška pri učitavanju adresa.");
      } finally {
        setLoading(false);
      }
    };

    loadAddresses();
  }, [user]);

  const totalPrice =
    cart?.items.reduce(
      (total, item) => total + item.price * item.quantity,
      0,
    ) || 0;

  if (!cart) {
    return null;
  }

  return (
    <div className="checkout-container">
      <div className="checkout-content">
        <button
          type="button"
          className="checkout-back-button"
          onClick={() => navigate("/cart")}
        >
          ← Nazad na korpu
        </button>

        <h1>Završetak porudžbine</h1>

        <section className="checkout-section">
          <h2>Adresa za dostavu</h2>

          {loading && <p>Učitavanje adresa...</p>}

          {error && <p className="checkout-error">{error}</p>}

          {!loading && !error && addresses.length === 0 && (
            <div className="checkout-no-address">
              <p>Nemaš sačuvanu adresu za dostavu.</p>

              <button
                type="button"
                onClick={() =>
                  navigate("/moje-adrese", { state: { returnTo: "/checkout" } })
                }
              >
                Dodaj adresu
              </button>
            </div>
          )}

          <div className="checkout-addresses">
            {addresses.map((address) => (
              <label
                className={`checkout-address-card ${
                  selectedAddressId === address.id ? "selected" : ""
                }`}
                key={address.id}
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
                  <span>{address.city}</span>
                </div>
              </label>
            ))}
          </div>
        </section>

        <section className="checkout-section">
          <h2>Pregled porudžbine</h2>

          <p className="checkout-restaurant">{cart.restaurantName}</p>

          <div className="checkout-items">
            {cart.items.map((item) => (
              <div className="checkout-item" key={item.cartItemId}>
                <div>
                  <strong>
                    {item.quantity}x {item.name}
                  </strong>

                  {item.selectedSides?.length > 0 && (
                    <p>
                      <b>Prilozi:</b> {item.selectedSides.join(", ")}
                    </p>
                  )}

                  {item.note && (
                    <p>
                      <b>Napomena:</b> {item.note}
                    </p>
                  )}
                </div>

                <span>{item.price * item.quantity} RSD</span>
              </div>
            ))}
          </div>

          <div className="checkout-total">Ukupno: {totalPrice} RSD</div>
        </section>

        <button
          type="button"
          className="confirm-order-button"
          disabled={!selectedAddressId}
        >
          Potvrdi porudžbinu
        </button>
      </div>
    </div>
  );
};

export default CheckoutPage;
