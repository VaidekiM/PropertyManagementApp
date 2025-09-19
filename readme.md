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
    ├── Controllers/             
    ├── Models/                  # Entity classes representing database tables
    ├── Data/                    # Database context
    ├── DTO/                     # Data Transfer Objects for front-end/back-end communication
    ├── Helper/                  # Seeding logic
    ├── Views/                   # Razor Views (UI pages)
    ├── wwwroot/                 # Static files (CSS)
    ├── Migrations/              # EF Core migrations
    ├── appsettings.json         # Default configuration
    ├── Program.cs               # Application entry point
    └── PropertyManagementApp.csproj # Project file

## Migrations
All migrations are included. To update the database, run:
    dotnet ef database update

## Seeding Data
A default admin user will be created automatically when the app runs for the first time.
    - **Email:** admin@test.com  
    - **Password:** Admin@123  

## Default start page is the login page (/Account/Login).