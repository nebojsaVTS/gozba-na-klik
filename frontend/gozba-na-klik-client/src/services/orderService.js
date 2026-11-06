import axios from "axios";

const API_URL = "http://localhost:5171/api/Orders";

export const createOrder = async (orderData) => {
  const response = await axios.post(API_URL, orderData);
  return response.data;
};

export const getOrdersByUser = async (userId) => {
  const response = await axios.get(`${API_URL}/user/${userId}`);
  return response.data;
};
