import { useTranslation } from "react-i18next";
import { Container, Row, Col, Card } from "react-bootstrap";
import "../../../styles/PrivacyPolicy.css";

// page for privacy policy.
function PrivacyPolicy() {
  const [t] = useTranslation("privacy_policy_page");
  return (
    <div className="privacyPage">
      <Row className="privacy-policy-content">
        <h1 className={"text-center pp-title"}>{t("title_heading")}</h1>
        <p className="text-center inleidingPrivacy">{t("stichting_info")}</p>
        <h2 className="pp-kop fs-4">{t("contact_kop")}</h2>
        <div className="contactgegevens">
          https://www.StichtingAccessibillity.com
          <br />
          Christiaan Krammlaan 2, 3571 AX Utrecht
          <br />
          +31302398270
        </div>
        <h2 className="pp-kop fs-4">{t("persgeg_kop")}</h2>
        <Card.Body className="pp-card-content">
          {t("persgeg_info")}
          <br />
          <br />
          {t("persgeg_info2")}
          <ul>
            <li>{t("Voor_achternaam")}</li>
            <li>{t("Geboortedatum")}</li>
            <li>{t("Adresgegevens")}</li>
            <li>{t("Telefoonnummer")}</li>
            <li>{t("mailadres")}</li>
            <li>{t("OverigeGegevens")}</li>
            <li>{t("Web_activiteit")}</li>
            <li>{t("Surfgedrag")}</li>
          </ul>
        </Card.Body>
        <h2 className="pp-kop fs-4">{t("Gevoelige_persgeg_kop")}</h2>
        <Card.Body className="pp-card-content">
          {t("Gevoelige_persgeg_info")}
          <ul>
            <li>{t("Gezondheid")}</li>
            <li>{t("Onder16Jaar")}</li>
          </ul>
        </Card.Body>
        <Card.Body className="pp-card-content">
          {t("Gevoelige_persgeg_info2")}
        </Card.Body>
        <h2 className="pp-kop fs-4">{t("Doel_persgeg_kop")}</h2>
        <Card.Body className="pp-card-content">
          {t("Doel_persgeg_info")}
          <ul>
            <li>{t("Contacteren")}</li>
            <li>{t("Informeren")}</li>
            <li>{t("Account_maken")}</li>
          </ul>
        </Card.Body>
        <h2 className="pp-kop fs-4">{t("Bewaar_duur")}</h2>
        <Card.Body className="pp-card-content">
          {t("Bewaar_duur_info")}
        </Card.Body>
        <h3 className="pp-kop3 fs-5">{t("Persoonsgegevens_kop")}</h3>
        <Card.Body className="pp-card-content">
          {t("Persoonsgegevens_info")}
          <ul>
            <li>{t("Identiteitsinformatie")}</li>
            <li>{t("Contactinformatie")}</li>
            <li>{t("Medische_informatie")}</li>
          </ul>
        </Card.Body>
        <h3 className="pp-kop3 fs-5">{t("Bedrijfsgegevens_kop")}</h3>
        <Card.Body className="pp-card-content">
          {t("Bedrijfsgegevens_info")}
          <ul>
            <li>{t("Bedrijfsnaam")}</li>
            <li>{t("Contactinformatie_bedrijf")}</li>
          </ul>
        </Card.Body>
        <h2 className="pp-kop fs-4">{t("Delen_persgeg_kop")}</h2>
        <Card.Body className="pp-card-content">
          {t("Delen_persgeg_info")}
        </Card.Body>
        <Card.Body className="pp-card-content">
          <ul>
            <li>{t("Aard_en_doel")}</li>
            <li>{t("Verantwoordelijkheden")}</li>
            <li>{t("Maatregelen")}</li>
            <li>{t("Procedures")} </li>
            <li>{t("Duur")}</li>
            <li>{t("Gegevens_verwerken")}</li>
          </ul>
        </Card.Body>
        <Card.Body className="pp-card-content">
          {t("Delen_persgeg_info2")}
        </Card.Body>
        <h2 className="pp-kop fs-4">{t("Cookies_kop")}</h2>
        <Card.Body className="pp-card-content">
          {t("Cookies_info")}
          <br />
          <br />
          <span>{t("Toelichting")}</span>
          <Card.Link
            className="pp-link"
            href="https://veiliginternetten.nl/themes/situatie/cookies-wat-zijn-het-en-wat-doe-ik-ermee/"
          >
            <br />
            <span>{t("Toelichting_link")}</span>
          </Card.Link>
        </Card.Body>
        <h2 className="pp-kop fs-4">{t("Geg_inzien_kop")}</h2>
        <Card.Body className="pp-card-content">
          {t("Geg_inzien_info")}
          <br />
          <br />
          {t("Geg_inzien_info2")}
          <Card.Link
            className="pp-link"
            href="https://autoriteitpersoonsgegevens.nl/nl/contact-met-de-autoriteit-persoonsgegevens/tip-ons"
          >
            <br />
            <span>{t("Klacht_link")}</span>
          </Card.Link>
        </Card.Body>
        <h2 className="pp-kop fs-4">{t("Pers_geg_beveiligen_kop")}</h2>
        <Card.Body className="pp-card-content">
          {t("Pers_geg_beveiligen_info")}
        </Card.Body>
        <br />
      </Row>
    </div>
  );
}

export default PrivacyPolicy;
