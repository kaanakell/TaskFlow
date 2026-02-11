TaskFlow API

    TaskFlow is a RESTful backend service built with ASP.NET Core (.NET 10) following Clean Architecture principles. It allows users to create, retrieve, and filter tasks while demonstrating best practices in backend development, testing, and containerization.

Tech Stack

    *.NET 10

    *ASP.NET Core Web API

    *EF Core (SQLite)

    *Clean Architecture

    *Repository + Service pattern

    *FluentValidation

    *xUnit Integration Tests

    *Docker (multi-stage build)

Architecture

    *The solution follows Clean Architecture separation:

    *TaskFlow.Domain — Core entities and business rules

    *TaskFlow.Application — DTOs, services, interfaces

    *TaskFlow.Infrastructure — EF Core, repository implementation

    *TaskFlow.Api — Controllers and API configuration

    *TaskFlow.Tests — Integration tests using WebApplicationFactory

Running Locally:

    "dotnet restore"
    "dotnet run --project TaskFlow.Api"

    Swagger avaliable at:

        "http://localhost:5033/swagger"


Running with Docker

    Build:

        "docker build -t taskflow-api ."


    Run:

        "docker run -p 8080:8080 taskflow-api"


    Swagger available at:

        "http://localhost:8080/swagger"

Running Tests
    "dotnet test"

    Integration tests use in-memory SQLite with a shared connection.


Features

    *Create tasks

    *Retrieve all tasks

    *Retrieve by ID

    *Filter by status and due date

    *Input validation

    *Health endpoint

    *Integration-tested API

    *Docker-ready deployment