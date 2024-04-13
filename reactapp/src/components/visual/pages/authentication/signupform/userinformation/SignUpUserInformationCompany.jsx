import { useTranslation } from "react-i18next";
import { Form } from "react-bootstrap";

/*
"companyName": "string",
  "companyDescription": "string",
  "companyAddress": "string",
  "companyCity": "string",
  "companyCountry": "string"
  "companyWebsite": "string"
*/

// importing props
import PropTypes from "prop-types";

// prop validation
SignUpFormUserInformationCompany.propTypes = {
  register: PropTypes.func.isRequired,
  errors: PropTypes.object.isRequired,
};

function SignUpFormUserInformationCompany({ register, errors }) {
  const { t } = useTranslation("signup_user_information_company_page");

  return (
    <>
      <Form.Label>{t("company_name")}</Form.Label>
      <Form.Control
        type="text"
        placeholder={t("company_name")}
        {...register("companyName", { required: true })}
      />
      <div>
        {errors.companyName && (
          <span className="text-danger">{t("company_name_required")}</span>
        )}
      </div>
      <Form.Label>{t("company_description")}</Form.Label>
      <Form.Control
        as="textarea"
        rows={3}
        placeholder={t("company_description")}
        {...register("companyDescription", { required: true })}
      />
      <div>
        {errors.companyDescription && (
          <span className="text-danger">
            {t("company_description_required")}
          </span>
        )}
      </div>
      <Form.Label>{t("company_address")}</Form.Label>
      <Form.Control
        type="text"
        placeholder={t("company_address")}
        {...register("companyAddress", { required: true })}
      />
      <div>
        {errors.companyAddress && (
          <span className="text-danger">{t("company_address_required")}</span>
        )}
      </div>
      <Form.Label>{t("company_city")}</Form.Label>
      <Form.Control
        type="text"
        placeholder={t("company_city")}
        {...register("companyCity", { required: true })}
      />
      <div>
        {errors.companyCity && (
          <span className="text-danger">{t("company_city_required")}</span>
        )}
      </div>
      <Form.Label>{t("company_country")}</Form.Label>
      <Form.Control
        type="text"
        placeholder={t("company_country")}
        {...register("companyCountry", { required: true })}
      />
      <div>
        {errors.companyCountry && (
          <span className="text-danger">{t("company_country_required")}</span>
        )}
      </div>
      <Form.Label>{t("company_website")}</Form.Label>
      <Form.Control
        type="text"
        placeholder={t("company_website")}
        {...register("companyWebsite", { required: true })}
      />
      <div>
        {errors.companyWebsite && (
          <span className="text-danger">{t("company_website")}</span>
        )}
      </div>
    </>
  );
}

export default SignUpFormUserInformationCompany;
