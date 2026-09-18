import { API_BASE_URL } from "../api";

export const getMenuItemsByRestaurant = async (restaurantId) => {
    const response = await fetch(
        `${API_BASE_URL}/menuitems/restaurant/${restaurantId}`
    );

    if (!response.ok) {
        throw new Error("Failed to load menu items.");
    }

    return await response.json();
};