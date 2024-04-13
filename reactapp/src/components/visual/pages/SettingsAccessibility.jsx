import { useTranslation } from "react-i18next";
import { Row, Col, Container, Form, Button } from "react-bootstrap";
import "../../../styles/Settings.css";

// Contact page
function SettingsAccessibility() {
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
          Toegankelijkheid
        </h1>
      </header>
      <h1
        style={{
          fontSize: 18,
          fontWeight: "bold",
          marginTop: 10,
          marginLeft: 280,
        }}
      >
        Dark mode
      </h1>
      <div className="theme-switcher dropdown" style={{ marginLeft: 266 }}>
        <button
          className="btn btn-link dropdown-toggle"
          aria-expanded="false"
          data-bs-toggle="dropdown"
          type="button"
          style={{ color: "var(--bs-body-color)", paddingTop: 0 }}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="1em"
            height="1em"
            fill="currentColor"
            viewBox="0 0 16 16"
            className="bi bi-sun-fill mb-1"
            style={{ fontSize: 24 }}
          >
            <path d="M8 12a4 4 0 1 0 0-8 4 4 0 0 0 0 8zM8 0a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 0zm0 13a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 13zm8-5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2a.5.5 0 0 1 .5.5zM3 8a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2A.5.5 0 0 1 3 8zm10.657-5.657a.5.5 0 0 1 0 .707l-1.414 1.415a.5.5 0 1 1-.707-.708l1.414-1.414a.5.5 0 0 1 .707 0zm-9.193 9.193a.5.5 0 0 1 0 .707L3.05 13.657a.5.5 0 0 1-.707-.707l1.414-1.414a.5.5 0 0 1 .707 0zm9.193 2.121a.5.5 0 0 1-.707 0l-1.414-1.414a.5.5 0 0 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .707zM4.464 4.465a.5.5 0 0 1-.707 0L2.343 3.05a.5.5 0 1 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .708z" />
          </svg>
        </button>
        <div className="dropdown-menu">
          <a
            className="dropdown-item d-flex align-items-center"
            href="#"
            data-bs-theme-value="light"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="1em"
              height="1em"
              fill="currentColor"
              viewBox="0 0 16 16"
              className="bi bi-sun-fill opacity-50 me-2"
            >
              <path d="M8 12a4 4 0 1 0 0-8 4 4 0 0 0 0 8zM8 0a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 0zm0 13a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-1 0v-2A.5.5 0 0 1 8 13zm8-5a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2a.5.5 0 0 1 .5.5zM3 8a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1 0-1h2A.5.5 0 0 1 3 8zm10.657-5.657a.5.5 0 0 1 0 .707l-1.414 1.415a.5.5 0 1 1-.707-.708l1.414-1.414a.5.5 0 0 1 .707 0zm-9.193 9.193a.5.5 0 0 1 0 .707L3.05 13.657a.5.5 0 0 1-.707-.707l1.414-1.414a.5.5 0 0 1 .707 0zm9.193 2.121a.5.5 0 0 1-.707 0l-1.414-1.414a.5.5 0 0 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .707zM4.464 4.465a.5.5 0 0 1-.707 0L2.343 3.05a.5.5 0 1 1 .707-.707l1.414 1.414a.5.5 0 0 1 0 .708z" />
            </svg>
            Light
          </a>
          <a
            className="dropdown-item d-flex align-items-center"
            href="#"
            data-bs-theme-value="dark"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="1em"
              height="1em"
              fill="currentColor"
              viewBox="0 0 16 16"
              className="bi bi-moon-stars-fill opacity-50 me-2"
            >
              <path d="M6 .278a.768.768 0 0 1 .08.858 7.208 7.208 0 0 0-.878 3.46c0 4.021 3.278 7.277 7.318 7.277.527 0 1.04-.055 1.533-.16a.787.787 0 0 1 .81.316.733.733 0 0 1-.031.893A8.349 8.349 0 0 1 8.344 16C3.734 16 0 12.286 0 7.71 0 4.266 2.114 1.312 5.124.06A.752.752 0 0 1 6 .278z" />
              <path d="M10.794 3.148a.217.217 0 0 1 .412 0l.387 1.162c.173.518.579.924 1.097 1.097l1.162.387a.217.217 0 0 1 0 .412l-1.162.387a1.734 1.734 0 0 0-1.097 1.097l-.387 1.162a.217.217 0 0 1-.412 0l-.387-1.162A1.734 1.734 0 0 0 9.31 6.593l-1.162-.387a.217.217 0 0 1 0-.412l1.162-.387a1.734 1.734 0 0 0 1.097-1.097l.387-1.162zM13.863.099a.145.145 0 0 1 .274 0l.258.774c.115.346.386.617.732.732l.774.258a.145.145 0 0 1 0 .274l-.774.258a1.156 1.156 0 0 0-.732.732l-.258.774a.145.145 0 0 1-.274 0l-.258-.774a1.156 1.156 0 0 0-.732-.732l-.774-.258a.145.145 0 0 1 0-.274l.774-.258c.346-.115.617-.386.732-.732L13.863.1z" />
            </svg>
            Dark
          </a>
          <a
            className="dropdown-item d-flex align-items-center"
            href="#"
            data-bs-theme-value="auto"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="1em"
              height="1em"
              fill="currentColor"
              viewBox="0 0 16 16"
              className="bi bi-circle-half opacity-50 me-2"
            >
              <path d="M8 15A7 7 0 1 0 8 1v14zm0 1A8 8 0 1 1 8 0a8 8 0 0 1 0 16z" />
            </svg>
            Auto
          </a>
        </div>
      </div>
      <h1
        style={{
          fontSize: 18,
          fontWeight: "bold",
          marginTop: 10,
          marginLeft: 280,
        }}
      >
        Taal
      </h1>
      <a
        className="btn btn-primary btn-lg text-start"
        role="button"
        style={{
          margin: "auto",
          width: "100%",
          borderColor: "#fff",
          color: "#303030",
          marginBottom: 2,
          fontWeight: 400,
          fontSize: 18,
          borderRadius: 0,
          marginLeft: 262,
          background: "transparent",
        }}
        href="Tools_EN.html"
      >
        <img
          src="assets/img/uk.svg"
          style={{ maxWidth: 24, marginRight: 12 }}
        />
        English
      </a>
      <div style={{ marginLeft: 280 }}>
        <fieldset>
          <h1 style={{ fontSize: 18, fontWeight: "bold", marginTop: 10 }}>
            Lettertype
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
              Helvetica
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
              Commic sanse
            </label>
          </div>
        </fieldset>
        <fieldset>
          <div className="custom-control custom-radio custom-control-inline">
            <h1 style={{ fontSize: 18, fontWeight: "bold", marginTop: 10 }}>
              Lettergrootte
            </h1>
            <input
              type="radio"
              id="customRadioInline2"
              className="custom-control-input"
              name="customRadioInline"
            />
            <label
              className="form-label custom-control-label"
              htmlFor="customRadioInline1"
              style={{ marginLeft: 6 }}
            >
              Klein
            </label>
          </div>
          <div className="custom-control custom-radio custom-control-inline">
            <input
              type="radio"
              id="customRadioInline1"
              className="custom-control-input"
              name="customRadioInline"
              defaultChecked=""
            />
            <label
              className="form-label custom-control-label"
              htmlFor="customRadioInline2"
              style={{ marginLeft: 6 }}
            >
              Standaard
            </label>
          </div>
          <div className="custom-control custom-radio custom-control-inline">
            <input
              type="radio"
              id="customRadioInline-1"
              className="custom-control-input"
              name="customRadioInline"
            />
            <label
              className="form-label custom-control-label"
              htmlFor="customRadioInline2"
              style={{ marginLeft: 6 }}
            >
              Groot
            </label>
          </div>
        </fieldset>
      </div>
    </>
  );
}

export default SettingsAccessibility;
