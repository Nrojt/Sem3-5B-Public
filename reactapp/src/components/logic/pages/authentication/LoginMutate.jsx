import { useSignIn } from "react-auth-kit";
import { handleSignInResponse } from "../../../../utils/api/authentication/authHandler";
import { useMutation } from "@tanstack/react-query";

export default function useLoginMutate(axiosPost) {
  // useSignIn hook from react-auth-kit
  const signIn = useSignIn();

  // The useMutation hook is used to call the Login function
  const { mutate, isPending, isError, isSuccess, error } = useMutation({
    mutationFn:
      // The mutation function, which is called when mutate() is invoked
      async (formData) => {
        // calling the correct login function
        const response = axiosPost(formData);
        return response;
      },

    // This function will fire when the mutationFn is successful
    onSuccess: (response) => {
      console.log("Response Data:", response);
      // checking if the login was successful
      if (response && response.status === 200) {
        console.log("Login successful");
        // checking if the response has data
        if (response.data) {
          // handling the sign in response
          const handledResponse = handleSignInResponse(response.data);

          // set the authentication state
          signIn({
            token: handledResponse.token,
            expiresIn: handledResponse.expiresIn,
            tokenType: handledResponse.authTokenType,
            authState: handledResponse.authState,
            refreshToken: handledResponse.refreshToken,
            refreshTokenExpireIn: handledResponse.refreshTokenExpireIn,
          });
        } else {
          // error handling, response should always have data
          throw new Error("Login failed, response has no data");
        }
      } else {
        // error handling, response should be 200
        throw new Error("Login failed, response not 200");
      }
    },

    onError: () => {
      console.log("Encountered an error while trying to log in");
    },
  });

  return {
    mutate,
    isPending,
    isError,
    isSuccess,
    error,
  };
}
