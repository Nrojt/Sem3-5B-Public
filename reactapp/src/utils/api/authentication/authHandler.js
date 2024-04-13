// class for getting the auth requirements for react-auth-kit from the response

import tokenExpiryInMinutes from "./tokenExpiry";

export function handleSignInResponse(data) {
  // storing the expiry date of the tokens in localstorage
  localStorage.setItem("authTokenExpiration", data.authTokenExpiration);
  localStorage.setItem("refreshTokenExpiration", data.refreshTokenExpiration);
  // storing the user type in localstorage
  localStorage.setItem("userType", data.userType);

  // calculating the expiry time of the token
  const expiresIn = tokenExpiryInMinutes(data.authTokenExpiration);

  // calculating the expiry time of the refresh token
  const refreshTokenExpireIn = tokenExpiryInMinutes(
    data.refreshTokenExpiration,
  );

  // print statements for debugging
  console.log("Data passed in to handleSignInResponse:", data);
  console.log("Expires in:", expiresIn);
  console.log("Refresh token expires in:", refreshTokenExpireIn);
  console.log("User type:", data.userType);

  // create an object with the necessary values and return it
  // we dont want to save the actual token in the cookie (it has httponly set to
  // false which can cause xss attacks), so we save a string that indicates that
  // the user is authenticated
  return {
    token: "authenticated",
    expiresIn: expiresIn,
    tokenType: data.authTokenType,
    authState: { userType: data.userType },
    refreshToken: "refreshToken",
    refreshTokenExpireIn: refreshTokenExpireIn,
  };
}
