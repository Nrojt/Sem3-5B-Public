import { useNavigate } from "react-router-dom";
import { useMutation } from "@tanstack/react-query";
import { useState } from "react";

import { PostRegister } from "../../../../utils/api/authentication/RegisterController";

export function useCreateAccountLogic() {
  // navigate from react-router-dom to change pages
  const navigate = useNavigate();

  // The useMutation hook is used to call the PostRegister function
  const { mutate, error, isPending, isSuccess, isError } = useMutation({
    mutationFn:
      // The mutation function, which is called when mutate() is invoked
      async (formData) => {
        // calling the correct register function
        const response = PostRegister(formData);
        return response;
      },
    onSuccess: (response) => {
      console.log("Response Data:", response);
      // checking if the register was successful
      if (response && response.status === 200) {
        console.log("Register successful");
        // checking if the response has data
        if (response.data) {
          // navigating to the login page
          navigate("/login");
        }
      }
    },
    onError: (error) => {
      console.log("Error:", error);
      if (error.response && error.response.status === 400) {
        // Assuming error.response.data is the object {"message":"Email is already in use"}
        alert(error.response.data.message);
      }
    },
  });

  return {
    mutate,
    isError,
    isSuccess,
    isPending,
    error,
  };
}

export function signUpBaseLogic() {
  const SCREENS = ["EMAIL_PASSWORD", "USER_TYPE", "USER_DETAILS"];

  const [screenIndex, setScreenIndex] = useState(0); // start with the first screen

  const [userData, setUserData] = useState({});

  // this function is used to go to the next screen
  const onNext = (data) => {
    console.log(screenIndex);
    if (screenIndex < SCREENS.length - 1) {
      setScreenIndex(screenIndex + 1); // go to the next screen
    }
    combineUserData(data); // save the data from the current screen
  };

  // this function is used to go to the previous screen
  const onPrevious = (data) => {
    console.log(screenIndex);
    if (screenIndex > 0) {
      setScreenIndex(screenIndex - 1); // go to the previous screen
    }
    combineUserData(data); // save the data from the current screen
  };

  const combineUserData = (data) => {
    const combinedData = { ...userData, ...data };
    setUserData(combinedData); // save the data from the current screen
    return combinedData;
  };

  return {
    SCREENS,
    screenIndex,
    setScreenIndex,
    userData,
    setUserData,
    onNext,
    onPrevious,
    combineUserData,
  };
}
