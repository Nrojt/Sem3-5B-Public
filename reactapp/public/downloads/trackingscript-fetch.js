export const trackingAnalytics = () => {
  // sendRequest is a helper function that sends a request to the api endpoint,
  // passing in the url and options
  const sendRequest = (options) => {
    fetch("https://api.sem3-5b.com/api/tracking/addTracking", options)
      .then((response) => response.json())
      .then((data) => console.log(data))
      .catch((error) => console.error("Error:", error));
  };

  // trackEventBearer uses the bearer token to send information to the api
  // endpoint
  const trackEventBearer = (bearerToken, eventData, userId) => {
    // stringifying the data
    const stringifiedData = JSON.stringify({ ...eventData, userId });

    // send information to api endpoint using fetch
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${bearerToken}`,
      },
      body: stringifiedData,
    };
    sendRequest(options);
  };

  // trackEventCookie uses the cookie to send information to the api endpoint
  const trackEventCookie = (eventData, userId) => {
    // send information to api endpoint using fetch
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        ...eventData,
        userId,
      }),
      credentials: "include",
    };
    sendRequest(options);
  };

  return { trackEventBearer, trackEventCookie };
};
