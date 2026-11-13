import { useEffect, useState } from "react";
import {
  getRestaurants,
  getRestaurantAvailability,
} from "../services/restaurantService";
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
        const restaurantData = await getRestaurants();

        const restaurantsWithAvailability = await Promise.all(
          restaurantData.map(async (restaurant) => {
            try {
              const availability = await getRestaurantAvailability(
                restaurant.id,
              );

              return {
                ...restaurant,
                availability,
              };
            } catch (availabilityError) {
              console.error(
                `Greška pri učitavanju dostupnosti restorana ${restaurant.id}:`,
                availabilityError,
              );

              return {
                ...restaurant,
                availability: null,
              };
            }
          }),
        );

        setRestaurants(restaurantsWithAvailability);
      } catch (err) {
        console.error(err);
        setError("Greška prilikom učitavanja restorana.");
      } finally {
        setLoading(false);
      }
    };

    loadRestaurants();
  }, []);

  const formatTime = (time) => {
    if (!time) {
      return "";
    }

    return time.slice(0, 5);
  };

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
          {restaurants.map((restaurant) => {
            const availability = restaurant.availability;

            return (
              <div
                className="restaurant-card"
                key={restaurant.id}
                onClick={() => navigate(`/restaurants/${restaurant.id}`)}
              >
                <h3>{restaurant.name}</h3>

                {availability && !availability.isOpen && (
                  <div className="restaurant-availability closed">
                    <strong>ZATVORENO</strong>

                    {availability.openTime && availability.closeTime && (
                      <span>
                        Radno vreme danas: {formatTime(availability.openTime)}–
                        {formatTime(availability.closeTime)}
                      </span>
                    )}
                  </div>
                )}

                {availability?.isOpen && availability.isClosingSoon && (
                  <div className="restaurant-availability closing-soon">
                    <strong>USKORO ZATVARA</strong>

                    {availability.closeTime && (
                      <span>
                        Radno vreme danas: do{" "}
                        {formatTime(availability.closeTime)}
                      </span>
                    )}
                  </div>
                )}

                <p>Pogledaj jelovnik</p>
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
};

export default CustomerRestaurants;
