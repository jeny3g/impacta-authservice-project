import axios from "axios";
import jsCookie from "js-cookie";

const api = axios.create({
  baseURL: "https://auth-service-web-app.azurewebsites.net",
});

api.interceptors.request.use(
  (config) => {
    const token = jsCookie.get("jwt_token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

export { api };
