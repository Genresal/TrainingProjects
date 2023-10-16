import axiosInstance from "./axiosInstance";

export const fetchData = async () => {
    const response = await axiosInstance.get(`/`)
    return response.data;
  };

  export const createItem = async (newItem) => {
    const response = await axiosInstance.post('/', newItem);
    return response.data;
};

export const updateItem = async (id, updatedItem) => {
    const response = await axiosInstance.put(`/${id}`, updatedItem);
    return response.data;
};

  export const deleteItem = async (id) => {
    const response = await axiosInstance.delete(`/${id}`)
    return response;
  };