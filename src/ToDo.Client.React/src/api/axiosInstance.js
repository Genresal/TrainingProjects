import axios from 'axios';

const baseURL = 'https://localhost:7084/todoitems'; // Replace with your API's base URL

const axiosInstance = axios.create({
    baseURL: baseURL,
    });

    // Inceseptors for adding auth token and so on

export default axiosInstance;