/* eslint-disable @typescript-eslint/no-namespace */

// Import commands.js using ES2015 syntax:
import './commands'

import { mount } from 'cypress/react18'

declare global {
    namespace Cypress {
        interface Chainable {
            mount: typeof mount
        }
    }
}

// Example use:
// cy.mount(<MyComponent />)
Cypress.Commands.add('mount', mount)

