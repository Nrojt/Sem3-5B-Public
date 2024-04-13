import { Button } from "react-bootstrap";
import PropTypes from "prop-types";
import { useTranslation } from "react-i18next";

// prop validation
NextPreviousButton.propTypes = {
  onClick: PropTypes.func,
  tabIndex: PropTypes.string,
  isNext: PropTypes.bool.isRequired,
};

function NextPreviousButton({ onClick, tabIndex, isNext }) {
  const { t } = useTranslation("next_previous_button");

  return (
    <Button
      variant="primary"
      className="shadow d-block w-100 mb-3"
      type="submit"
      onClick={onClick}
      tabIndex={tabIndex}
    >
      {isNext ? t("next") : t("previous")}
    </Button>
  );
}

export default NextPreviousButton;
