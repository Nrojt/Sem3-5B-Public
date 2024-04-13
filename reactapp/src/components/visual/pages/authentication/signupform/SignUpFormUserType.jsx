import { Form } from "react-bootstrap";
import PropTypes from "prop-types";
import { useTranslation } from "react-i18next";
import { useState, useEffect } from "react";
import { useForm } from "react-hook-form";

// importing the next and previous button
import NextPreviousButton from "./NextPreviousButton";

// prop validation
SignUpFormUserType.propTypes = {
  onNext: PropTypes.func.isRequired,
  onPrevious: PropTypes.func.isRequired,
  userData: PropTypes.object.isRequired,
};

// this form part is used to select the user type the user wants to sign up as
function SignUpFormUserType({ onNext, onPrevious, userData }) {
  const { t } = useTranslation("signup_user_type_page");

  // useState for user type
  const [userType, setUserType] = useState("");

  // The useForm hook is used to handle the form
  const {
    register,
    watch,
    getValues,
    handleSubmit,
    formState: { errors },
  } = useForm({
    // the default values are set to the values from the userData prop
    defaultValues: {
      userType: userType || (userData && userData.userType) || "",
    },
  });

  useEffect(() => {
    setUserType(watch());
    console.log(watch());
  }, [watch]);

  const onSubmit = (data) => {
    console.log(data);
    setUserType(data);
  };

  const onSubmitNext = (data) => {
    onSubmit(data);
    onNext(data);
  };

  const onSubmitPrevious = () => {
    // getting the values from the form
    const data = getValues();

    // handling the data
    onSubmit(data);
    onPrevious(data);
  };

  return (
    <>
      <h1 className="fw-bold" style={{ color: "#2a54de" }}>
        {t("title_heading")}
      </h1>
      <p className="fw-semibold" style={{ fontSize: 18 }}>
        {t("choose_user_type")}
      </p>
      <Form data-bs-theme="light">
        <Form.Group className="mb-3">
          <Form.Check
            type="radio"
            label={t("i_am_a") + t("disability_expert")}
            name="formUserTypeRadios"
            id="formUserTypeDisabilityExpert"
            tabIndex="1"
            {...register("userType", {
              required: "User type is required",
            })}
            value="disabilityExpert"
          />
          <Form.Check
            type="radio"
            label={t("i_am_a") + t("guardian")}
            name="formUserTypeRadios"
            id="formUserTypeGuardian"
            tabIndex="2"
            {...register("userType", {
              required: "User type is required",
            })}
            value="guardian"
          />
          <Form.Check
            type="radio"
            label={t("i_am_a") + t("company")}
            name="formUserTypeRadios"
            id="formUserTypeCompany"
            tabIndex="3"
            {...register("userType", {
              required: "User type is required",
            })}
            value="company"
          />
          {errors.formUserTypeRadios && (
            <p>{errors.formUserTypeRadios.message}</p>
          )}
        </Form.Group>
        <NextPreviousButton
          onClick={handleSubmit(onSubmitNext)}
          tabIndex="4"
          isNext={true}
        />
        <NextPreviousButton
          onClick={onSubmitPrevious}
          tabIndex="5"
          isNext={false}
        />
      </Form>
    </>
  );
}

export default SignUpFormUserType;
