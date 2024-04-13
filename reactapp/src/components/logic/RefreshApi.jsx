import { createRefresh } from "react-auth-kit";
import { PostRefreshBearerToken } from "../../utils/api/authentication/RefreshTokenController";
import { handleSignInResponse } from "../../utils/api/authentication/authHandler";
import { useSignIn } from "react-auth-kit";
import tokenExpiryInMinutes from "../../utils/api/authentication/tokenExpiry";

export const createRefreshApi = createRefresh({
  interval: 50, // Refreshs the token in every 50 minutes. Bearer token expires in 60 minutes
  refreshApiCallback: async ({ authUserState }) => {
    // logic for refresh token
    try {
      // calling the refresh token function
      const response = await PostRefreshBearerToken(authUserState.userType);
      console.log("Refresh token response: " + response);

      // handling the response
      const handledResponse = handleSignInResponse(response.data);

      return {
        isSuccess: true,
        newAuthToken: handledResponse.token,
        newAuthType: handledResponse.tokenType,
        newAuthTokenExpireIn: 60,
        newAuthUserState: handledResponse.authState, // the usertype might be different, so we need to update it
      };
    } catch (err) {
      console.log(err);
      return {
        isSuccess: false,
      };
    }
  },
});

// method to refresh the bearer token, called from App.jsx to refresh the bearer token on first page load
export const useRefreshBearerToken = () => {
  const signIn = useSignIn();

  const refreshBearerToken = async () => {
    console.log("Checking if the bearer token needs to be refreshed");
    // getting the refreshtoken and bearer token expiry time from the local storage
    const refreshTokenExpiration = localStorage.getItem(
      "refreshTokenExpiration",
    );
    const authTokenExpiration = localStorage.getItem("authTokenExpiration");
    // getting the user type from local storage
    const userTypeFromLocalStorage = localStorage.getItem("userType");

    // check for null values
    if (
      refreshTokenExpiration === null ||
      authTokenExpiration === null ||
      userTypeFromLocalStorage === null
    ) {
      console.log("No tokens found in local storage");
      return;
    }

    // getting the current ISO time
    const now = new Date().toISOString();
    console.log("Current time:", now);

    if (now > refreshTokenExpiration) {
      console.log("Refresh token has expired");
      throw new Error("Refresh token has expired");
    }

    // if the bearer token has expired, call the refresh token function
    if (now > authTokenExpiration) {
      // call the refresh token function and use signIn function to update the auth state
      console.log("Bearer token has expired");

      // Call the refreshApiCallback function
      const response = await createRefreshApi.refreshApiCallback({
        authUserState: { userType: userTypeFromLocalStorage },
      });

      // check if the refresh token was successful
      if (response.isSuccess) {
        console.log(response);
        // update the auth state
        signIn({
          token: response.newAuthToken,
          expiresIn: response.newAuthTokenExpireIn,
          tokenType: response.newAuthType,
          authState: response.newAuthUserState,
        });
        console.log("Refresh token successful");
        return;
      } else {
        // refresh token failed
        console.log("Refresh token failed");
        return;
      }
    }

    console.log("Bearer token is still valid, updating the auth state");
    const refreshTokenExpireIn = tokenExpiryInMinutes(
      localStorage.getItem("refreshTokenExpiration"),
    );

    // update the auth state
    signIn({
      token: "authenticated",
      expiresIn: 60,
      tokenType: "Bearer",
      authState: { userType: userTypeFromLocalStorage },
      refreshToken: "refreshToken",
      refreshTokenExpireIn: refreshTokenExpireIn,
    });
  };

  return refreshBearerToken;
};
