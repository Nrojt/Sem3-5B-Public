import { useTranslation } from "react-i18next";
import * as yup from "yup";
import getYupSchema from "../src/components/logic/pages/authentication/YupValidation";
import { expect } from "@jest/globals";

jest.mock("react-i18next", () => ({
  useTranslation: jest.fn().mockReturnValue({ t: jest.fn() }),
}));

describe("getYupSchema", () => {
  // Mock the useTranslation hook with an empty function
  const mockT = jest.fn();
  useTranslation.mockReturnValue({ t: mockT });

  var schema;

  // Reset the mock before each test and get a new schema
  beforeEach(() => {
    schema = getYupSchema();
    useTranslation.mockClear();
  });

  it("should return a valid yup schema", () => {
    // Check that the schema is a yup.object
    expect(schema).toBeInstanceOf(yup.object);
    // Check that the schema has the correct fields
    expect(schema.fields).toHaveProperty("password");
    expect(schema.fields).toHaveProperty("confirmPassword");
    expect(schema.fields).toHaveProperty("email");
  });

  it("should validate the password field correctly", async () => {
    const validPassword = "Password1!";
    const invalidPassword = "password";

    // Check that the schema validates the password correctly
    await expect(
      schema.validateAt("password", { password: validPassword }),
    ).resolves.toBe(validPassword);

    // Check that the schema throws an error for an invalid password
    await expect(
      schema.validateAt("password", { password: invalidPassword }),
    ).rejects.toThrow();
  });

  it("should validate the confirmPassword field correctly", async () => {
    const password = "Password1!";
    const confirmPassword = "Password1!";
    const invalidConfirmPassword = "different";

    // Check that the schema validates the confirmPassword correctly
    await expect(
      schema.validateAt("confirmPassword", { password, confirmPassword }),
    ).resolves.toBe(confirmPassword);

    // Check that the schema throws an error for an invalid confirmPassword
    await expect(
      schema.validateAt("confirmPassword", {
        password,
        confirmPassword: invalidConfirmPassword,
      }),
    ).rejects.toThrow();
  });

  it("should validate the email field correctly", async () => {
    const validEmail = "johndoe@email.com";
    const invalidEmail = "john.doe.com";

    // Check that the schema validates the email correctly
    await expect(
      schema.validateAt("email", { email: validEmail }),
    ).resolves.toBe(validEmail);

    // Check that the schema throws an error for an invalid email
    await expect(
      schema.validateAt("email", { email: invalidEmail }),
    ).rejects.toThrow();
  });
});
