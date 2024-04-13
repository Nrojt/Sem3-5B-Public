import { useTranslation } from "react-i18next";
import { Row, Container, Form, Button, Card } from "react-bootstrap";
import { useForm } from "react-hook-form";
import "../../../styles/AddResearch.css";
import { postResearch } from "../../../utils/api/pages/addresearchcontroller";

function AddResearch() {
  const [t] = useTranslation("contact_page");
  const { register, handleSubmit } = useForm();

  const onSubmit = (data) => {
    // combine date and time into datetime
    const datetime = `${data.date}T${data.time}:00.000Z`;
    data.date = datetime;
    delete data.time;

    console.log(data);
    // TODO axios call to backend
    postResearch(data);
  };

  // just so I dont have to type it out every time
  const registerRequired = (name) => register(name, { required: true });

  return (
    <Container>
      <h1 style={{ marginTop: 40, textAlign: "center", marginBottom: 14 }}>
        Onderzoek aanmaken
      </h1>
      <Card style={{ marginLeft: 40, marginRight: 40, marginBottom: 70 }}>
        <Card.Body
          style={{
            textAlign: "center",
            background: "var(--bs-body-bg)",
            borderRadius: 20,
          }}
        >
          <Form onSubmit={handleSubmit(onSubmit)} className="mb-3">
            <Form.Group style={{ display: "flex", flexDirection: "column" }}>
              <Form.Label>Titel:</Form.Label>
              <Form.Control
                {...registerRequired("title")}
                placeholder="Voer hier de titel voor het onderzoek in"
              />
              <Form.Label>Type onderzoek:</Form.Label>
              <Form.Control
                {...registerRequired("researchType")}
                placeholder="Voer hier het soort onderzoek in"
              />
              <Form.Label>Beschrijving:</Form.Label>
              <Form.Control
                as="textarea"
                {...registerRequired("description")}
                placeholder="Voer hier een beschrijving in waar het onderzoek over gaat"
              />
              <Form.Label>Locatie:</Form.Label>
              <Form.Control
                as="textarea"
                {...registerRequired("location")}
                placeholder="Voer hier de locatie in waar het onderzoek plaatsvindt"
              />
              <Form.Label>Leeftijdsgroep:</Form.Label>
              <Form.Control
                {...registerRequired("ageRange")}
                placeholder="Voer hier de leeftijdsgroep voor het onderzoek in"
              />
              <Form.Label>Beloning:</Form.Label>
              <Form.Control
                {...registerRequired("reward")}
                placeholder="Voer hier de leeftijdsgroep voor het onderzoek in"
              />
              <Form.Label>Datum en tijd:</Form.Label>
              <Row className="mb-3">
                <Form.Control
                  type="date"
                  {...registerRequired("date")}
                  placeholder="Voer hier de datum in waarop het onderzoek plaatsvindt"
                />
                <Form.Control
                  type="time"
                  {...registerRequired("time")}
                  placeholder="Voer hier de tijd in waarop het onderzoek plaatsvindt"
                />
              </Row>
            </Form.Group>
            <Button
              className="shadow"
              type="submit"
              style={{
                background: "#108670",
                textAlign: "center",
              }}
            >
              Aanmaken
            </Button>
          </Form>
        </Card.Body>
      </Card>
    </Container>
  );
}

export default AddResearch;
