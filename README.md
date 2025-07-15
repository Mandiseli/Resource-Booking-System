# Internal Resource Booking System

A web-based application built with **ASP.NET Core MVC** and **MySQL** for managing shared company resources such as meeting rooms, vehicles, and equipment. This system enables employees to view available resources, make bookings, and avoid conflicts caused by double-booking.

---

## Tech Stack

- **Backend**: ASP.NET Core MVC (.NET 8)
- **Frontend**: Razor Views, Bootstrap 5
- **Database**: MySQL with Entity Framework Core (Pomelo provider)
- **ORM**: Entity Framework Core 8.0.13
- **IDE**: Visual Studio 2022 / Visual Studio Code

---

## Features

### Core Functionalities
- Add, view, update, and delete resources
- Add, view, update, and delete bookings
- Booking validation with conflict checking
- Server-side and client-side form validation
- Simple user interface using Bootstrap

### Booking Logic
- Prevents overlapping bookings for the same resource
- Requires end time to be after start time
- Displays all upcoming bookings per resource

---

## Seed Data

On first run, the database is seeded with sample data:

### Resources
- Meeting Room A (10 capacity)
- Company Car 1 (5 seats)

### Bookings
- Alice books Meeting Room A on 15 July 2025
- Bob books Company Car 1 on 16 July 2025

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- Optional: Visual Studio or VS Code

### ⚙️ Configuration

1. Update your MySQL connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ResourceBookingDb;User=root;Password=your_password;"
}

### Apply migrations and update the database

- dotnet ef database update
- dotnet run

### Folder Structure

ResourceBookingSystem/
│
├── Controllers/        → MVC Controllers for Resources and Bookings
├── Models/             → C# Entity classes (Resource, Booking, DbContext)
├── Views/
│   ├── Bookings/       → Razor views for Booking CRUD
│   ├── Resources/      → Razor views for Resource CRUD
│   └── Shared/         → _Layout and validation partials
├── Migrations/         → EF Core migration snapshots
├── wwwroot/            → Static files (CSS, JS)
├── appsettings.json    → DB connection and config
└── Program.cs          → App configuration and startup

### Author

Mandiseli Mfeya
Email: mandiseli@outlook.com
GitHub: github.com/Mandiseli
