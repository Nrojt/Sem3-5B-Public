import { useTranslation } from "react-i18next";
import { Row, Col, Container, Form, Button } from "react-bootstrap";
import "../../../styles/Settings.css";

// Contact page
function SettingsPrivacy() {
  const [t] = useTranslation("contact_page");
  return (
    <>
      <header
        style={{
          height: 63,
          background: "#346d5c",
          borderStyle: "none",
          borderTopStyle: "none",
          borderBottomStyle: "solid",
          borderBottomColor: "var(--bs-body-bg)",
          borderLeftStyle: "none",
        }}
      >
        <h1
          className="text-start"
          style={{ marginLeft: 280, color: "var(--bs-body-bg)" }}
        >
          Privacy
        </h1>
      </header>
      <div style={{ marginLeft: 280 }}>
        <fieldset>
          <h1 style={{ fontSize: 18, fontWeight: "bold", marginTop: 10 }}>
            Voorkeur benadering
          </h1>
          <div className="custom-control custom-radio">
            <input
              type="radio"
              id="customRadio1"
              className="custom-control-input"
              name="customRadio"
              defaultChecked=""
            />
            <label
              className="form-label custom-control-label"
              htmlFor="customRadio1"
              style={{ marginLeft: 6 }}
            >
              Per mail
            </label>
          </div>
          <div className="custom-control custom-radio">
            <input
              type="radio"
              id="customRadio2"
              className="custom-control-input"
              name="customRadio"
            />
            <label
              className="form-label custom-control-label"
              htmlFor="customRadio2"
              style={{ marginLeft: 6 }}
            >
              Per brief
            </label>
          </div>
        </fieldset>
        <fieldset>
          <div className="custom-control custom-checkbox custom-control-inline">
            <h1 style={{ fontSize: 18, fontWeight: "bold", marginTop: 10 }}>
              Cookie-voorkeur
            </h1>
            <input
              type="checkbox"
              id="customCheckInline1"
              className="custom-control-input"
              defaultChecked=""
            />
            <label
              className="form-label custom-control-label"
              htmlFor="customCheckInline1"
              style={{ marginLeft: 6 }}
            >
              Commerciele cookies&nbsp;
            </label>
          </div>
        </fieldset>
      </div>
    </>
  );
}

export default SettingsPrivacy;
