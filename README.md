# Time Logger Web Application

This is a simple time logger web application developed as part of the recruitment process for **e-conomic**. The application is designed to address the following user stories:

1. **As a freelancer, I want to be able to register how I spend time on my projects, so that I can provide my customers with an overview of my work.**

2. **As a freelancer, I want to be able to get an overview of my time registrations per project, so that I can create correct invoices for my customers.**

3. **As a freelancer, I want to be able to sort my projects by their deadline, so that I can prioritize my work.**

## Backend (BE)

### Technologies Used
- .NET Core v3.1
- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- OpenAPI (Swagger) - can be found in `localhost:3001/docs` 

### Design Decisions
- Clean Architecture: The backend is structured using Clean Architecture principles, which promotes separation of concerns and maintainability.
- CQRS: Command Query Responsibility Segregation is implemented to segregate the read and write operations, improving scalability and performance.
- Unit and Integration Tests: While not every functionality is covered by tests, a few unit and integration tests have been included to showcase testing skills.

### How to Run
1. Clone the repository.
2. Navigate to the `server` directory.
3. Run `dotnet restore` to restore dependencies.
4. Run `dotnet build` to build the project.
5. Run `dotnet run` to start the backend server.

## Frontend (FE)

### Technologies Used
- React with Next.js
- TypeScript
- cypress (E2E)
- jest (unit test)

### Design Decisions
- React with Next.js: Next.js is chosen to leverage server-side rendering (SSR) and proper error handling, enhancing user experience.
- Testing: Although not every functionality is covered, a few end-to-end (E2E) and unit tests have been included in the source code.

### How to Run
1. Navigate to the `client` directory.
2. Run `npm install` to install dependencies.
3. Run `npm run dev` to start the development server.

## Additional Notes
- Authentication: Authentication has been omitted for simplicity, assuming the application is already authenticated.
- Database: Data is hardcoded in the appropriate places within the application as if it were coming from a database.

## Presentation
During the presentation, I will discuss the following:
- How the application solves the specified user stories.
- The use of Clean Architecture and CQRS in the backend, emphasizing separation of concerns and scalability.
- Testing strategies and the importance of unit and integration tests.
- The choice of React with Next.js in the frontend for SSR and improved error handling.
- Demonstration of key features and functionalities.
- Future scalability considerations and architectural decisions.

