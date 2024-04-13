import "../../../styles/About.css";
import { useTranslation } from "react-i18next";
import { Container, Row, Col } from "react-bootstrap";
import PageIndex from "../PageIndex";
import MemberCard from "../MemberCard";

// About page
function About() {
  // loading in translation
  const [t] = useTranslation("about_page");

  return (
    <div className="main-about">
      <Container>
        <h1 className="about-title">{t("title_heading")}</h1>
        <h2 className="about-subtitle">{t("index_heading")}</h2>
        <PageIndex />
        <Row>
          <Col>
            <section id="introduction">
              <h2 className="about-kop">{t("introduction_titel")}</h2>
              <p>{t("inleiding")}</p>
            </section>
            <br />
            <section id="our-mission">
              <h2 className="about-kop">{t("missie-titel")}</h2>
              <p>{t("missie-tekst")}</p>
            </section>
            <br />
            <section id="background">
              <h2 className="about-kop">{t("Achtergrond-titel")}</h2>
              <p>{t("Achtergrond-tekst")}</p>
            </section>
          </Col>
        </Row>
        <Row>
          <section id="our-team">
            <Row>
              <h2 className="about-kop">{t("team-title")}</h2>
            </Row>
            <Row>
              <Col>
                <MemberCard
                  imageUrl="https://cdn.bootstrapstudio.io/placeholders/1400x800.png"
                  name="Yazan Nemo"
                  subtitle="Erat netus"
                />
              </Col>
              <Col>
                <MemberCard
                  imageUrl="https://cdn.bootstrapstudio.io/placeholders/1400x800.png"
                  name="Tjorn Brederoo"
                  subtitle="Erat netus"
                />
              </Col>
              <Col>
                <MemberCard
                  imageUrl="https://cdn.bootstrapstudio.io/placeholders/1400x800.png"
                  name="Raoul Nedermeijer"
                  subtitle="Erat netus"
                />
              </Col>
              <Col>
                <MemberCard
                  imageUrl="https://cdn.bootstrapstudio.io/placeholders/1400x800.png"
                  name="Noah Stolk"
                  subtitle="Erat netus"
                />
              </Col>
            </Row>
          </section>
        </Row>
      </Container>
    </div>
  );
}

export default About;
