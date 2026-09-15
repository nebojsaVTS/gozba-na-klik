import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getMenuItemsByRestaurant } from "../services/menuItemService";
import { getRestaurantById } from "../services/restaurantService";

const RestaurantMenu = () => {
  const { restaurantId } = useParams();
  const [restaurant, setRestaurant] = useState(null);
  const [menuItems, setMenuItems] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    const loadRestaurantData = async () => {
      try {
        const restaurantData = await getRestaurantById(restaurantId);
        const menuItemsData = await getMenuItemsByRestaurant(restaurantId);

        setRestaurant(restaurantData);
        setMenuItems(menuItemsData);
      } catch {
        setError("Greška prilikom učitavanja restorana.");
      }
    };

    loadRestaurantData();
  }, [restaurantId]);

  return (
    <div>
      <h2>{restaurant ? restaurant.name : "Jelovnik"}</h2>
      {error && <p>{error}</p>}
      {menuItems.map((menuItem) => (
        <div key={menuItem.id}>
          <h3>{menuItem.name}</h3>
          <p>{menuItem.description}</p>
          <p>{menuItem.price} RSD</p>
        </div>
      ))}
    </div>
  );
};

export default RestaurantMenu;
