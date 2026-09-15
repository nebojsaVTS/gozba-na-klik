import { useEffect, useState } from "react";
import { getRestaurants } from "../services/restaurantService";
import { useNavigate } from "react-router-dom";

const CustomerRestaurants = () => {
  const [restaurants, setRestaurants] = useState([]);
  const [error, setError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const loadRestaurants = async () => {
      try {
        const data = await getRestaurants();
        setRestaurants(data);
      } catch {
        setError("Greška prilikom učitavanja restorana.");
      }
    };
    loadRestaurants();
  }, []);

  return (
    <div>
      <h2>Restorani</h2>
      {error && <p>{error}</p>}

      {restaurants.map((restaurant) => (
        <div
          key={restaurant.id}
          onClick={() => navigate(`/restaurants/${restaurant.id}`)}
        >
          <h3>{restaurant.name}</h3>
        </div>
      ))}
    </div>
  );
};

export default CustomerRestaurants;
