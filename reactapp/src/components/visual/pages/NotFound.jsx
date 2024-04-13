import { Link } from "react-router-dom";
import "../../../styles/NotFound.css";
import { useTranslation } from "react-i18next";
import { Container } from "react-bootstrap";

// 404 not found page
function NotFound() {
  // loading in translation
  const [t] = useTranslation("not_found_page");

  return (
    <Container className="error-container">
      <div className="d-flex align-items-center justify-content-center">
        <div className="text-center">
          <h1 className="display-1 fw-bold">404</h1>
          <p className="fs-3">
            <span className="text-danger">{t("oeps")}</span>
            {t("notFound")}
          </p>
          <p className="lead">{t("doesntExist")}</p>
          <Link to="/" className="btn btn-primary">
            {t("home")}
          </Link>
        </div>
      </div>
    </Container>
  );
}

export default NotFound;
