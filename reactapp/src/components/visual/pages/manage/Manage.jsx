import { useTranslation } from "react-i18next";
import { Row, Col, Container, Form, Button } from "react-bootstrap";
import "../../../../styles/Manage.css";
import ApprovedUsers from "./ApprovedUsers";
import UnapprovedUsers from "./UnapprovedUsers";

function Manage() {
  const [t] = useTranslation("contact_page");
  return (
    <>
      <div style={{ marginTop: 10 }}>
        <p style={{ fontSize: 30, marginTop: 20, textAlign: "center" }}>
          Geregistreerde accounts&nbsp; &nbsp;&nbsp;
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="1em"
            height="1em"
            fill="currentColor"
            viewBox="0 0 16 16"
            className="bi bi-search"
          >
            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
          </svg>
          &nbsp;
          <input type="search" style={{ borderStyle: "solid" }} />
        </p>
        <div
          className="container custom-container-manage"
          style={{
            background: "#2a54de",
            paddingTop: 1,
            paddingRight: 10,
            paddingLeft: 10,
            height: 350,
            borderRadius: 5,
          }}
        >
          <ul className="manage-list" style={{ height: 350 }}>
            {/* Hier worden 3 ProfileCards toegevoegd aan de eerste container */}
            <ApprovedUsers
              fullName="Volledige naam 1"
              userName="Gebruikersnaam/email 1"
              accountDetails="Datum aanmaken account 1, handicap etc."
            />
            <ApprovedUsers
              fullName="Volledige naam 2"
              userName="Gebruikersnaam/email 2"
              accountDetails="Datum aanmaken account 2, handicap etc."
            />
            <ApprovedUsers
              fullName="Volledige naam 3"
              userName="Gebruikersnaam/email 3"
              accountDetails="Datum aanmaken account 3, handicap etc."
            />
          </ul>
        </div>
      </div>
      <div style={{ marginTop: 80 }}>
        <p style={{ fontSize: 30, marginTop: 20, textAlign: "center" }}>
          Ongeactiveerde accounts&nbsp; &nbsp;&nbsp;
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="1em"
            height="1em"
            fill="currentColor"
            viewBox="0 0 16 16"
            className="bi bi-search"
          >
            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
          </svg>
          &nbsp;
          <input type="search" style={{ borderStyle: "solid" }} />
        </p>
        <div
          className="container custom-container-manage"
          style={{
            background: "#2a54de",
            paddingTop: 1,
            paddingRight: 10,
            paddingLeft: 10,
            height: 350,
            borderRadius: 5,
          }}
        >
          <ul className="manage-list" style={{ height: 350 }}>
            {/* Hier worden 3 ProfileCards2 toegevoegd aan de tweede container */}
            <UnapprovedUsers
              fullName="Volledige naam 4"
              userName="Gebruikersnaam/email 4"
              accountDetails="Datum aanmaken account 4, handicap etc."
            />
            <UnapprovedUsers
              fullName="Volledige naam 5"
              userName="Gebruikersnaam/email 5"
              accountDetails="Datum aanmaken account 5, handicap etc."
            />
            <UnapprovedUsers
              fullName="Volledige naam 6"
              userName="Gebruikersnaam/email 6"
              accountDetails="Datum aanmaken account 6, handicap etc."
            />
          </ul>
        </div>
      </div>
    </>
  );
}

export default Manage;
