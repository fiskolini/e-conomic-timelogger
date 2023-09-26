<p align="center">
  <h1 align="center">Time Logger Web Application</h1>
</p>



This is a simple time logger web application developed as part of the recruitment process for **e-conomic**. The
application is designed to address the following user stories:

1. **As a freelancer, I want to be able to register how I spend time on my projects, so that I can provide my customers
   with an overview of my work.**

2. **As a freelancer, I want to be able to get an overview of my time registrations per project, so that I can create
   correct invoices for my customers.**

3. **As a freelancer, I want to be able to sort my projects by their deadline, so that I can prioritize my work.**

## Backend (BE)

### Technologies Used

- .NET Core v3.1
- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- OpenAPI (Swagger) - can be found in `localhost:3001/docs`

### Design Decisions

- Clean Architecture: The backend is structured using Clean Architecture principles, which promotes separation of
  concerns and maintainability.
- CQRS: Command Query Responsibility Segregation is implemented to segregate the read and write operations, improving
  scalability and performance.
- Unit and Integration Tests: While not every functionality is covered by tests, a few unit and integration tests have
  been included to showcase testing skills.
- Small Controller actions: Every Controller Action is really small (_four_ lines maximum).

### How to Run

1. Clone the repository.
2. Navigate to the `server` directory.
3. Run `dotnet restore` to restore dependencies.
4. Run `dotnet build` to build the project.
5. Run `dotnet run --project TimeLogger.WebApi` to start the backend server.

### Considerations

1. Every case from the acceptance criteria was implemented.
2. A few Integration tests from the Presentation Web App can be found in
   the [TimeLogger.WebApi.Tests](/server/TimeLogger.WebApi.Tests) project
3. A few Unit Tests from the Application can be found in
   the [TimeLogger.Application.Tests](/server/TimeLogger.Application.Tests) project

## Frontend (FE)

### Technologies Used

- React
- Next.js
- Tailwindcss
- TypeScript
- cypress (E2E)
- jest (unit test)

### Design Decisions

- React with Next.js: Next.js is chosen to leverage server-side rendering (SSR) and proper error handling, enhancing
  user experience.
- Testing: Although not every functionality is covered, a few end-to-end (E2E) and unit tests have been included in the
  source code.

### How to Run

1. Navigate to the `client` directory.
2. Create a `.env` file from the `.env.example` file
3. Run `npm install` to install dependencies.
4. Run `npm run dev` to start the development server.

### Considerations

1. Every case from the acceptance criteria was implemented.
2. A few E2E tests can be found in the [/client/cypress](/client/cypress) directory
3. A few Unit Tests can be found in the [/client/cypress](/client/tests) directory

---

## Additional Notes

- Authentication: Authentication has been omitted for simplicity, assuming the application is already authenticated.
- Database: Data is hardcoded in the appropriate places within the application as if it were coming from a database.

## Extra

All the following functionalities were added, even though not being required initially

#### 1. Pagination

While loading a _list of resources_, it is possible to get paginated results. `pageNumber` and `pageSize` properties can
be passed in the request to determine the page number to load and the number of items to get.
The default value for `pageSize` is 500, because... we have to limit it somehow to make it scalable.

```url
GET /api/customers?pageNumber=1
```

#### 2. Search

Search is also implemented while loading a _list or resources_ through `search` get param.

```url
/api/customers?searchfoo
```

```url
GET /api/customers?pageNumber=1
```

#### 3. Soft Delete

While being arguable if it fits well for any kind of project, _soft delete_ was included in this project.
It allow any delete to not delete the entity, but instead update it to consider a `timeDeleted` property.
While loading a resource list, it is possible to load also records that were _soft deleted_ before.

```url
/api/customers?considerDeleted=false
```

#### 4. Open API

Open API was included in the Back-end, so that users can see the API contracts and try a few requests through that.
It can be found on http://localhost:3001/docs after running Back-end.
