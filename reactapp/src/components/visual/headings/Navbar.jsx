import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import "../../../styles/Navbar.css";
import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";

// import authentication
import { useIsAuthenticated } from "react-auth-kit";

// import tracking
import { dispatchHook } from "../../../utils/tracking/DispatchHook";
import { useTracking } from "react-tracking";

// importing the navbar auth components
import { NavbarLoggedIn } from "./Navbar-loggedIn";
import { NavbarLoggedOut } from "./Navbar-loggedOut";

export const NavbarHTML = () => {
  const [t, i18n] = useTranslation("navbar");
  const { dispatchData } = dispatchHook();
  const { trackEvent } = useTracking({}, { dispatch: dispatchData });
  const isAuthenticated = useIsAuthenticated();

  const changeLanguage = (lng) => {
    trackEvent({
      eventName: "Language change",
      category: "Button",
      action: "changing language",
      label: lng,
    });

    i18n.changeLanguage(lng);
    localStorage.setItem("i18nextLng", lng);
  };

  const trackButtonClick = (event) => {
    trackEvent({
      eventName: "Button Click",
      category: "Button",
      action: "navigating to",
      label: event.target.className ? event.target.className : "unknown",
    });
  };

  return (
    <Navbar expand="lg" variant="light">
      <Container fluid className="container-xxl">
        <Navbar.Brand href="/" onClick={trackButtonClick}>
          <img
            alt="Logo Stichting Accessibility"
            src="img/logo/icon_accessibility_on-blue_transp.png"
            width={40}
            height={48}
            className="d-inline-block align-top"
          />
          <span className="logoText">Accessibility</span>
        </Navbar.Brand>
        <Navbar.Toggle
          aria-controls="navcol-1"
          style={{ backgroundColor: "#F2F2F2" }}
        />
        <Navbar.Collapse
          id="navcol-1"
          className="justify-content-end align-center"
        >
          <Nav className="linkerNav">
            <Link to="/" className="nav-link" onClick={trackButtonClick}>
              {t("home")}
            </Link>
            <Link to="/about" className="nav-link" id="navAbout">
              {t("about_us")}
            </Link>
            <Nav className="rechterNav">
              <NavDropdown title={t("language")} id="basic-nav-dropdown">
                <NavDropdown.Item onClick={() => changeLanguage("nl")}>
                  Nederlands
                </NavDropdown.Item>
                <NavDropdown.Item onClick={() => changeLanguage("en")}>
                  English
                </NavDropdown.Item>
              </NavDropdown>
              {/* Home and About Us links... */}
              <NavDropdown title={t("settings")} id="basic-nav-dropdown">
                <Link to="/settings-account" className="dropdown-item">
                  {t("account_settings")}
                </Link>
                <Link to="/settings-accessibility" className="dropdown-item">
                  {t("accessibility_settings")}
                </Link>
                <Link to="/settings-privacy" className="dropdown-item">
                  {t("privacy_settings")}
                </Link>
              </NavDropdown>
              {isAuthenticated() ? <NavbarLoggedIn /> : <NavbarLoggedOut />}
            </Nav>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};
