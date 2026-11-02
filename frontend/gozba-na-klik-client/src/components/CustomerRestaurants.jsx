import { useEffect, useState } from "react";
import { getRestaurants } from "../services/restaurantService";
import { useNavigate } from "react-router-dom";
import "./CustomerRestaurants.scss";

const CustomerRestaurants = () => {
  const [restaurants, setRestaurants] = useState([]);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  const storedCart = localStorage.getItem("gozbaCart");
  const cart = storedCart ? JSON.parse(storedCart) : null;

  const cartItemCount =
    cart?.items?.reduce((total, item) => total + item.quantity, 0) || 0;

  useEffect(() => {
    const loadRestaurants = async () => {
      try {
        const data = await getRestaurants();
        setRestaurants(data);
      } catch {
        setError("Greška prilikom učitavanja restorana.");
      } finally {
        setLoading(false);
      }
    };

    loadRestaurants();
  }, []);

  return (
    <div className="restaurants-container">
      <div className="restaurants-content">
        <div className="restaurants-header">
          <button
            type="button"
            className="restaurants-back-button"
            onClick={() => navigate("/kupac")}
          >
            ← Nazad na početnu
          </button>

          <h2>Restorani</h2>

          {cartItemCount > 0 ? (
            <button
              type="button"
              className="restaurants-cart-button"
              onClick={() => navigate("/cart")}
            >
              Korpa ({cartItemCount})
            </button>
          ) : (
            <div className="restaurants-header-placeholder" />
          )}
        </div>

        {loading && (
          <p className="restaurants-message">Učitavanje restorana...</p>
        )}

        {error && <p className="restaurants-message error">{error}</p>}

        {!loading && !error && restaurants.length === 0 && (
          <p className="restaurants-message">
            Trenutno nema dostupnih restorana.
          </p>
        )}

        <div className="restaurants-grid">
          {restaurants.map((restaurant) => (
            <div
              className="restaurant-card"
              key={restaurant.id}
              onClick={() => navigate(`/restaurants/${restaurant.id}`)}
            >
              <h3>{restaurant.name}</h3>
              <p>Pogledaj jelovnik</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default CustomerRestaurants;
