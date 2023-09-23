/* eslint-disable */
// Disable ESLint to prevent failing linting inside the Next.js repo.

// Cypress E2E Test
describe('Navigation', () => {
    it('should navigate to the about page', () => {
        // Start from the index page
        cy.visit('http://localhost:3000/')

        // Find a link with an href attribute containing "about" and click it
        cy.get('a[href*="/customers/2"]').click()

        // The new url should include "/about"
        cy.url().should('include', '/customers/2')
    })
})

// Prevent TypeScript from reading file as legacy script
export {}