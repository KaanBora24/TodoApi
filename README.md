# TodoApi (ASP.NET Core Web API)

A RESTful Todo API built with ASP.NET Core and Entity Framework Core (SQLite).  
This project demonstrates layered architecture, dependency injection, and OpenAI-powered AI endpoints for task suggestion and motivational sentence generation.

## Features

- CRUD operations for Todo items
- AI-based task suggestion generation
- AI-based motivational sentence generation
- Layered architecture (Controller / Service / Data)
- Entity Framework Core (Code-First) with SQLite
- Swagger (OpenAPI) integration

## Technologies

- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- OpenAI Chat Client SDK
- Swagger / Swashbuckle

## API Endpoints

### AI Endpoints

#### POST /api/ai/suggest  
Generates structured task suggestions from free-text input.

Example request:

```json
{
  "text": "Today I will work on my backend project and go to the gym"
}
```

Example response:

```json
{
  "items": [
    "Work on backend project",
    "Go to the gym",
    "Review daily goals",
    "Check emails",
    "Plan tomorrow's tasks"
  ]
}
```
## Run Locally

Clone the repository:

```bash
git clone https://github.com/KaanBora24/TodoApi.git
cd TodoApi
```

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update
```

Run the project:

```bash
dotnet run
```

Swagger UI will be available at:

https://localhost:7145/swagger
