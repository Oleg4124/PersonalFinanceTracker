# PersonalFinanceTracker

A personal finance tracking application built with ASP.NET Core following Clean Architecture principles.

## Architecture

The solution is organized into the following projects:

| Project | Description |
|---------|-------------|
| `PersonalFinanceTracker.Api` | ASP.NET Core Web API — controllers and HTTP pipeline |
| `PersonalFinanceTracker.Application` | Use cases, application services and interfaces |
| `PersonalFinanceTracker.Domain` | Core business entities, value objects and domain logic |
| `PersonalFinanceTracker.Infrastructure` | EF Core DbContext, repository implementations and external services |
| `PersonalFinanceTracker.UnitTests` | xUnit unit tests for domain and application logic |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL](https://www.postgresql.org/) (or Docker)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/Oleg4124/PersonalFinanceTracker.git
cd PersonalFinanceTracker
```

### 2. Configure the database connection

Edit `PersonalFinanceTracker.Api/appsettings.json` and set your PostgreSQL connection string, or use [.NET User Secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) to avoid storing credentials in source code:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=PersonalFinanceTracker;Username=postgres;Password=yourpassword" --project PersonalFinanceTracker.Api
```

### 3. Restore dependencies and build

```bash
dotnet restore
dotnet build
```

### 4. Run the application

```bash
dotnet run --project PersonalFinanceTracker.Api
```

The API will be available at `https://localhost:7xxx` (see console output for the exact port).

### 5. Run the tests

```bash
dotnet test
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/account` | Get all accounts |
| `GET` | `/api/account/{id}` | Get account by ID |
| `POST` | `/api/account` | Create a new account |
| `DELETE` | `/api/account/{id}` | Delete an account |

## Technologies

- **ASP.NET Core 10** — Web framework
- **Entity Framework Core** + **Npgsql** — ORM with PostgreSQL
- **xUnit** + **FluentAssertions** — Unit testing
