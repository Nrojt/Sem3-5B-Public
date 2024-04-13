// External libraries
import { useForm } from "react-hook-form";
import { useIsAuthenticated } from "react-auth-kit";

// importing the schema for yup
import { yupResolver } from "@hookform/resolvers/yup";
import getYupSchema from "./YupValidation";

// Internal modules
import { PostLogin } from "../../../../utils/api/authentication/LoginController";
import useLoginMutate from "./LoginMutate";

export function useLoginFormLogic() {
  // checking if the user is authenticated
  const isAuthenticated = useIsAuthenticated();

  // yup form validation
  const schema = getYupSchema();

  // useForm hook from react-hook-form
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    // resolver for email validation
    resolver: yupResolver(schema),
  });

  const { mutate, isPending, isError, isSuccess, error } =
    useLoginMutate(PostLogin);

  // returning the variables and functions that will be used in the LoginForm component
  return {
    isAuthenticated,
    register,
    handleSubmit,
    errors,
    mutate,
    isPending,
    isError,
    isSuccess,
    error,
  };
}
