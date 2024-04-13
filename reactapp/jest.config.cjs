module.exports = {
  moduleNameMapper: {
    "\\.(css|less|scss|sss|styl)$": "<rootDir>/__mocks__/styleMock.js",
  },
  testEnvironment: "jest-environment-jsdom",
};
