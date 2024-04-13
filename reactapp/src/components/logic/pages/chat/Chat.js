import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { useState } from "react";

export function useManageChat() {
  const [connection, setConnection] = useState();

  const joinChatRoom = async (companyId, disabilityExpertId) => {
    if (companyId !== null && disabilityExpertId !== null) {
      throw new Error("Cannot pass both companyId and disabilityExpertId");
    }

    try {
      // initiate connection
      const conn = new HubConnectionBuilder()
        .withUrl(import.meta.env.VITE_ASP_API_URL + "chat")
        .configureLogging(LogLevel.Information)
        .withAutomaticReconnect()
        .build();

      // create a UserConnection object
      const userConnection = {
        disabilityExpertId: disabilityExpertId,
        companyId: companyId,
      };

      // start connection
      await conn.start();
      await conn.invoke("JoinChatNoResearch", userConnection);

      // set connection
      setConnection(conn);
    } catch (error) {
      console.log(error);
    }
  };

  const sendMessage = async (message) => {
    try {
      await connection.invoke("SendMessage", message);
    } catch (error) {
      console.log(error);
    }
  };

  const receiveMessage = (callback) => {
    try {
      connection.on("ReceiveMessage", (message) => {
        console.log(message);
        callback(message);
      });
    } catch (error) {
      console.log(error);
    }
  };

  return { joinChatRoom, sendMessage, receiveMessage };
}
