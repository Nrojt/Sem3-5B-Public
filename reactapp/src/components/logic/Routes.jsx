// importing the pages from the components folder
import Home from "../visual/pages/Home.jsx";
import About from "../visual/pages/About.jsx";
import Contact from "../visual/pages/Contact.jsx";
import PrivacyPolicy from "../visual/pages/PrivacyPolicy.jsx";
import SettingsAccessibility from "../visual/pages/SettingsAccessibility.jsx";
import SettingsPrivacy from "../visual/pages/SettingsPrivacy.jsx";
import SettingsAccount from "../visual/pages/SettingsAccount.jsx";
import IndividualResearch from "../visual/pages/IndividualResearch.jsx";
import Manage from "../visual/pages/manage/Manage.jsx";
import AddResearch from "../visual/pages/AddResearch.jsx";

// import authentication pages
import LoginForm from "../visual/pages/authentication/LoginForm.jsx";
import SignUpFormBase from "../visual/pages/authentication/signupform/SignUpFormBase.jsx";
import TrackingScript from "../visual/pages/TrackingScript.jsx";

// import fail pages
import NotFound from "../visual/pages/NotFound.jsx";
import Forbidden from "../visual/pages/Forbidden.jsx";

// importing the site map
import { SitemapHTML } from "./SiteMap.jsx";

//import profile page
import Profile from "../visual/pages/profile/ProfileBase.jsx";

// importing chat page
import ChatConnectionBase from "../visual/pages/chat/ChatConnectionBase.jsx";

import { RequireAuth, useAuthUser } from "react-auth-kit";

// import react-router-dom
import { Routes, Route, Navigate } from "react-router-dom";

// path to the login page, since we need it at multiple places
const loginPath = "/login";

// import prop-types for prop validation
import PropTypes from "prop-types";
import AllResearch from "@/components/visual/pages/AllResearch.jsx";

// function for checking the user type
function RequireUserType({ userTypesRequired, children }) {
  // get the user type from the auth hook
  const auth = useAuthUser();
  // check if the user type is in the array of allowed user types
  if (userTypesRequired.includes(auth().userType)) {
    // if the user type is allowed, return the children, which is the page that was requested
    return children;
  } else {
    return <Navigate to="/unauthorized" />;
  }
}

// prop validation
RequireUserType.propTypes = {
  userTypesRequired: PropTypes.arrayOf(PropTypes.string).isRequired,
  children: PropTypes.node.isRequired,
};

export default function PageRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/about" element={<About />} />
      <Route
        path="/addresearch"
        element={
          <RequireAuth loginPath={loginPath}>
            <RequireUserType userTypesRequired={["CompanyApproved"]}>
              <AddResearch />
            </RequireUserType>
          </RequireAuth>
        }
      />
      <Route path="/contact" element={<Contact />} />
      <Route
        path="/manage"
        element={
          <RequireAuth loginPath={loginPath}>
            <RequireUserType userTypesRequired={["Employee"]}>
              <Manage />
            </RequireUserType>
          </RequireAuth>
        }
      />
      <Route path="/privacypolicy" element={<PrivacyPolicy />} />
      <Route
        path="/settings-accessibility"
        element={<SettingsAccessibility />}
      />
      <Route path="/settings-account" element={<SettingsAccount />} />
      <Route path="/settings-privacy" element={<SettingsPrivacy />} />
      <Route
        path="/research"
        element={
          <RequireAuth loginPath={loginPath}>
            {<IndividualResearch />}
          </RequireAuth>
        }
      />
      <Route
        path="/profile"
        element={
          <RequireAuth loginPath={loginPath}>
            {/* if the user is not logged in, redirect to the login page.*/}
            <Profile />
          </RequireAuth>
        }
      />
      <Route path={loginPath} element={<LoginForm />} />
      <Route path="/register" element={<SignUpFormBase />} />
      <Route
        path="/allresearch"
        element={
          <RequireAuth loginPath={loginPath}>
            {" "}
            {/* if the user is not logged in, redirect to the login page */}
            <AllResearch />
          </RequireAuth>
        }
      />
      <Route
        path="/trackingscript"
        element={
          <RequireAuth loginPath={loginPath}>
            {" "}
            {/* if the user is not logged in, redirect to the login page */}
            <RequireUserType userTypesRequired={["CompanyApproved"]}>
              <TrackingScript />
            </RequireUserType>
          </RequireAuth>
        }
      />
      <Route path="/sitemap" element={<SitemapHTML />} />
      <Route
        path="/chat"
        element={
          <RequireAuth loginPath={loginPath}>
            {" "}
            <RequireUserType
              userTypesRequired={[
                "CompanyApproved",
                "DisabilityExpertWithGuardian",
                "DisabilityExpertWithoutGuardian",
              ]}
            >
              <ChatConnectionBase />
            </RequireUserType>
          </RequireAuth>
        }
      />
      <Route path="/unauthorized" element={<Forbidden />} />
      <Route path="*" element={<NotFound />} />{" "}
      {/* fallback route to a 404 page*/}
    </Routes>
  );
}
