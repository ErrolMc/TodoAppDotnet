# TodoAppDotnet

A full-stack Todo application built with ASP.NET Web API and ASP.NET MVC, featuring user authentication and complete CRUD operations.

## Project Structure

```
TodoAppDotnet/
├── TodoAppBackend/      # ASP.NET Web API (REST API)
├── TodoAppFrontend/     # ASP.NET MVC (Web UI)
└── TodoAppShared/       # Shared DTOs and models
```

## Tech Stack

| Layer    | Technology                                          |
|----------|-----------------------------------------------------|
| Backend  | ASP.NET Web API 5.3, Entity Framework 6.5, Autofac |
| Frontend | ASP.NET MVC 5.2, Bootstrap 5, jQuery               |
| Database | SQL Server LocalDB                                  |
| Auth     | BCrypt.Net password hashing                         |
| Docs     | Swagger / Swashbuckle                               |

All projects target **.NET Framework 4.7.2**.

## Features

- **Authentication** &mdash; Register and login with BCrypt-hashed passwords
- **Todo CRUD** &mdash; Create, read, update, and delete todo items
- **Toggle completion** &mdash; Mark todos as complete/incomplete
- **Timestamps** &mdash; Tracks creation date, completion date, and last edited date
- **User-scoped data** &mdash; Each user sees only their own todos
- **Responsive UI** &mdash; Bootstrap layout with modal dialogs for add/edit

## API Endpoints

### Authentication

| Method | Route                | Description      |
|--------|----------------------|------------------|
| POST   | `/api/auth/login`    | Login            |
| POST   | `/api/auth/register` | Register         |

### Todo Items

| Method | Route                              | Description              |
|--------|------------------------------------|--------------------------|
| GET    | `/api/todoitems/{userId}`          | Get all todos for a user |
| GET    | `/api/todoitem/{itemId}`           | Get a single todo        |
| POST   | `/api/todoitem/create`             | Create a todo            |
| PUT    | `/api/todoitem/update`             | Update a todo            |
| DELETE | `/api/todoitem/delete/{itemId}`    | Delete a todo            |
| DELETE | `/api/todoitems/deleteall/{userId}`| Delete all user todos    |

## Prerequisites

- Visual Studio 2019+ (or any IDE with .NET Framework support)
- .NET Framework 4.7.2 Developer Pack
- SQL Server LocalDB (included with Visual Studio)

## Getting Started

1. **Clone the repo**
   ```bash
   git clone <repo-url>
   ```

2. **Open the solution** in Visual Studio (`TodoAppDotnet.sln`)

3. **Build the solution** &mdash; NuGet packages restore automatically

4. **Run both projects** &mdash; Right-click the solution > Properties > Multiple startup projects, then set both `TodoAppBackend` and `TodoAppFrontend` to **Start**

5. **Access the app**

   | Service  | URL                                              |
   |----------|--------------------------------------------------|
   | Frontend | `http://localhost:59409/`                         |
   | API      | `http://localhost:53667/api/`                     |
   | Swagger  | `http://localhost:53667/swagger/ui/index.html`    |

The database is created automatically on first run &mdash; no migrations needed.

## Database Schema

**Users** &mdash; `UserID`, `Username`, `PasswordHash`

**TodoItems** &mdash; `Id`, `UserID`, `Title`, `Description`, `IsCompleted`, `CreateDate`, `CompletedDate`, `LastEditedDate`
