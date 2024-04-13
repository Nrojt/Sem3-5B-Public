import axios from "axios";

export async function PostRegister(userData) {
  // checking if the userData has a userType property
  if (!userData.userType) {
    throw new Error("userData must have a userType property");
  }

  // get the url to send the request to
  const requestUrl =
    import.meta.env.VITE_ASP_API_URL + "register/" + userData.userType;
  console.log("making request to " + requestUrl);
  console.log(userData);

  const response = axios.post(requestUrl, userData);

  return response;
}
