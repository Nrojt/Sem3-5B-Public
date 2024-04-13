import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

export async function postResearch(researchData) {
  const url = `${import.meta.env.VITE_ASP_API_URL}research/`;

  const response = axiosInstance.post(url, researchData);

  return await response;
}
