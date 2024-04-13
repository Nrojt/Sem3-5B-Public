// Import the necessary modules
import { useEffect } from "react";
import { useLocation } from "react-router-dom";
import { useTracking } from "react-tracking";

// Define a custom component that tracks page views
export const PageViewTracker = () => {
  // Get the current location from react-router
  const location = useLocation();

  // Get the trackEvent function from react-tracking
  const { trackEvent } = useTracking({ page: location.pathname });

  // Track the page view whenever the location changes
  useEffect(() => {
    // defining the event data to be tracked
    const eventData = {
      category: "Page view",
      action: "Visit",
      label: location.pathname,
    };

    // Send the page view to react-tracking
    trackEvent(eventData);

    // logging the page view
    //console.log("Page view: ", eventData);
  }, [location]); // Only re-run effect if pathname changes

  return null;
};
