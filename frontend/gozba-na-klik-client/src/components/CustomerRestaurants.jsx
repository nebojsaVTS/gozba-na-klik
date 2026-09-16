import { useEffect, useState } from "react";
import { getRestaurants } from "../services/restaurantService";
import { useNavigate } from "react-router-dom";
import "./CustomerRestaurants.scss";

const CustomerRestaurants = () => {
  const [restaurants, setRestaurants] = useState([]);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

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
        <h2>Restorani</h2>

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
