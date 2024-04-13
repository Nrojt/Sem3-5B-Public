describe("Contact Page Test", () => {
  beforeEach(() => {
    cy.visit("/contact");
  });

  it("should display the contact form", () => {
    cy.contains("h1", "Contact").should("exist");
    cy.get("form").should("exist");
  });

  it("should fill and submit the contact form", () => {
    cy.get("#name-1").type("Meneer Test");
    cy.get("#email-1").type("test@test.com");
    cy.get("#message-text-area").type("Dit is een test.");

    cy.get("form").submit();

    cy.contains("h1", "Contact").should("exist");
  });
});
