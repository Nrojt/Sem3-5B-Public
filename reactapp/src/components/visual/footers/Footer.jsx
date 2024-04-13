import { useTranslation } from "react-i18next";
import "../../../styles/Footer.css";
import { Container, Row, Col, Card } from "react-bootstrap";
import { Link } from "react-router-dom";

// import tracking
import { dispatchHook } from "../../../utils/tracking/DispatchHook";
import { useTracking } from "react-tracking";

export const FooterHTML = () => {
  // importing translations
  const [t] = useTranslation("footer");

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
    <>
      <footer>
        <Container className="footer-container">
          <Row>
            <Col>
              <Card.Title className="fs-6 text-center">
                <Link
                  className="footer-link"
                  to="/contact"
                  onClick={trackButtonClick}
                >
                  {t("contact")}
                </Link>
              </Card.Title>
            </Col>
            <Col>
              <Card.Title className="fs-6 text-center">
                <Link
                  className="footer-link"
                  to="/privacypolicy"
                  onClick={trackButtonClick}
                >
                  {t("privacy_policy")}
                </Link>
              </Card.Title>
              <ul className="list-unstyled" />
            </Col>
            <Col>
              <Card.Title className="fs-6 text-center">
                <Link
                  className="footer-link"
                  to="/sitemap"
                  onClick={trackButtonClick}
                >
                  {t("sitemap")}
                </Link>
              </Card.Title>
              <ul className="list-unstyled" />
            </Col>
          </Row>
          <hr style={{ marginBottom: "1px", marginTop: "-10px" }} />
          <Row>
            <p className="mb-0 copyright-notice">
              Copyright © 2024 Stichting Accessibility
            </p>
          </Row>
        </Container>
      </footer>
    </>
  );
};
