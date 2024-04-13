import { useTranslation } from "react-i18next";
import { Container, Card, Row, Col, Button } from "react-bootstrap";
import "../../../../styles/Profile.css";
import { useAuthUser } from "react-auth-kit";
import { useState } from "react";

// importing the profile pages
import ProfileDisabilityExpert from "./ProfileDisabilityExpert";
import { putUser } from "@/utils/api/pages/profilecontroller.js";

function Profile() {
  // get the translation component
  const [t] = useTranslation("profile_page");

  // get the user type from the auth hook
  const auth = useAuthUser();

  const [user, setUser] = useState({});

  const handleOnClick = async () => {
    const response = await putUser(user);
    console.log(response);
  };

  return (
    <Container>
      <Row>
        <Col>
          <Card className="profile-card">
            <h2 className="profile_title">{t("title")}</h2>
            {auth().userType === "DisabilityExpertWithoutGuardian" ||
            auth().userType === "DisabilityExpertWithGuardian" ? (
              <ProfileDisabilityExpert
                loadingText={t("loading")}
                disabilityExpert={user}
                setDisabilityExpert={setUser}
              />
            ) : (
              <p>{t("not_supported")}</p>
            )}
          </Card>
        </Col>
      </Row>
      <Row>
        <Button onClick={handleOnClick} type="primary" className="profile-btn">
          {t("submit")}
        </Button>
      </Row>
    </Container>
  );
}

export default Profile;
