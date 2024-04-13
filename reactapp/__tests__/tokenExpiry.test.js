import tokenExpiryInMinutes from "../src/utils/api/authentication/tokenExpiry";

describe("tokenExpiryInMinutes", () => {
  it("should return the correct token expiry time in minutes", () => {
    // get the current unix timestamp
    const currentUnixTimestamp = Date.now() / 1000;

    // Mock the token expiry date time
    const tokenExpiryDateTime = "2222-01-01T00:00:00Z";

    // Calculate the expected token expiry time in minutes
    const expectedTokenExpiryTimeInMinutes = Math.floor(
      (new Date(tokenExpiryDateTime).getTime() / 1000 - currentUnixTimestamp) /
        60,
    );

    // Call the tokenExpiryInMinutes function
    const actualTokenExpiryTimeInMinutes =
      tokenExpiryInMinutes(tokenExpiryDateTime);

    // Assert that the actual token expiry time matches the expected token
    // expiry time
    expect(actualTokenExpiryTimeInMinutes).toBe(
      expectedTokenExpiryTimeInMinutes,
    );
  });
});
