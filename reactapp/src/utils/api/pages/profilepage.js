import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

export async function getProfilePageDisabilityExpert() {
  console.log("Calling getProfilePageDisabilityExpert");
  const response = await axiosInstance.get(
    import.meta.env.VITE_ASP_API_URL + "disabilityexpert",
  );
  return response;
}
