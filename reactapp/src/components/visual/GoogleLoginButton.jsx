import { GoogleLogin } from "@react-oauth/google";

import PropTypes from "prop-types";
import { useEffect, useState } from "react";

// importing the logic for the google login
import useLoginMutate from "../logic/pages/authentication/LoginMutate";
import { PostGoogleLogin } from "../../utils/api/authentication/GoogleLoginController";

// importing use navigate
import { useNavigate } from "react-router-dom";

// props definition
useGoogleLoginButton.propTypes = {
  login: PropTypes.bool,
};

export default function useGoogleLoginButton(login) {
  // state for the credential response
  const [credentialResponse, setCredentialResponse] = useState(null);

  const {
    mutate: mutateLogin,
    isPending,
    isError,
    error,
    isSuccess,
  } = useLoginMutate(PostGoogleLogin);

  const navigate = useNavigate();

  // navigating to the register page with the externalLogin param set to true
  const googleRegister = (credentialResponse) => {
    const googleLoginData = {
      jwtToken: credentialResponse.credential,
      provider: "Google",
    };
    navigate("/register?externalLogin=true", {
      state: {
        externalLoginData: googleLoginData,
      },
    });
  };

  // method to be called when the google login is successful
  const googleLoginSuccess = async (googleResponse) => {
    if (login) {
      mutateLogin(googleResponse);
    } else {
      googleRegister(googleResponse);
    }
  };

  useEffect(() => {
    if (isError) {
      if (error.response.status == 400) {
        console.log(error);
      }
      // if the google user is not found, redirect to the register page
      if (error.response.status == 401) {
        console.log("Google user not found with ", credentialResponse);
        googleRegister(credentialResponse);
      }
    }

    if (isSuccess) {
      // redirecting to the home page if the login is successful
      console.log("Redirecting to home page");
      navigate("/");
    }
  }, [isError, isSuccess]);

  // the google login button
  const GoogleLoginButtonHtml = (
    <GoogleLogin
      onSuccess={(googleResponse) => {
        setCredentialResponse(googleResponse);
        googleLoginSuccess(googleResponse);
      }}
      onError={() => {
        console.log("Login Failed");
      }}
    />
  );

  return { GoogleLoginButtonHtml, isPending, isError };
}
