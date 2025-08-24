import axios from "axios";

const api = axios.create({
   baseURL: "http://localhost:5020",
});

export const getCafes = (location) =>
  location ? api.get(`/cafes?location=${location}`) : api.get(`/cafes`);

export const createCafe = (data) => api.post(`/cafes`, data);
export const updateCafe = (data) => api.put(`/cafes`, data);
export const deleteCafe = (id) => api.delete(`/cafes/${id}`);



export const getEmployees = (cafe) =>
  cafe ? api.get(`/employees?cafe=${cafe}`) : api.get(`/employees`);

export const createEmployee = (data) => api.post("/employees", data);
export const updateEmployee = (data) =>
  api.put("/employees", data);

export const deleteEmployee = (id) => api.delete(`/employees/${id}`);
