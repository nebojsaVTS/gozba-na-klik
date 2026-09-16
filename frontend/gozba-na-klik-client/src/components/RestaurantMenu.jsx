import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getMenuItemsByRestaurant } from "../services/menuItemService";
import { getRestaurantById } from "../services/restaurantService";
import "./RestaurantMenu.scss";

const RestaurantMenu = () => {
  const { restaurantId } = useParams();
  const [restaurant, setRestaurant] = useState(null);
  const [menuItems, setMenuItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const loadRestaurantData = async () => {
      try {
        const restaurantData = await getRestaurantById(restaurantId);
        const menuItemsData = await getMenuItemsByRestaurant(restaurantId);

        setRestaurant(restaurantData);
        setMenuItems(menuItemsData);
      } catch {
        setError("Greška prilikom učitavanja jelovnika.");
      } finally {
        setLoading(false);
      }
    };

    loadRestaurantData();
  }, [restaurantId]);

  return (
    <div className="menu-container">
      <div className="menu-content">
        {loading && <p className="menu-message">Učitavanje jelovnika...</p>}

        <h2>{restaurant ? restaurant.name : "Jelovnik"}</h2>

        {!loading && !error && menuItems.length === 0 && (
          <p className="menu-message">
            Ovaj restoran trenutno nema dostupnih jela.
          </p>
        )}

        {error && <p className="menu-message error">{error}</p>}

        <div className="menu-grid">
          {menuItems.map((menuItem) => (
            <div className="menu-item-card" key={menuItem.id}>
              <h3>{menuItem.name}</h3>
              <p className="menu-item-description">{menuItem.description}</p>
              <p className="menu-item-price">{menuItem.price} RSD</p>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default RestaurantMenu;
