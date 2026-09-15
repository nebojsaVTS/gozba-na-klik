import { API_BASE_URL } from "../api";

export const getRestaurants = async () => {
    const response = await fetch(`${API_BASE_URL}/restaurants`);

    if (!response.ok) {
        throw new Error("Failed to load restaurants.");
    }

    return await response.json();
};

export const getRestaurantById = async (restaurantId) => {
    const response = await fetch(`${API_BASE_URL}/restaurants/${restaurantId}`);

    if (!response.ok) {
        throw new Error("Failed to load restaurant.")
    }

    return await response.json();
}; 