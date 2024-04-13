describe("Navbar Tests", () => {
  beforeEach(() => {
    cy.visit("/");
  });

  it("should navigate to home when logo is clicked", () => {
    // Assuming the logo is the first child of Navbar.Brand
    cy.get(".navbar-brand").click();

    // Add assertions as needed
    cy.url().should("include", "/");
  });

  it('should navigate to about page when "About Us" of "Over ons" link is clicked', () => {
    cy.get("#navAbout").click();

    cy.url().should("include", "/about");
  });

  it("should change language when language dropdown is used", () => {
    cy.get("#basic-nav-dropdown").click();
    cy.contains("English").click();
    cy.contains("Home");

    cy.get("#basic-nav-dropdown").click();
    cy.contains("Nederlands").click();
    cy.contains("Start");
  });

  it('should navigate to login page when "Login" or "Inloggen" link is clicked', () => {
    cy.get("#navLogin").click();

    cy.url().should("include", "/login");
  });

  it('should navigate to login page when "Register" or "Registreer" link is clicked', () => {
    cy.get("#navRegister").click();

    cy.url().should("include", "/register");
  });
});
