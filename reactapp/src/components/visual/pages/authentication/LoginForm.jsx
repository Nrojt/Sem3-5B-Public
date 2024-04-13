import React from "react";
import "../../../../styles/LoginForm.css";
import { useTranslation } from "react-i18next";
import {
  Container,
  Row,
  Col,
  Form,
  Button,
  Card,
  Image,
  InputGroup,
} from "react-bootstrap";

// importing usenavigate from react-router-dom
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";

// importing custom google login button
import useGoogleLoginButton from "../../GoogleLoginButton";

// importing the show hide password button
import ShowHidePasswordButton from "./ShowHidePasswordButton";

// importing the logic for the login form
import { useLoginFormLogic } from "../../../../components/logic/pages/authentication/LoginFormLogic";

function LoginForm() {
  const {
    register,
    handleSubmit,
    errors,
    mutate,
    isPending,
    isError,
    isSuccess,
    isAuthenticated,
  } = useLoginFormLogic();
  // Define a function to handle the form submission
  const onSubmit = async (data) => {
    // Pass the form data and userType to the mutate function
    await mutate(data);
  };

  // loading in the translations
  const [t] = useTranslation("login_page");

  // importing the returns from the show hide password button
  const { showHidePasswordButton, showPassword } = ShowHidePasswordButton();

  // importing the google login button components
  const {
    GoogleLoginButtonHtml,
    isPending: isPendingGoogle,
    isError: isErrorGoogle,
  } = useGoogleLoginButton(true);

  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated()) {
      navigate("/");
    }
  }, [isAuthenticated()]);

  return (
    <section id="loginpage">
      <Container
        fluid
        className="p-3 align-items-center justify-content-center"
        id="login-box"
      >
        <Row className=" justify-content-center">
          <Col md={6} xl={4}>
            <Card>
              <Card.Body className="text-center d-flex flex-column align-items-center">
                <h2 className="fw-bold" style={{ color: "#2a54de" }}>
                  <Image
                    alt="Logo Stichting Accessibility"
                    src="/img/logo/icon_accessibility_on-blue_transp.png"
                    style={{ maxHeight: 50, marginRight: 10 }}
                  />
                  Accessibility
                </h2>
                <h1 className="fw-bold">{t("title_heading")}</h1>
                <Card className="login-box-content">
                  <Card.Body>{GoogleLoginButtonHtml}</Card.Body>
                </Card>
                <Row className="flex-row align-items-center">
                  <Col xs="auto" className="login-box-seperator-text">
                    <p>{t("or")}</p>
                  </Col>
                </Row>
                <Form onSubmit={handleSubmit(onSubmit)} data-bs-theme="light">
                  <Form.Group className="email-login">
                    <Form.Label>{t("email")}</Form.Label>
                    <Form.Control
                      {...register("email", { required: true, minLength: 6 })}
                      type="email"
                      className="email-input mb-3"
                      placeholder={t("email")}
                    />
                    {errors.email && (
                      <Form.Text className="text-muted">
                        {errors.email.message}
                      </Form.Text>
                    )}
                    <Form.Label>{t("password")}</Form.Label>
                    <InputGroup className="mb-3">
                      <Form.Control
                        {...register("password", {
                          required: true,
                          minLength: 6,
                        })}
                        type={showPassword ? "text" : "password"}
                        className="password-input mb-3"
                        placeholder={t("password")}
                      />
                      {showHidePasswordButton}
                    </InputGroup>
                    {errors.password && (
                      <Form.Text className="text-muted">
                        {errors.password.message}
                      </Form.Text>
                    )}
                  </Form.Group>
                  <Row className="mb-3">
                    <Form.Check
                      type="checkbox"
                      id="formCheck-1"
                      label={t("remember_me")}
                      className="form-check-inline"
                    />
                  </Row>
                  {(isError || isErrorGoogle) && <p>{t("login_failed")}</p>}{" "}
                  {(isPending || isPendingGoogle) && <p>{t("loading")}</p>}
                  {isSuccess && <p>{t("redirect")}</p>}
                  <Button
                    className="btn btn-primary shadow d-block w-100 mb-3"
                    type="submit"
                    id="submit-id-submit"
                  >
                    {t("title_heading")}
                  </Button>
                </Form>
                <a
                  id="forgot-password-link"
                  href="/todo"
                  className="fw-semibold"
                  style={{ color: "#2a54de", textDecoration: "underline" }}
                >
                  {t("forgot_password")}
                </a>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </section>
  );
}

export default LoginForm;
