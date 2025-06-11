# 🛒 Shop Management Dashboard (Products ↔ Categories)

A modern, responsive **CRUD dashboard** built with ASP.NET Core 8 MVC and Razor Pages. The application manages products and categories with a many-to-many relationship using PostgreSQL and Dapper.

---

## 🧰 Technologies Used

| Layer | Tech Stack |
|-------|------------|
| **Backend** | ASP.NET Core 8 MVC, Dapper, EF Core (for migrations only) |
| **Frontend** | Razor Views, Bootstrap 5 (via LibMan), jQuery, AJAX, Bootstrap Icons |
| **Database** | PostgreSQL (locally hosted) |
---

## 🧱 Project Architecture

```
/Shop.Solution
│
├── Shop.Web            # ASP.NET Core MVC frontend (Views, Controllers)
├── Shop.Core           # Domain models, DTOs, interfaces, custom exceptions
├── Shop.Infrastructure # Dapper-based implementations of data access
├── Shop.Database       # EF Core migrations & database seeding
└── README.md           # This file
```

> ⚠️ EF Core is used **only for migrations and seeding**. All queries in runtime are executed with Dapper.

---

## 🗃️ Database Setup Instructions (PostgreSQL)

### ✅ Requirements

- PostgreSQL 14+
- .NET 8 SDK
- EF Core CLI (`dotnet ef`)

---

### 🔧 Step 1: Configure Connection String

In `Shop.Database/appsettings.json` or `Shop.Web/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=ShopDb;Username=postgres;Password=yourpassword"
  }
}
```

Make sure the PostgreSQL instance is running and accepts local connections.

---

### 🏗️ Step 2: Run Migrations

In terminal, navigate to the `.Database` project:

```bash
cd Shop.Database
dotnet ef database update
```

This will create the `ShopDb` database and apply schema (tables, relations).

---

### 🌱 Step 3: Seed Data (Optional but recommended)

You can seed initial data with the included EF logic.

The `DbSeeder.cs` class contains dummy products and categories. It is automatically run at startup of `.Database`.

If you want to re-seed:

```bash
dotnet run --project Shop.Database
```

This will:
- Create the database (if missing)
- Apply migrations
- Add sample products and categories

---

## ▶️ Running the Application

1. Set `Shop.Web` as **Startup Project** in Visual Studio
2. Press **F5** or click **Run**
3. App launches at `https://localhost:xxxx/`
4. Test:
   - Browse to `/Product`
   - Add/Delete products
   - Toast notifications appear
   - Modals used for forms

---

## 🛡️ Folder Overview

| Folder | Purpose |
|--------|---------|
| `Shop.Web/Views` | Razor pages and partials |
| `Shop.Web/wwwroot/js` | Modular JavaScript (AJAX, toast, etc.) |
| `Shop.Core` | Models, DTOs, Interfaces |
| `Shop.Infrastructure` | Dapper logic, SQL queries |
| `Shop.Database` | EF Core context + Migrations + Seed |

---
