import Nav from "react-bootstrap/Nav";
import "../../../styles/Navbar.css";
import { useTranslation } from "react-i18next";
import { Link, useNavigate } from "react-router-dom";

// signout hook
import { useSignOut } from "react-auth-kit";

// react-query
import { useMutation } from "@tanstack/react-query";

// import tracking
import { dispatchHook } from "../../../utils/tracking/DispatchHook";
import { useTracking } from "react-tracking";

import { PostLogout } from "../../../utils/api/authentication/LogoutController";

import { useAuthUser } from "react-auth-kit";

export const NavbarLoggedIn = () => {
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

  // getting the signout hook from react-auth-kit
  const signOut = useSignOut();

  // navigate from react-router-dom
  const navigate = useNavigate();

  // using the useMutation hook to call the logout api and then signout from react-auth-kit on success
  const { mutate } = useMutation({
    mutationFn: async () => {
      // calling the logout api
      const response = PostLogout();
      return response;
    },
    // This function will fire when the mutation is successful
    onSuccess: (response) => {
      // checking if the logout was successful
      if (response && response.status === 200) {
        console.log("Logout successful");
        signOut();

        // deleting the token expiration from local storage
        localStorage.removeItem("refreshTokenExpiration");
        localStorage.removeItem("authTokenExpiration");

        // redirect to home page using react-router-dom
        console.log("Redirecting to home page");
        navigate("/");
      } else {
        console.log("Logout failed");
      }
    },
    onError: (error) => {
      console.log("Error:", error);
    },
  });

  const logoutHandler = (event) => {
    // Track logout event
    trackButtonClick(event);

    // mutate the logout api
    mutate();
  };

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

  const auth = useAuthUser();

  return (
    <Nav className="rechterNav">
      {auth().userType === "CompanyApproved" ? (
        <Link
          to="/addresearch"
          className="nav-link addResearch"
          onClick={trackButtonClick}
        >
          {t("addResearch")}
        </Link>
      ) : auth().userType === "Employee" ? (
        <Link
          to="/manage"
          className="nav-link manage"
          onClick={trackButtonClick}
        >
          {t("manage")}
        </Link>
      ) : (
        <Link
          to="/allresearch"
          className="nav-link allResearch"
          onClick={trackButtonClick}
        >
          {t("allResearches")}
        </Link>
      )}
      <Link
        to="/chat"
        className="nav-link myResearch"
        onClick={trackButtonClick}
      >
        {t("chat")}
      </Link>
      <Link
        to="/profile"
        className="nav-link profile"
        onClick={trackButtonClick}
      >
        {t("profile")}
      </Link>
      <Nav.Link className="nav-link logout" onClick={logoutHandler}>
        {t("logout")}
      </Nav.Link>
    </Nav>
  );
};
