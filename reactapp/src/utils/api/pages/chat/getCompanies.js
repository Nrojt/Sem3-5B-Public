import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

export async function getCompanies() {
  const url = `${import.meta.env.VITE_ASP_API_URL}company/all`;

  const response = axiosInstance.get(url);

  return await response;
}
