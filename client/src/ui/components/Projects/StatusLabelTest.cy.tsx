import StatusLabel from './StatusLabel';
import {Project} from "@/app/types/entities/Project";
/* tslint-disable */


describe('<StatusLabel />', () => {
    it('should render as Delayed task when having completedAt as null and past deadline', () => {
        let delayedProjectOne: Project = {
            id: 0, name: "", timeAllocated: 0, dateCreated: "", customerId: 0,
            completedAt: null,
            deadline: new Date(Date.now() - 86400000).toISOString().slice(0, 10)
        };

        // Mount the React component for the About page
        cy.mount(<StatusLabel project={delayedProjectOne}/>)

        // The new page should contain an h1 with "About page"
        cy.get('span').contains('Delayed').should('have.class', 'text-red-800');
    })

    it('should render as Completed when having completedAt set and deadline null', () => {
        let delayedProjectOne: Project = {
            id: 0, name: "", timeAllocated: 0, dateCreated: "", customerId: 0,
            completedAt: new Date(Date.now() - 86400000).toISOString().slice(0, 10),
            deadline: null
        };

        // Mount the React component for the About page
        cy.mount(<StatusLabel project={delayedProjectOne}/>)

        // The new page should contain an h1 with "About page"
        cy.get('span').contains('Completed').should('have.class', 'text-green-800');
    })

    it('should render as Ongoing when having completedAt null and future deadline', () => {
        let delayedProjectOne: Project = {
            id: 0, name: "", timeAllocated: 0, dateCreated: "", customerId: 0,
            completedAt: null,
            deadline: new Date(Date.now() + 86400000).toISOString().slice(0, 10),
        };

        // Mount the React component for the About page
        cy.mount(<StatusLabel project={delayedProjectOne}/>)

        // The new page should contain an h1 with "About page"
        cy.get('span').contains('Ongoing').should('have.class', 'text-gray-800');
    })

    it('should render and display expected content for a complete task', () => {
        let delayedProjectOne: Project = {
            id: 0, name: "", timeAllocated: 0, dateCreated: "", customerId: 0,
            completedAt: "2023-09-13",
            deadline: "2023-11-07"
        };

        // Mount the React component for the About page
        cy.mount(<StatusLabel project={delayedProjectOne}/>)

        // The new page should contain an h1 with "About page"
        cy.get('span').contains('Complete').should('have.class', 'text-green-800');
    })
})

export {}