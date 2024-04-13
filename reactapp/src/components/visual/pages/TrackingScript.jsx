import { useTranslation } from "react-i18next";
import { Container, Row, Col, Button } from "react-bootstrap";
import PageIndex from "../PageIndex";
import MarkDownCodeSnippet from "../MarkDownCodeSnippet";

// import tracking
import { dispatchHook } from "../../../utils/tracking/DispatchHook";
import { useTracking } from "react-tracking";

//TODO lock this page behind auth
function TrackingScript() {
  const [t] = useTranslation("trackingscript_page");

  // Use the useTracking hook to track an event and dispatch it to the analytics service
  const { dispatchData } = dispatchHook();
  const { trackEvent } = useTracking(
    {},
    {
      dispatch: dispatchData,
    },
  );

  // const for the markdown code snippets. Markdown cannot be indented.
  const importMarkdown = `
\`\`\`js
import { trackingAnalytics } from "path/to/trackingscript.js";
\`\`\`
`;

  const trackEventBearerMarkdown = `
\`\`\`js
const { trackEventBearer } = trackingAnalytics();
trackEventBearer("UwBearerToken", eventData, "userId");
\`\`\`
`;

  const trackEventCookieMarkdown = `
\`\`\`js
const { trackEventCookie } = trackingAnalytics();
trackEventCookie(eventData, "userId");
\`\`\`
`;

  const eventDataMarkdown = `
\`\`\`json
{
  "eventName": "Button Click",
  "category": "Button",
  "action": "navigating to",
  "label": "homepage button",
  "timestamp": "2021-06-01T12:00:00.000Z",
  "userId":"voorbeeld",
}
\`\`\`
`;

  //
  const handleDownloadFile = (fileName) => {
    // Track button click event
    trackEvent({
      eventName: "Button Click",
      category: "Button",
      action: "downloading file",
      label: fileName,
    });

    const fileUrl = "/downloads/" + fileName;
    console.log(fileUrl);
    const link = document.createElement("a");
    link.href = fileUrl;
    link.download = fileName;
    link.click();
  };

  return (
    <>
      <Container fluid className="p-3 custom-container">
        <h1 className="text-center titleKop">
          <strong>{t("title_heading")}</strong>
        </h1>
        <br />
        <PageIndex />
        <section id="introduction">
          <Container>
            <p className="text-left">{t("introduction_paragraph")}</p>
            <br />
            <h2 className="kop text-center">{t("tracking_title")}</h2>
            <p className="text-left">{t("tracking_paragraph")}</p>
          </Container>
        </section>
        <section id="download">
          <Container>
            <h2 className="kop text-center">{t("download_title")}</h2>
            <Row className="justify-content-center">
              <Col xs="auto" className="p-2">
                <Button
                  variant="primary"
                  onClick={() => handleDownloadFile("trackingscript-fetch.js")}
                >
                  {t("download_button_fetch")}
                </Button>
              </Col>
              <Col xs="auto" className="p-2">
                <Button
                  variant="primary"
                  onClick={() => handleDownloadFile("trackingscript-axios.js")}
                >
                  {t("download_button_axios")}
                </Button>
              </Col>
            </Row>
          </Container>
        </section>
        <section id="how-to-use">
          <Container>
            <h2 className="kop text-center">{t("how_to_use_title")}</h2>
            <h3 className="text-center">
              <strong>{t("important_authentication")}</strong>
            </h3>
            <p className="text-left">{t("how_to_use_paragraph")}</p>
          </Container>
        </section>
        <section id="step-one">
          <Container>
            <h3 className="text-center kop3">{t("step_one_title")}</h3>
            <p className="text-left">
              <span>{t("step_one_paragraph")}</span>
              <br />
            </p>
          </Container>
        </section>
        <section id="step-two">
          <Container>
            <h3 className="text-center kop3">{t("step_two_title")}</h3>
            <p className="text-left">{t("step_two_import_paragraph")}</p>
            <MarkDownCodeSnippet
              markdown={importMarkdown}
              markdownTitle="Javascript"
            />
            <p className="text-left">{t("step_two_track_event_paragraph")}</p>
          </Container>
        </section>
        <section id="trackEventBearer">
          <Container>
            <h4 className="kop4 text-center">TrackEventBearer</h4>
            <p>{t("step_two_track_event_bearer_paragraph")}</p>
            <MarkDownCodeSnippet
              markdown={trackEventBearerMarkdown}
              markdownTitle="Javascript"
            />
          </Container>
        </section>
        <section id="trackEventCookies">
          <Container>
            <h4 className="kop4 text-center">TrackEventCookie</h4>
            <p>{t("step_two_track_event_cookie_paragraph")}</p>
            <MarkDownCodeSnippet
              markdown={trackEventCookieMarkdown}
              markdownTitle="Javascript"
            />
          </Container>
        </section>
        <section id="step-three">
          <Container>
            <h3 className="kop3 text-center">{t("step_three_title")}</h3>
            <p>{t("step_three_paragraph")}</p>
            <MarkDownCodeSnippet
              markdown={eventDataMarkdown}
              markdownTitle="Json"
            />
            <br />
            <Row>
              <Col>
                <h3 className="kop4">{t("step_three_event_name_title")}</h3>
                <p>{t("step_three_event_name_paragraph")}</p>
                <br />
              </Col>
            </Row>
            <Row>
              <Col>
                <h3 className="kop4">{t("step_three_category_title")}</h3>
                <p>{t("step_three_category_paragraph")}</p>
                <br />
              </Col>
            </Row>
            <Row>
              <Col>
                <h3 className="kop4">{t("step_three_action_title")}</h3>
                <p>{t("step_three_action_paragraph")}</p>
                <br />
              </Col>
            </Row>
            <Row>
              <Col>
                <h3 className="kop4">{t("step_three_label_title")}</h3>
                <p>{t("step_three_label_paragraph")}</p>
                <br />
              </Col>
            </Row>
            <Row>
              <Col>
                <h3 className="kop4">{t("step_three_timestamp_title")}</h3>
                <p>{t("step_three_timestamp_paragraph")}</p>
                <br />
              </Col>
            </Row>
            <Row>
              <Col>
                <h3 className="kop4">{t("step_three_userId_title")}</h3>
                <p>{t("step_three_userId_paragraph")}</p>
              </Col>
            </Row>
          </Container>
        </section>
        <section id="support">
          <Container>
            <h2 className="kop3 text-center">{t("support_title")}</h2>
            <p>{t("support_paragraph")}</p>
          </Container>
        </section>
      </Container>
    </>
  );
}

export default TrackingScript;
