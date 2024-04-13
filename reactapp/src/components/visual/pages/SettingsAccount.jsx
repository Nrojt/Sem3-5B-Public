import { useTranslation } from "react-i18next";
import { Row, Col, Container, Form, Button } from "react-bootstrap";
import "../../../styles/Settings.css";

// Contact page
function SettingsAccount() {
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
          Account
        </h1>
      </header>
      <div
        className="col"
        style={{ width: 2000, marginLeft: 280, marginTop: 20 }}
      >
        <h1 style={{ fontSize: 18, fontWeight: "bold" }}>
          Gebruikersnaam wijzigen
        </h1>
        <input
          type="text"
          placeholder="Huidige gebruikersnaam"
          style={{ marginRight: 30, width: 400 }}
        />
      </div>
      <div
        className="col"
        style={{ width: 2000, marginLeft: 280, marginTop: 20 }}
      >
        <h1 style={{ fontSize: 18, fontWeight: "bold" }}>Email wijzigen</h1>
        <input type="text" placeholder="Huidige Email" style={{ width: 400 }} />
      </div>
      <div
        className="col"
        style={{ width: 2000, marginLeft: 280, marginTop: 20 }}
      >
        <h1 style={{ fontSize: 18, fontWeight: "bold" }}>
          Wachtwoord wijzigen
        </h1>
        <input
          type="text"
          placeholder="Huidige wachtwoord"
          style={{ width: 400 }}
        />
      </div>
      <div
        className="col"
        style={{ width: 2000, marginLeft: 280, marginTop: 20 }}
      >
        <h1 style={{ fontSize: 18, fontWeight: "bold" }}>
          Wachtwoord herhalen
        </h1>
        <input
          type="text"
          placeholder="Huidige wachtwoord herhalen"
          style={{ width: 400 }}
        />
      </div>
    </>
  );
}

export default SettingsAccount;
