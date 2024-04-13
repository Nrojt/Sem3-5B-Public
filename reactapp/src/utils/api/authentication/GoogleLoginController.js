import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

// login function to post user data to the server
export function PostGoogleLogin(credentials) {
  console.log(
    "making request to " +
      import.meta.env.VITE_ASP_API_URL +
      "googlelogin?useCookies=true",
  );

  // importing the url from the .env file
  const response = axiosInstance.post(
    import.meta.env.VITE_ASP_API_URL + "googlelogin?useCookies=true",
    { jwtToken: credentials.credential }, // Convert credentials to JSON format
  );
  return response;
}
