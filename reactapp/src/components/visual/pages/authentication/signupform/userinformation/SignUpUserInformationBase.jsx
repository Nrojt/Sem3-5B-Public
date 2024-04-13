//import the previous button
import NextPreviousButton from "../NextPreviousButton";

import { useTranslation } from "react-i18next";
import { useState } from "react";
import { useForm } from "react-hook-form";

import { Button, Form } from "react-bootstrap";

// importing the getuserinformationpage function
import GetUserInformationPage from "./GetUserInformationPage";

// importing the login function
import { useCreateAccountLogic } from "../../../../../logic/pages/authentication/SignUpFormLogic";

// prop types
import PropTypes from "prop-types";

// prop validation
SignUpFormUserInformationBase.propTypes = {
  onPrevious: PropTypes.func.isRequired,
  userData: PropTypes.object.isRequired,
  combineUserData: PropTypes.func.isRequired,
  externalLogin: PropTypes.bool.isRequired,
};

function SignUpFormUserInformationBase({
  onPrevious,
  userData,
  combineUserData,
  externalLogin,
}) {
  const { t } = useTranslation("signup_user_information_base_page");

  const [userInformationValues, setUserInformationValues] = useState({});

  // The useForm hook is used to handle the form
  const {
    register,
    handleSubmit,
    getValues,
    formState: { errors },
  } = useForm({
    // the default values are set to the values from the userData prop
    defaultValues: {
      postalcode:
        userInformationValues.postalcode ||
        (userData && userData.postalcode) ||
        "",
      phoneNumber:
        userInformationValues.phoneNumber ||
        (userData && userData.phoneNumber) ||
        "",
    },
  });

  // The UseRegistrationFormLogic hook is used to handle the form submission
  const { mutate, error, isPending, isError } = useCreateAccountLogic();

  const onSubmit = (data) => {
    setUserInformationValues(data);
    console.log(data);
  };

  const onSubmitPrevious = () => {
    // getting the data from the forms
    const data = getValues();

    // handling the data
    onSubmit(data);
    onPrevious(data);
  };

  const create_account = (data) => {
    onSubmit(data);
    const combinedUserData = combineUserData(data);
    console.log(combinedUserData);

    // calling the mutate function to call the PostRegister function
    mutate(combinedUserData);
  };

  return (
    <>
      <h1 className="fw-bold" style={{ color: "#2a54de" }}>
        {t("title_heading")}
      </h1>
      {error &&
        error.response &&
        error.response.data &&
        error.response.data.message && (
          <div className="alert alert-danger" role="alert">
            {error.response.data.message}
          </div>
        )}
      <Form onSubmit={handleSubmit(create_account)} data-bs-theme="light">
        <Form.Group className="mb-3">
          <Form.Label>{t("postalcode")}</Form.Label>
          <Form.Control
            type="text"
            placeholder={t("postalcode_placeholder")}
            {...register("postalcode", { required: true })}
          />
          <div>
            {errors.postalcode && (
              <span className="text-danger">{t("postalcode_error")}</span>
            )}
          </div>
          <Form.Label>{t("phone_number")}</Form.Label>
          <Form.Control
            type="text"
            placeholder={t("phone_number_placeholder")}
            {...register("phoneNumber", { required: true })}
          />
          <div>
            {errors.phoneNumber && (
              <span className="text-danger">{t("phoneNumber_error")}</span>
            )}
          </div>
          <GetUserInformationPage
            userType={userData.userType}
            register={register}
            errors={errors}
            externalLogin={externalLogin}
          />
        </Form.Group>
        <Button
          variant="primary"
          className="shadow d-block w-100 mb-3"
          type="submit"
        >
          {t("create_account")}
        </Button>
        <NextPreviousButton isNext={false} onClick={onSubmitPrevious} />
      </Form>
      {isPending && <div className="alert alert-info">{t("loading")}</div>}
      {isError && (
        <div className="alert alert-danger">{t("account_creation_error")}</div>
      )}
    </>
  );
}

export default SignUpFormUserInformationBase;
