import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Button from "react-bootstrap/Button";
import Col from "react-bootstrap/Col";
import { useTranslation, Trans } from "react-i18next";

function Home() {
  // loading in the translations
  // we use Trans component to translate the html tags
  const [t] = useTranslation("home_page");

  const handleClick = () => {
    console.log("clicked");
  };

  return (
    <>
      <h1 className="text-center">{t("title_heading")}</h1>
      <section className="py-4 py-xl-5">
        <Container fluid>
          <Row>
            <Col
              md={10}
              xl={8}
              className="text-center d-flex justify-content-center align-items-center mx-auto justify-content-xl-center"
            >
              <div>
                <h2 className="text-uppercase fw-bold mb-3">
                  <Trans>{t("title_page")}</Trans>
                </h2>
                <p className="mb-4">{t("subtitle")}</p>
                <Button variant="primary" onClick={handleClick}>
                  {t("research_button_text")}
                </Button>
              </div>
            </Col>
          </Row>
        </Container>
      </section>
    </>
  );
}

export default Home;
