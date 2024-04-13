import { useGoogleAnalytics } from "./GoogleAnalyticsHook";

// This hook is used to send tracked events to Google Analytics
// Would allow you to use a different tracking service, or add additional
// tracking services relatively easily
export const dispatchHook = () => {
  const { trackGAEvent } = useGoogleAnalytics();

  const dispatchData = (eventData) => {
    // adding a timestamp to the event data
    eventData["timestamp"] = new Date().toISOString();

    // logging the event data to the console
    console.log("trackEvent", eventData);

    // after the event is tracked, we send it to Google Analytics
    // need to make a copy of the event data because the trackGAEvent function
    // mutates the object
    const eventDataCopy = { ...eventData };
    trackGAEvent(eventDataCopy);

    // additional tracking services could be added here
  };

  return { dispatchData: dispatchData };
};
