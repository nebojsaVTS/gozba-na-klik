import axios from "axios";

const API_URL = "http://localhost:5171/api/Orders";

export const createOrder = async (orderData) => {
  const response = await axios.post(API_URL, orderData);
  return response.data;
};
