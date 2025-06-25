# Airport Registration API

This is a .NET 8 Web API that allows users to register themselves to one of the major airports in Spain.  
The goal is to demonstrate clean design, separation of concerns, proper testing, and scalable architecture.

---

## Features

- Clean Architecture (Domain, Application, Infrastructure, API)
- Entity Framework Core with SQLite (file-based)
- FluentValidation for request validation
- AutoMapper for DTO mapping
- Global exception handling with structured error responses
- Structured logging using `ILogger`
- Swagger UI for testing and documentation
- Full CRUD for `Person` entity
- Optional filtering by airport code
- Airport list seeded at startup

---

## Technologies Used

- ASP.NET Core 8
- Entity Framework Core + SQLite
- FluentValidation
- AutoMapper
- xUnit + Moq + FluentAssertions
- WebApplicationFactory (for integration testing)

---

## How to Run

1. Clone the repository
2. Open the solution in Visual Studio 2022
3. Build and run the `AirportRegistration.API` project
4. Navigate to: `https://localhost:{port}/swagger`

The database will be created automatically at `Data/airport.db`  
Initial data includes a set of major Spanish airports.

---

## API Overview

### People Endpoints

- `GET /api/people`  
  Get all registered people

- `GET /api/people/{id}`  
  Get a specific person by ID

- `POST /api/people`  
  Register a new person

- `PUT /api/people/{id}`  
  Update an existing person

- `DELETE /api/people/{id}`  
  Delete a person

- `GET /api/people/airport/{code}`  
  Get all people registered at a specific airport  
  _(Added as an enhancement)_

### Airport Endpoints

- `GET /api/airports`  
  Get the full list of airports  
  _(Added for usability in Swagger/UI)_

---

## Testing Strategy

Unit and integration tests are included for the following:

- DTO validation using FluentValidation
- Application service logic with mock repositories
- Controller behavior with mocked services
- End-to-end HTTP testing using WebApplicationFactory and SQLite In-Memory

Test coverage includes valid and invalid scenarios.

---

## Notes on Design Decisions

The following improvements were made beyond what was explicitly required:

- **Global exception handling** with unified error output
- **Structured logging** for service and controller layers
- **AutoMapper** to simplify and decouple mapping logic
- **Airport filtering and listing APIs** to simulate real-world usage
- **Integration testing** for full request-response verification

These features were included to showcase attention to quality, maintainability, and scalability.

---

## Deliverables

- All code is modular, layered, and testable
- Git history reflects incremental, meaningful commits
- The solution is ready to run and validate via Swagger UI
