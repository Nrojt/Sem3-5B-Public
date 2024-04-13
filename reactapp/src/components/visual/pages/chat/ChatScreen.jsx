import React, { useState, useEffect, useRef } from "react";
import { Row, Col, Form, Button } from "react-bootstrap";

import PropTypes from "prop-types";

//importing the css
import "../../../../styles/ChatScreen.css";

import { useTranslation } from "react-i18next";

export function ChatScreen({ sendMessage, receiveMessage, displayName }) {
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState([]);
  const hasReceivedMessage = useRef(false);

  useEffect(() => {
    if (!hasReceivedMessage.current) {
      receiveMessage((newMessage) => {
        setMessages((prevMessages) => [
          ...prevMessages,
          { text: newMessage, isSent: false },
        ]);
      });
      hasReceivedMessage.current = true;
    }
  }, []);

  const handleFormSubmit = (event) => {
    event.preventDefault();
    sendMessage(message);
    setMessages((prevMessages) => [
      ...prevMessages,
      { text: message, isSent: true },
    ]);
    setMessage("");
  };

  const { t } = useTranslation("chat_page");

  return (
    <div className="chat-screen">
      <Row className="chat-row">
        <Col className="chat-col">
          <h1 className="chat-title">{t("title_heading")}</h1>
          <h2 className="chat-subtitle">
            {t("title_subheading", { displayName })}
          </h2>
          <div
            className="messages"
            style={{
              overflowY: "auto",
              height: "300px",
              background: "white",
              padding: "10px",
              borderRadius: "5px",
            }}
          >
            {messages.map((msg, index) => (
              <p
                key={index}
                className={`message ${
                  msg.isSent ? "sent-message" : "received-message"
                }`}
              >
                {msg.text}
              </p>
            ))}
          </div>
        </Col>
      </Row>
      <Row className="chat-row">
        <Form onSubmit={handleFormSubmit} className="message-form">
          <Form.Group className="message-form-group">
            <Row>
              <Col>
                <Form.Control
                  type="text"
                  value={message}
                  onChange={(e) => setMessage(e.target.value)}
                  placeholder="Type a message"
                  className="message-input"
                />
              </Col>
              <Col xs="auto">
                <Button type="submit" className="send-button">
                  {t("send_button")}
                </Button>
              </Col>
            </Row>
          </Form.Group>
        </Form>
      </Row>
    </div>
  );
}

ChatScreen.propTypes = {
  sendMessage: PropTypes.func.isRequired,
  receiveMessage: PropTypes.func.isRequired,
  displayName: PropTypes.string.isRequired,
};
