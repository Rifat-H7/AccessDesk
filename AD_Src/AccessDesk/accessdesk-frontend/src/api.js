import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5000/api/auth/", // match your working Postman URL
  withCredentials: true // so cookies/tokens work if needed
});

export default api;
