import React, { useState } from "react";
import { Button, Row, Form, Container } from "react-bootstrap";
import { useQuery } from "@tanstack/react-query";
import { getCompanies } from "../../../../utils/api/pages/chat/getCompanies";
import Select from "react-select";

import { useManageChat } from "../../../logic/pages/chat/Chat";
import { ChatScreen } from "./ChatScreen";

import { useTranslation } from "react-i18next";

export function ChatConnectionDisabilityExpert() {
  const { t } = useTranslation("chat_connection_page");
  const { joinChatRoom, sendMessage, receiveMessage } = useManageChat();

  const [selectedCompany, setSelectedCompany] = useState("");
  const [isChatJoined, setIsChatJoined] = useState(false);

  // query for getting the companies, query can be used instead of mutation with get request
  const result = useQuery({
    queryKey: ["companies"],
    queryFn: getCompanies,
    enabled: true, // This will make the query auto-run when the component is mounted
  });

  const options =
    result.isSuccess &&
    result.data.data.map((company) => ({
      value: company.companyId,
      label: company.companyName,
    }));

  const handleJoinChat = () => {
    joinChatRoom(selectedCompany.companyId, null)
      .then(() => {
        // Set isChatJoined to true after successfully joining the chat room
        setIsChatJoined(true);
      })
      .catch((error) => {
        // Handle any errors here
        console.error("Failed to join chat room:", error);
      });
  };

  // method for handling the change of the select
  const handleChange = (selectedOption) => {
    const selectedExpert = result.data.data.find(
      (company) => company.companyId === selectedOption.value,
    );
    setSelectedCompany(selectedExpert);
  };

  if (isChatJoined) {
    return (
      <ChatScreen
        sendMessage={sendMessage}
        receiveMessage={receiveMessage}
        displayName={selectedCompany.companyName}
      />
    );
  }

  return (
    <Container>
      <h1>{t("title_heading")}</h1>
      <h2>{t("title_subheading_disabilityexpert")}</h2>
      <Form
        onSubmit={(e) => {
          handleJoinChat();
          e.preventDefault();
        }}
      >
        <Form.Group as={Row} controlId="formPlaintextEmail">
          <Select
            options={options}
            isClearable={true}
            onChange={handleChange}
          />
        </Form.Group>
        <br />
        <Button variant="success" type="submit">
          {t("join_chat")}
        </Button>
      </Form>
    </Container>
  );
}
