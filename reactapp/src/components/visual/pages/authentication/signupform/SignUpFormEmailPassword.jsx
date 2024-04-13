import { Form, InputGroup } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import { useForm } from "react-hook-form";
import { useState } from "react";

// importing the schema for yup
import { yupResolver } from "@hookform/resolvers/yup";
import getYupSchema from "../../../../logic/pages/authentication/YupValidation";

//importing the next button
import NextPreviousButton from "./NextPreviousButton";

// importing the show hide password button
import ShowHidePasswordButton from "../ShowHidePasswordButton";

// importing the google login button
import useGoogleLoginButton from "../../../GoogleLoginButton";

// The SignUpFormEmailPassword component is used to render the email and password form
import PropTypes from "prop-types";

// prop validation
SignUpFormEmailPassword.propTypes = {
  onNext: PropTypes.func.isRequired,
  userData: PropTypes.object.isRequired,
  externalLogin: PropTypes.bool.isRequired,
};

function SignUpFormEmailPassword({ onNext, userData, externalLogin }) {
  // translations
  const { t } = useTranslation("signup_email_password_page");

  // yup form validation
  const schema = getYupSchema();

  // disable email field if external login
  if (externalLogin) {
    schema.fields.email = undefined;
  }

  const [emailPasswordValues, setEmailPasswordValues] = useState({});

  // importing the returns from the show hide password button
  const { showHidePasswordButton: shpButton1, showPassword: showPassword1 } =
    ShowHidePasswordButton();
  const { showHidePasswordButton: shpButton2, showPassword: showPassword2 } =
    ShowHidePasswordButton();

  // using google login button
  const {
    GoogleLoginButtonHtml,
    isPending: isPendingGoogle,
    isError: isErrorGoogle,
  } = useGoogleLoginButton(false);

  // The useForm hook is used to handle the form
  const {
    register,
    watch,
    handleSubmit,
    formState: { errors },
  } = useForm({
    // resolver for password validation
    resolver: yupResolver(schema),
    // the default values are set to the values from the userData prop
    defaultValues: {
      email:
        emailPasswordValues.email ||
        (userData && userData.email) ||
        "placeholder@email.com",
      password:
        emailPasswordValues.password || (userData && userData.password) || "",
      confirmPassword:
        emailPasswordValues.confirmPassword ||
        (userData && userData.confirmPassword) ||
        "",
    },
  });

  // handling the form submission
  const onSubmit = (data) => {
    setEmailPasswordValues(data);
    onNext(data); // go to the next screen
  };

  // Watch the 'password' field to get its value
  const passwordValue = watch("password", "");

  return (
    <>
      <h1 className="fw-bold" style={{ color: "#2a54de" }}>
        {t("title_heading")}
      </h1>
      <p className="fw-semibold" style={{ fontSize: 18 }}>
        {!externalLogin && (
          <>
            {t("join_us")}
            <br />
            {t("already_has_account")}{" "}
            <a href="/login" style={{ textDecoration: "underline" }}>
              {t("login_here")}
            </a>
          </>
        )}
        {externalLogin && <>{t("external_login_provider")}</>}
      </p>

      <Form data-bs-theme="light">
        <Form.Group className="email-signup mb-3">
          {!externalLogin && (
            <>
              <Form.Label className="fw-bold">
                {t("email_placeholder")}
              </Form.Label>
              <Form.Control
                {...register("email", { required: true })}
                type="email"
                className="email mb-3"
                placeholder={t("email_placeholder")}
              />
              <div>
                {errors.email && (
                  <Form.Text className="text-muted">
                    {errors.email.message}
                  </Form.Text>
                )}
              </div>
            </>
          )}
          <Form.Label className="fw-bold">
            {t("password_placeholder")}
          </Form.Label>
          <InputGroup className="mb-3">
            <Form.Control
              {...register("password", {
                required: true,
              })}
              type={showPassword1 ? "text" : "password"}
              className="password mb-3"
              placeholder={t("password_placeholder")}
            />
            {shpButton1}
          </InputGroup>
          <div>
            {errors.password && (
              <Form.Text className="text-muted">
                {errors.password.message}
              </Form.Text>
            )}
          </div>
          <Form.Label className="fw-bold">
            {t("password_verify_placeholder")}{" "}
          </Form.Label>
          <InputGroup className="mb-3 position-relative">
            <Form.Control
              {...register("confirmPassword", {
                validate: (value) =>
                  value === passwordValue || t("passwords_must_match"),
              })}
              type={showPassword2 ? "text" : "password"}
              className="password-verify mb-3"
              placeholder={t("password_verify_placeholder")}
            />
            {shpButton2}
          </InputGroup>
          <div>
            {errors.confirmPassword && (
              <Form.Text className="text-muted">
                {errors.confirmPassword.message}
              </Form.Text>
            )}
          </div>
        </Form.Group>
        <NextPreviousButton onClick={handleSubmit(onSubmit)} isNext={true} />
        <hr />
        {!externalLogin &&
          !isPendingGoogle &&
          !isErrorGoogle &&
          GoogleLoginButtonHtml}
        {isPendingGoogle && <p>{t("loading")}</p>}
        {isErrorGoogle && <p>{t("google_login_failed")}</p>}
      </Form>
    </>
  );
}

export default SignUpFormEmailPassword;
