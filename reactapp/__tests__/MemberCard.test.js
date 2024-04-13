/**
 * @jest-environment jsdom
 */

import { render, screen } from "@testing-library/react";
import React from "react";

import MemberCard from "../src/components/visual/MemberCard";

test("membercard displays correct information", () => {
  // arrange
  const name = "abc";
  const subtitle = "test";
  const imageUrl = "abc";

  // act
  render(<MemberCard imageUrl={imageUrl} subtitle={subtitle} name={name} />);
  const memberCardElement = screen.getByTestId("membercard");
  const nameElement = screen.getByText(name);
  const subtitleElement = screen.getByText(subtitle);

  //assert
  expect(memberCardElement).toBeInTheDocument;
  expect(nameElement).toBeInTheDocument;
  expect(subtitleElement).toBeInTheDocument;
});
