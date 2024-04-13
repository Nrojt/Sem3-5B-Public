import Nav from "react-bootstrap/Nav";

import "../../../styles/Navbar.css";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";

// import tracking
import { dispatchHook } from "../../../utils/tracking/DispatchHook";
import { useTracking } from "react-tracking";

export const NavbarLoggedOut = () => {
  // t is the translation function, i18n is the i18n instance itself
  const [t] = useTranslation("navbar");

  // dispatchHook is a custom hook that allows us to dispatch events to an analytics service
  const { dispatchData } = dispatchHook();

  // Use the useTracking hook to track an event and dispatch it to the analytics service
  const { trackEvent } = useTracking(
    {},
    {
      dispatch: dispatchData,
    },
  );

  const trackButtonClick = (event) => {
    console.log("Button clicked: ", event.target.className);
    // Track button click event
    trackEvent({
      eventName: "Button Click",
      category: "Button",
      action: "navigating to",
      label: event.target.className,
    });
  };

  return (
    <Nav className="rechterNav">
      <Link
        to="/login"
        id="navLogin"
        className="nav-link login"
        onClick={trackButtonClick}
      >
        {t("login")}
      </Link>
      <Link
        to="/register"
        id="navRegister"
        className="nav-link register"
        onClick={trackButtonClick}
      >
        {t("register")}
      </Link>
    </Nav>
  );
};
