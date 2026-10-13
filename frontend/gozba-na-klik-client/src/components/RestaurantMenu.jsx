import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMenuItemsByRestaurant } from "../services/menuItemService";
import { getRestaurantById } from "../services/restaurantService";
import "./RestaurantMenu.scss";

const RestaurantMenu = () => {
  const { restaurantId } = useParams();

  const navigate = useNavigate();

  const [cartItemCount, setCartItemCount] = useState(() => {
    const storedCart = localStorage.getItem("gozbaCart");

    if (!storedCart){
      return 0;
    }
  
  const cart = JSON.parse(storedCart);
  
  return cart.items.reduce(
    (total, item) => total + item.quantuty,
    0
  );
});

  const [restaurant, setRestaurant] = useState(null);
  const [menuItems, setMenuItems] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [cartMessage, setCartMessage] = useState("");

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

  const handleAddToCart = (menuItem) => {
    const storedCart = localStorage.getItem("gozbaCart");

    let cart = storedCart
      ? JSON.parse(storedCart)
      : {
          restaurantId,
          restaurantName: restaurant.name,
          items: [],
        };

    if (String(cart.restaurantId) !== String(restaurantId)) {
      const shouldReplaceCart = window.confirm(
        "Korpa već sadrži proizvode iz drugog restorana. Da li želite da ispraznite korpu i dodate ovo jelo?"
      );

      if (!shouldReplaceCart) {
        return;
      }

      cart = {
        restaurantId,
        restaurantName: restaurant.name,
        items: [],
      };
    }

    const existingItem = cart.items.find(
      (item) => item.id === menuItem.id
    );

    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      cart.items.push({
        id: menuItem.id,
        name: menuItem.name,
        price: menuItem.price,
        quantity: 1,
      });
    }

    localStorage.setItem("gozbaCart", JSON.stringify(cart));

    const itemCount = cart.items.reduce(
      (total, item) => total + item.quantity,
      0
    );

    setCartItemCount(itemCount);

    setCartMessage(`${menuItem.name} je dodat u korpu.`);
  };

  return (
    <div className="menu-container">
      <div className="menu-content">
        {loading && <p className="menu-message">Učitavanje jelovnika...</p>}

        <div className="menu-header">
        <h2>{restaurant ? restaurant.name : "Jelovnik"}</h2>

        <button type="button"
        className="cart-button"
        onClick={() => navigate("/cart")}>
          Korpa ({cartItemCount})
        </button>

        </div>
        {cartMessage && (
          <p className="menu-message">{cartMessage}</p>
        )}

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

              <p className="menu-item-description">
                {menuItem.description}
              </p>

              <p className="menu-item-price">
                {menuItem.price} RSD
              </p>

              <button
                type="button"
                className="add-to-cart-button"
                onClick={() => handleAddToCart(menuItem)}
              >
                Dodaj u korpu
              </button>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default RestaurantMenu;