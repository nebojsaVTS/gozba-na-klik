import "./RestaurantOperatorHomePage.scss";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from "../api";

function RestaurantOperatorHomePage() {
  const navigate = useNavigate();
  const [user, setUser] = useState(null);
  const [restaurantName, setRestaurantName] = useState("");
  const [loadError, setLoadError] = useState("");

  useEffect(() => {
    const stored = localStorage.getItem("gozbaUser");
    if (!stored) {
      navigate("/login");
      return;
    }
    setUser(JSON.parse(stored));
  }, [navigate]);

  useEffect(() => {
    if (!user) {
      return;
    }

    fetch(`${API_BASE_URL}/restaurant-operator-assignments`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Greška pri učitavanju dodele.");
        }
        return response.json();
      })
      .then((data) => {
        const assignment = data.find((a) => a.userId === user.id);
        if (assignment) {
          setRestaurantName(assignment.restaurantName);
        } else {
          setLoadError("Nisi dodeljen nijednom restoranu.");
        }
      })
      .catch(() => setLoadError("Greška pri učitavanju podataka o restoranu."));
  }, [user]);

  const handleLogout = () => {
    localStorage.removeItem("gozbaUser");
    navigate("/login", {
      state: { logoutMessage: "Uspešno ste se odjavili." },
    });
  };

  if (!user) {
    return null;
  }

  return (
    <div className="operator-home-container">
      <div className="operator-home-content">
        <div className="operator-home-header">
          <h1>Gozba na klik</h1>
          <button
            className="operator-logout-button"
            type="button"
            onClick={handleLogout}
          >
            Odjavi se
          </button>
        </div>

        {restaurantName && <p className="operator-restaurant-name">{restaurantName}</p>}
        {loadError && <p className="operator-error">{loadError}</p>}

        <p className="operator-orders-count">Nove porudžbine: 0</p>

        <div className="operator-options">
          <button className="operator-option-card" type="button" disabled>
            <h2>Nove porudžbine</h2>
            <span>Uskoro</span>
          </button>

          <button className="operator-option-card" type="button" disabled>
            <h2>Aktivne porudžbine</h2>
            <span>Uskoro</span>
          </button>
        </div>
      </div>
    </div>
  );
}

export default RestaurantOperatorHomePage;
