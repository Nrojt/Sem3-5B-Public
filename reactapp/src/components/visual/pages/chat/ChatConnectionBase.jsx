import { Container } from "react-bootstrap";

import { ChatConnectionDisabilityExpert } from "./ChatConnectionDisabilityExpert";
import { ChatConnectionCompany } from "./ChatConnectionCompany";

import { useAuthUser } from "react-auth-kit";

const ChatConnectionBase = () => {
  // get the user type from the auth hook
  const auth = useAuthUser();

  console.log(auth().userType);

  return (
    <Container>
      {(auth().userType === "DisabilityExpertWithoutGuardian" ||
        auth().userType === "DisabilityExpertWithGuardian") && (
        <ChatConnectionDisabilityExpert />
      )}
      {auth().userType === "CompanyApproved" && <ChatConnectionCompany />}
    </Container>
  );
};
export default ChatConnectionBase;
