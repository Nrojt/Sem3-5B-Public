import { Container, Row, Col, Card } from "react-bootstrap";
import { useEffect } from "react";

// importing the form parts
import SignUpFormEmailPassword from "./SignUpFormEmailPassword";
import SignUpFormUserType from "./SignUpFormUserType";
import SignUpFormUserInformationBase from "./userinformation/SignUpUserInformationBase";

// importing singupform logic
import { signUpBaseLogic } from "../../../../logic/pages/authentication/SignUpFormLogic";

// importing useLocation from react router dom to get the query params
import { useLocation } from "react-router-dom";

function SignUpForm() {
  // getting the query params
  const location = useLocation();
  const query = new URLSearchParams(location.search);

  // checking if the externalLogin param is set to true
  const externalLogin = query.get("externalLogin") === "true";
  console.log("External Login: ", externalLogin);

  // getting the data from the signUpBaseLogic hook
  const {
    SCREENS,
    screenIndex,
    onNext,
    onPrevious,
    userData,
    combineUserData,
  } = signUpBaseLogic();

  // if the externalLogin is true and the credentialResponse is not null
  // then we add the jwt token to userData
  useEffect(() => {
    if (externalLogin) {
      const loginState = location.state;
      const externalLoginData =
        loginState == null ? null : loginState.externalLoginData;

      if (externalLoginData != null) {
        combineUserData(externalLoginData);
      }
    }
  }, [externalLogin, location.state]); // only run when these variables change

  useEffect(() => {
    console.log("User Data: ", userData);
  }, [userData]);

  return (
    <Container className="py-5">
      <Row className="d-flex justify-content-center">
        <Col md={6} xl={4}>
          <Card>
            <Card.Body className="text-center d-flex flex-column align-items-center">
              {SCREENS[screenIndex] === "EMAIL_PASSWORD" && (
                <SignUpFormEmailPassword
                  onNext={onNext}
                  userData={userData}
                  externalLogin={externalLogin}
                />
              )}
              {SCREENS[screenIndex] === "USER_TYPE" && (
                <SignUpFormUserType
                  onNext={onNext}
                  onPrevious={onPrevious}
                  userData={userData}
                />
              )}
              {SCREENS[screenIndex] === "USER_DETAILS" && (
                <SignUpFormUserInformationBase
                  onPrevious={onPrevious}
                  userData={userData}
                  combineUserData={combineUserData}
                  externalLogin={externalLogin}
                />
              )}
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
}

export default SignUpForm;
