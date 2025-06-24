# Freelancer Directory API (.NET Core Web API)

A backend assessment project for managing a list of freelancers, built using **ASP.NET Core Web API**, **Entity Framework Core**, and **SQL Server**.

This project was developed as part of a job application for a backend developer role. It includes CRUD, search, and archive functionality.

---

## ✅ Features Implemented

- Setup of ASP.NET Core Web API project
- Data model design for:
  - Freelancers
  - SkillSets (one-to-many)
  - Hobbies (one-to-many)
- SQL Server integration using Entity Framework Core
- Swagger UI documentation enabled

---

## ⚠️ Known Issues

Several API endpoints are **not yet functioning fully**, due to model binding or entity relationship errors:

### ❌ Not Working:

- `POST /api/Freelancers` — model validation fails when inserting nested child collections (`SkillSets`, `Hobbies`)
- `GET /api/Freelancers` — fails when trying to include related entities
- `GET /api/Freelancers/search?term=rubini` — similar issue with nested relationships
- `PUT /api/Freelancers/{id}/archive` — archive logic not completing due to model state
- `GET` after archive — doesn't reflect changes due to above error

I attempted multiple fixes:
- Using `[JsonIgnore]` on navigation properties
- Clearing references in controller
- Setting `FreelancerId` manually
- Using `.Include()` for loading related entities

Despite this, validation errors persist when the API tries to parse and save the nested data structures.

---

## 💡 Learning Reflection

Although not all endpoints are working, this project helped me learn:
- How to structure a Web API using .NET Core
- How EF Core handles relationships
- How to troubleshoot controller actions and validation errors
- Swagger testing for RESTful APIs

I now have clearer knowledge of what I need to learn next, and I’m actively working to close those gaps.

---

## 🛠 Tech Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server Express
- Swagger

---

## 🧪 How to Run This Project

1. Clone the repo
2. Update your `appsettings.json` with a valid SQL Server connection string
3. Run Entity Framework migration:

```bash
dotnet ef database update
