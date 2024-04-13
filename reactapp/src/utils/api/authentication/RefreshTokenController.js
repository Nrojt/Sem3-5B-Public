/*
 * Controller for the refresh bearer token endpoint
 * This controller is responsible for refreshing the access token
 * It is called when the access token is expired and the user wants to continue
 * using the application It is also called when the user wants to refresh the
 * access token before it expires The refresh token is used to get a new access
 * token The refresh token is not refreshed. This means the user has to login
 * again after the refresh token expires
 */

// Imports
import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

// refresh token function to post user data to the server
export async function PostRefreshBearerToken(userType) {
  console.log("making request to /refreshbearertoken");
  // importing the url from the .env file
  const response = axiosInstance.post(
    import.meta.env.VITE_ASP_API_URL +
      "refresh/refreshbearertoken?useCookies=true",
    // sending the user type to the server
    { userType: userType },
  );
  // returning the response
  return response;
}
