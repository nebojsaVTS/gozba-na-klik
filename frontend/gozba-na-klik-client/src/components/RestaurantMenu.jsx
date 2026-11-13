import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getMenuItemsByRestaurant } from "../services/menuItemService";
import {
  getRestaurantById,
  getRestaurantAvailability,
} from "../services/restaurantService";
import MenuItemCustomizationModal from "./MenuItemCustomizationModal";
import "./RestaurantMenu.scss";

const RestaurantMenu = () => {
  const { restaurantId } = useParams();
  const navigate = useNavigate();

  const [cartItemCount, setCartItemCount] = useState(() => {
    const storedCart = localStorage.getItem("gozbaCart");

    if (!storedCart) {
      return 0;
    }

    const cart = JSON.parse(storedCart);

    return cart.items.reduce((total, item) => total + item.quantity, 0);
  });

  const [restaurant, setRestaurant] = useState(null);
  const [availability, setAvailability] = useState(null);
  const [menuItems, setMenuItems] = useState([]);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [cartMessage, setCartMessage] = useState("");
  const [selectedMenuItem, setSelectedMenuItem] = useState(null);

  useEffect(() => {
    const loadRestaurantData = async () => {
      try {
        const [restaurantData, menuItemsData, availabilityData] =
          await Promise.all([
            getRestaurantById(restaurantId),
            getMenuItemsByRestaurant(restaurantId),
            getRestaurantAvailability(restaurantId),
          ]);

        setRestaurant(restaurantData);
        setMenuItems(menuItemsData);
        setAvailability(availabilityData);
      } catch (err) {
        console.error(err);
        setError("Greška prilikom učitavanja jelovnika.");
      } finally {
        setLoading(false);
      }
    };

    loadRestaurantData();
  }, [restaurantId]);

  const formatTime = (time) => {
    if (!time) {
      return "";
    }

    return time.slice(0, 5);
  };

  const handleMenuItemClick = (menuItem) => {
    if (!availability?.isOpen) {
      return;
    }

    setSelectedMenuItem(menuItem);
  };

  const handleAddToCart = (customizedItem) => {
    if (!availability?.isOpen) {
      setSelectedMenuItem(null);
      setCartMessage("Restoran je trenutno zatvoren i ne prima porudžbine.");
      return;
    }

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
        "Korpa već sadrži proizvode iz drugog restorana. Da li želite da ispraznite korpu i dodate ovo jelo?",
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

    const configurationKey = JSON.stringify({
      sides: [...customizedItem.selectedSides].sort(),
      note: customizedItem.note,
    });

    const existingItem = cart.items.find(
      (item) =>
        item.menuItemId === customizedItem.id &&
        item.configurationKey === configurationKey,
    );

    if (existingItem) {
      existingItem.quantity += 1;
    } else {
      cart.items.push({
        cartItemId: crypto.randomUUID(),
        menuItemId: customizedItem.id,
        name: customizedItem.name,
        price: customizedItem.price,
        quantity: 1,
        selectedSides: customizedItem.selectedSides,
        note: customizedItem.note,
        configurationKey,
      });
    }

    localStorage.setItem("gozbaCart", JSON.stringify(cart));

    const itemCount = cart.items.reduce(
      (total, item) => total + item.quantity,
      0,
    );

    setCartItemCount(itemCount);
    setCartMessage(`${customizedItem.name} je dodat u korpu.`);
    setSelectedMenuItem(null);
  };

  return (
    <div className="menu-container">
      <div className="menu-content">
        {loading && <p className="menu-message">Učitavanje jelovnika...</p>}

        <div className="menu-header">
          <button
            type="button"
            className="menu-back-button"
            onClick={() => navigate("/restaurants")}
          >
            ← Nazad na restorane
          </button>

          <h2>{restaurant ? restaurant.name : "Jelovnik"}</h2>

          <button
            type="button"
            className="cart-button"
            onClick={() => navigate("/cart")}
          >
            Korpa ({cartItemCount})
          </button>
        </div>

        {!loading && availability && !availability.isOpen && (
          <div className="menu-availability closed">
            <strong>
              Restoran je trenutno zatvoren i ne prima porudžbine.
            </strong>

            {availability.openTime && availability.closeTime && (
              <span>
                Radno vreme danas: {formatTime(availability.openTime)}–
                {formatTime(availability.closeTime)}
              </span>
            )}
          </div>
        )}

        {!loading && availability?.isOpen && availability.isClosingSoon && (
          <div className="menu-availability closing-soon">
            <strong>Restoran se uskoro zatvara.</strong>

            {availability.closeTime && (
              <span>
                Radno vreme danas je do {formatTime(availability.closeTime)}.
              </span>
            )}
          </div>
        )}

        {cartMessage && <p className="menu-message">{cartMessage}</p>}

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

              <button
                type="button"
                className="add-to-cart-button"
                disabled={!availability?.isOpen}
                onClick={() => handleMenuItemClick(menuItem)}
              >
                {availability?.isOpen
                  ? "Dodaj u korpu"
                  : "Restoran je zatvoren"}
              </button>
            </div>
          ))}
        </div>
      </div>

      {selectedMenuItem && availability?.isOpen && (
        <MenuItemCustomizationModal
          menuItem={selectedMenuItem}
          onClose={() => setSelectedMenuItem(null)}
          onAddToCart={handleAddToCart}
        />
      )}
    </div>
  );
};

export default RestaurantMenu;
