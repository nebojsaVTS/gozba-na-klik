import { useNavigate } from "react-router-dom";
import "./CustomerHomePage.scss";

const CustomerHomePage = () => {
  const navigate = useNavigate();

  const storedUser = localStorage.getItem("gozbaUser");
  const user = storedUser ? JSON.parse(storedUser) : null;

  const storedCart = localStorage.getItem("gozbaCart");
  const cart = storedCart ? JSON.parse(storedCart) : null;

  const cartItemCount =
    cart?.items?.reduce((total, item) => total + item.quantity, 0) || 0;

  const handleLogout = () => {
    localStorage.removeItem("gozbaUser");

    navigate("/login", {
      state: { logoutMessage: "Uspešno ste se odjavili." },
    });
  };

  return (
    <div className="customer-home-container">
      <div className="customer-home-content">
        <div className="customer-home-header">
          <div className="customer-user">
            <span className="customer-user-icon">👤</span>

            <span
              className="customer-username"
              title={user?.username || "Kupac"}
            >
              {user?.username || "Kupac"}
            </span>
          </div>

          <p className="customer-location">Šid</p>

          <div className="customer-header-actions">
            {cartItemCount > 0 && (
              <button
                className="customer-cart-button"
                type="button"
                onClick={() => navigate("/cart")}
              >
                Korpa ({cartItemCount})
              </button>
            )}

            <button
              className="customer-logout-button"
              type="button"
              onClick={handleLogout}
            >
              Odjavi se
            </button>
          </div>
        </div>

        <h1>Gozba na klik</h1>
        <p className="customer-subtitle">Šta želiš danas?</p>

        <div className="customer-options">
          <button
            className="customer-option-card"
            type="button"
            onClick={() => navigate("/restaurants")}
          >
            <h2>Restorani</h2>
            <p>Pronađi restoran i pogledaj jelovnik</p>
          </button>

          <button
            className="customer-option-card"
            type="button"
            onClick={() => navigate("/moje-adrese")}
          >
            <h2>Moje adrese</h2>
            <p>Upravljaj adresama za dostavu</p>
          </button>

          <button
            className="customer-option-card"
            type="button"
            onClick={() => navigate("/moje-porudzbine")}
          >
            <h2>Moje porudžbine</h2>
            <p>Pogledaj svoje porudžbine</p>
          </button>
        </div>
      </div>
    </div>
  );
};

export default CustomerHomePage;
