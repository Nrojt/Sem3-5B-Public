import { useTranslation } from "react-i18next";
import { Card, Button } from "react-bootstrap";
import { useState } from "react";

import PropTypes from "prop-types";

ProfileCard.propTypes = {
  label: PropTypes.string.isRequired,
  originalValue: PropTypes.string,
  setValueParent: PropTypes.func.isRequired,
};

export default function ProfileCard({ label, originalValue, setValueParent }) {
  const [t] = useTranslation("profile_card");

  const [editingValue, setEditingValue] = useState(false);
  const [value, setValue] = useState(originalValue);

  const handleValueChange = (e) => {
    setValue(e.target.value);
    setValueParent(e.target.value);
  };

  return (
    <Card.Text className="border-bottom mb-2 pb-2 profile-Card-Text">
      <strong>{label}</strong>{" "}
      {editingValue ? (
        <input type="text" value={value} onChange={handleValueChange} />
      ) : (
        value
      )}
      <Button
        type="primary"
        className="profile-btn"
        onClick={() => setEditingValue(!editingValue)}
      >
        {editingValue ? t("save") : t("edit")}
      </Button>
    </Card.Text>
  );
}
