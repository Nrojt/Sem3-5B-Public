import React from "react";
import { Modal, Container, Row, Col, Card, Button } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { useLocation, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import {
  applyUser,
  getResearchById,
} from "@/utils/api/pages/researchcontroller.js";

function IndividualResearch() {
  // getting the translation method
  const [t] = useTranslation("research_page");

  const [research, setResearch] = useState([]);
  const [loading, setLoading] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [successMessage, setSuccessMessage] = useState(null);
  const [errorMessage, setErrorMessage] = useState(null);

  const resetModalState = () => {
    setShowModal(false);
    setSuccessMessage(null);
    setErrorMessage(null);
  };

  const handleModalShow = () => setShowModal(true);
  const handleModalClose = () => setShowModal(false);

  // getting the query params
  const location = useLocation();
  const query = new URLSearchParams(location.search);
  const navigate = useNavigate();

  useEffect(() => {
    // getting the researchId from the query params
    const researchId = query.get("researchId");
    console.log("ResearchId: ", researchId);

    // if the researchId is null, redirect to all research page
    if (researchId == null) {
      navigate("/allresearch");
      return;
    }

    const loginState = location.state;

    if (loginState != null) {
      console.log("LoginState: ", loginState.researchData);
      setResearch(loginState.researchData);
    } else {
      setLoading(true);
      // get the research from the database by id
      const fetchData = async () => {
        try {
          const response = await getResearchById(researchId);
          console.log(response.data);

          // Update state with fetched data
          setResearch(response.data);
        } catch (error) {
          console.error("Error fetching data:", error);
        } finally {
          handleModalShow();
        }
      };

      fetchData();
      console.log("ResearchId: ", research.Id);
    }
  }, []); // only run when these variables change, so only once

  const handleApplyButtonClick = async () => {
    try {
      const researchId = query.get("researchId");
      console.log("ResearchId: ", researchId);
      await applyUser(researchId);

      setSuccessMessage("Applied to research successfully!");
      setErrorMessage(null); // Clear any previous error message
    } catch (error) {
      console.error("Error applying to research:", error.message);
      setSuccessMessage(null); // Clear any previous success message
      setErrorMessage("Error applying to research. Please try again.");
    } finally {
      setShowModal(true); // Set showModal to true to show the modal
    }
  };

  return (
    <Container style={{ borderRadius: "0px", background: "#cdf5fd" }}>
      {loading ? (
        <h2 className="text-center mb-3">{t("loading")}</h2>
      ) : (
        <>
          <Row>
            <Col
              md={12}
              style={{ background: "var(--bs-body-bg)", marginTop: "20px" }}
            >
              <h1
                style={{
                  textAlign: "center",
                  marginBottom: "20px",
                  marginTop: "10px",
                }}
              >
                {research.title}
              </h1>
              <p
                style={{
                  textAlign: "center",
                  fontWeight: "bold",
                  marginBottom: "30px",
                }}
              >
                {t("by_company")} {research.companyName}
              </p>
            </Col>
          </Row>
          <Row>
            <Col
              md={6}
              style={{ background: "var(--bs-body-bg)", marginBottom: "60px" }}
            >
              <Card
                style={{
                  borderStyle: "none",
                  borderRightStyle: "none",
                  background: "var(--bs-body-bg)",
                }}
              >
                <Card.Body
                  style={{
                    background: "#D9EBF7",
                    filter: "blur(0px) brightness(100%)",
                    borderRadius: "18px",
                    marginBottom: "20px",
                    borderStyle: "none",
                    borderRightStyle: "none",
                  }}
                >
                  <h2 style={{ textAlign: "center" }}>{t("information")}</h2>
                  <Card.Text style={{ marginBottom: "0px" }}>
                    - {t("date")} {research.date}
                    <br />- {t("location")} {research.location}
                    <br />- {t("reward")} {research.reward}
                    <br />- {t("type")} {research.researchType}
                  </Card.Text>
                </Card.Body>
              </Card>
            </Col>
            <Col
              md={6}
              style={{ background: "var(--bs-body-bg)", marginBottom: "60px" }}
            >
              <h2>{t("goal")}</h2>
              <p>{research.description}</p>
              <Card style={{ borderStyle: "none" }}>
                <Card.Body
                  style={{
                    borderStyle: "none",
                    borderColor: "transparent",
                    background: "var(--bs-body-bg)",
                  }}
                >
                  <Button
                    onClick={() => {
                      handleApplyButtonClick();
                      handleModalShow();
                    }}
                    variant="primary"
                    type="button"
                    style={{ borderRadius: "18px", textAlign: "right" }}
                  >
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      width="1em"
                      height="1em"
                      fill="currentColor"
                      viewBox="0 0 16 16"
                      className="bi bi-person-circle"
                      style={{ fontSize: "50px", marginRight: "10px" }}
                    >
                      <path d="M11 6a3 3 0 1 1-6 0 3 3 0 0 1 6 0z"></path>
                      <path
                        fillRule="evenodd"
                        d="M0 8a8 8 0 1 1 16 0A8 8 0 0 1 0 8zm8-7a7 7 0 0 0-5.468 11.37C3.242 11.226 4.805 10 8 10s4.757 1.225 5.468 2.37A7 7 0 0 0 8 1z"
                      ></path>
                    </svg>
                    {t("apply")}
                  </Button>

                  <Modal show={showModal} onHide={resetModalState}>
                    <Modal.Header closeButton>
                      <Modal.Title>{t("confirmation")}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                      {successMessage && (
                        <p style={{ color: "green" }}>{t("succesMessage")}</p>
                      )}
                      {errorMessage && (
                        <p style={{ color: "red" }}>{t("errorMessage")}</p>
                      )}
                    </Modal.Body>
                    <Modal.Footer>
                      <Button variant="secondary" onClick={handleModalClose}>
                        {t("close")}
                      </Button>
                    </Modal.Footer>
                  </Modal>
                </Card.Body>
              </Card>
            </Col>
          </Row>
        </>
      )}
    </Container>
  );
}

export default IndividualResearch;
