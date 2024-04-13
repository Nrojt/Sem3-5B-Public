import React, { useState } from "react";
import { Button, Row, Form, Container } from "react-bootstrap";
import { useQuery } from "@tanstack/react-query";
import { getDisabilityExperts } from "../../../../utils/api/pages/chat/getDisabilityExperts";
import Select from "react-select";

import { useManageChat } from "../../../logic/pages/chat/Chat";

import { ChatScreen } from "./ChatScreen";

import { useTranslation } from "react-i18next";

export function ChatConnectionCompany() {
  const { t } = useTranslation("chat_connection_page");
  const { joinChatRoom, sendMessage, receiveMessage } = useManageChat();

  const [selectedExpert, setSelectedExpert] = useState(null);
  const [isChatJoined, setIsChatJoined] = useState(false);

  // query for getting the companies, query can be used instead of mutation with get request
  const result = useQuery({
    queryKey: ["disabilityExperts"],
    queryFn: getDisabilityExperts,
    enabled: true, // This will make the query auto-run when the component is mounted
  });

  const options =
    result.isSuccess &&
    result.data.data.map((disabilityExpert) => ({
      value: disabilityExpert.disabilityExpertId,
      label: disabilityExpert.firstName + " " + disabilityExpert.lastName,
    }));

  // method for handling the change of the select
  const handleChange = (selectedOption) => {
    const selectedExpert = result.data.data.find(
      (expert) => expert.disabilityExpertId === selectedOption.value,
    );
    setSelectedExpert(selectedExpert);
  };

  const handleJoinChat = () => {
    joinChatRoom(null, selectedExpert.disabilityExpertId)
      .then(() => {
        // Set isChatJoined to true after successfully joining the chat room
        setIsChatJoined(true);
      })
      .catch((error) => {
        // Handle any errors here
        console.error("Failed to join chat room:", error);
      });
  };

  if (isChatJoined && selectedExpert) {
    return (
      <ChatScreen
        sendMessage={sendMessage}
        receiveMessage={receiveMessage}
        displayName={selectedExpert.firstName + " " + selectedExpert.lastName}
      />
    );
  }

  return (
    <Container>
      <h1 className="text-center">{t("title_heading")}</h1>
      <h2>{t("title_subheading_company")}</h2>
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
