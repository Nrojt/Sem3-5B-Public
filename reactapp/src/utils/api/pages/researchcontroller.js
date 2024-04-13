import axios from "axios";

// Custom Axios instance
const axiosInstance = axios.create({
  withCredentials: true, // required to handle cookies
});

export async function getAllResearches() {
  console.log("Calling getAllResearches");
  const response = await axiosInstance.get(
    import.meta.env.VITE_ASP_API_URL + "Research",
  );
  return response;
}

export async function getResearchById(id) {
  console.log("Calling getResearchById");
  const response = await axiosInstance.get(
    import.meta.env.VITE_ASP_API_URL + "Research/" + id,
  );
  return response;
}

export async function applyUser(researchId) {
  const url =
    import.meta.env.VITE_ASP_API_URL + "applyresearch?researchId=" + researchId;
  console.log("Applying user to research: ", researchId);

  try {
    const response = axiosInstance.put(url);

    return response;
  } catch (error) {
    console.error("Error applying user to research:", error);
    throw error;
  }
}
