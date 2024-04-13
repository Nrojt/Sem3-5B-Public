import { useTranslation } from "react-i18next";
import { Form } from "react-bootstrap";

// importing props
import PropTypes from "prop-types";

/*
"firstName": "string",
  "lastName": "string",
  "birthYear": 0,
*/

// prop validation
SignUpFormUserInformationDisabilityExpertGuardian.propTypes = {
  register: PropTypes.func.isRequired,
  errors: PropTypes.object.isRequired,
  externalLogin: PropTypes.bool.isRequired,
};

function SignUpFormUserInformationDisabilityExpertGuardian({
  register,
  errors,
  externalLogin,
}) {
  const { t } = useTranslation(
    "signup_user_information_disabilityexpertguardian_page",
  );

  return (
    <>
      {!externalLogin && (
        <>
          <Form.Label>{t("first_name")}</Form.Label>
          <Form.Control
            type="text"
            placeholder={t("first_name")}
            {...register("firstName", { required: true })}
          />
          <div>
            {errors.firstName && (
              <span className="text-danger">{t("first_name_required")}</span>
            )}
          </div>
          <Form.Label>{t("last_name")}</Form.Label>
          <Form.Control
            type="text"
            placeholder={t("last_name")}
            {...register("lastName", { required: true })}
          />
          <div>
            {errors.lastName && (
              <span className="text-danger">{t("last_name_required")}</span>
            )}
          </div>
        </>
      )}
      <Form.Label>{t("birth_year")}</Form.Label>
      <Form.Control
        type="number"
        placeholder={t("birth_year")}
        {...register("birthYear", { required: true })}
      />
      <div>
        {errors.birthYear && (
          <span className="text-danger">{t("birth_year_required")}</span>
        )}
      </div>
    </>
  );
}

export default SignUpFormUserInformationDisabilityExpertGuardian;
