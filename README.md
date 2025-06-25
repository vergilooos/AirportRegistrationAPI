
Airport Registration API
This is a .NET 8 Web API that allows users to register themselves to one of the major airports in Spain.
The goal is to demonstrate clean design, separation of concerns, proper testing, and scalable architecture.

🧩 Features
Clean Architecture (Domain, Application, Infrastructure, API)

Entity Framework Core with SQLite (file-based)

FluentValidation for request validation

AutoMapper for DTO mapping

Global exception handling with structured error responses

Structured logging using ILogger

Swagger UI for testing and documentation

Full CRUD for Person entity

Optional filtering by airport code

Airport list seeded at startup

📋 Technologies Used
ASP.NET Core 8

Entity Framework Core with SQLite

FluentValidation

AutoMapper

xUnit, Moq, FluentAssertions

WebApplicationFactory (for integration testing)

🧪 Testing
All layers are fully tested:

Validation Tests for PersonCreateDto

Unit Tests for PersonService

Controller Tests using mock services

Integration Tests using in-memory SQLite and WebApplicationFactory

Tests cover both expected flows and edge cases, and ensure that all new functionality is verified.

📁 How to Run
Clone the repository

Open the solution in Visual Studio 2022

Build and run the API project

Swagger UI will open at:
https://localhost:{port}/swagger

The database file will be created automatically in Data/airport.db, and seeded with a list of Spanish airports.

🔧 API Overview
POST /api/people
Register a new person
→ Validates names, passport format, and airport code

GET /api/people
Get all registered people

GET /api/people/{id}
Get a specific person by ID

PUT /api/people/{id}
Update an existing person

DELETE /api/people/{id}
Delete a person by ID

GET /api/people/airport/{code}
Get all people registered at a specific airport
(Added for real-world usability, not required by original task)

GET /api/airports
Get full list of available airports
(Added for UI or Swagger usability)

✅ Notes Beyond the Original Task
The original task did not require some features that were included for better design and real-world completeness:

Global exception handling with consistent error output

Structured logging for all operations

AutoMapper to avoid manual entity-DTO conversions

Airport filtering API (GET /api/people/airport/{code})

Airport list API (GET /api/airports)

Integration Testing with full HTTP request/response flow

These were included intentionally to demonstrate a more scalable, production-ready approach.

📦 Deliverables
All code is committed step by step

Each feature is broken down logically into Git commits

Testing is comprehensive and reflects real scenarios
