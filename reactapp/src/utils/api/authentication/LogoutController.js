import axios from "axios";

// file for the logout api endpoint
// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

export async function PostLogout() {
  console.log("making request to /logout");
  const response = axiosInstance.post(
    import.meta.env.VITE_ASP_API_URL + "logout?useCookies=true",
  );
  return response;
}
