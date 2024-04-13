import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

export async function putUser(userdata) {
  const url = `${import.meta.env.VITE_ASP_API_URL}${userdata.userType}/`;
  console.log("Updating user data: ", userdata);

  const response = axiosInstance.put(url, userdata);

  return await response;
}
