import { InputGroup, Button } from "react-bootstrap";
import { useState } from "react";
import { useTranslation } from "react-i18next";

// import styling
import "../../../../styles/ShowHidePasswordButton.css";

export default function ShowHidePasswordButton() {
  const [showPassword, setShowPassword] = useState(false);

  const { t } = useTranslation("show_hide_button");

  const showHidePasswordButton = (
    <>
      <InputGroup.Text className="password-show-hide-input-text">
        <Button
          variant="outline-secondary"
          onClick={() => setShowPassword(!showPassword)}
          className="password-show-hide-button"
        >
          {showPassword ? t("hide") : t("show")}
        </Button>
      </InputGroup.Text>
    </>
  );

  return {
    showHidePasswordButton,
    showPassword,
  };
}
