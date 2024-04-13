import "../../../styles/NotFound.css";
import { useTranslation } from "react-i18next";

// 404 not found page
function NotFound() {
  // loading in translation
  const [t] = useTranslation("forbidden_page");

  return (
    <>
      <div className="d-flex align-items-center justify-content-center">
        <div className="text-center">
          <h1 className="display-1 fw-bold">403</h1>
          <p className="fs-3">
            <span className="text-danger">{t("oeps")}</span>
            {t("forbidden")}
          </p>
          <a href="/" className="btn btn-primary">
            {t("home")}
          </a>
        </div>
      </div>
    </>
  );
}

export default NotFound;
