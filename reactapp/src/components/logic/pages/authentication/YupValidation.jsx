// importing yup for form validation
import * as yup from "yup";

import { useTranslation } from "react-i18next";

//yup form validation
export default function getYupSchema() {
  const { t } = useTranslation("yup_validation");
  return yup.object().shape({
    password: yup
      .string()
      .required(t("password_required"))
      .matches(
        /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s])[A-Za-z\d@$!%^*?&-]{8,}$/,
        t("password_validation"),
      )
      .min(6, t("password_validation_min"))
      .max(100, t("password_validation_max")),
    confirmPassword: yup
      .string()
      .oneOf([yup.ref("password"), null], t("passwords_must_match")),
    email: yup.string().required(t("email_required")).email(t("email_invalid")),
  });
}
