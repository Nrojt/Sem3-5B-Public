import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

// login function to post user data to the server
export async function PostLogin(userData) {
  console.log("making request to /login");
  // importing the url from the .env file
  const response = axiosInstance.post(
    import.meta.env.VITE_ASP_API_URL + "login?useCookies=true",
    userData,
  );
  return response;
}
