// import usertype information pages
import SignUpFormUserInformationCompany from "./SignUpUserInformationCompany";
import SignUpFormUserInformationDisabilityExpertGuardian from "./SignUpUserInformationDisabilityExpertGuardian";

//importing props
import PropTypes from "prop-types";

// prop validation
GetUserInformationPage.propTypes = {
  userType: PropTypes.string.isRequired,
  register: PropTypes.func.isRequired,
  errors: PropTypes.object.isRequired,
  externalLogin: PropTypes.bool.isRequired,
};

// function to get user information page per user type
function GetUserInformationPage({ userType, register, errors, externalLogin }) {
  return (
    <>
      {userType === "company" ? (
        <SignUpFormUserInformationCompany register={register} errors={errors} />
      ) : userType === "disabilityExpert" || userType === "guardian" ? (
        <SignUpFormUserInformationDisabilityExpertGuardian
          register={register}
          errors={errors}
          externalLogin={externalLogin}
        />
      ) : null}
    </>
  );
}

export default GetUserInformationPage;
