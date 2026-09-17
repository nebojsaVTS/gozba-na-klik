import { useNavigate } from "react-router-dom";
import "./CustomerHomePage.scss";

const CustomerHomePage = () => {
  const navigate = useNavigate();

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
          <p className="customer-location">Šid</p>

          <button
            className="customer-logout-button"
            type="button"
            onClick={handleLogout}
          >
            Odjavi se
          </button>
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

          <button className="customer-option-card" type="button" disabled>
            <h2>Moje porudžbine</h2>
            <p>Pogledaj svoje porudžbine</p>
            <span>Uskoro</span>
          </button>
        </div>
      </div>
    </div>
  );
};

export default CustomerHomePage;
