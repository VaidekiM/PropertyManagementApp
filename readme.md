# Property Management App
This is an ASP.NET Core MVC project for property management.

## Features
- User registration and login
- Propery management by admin user
- CRUD operations for properties
- Session management
- Logging

## Setup
1. Clone the repo.
2. Update `appsettings.json` with your database connection.
3. Run migrations:
    dotnet ef database update

## Folder Structure
    PropertyManagementApp/
    │
    ├── Controllers/             # This contains the project main controllers to handle login, registration, dashboard CRUD
    ├── Models/                  # Entity classes representing database tables
    ├── Data/                    # Database context
    ├── DTO/                     # Data Transfer Objects for front-end/back-end communication
    ├── Views/                   # Razor Views (UI pages)
    ├── wwwroot/                 # Static files (CSS)
    ├── Migrations/              # EF Core migrations
    ├── appsettings.json         # Default configuration
    ├── Program.cs               # Application entry point
    └── PropertyManagementApp.csproj # Project file

## Migrations
All migrations are included. To update the database, run:
    dotnet ef database update  

## Default start page is the login page (/Account/Login).

1. The application allows only one admin user by default.
2. During registration, users can select the Admin checkbox. If an admin already exists, the system temporarily prevents additional admin registrations. (Note: functionality to contact the admin for admin access can be implemented in the future)
3. The admin user has full access to the dashboard, including adding, editing, and deleting properties.
4. Regular users have view-only access to the properties on the dashboard.