import axios from "axios";

export const trackingAnalytics = () => {
  // create an axios instance with a base URL, base method and some common
  // options
  const axiosInstance = axios.create({
    // TODO change url to production url
    baseURL: "https://api.sem3-5b.com/api/tracking/addTracking",
    method: "post",
    headers: {
      "Content-Type": "application/json",
    },
    withCredentials: true, // this can be set to true, won't affect the request
    // using bearer token
  });

  // sendRequest is a helper function that sends a request to the api endpoint,
  // passing in the url and options
  const sendRequest = async (options) => {
    try {
      const response = await axiosInstance(options);
      console.log(response.data); // this is the response from the api endpoint
    } catch (error) {
      console.error("Error:", error);
    }
  };

  const trackEventBearer = (bearerToken, eventData, userId) => {
    const options = {
      headers: {
        Authorization: `Bearer ${bearerToken}`,
      },
      data: { ...eventData, userId },
    };
    sendRequest(options);
  };

  const trackEventCookie = (eventData, userId) => {
    const options = {
      data: { ...eventData, userId },
    };
    sendRequest(options);
  };

  return { trackEventBearer, trackEventCookie };
};
