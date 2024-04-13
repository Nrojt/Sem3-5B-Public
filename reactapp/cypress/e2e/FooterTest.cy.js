describe("FooterHTML Test", () => {
  beforeEach(() => {
    // generating a random uuid to append to the url, the 404 page should also
    // have a footer
    const uuid = () => Cypress._.random(0, 1e6);
    cy.visit("/" + uuid());
  });

  it("renders successfully", () => {
    cy.get("footer").should("exist");
  });

  it("has the /privacypolicy link", () => {
    cy.get("a[href='/privacypolicy']").should("exist");
  });

  it("has the /contact link", () => {
    cy.get("a[href='/contact']").should("exist");
  });

  it("navigates to /privacypolicy when clicked", () => {
    cy.get("a[href='/privacypolicy']").click();
    cy.url().should("include", "/privacypolicy");
  });

  it("navigates to /contact when clicked", () => {
    cy.get("a[href='/contact']").click();
    cy.url().should("include", "/contact");
  });

  it("sits at the bottom of the page after scrolling", () => {
    cy.visit("/privacypolicy");
    cy.get("footer").isInViewport;
  });
});
