export const useGoogleAnalytics = () => {
  const trackGAEvent = (eventData) => {
    // if no event data provided, return
    if (!eventData) return;
    if (!eventData.eventName) return console.error("No event name provided");

    // extract event name from eventData
    const eventName = eventData.eventName;
    delete eventData.eventName;
    // send event to Google Analytics (gtag is defined in index.html)
    if (window.gtag) {
      window.gtag("event", eventName, eventData);
      console.log(`Event ${eventName} sent to Google Analytics`);
    } else {
      return console.error("Google Analytics is not initialized");
    }
  };

  return { trackGAEvent };
};
