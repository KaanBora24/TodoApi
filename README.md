# Todo API (ASP.NET Core Web API)

A RESTful Todo API built with ASP.NET Core.  
This project demonstrates layered architecture, Service Layer usage, Dependency Injection, and Entity Framework Core with SQLite.

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core (Code-First)
- SQLite
- Swagger / OpenAPI
- Service Layer Architecture
- Dependency Injection

## Features

- CRUD operations for Todo items
- EF Core Migrations
- SQLite database integration
- Layered architecture (Controller / Service / Data)
- AI suggestion endpoint (mock implementation)
- Swagger UI for endpoint testing

## API Endpoints

### Todo Endpoints

- GET /api/todos
- GET /api/todos/{id}
- POST /api/todos
- PUT /api/todos/{id}
- DELETE /api/todos/{id}

### AI Suggest Endpoint

POST /api/ai/suggest

Generates structured todo suggestions from free text input.

Example request:

{
  "text": "Today I will work on my backend project and go to the gym"
}

Example response:

{
  "items": [
    "Go to gym (45 min session)",
    "Work on backend project",
    "Start with the most difficult task",
    "Use Pomodoro technique"
  ]
}

## Architecture

The project follows a layered structure:

- Controllers: Handle HTTP requests
- Services: Business logic layer
- Data: Database context configuration
- Models: Entity definitions

## How to Run

1. Clone the repository
2. Open the solution in Visual Studio
3. Run the project
4. Swagger UI will open automatically
5. Test endpoints via Swagger

## Purpose

This project was developed to practice:

- Backend development with ASP.NET Core
- Clean architecture principles
- EF Core Code-First migrations
- REST API design
- Service Layer implementation
