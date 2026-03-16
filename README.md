<div align="center">

# Gym Management System

**A full-featured web application for managing gym operations —**
**from member registration to trainer scheduling and analytics.**

[![C#](https://img.shields.io/badge/C%23-1a3a6b?style=flat-square&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![ASP.NET](https://img.shields.io/badge/ASP.NET-1a3a6b?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![HTML](https://img.shields.io/badge/HTML-1a3a6b?style=flat-square&logo=html5&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/HTML)
[![CSS](https://img.shields.io/badge/CSS-1a3a6b?style=flat-square&logo=css3&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/CSS)
[![JavaScript](https://img.shields.io/badge/JavaScript-1a3a6b?style=flat-square&logo=javascript&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
[![SQL Server](https://img.shields.io/badge/SQL_Server-1a3a6b?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![SignalR](https://img.shields.io/badge/SignalR-1a3a6b?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr)

[**Visit the live demo →**](http://powerfit.runasp.net)

</div>

---

## About

The Gym Management System is a comprehensive solution designed to streamline the day-to-day operations of a gym or fitness center. It covers everything from member registration and subscription tracking to trainer management and real-time support chat — all within a clean, organized interface.

---

## Screenshots

<div align="center">
  <img src="screenshots/1.png" alt="Home" width="800"/>
  <br/><br/>
  <img src="screenshots/22.png" alt="Plans" width="800"/>
  <br/><br/>
  <img src="screenshots/3.png" alt="Memberships" width="800"/>
  <br/><br/>
  <img src="screenshots/4.png" alt="User Side Support Chat" width="800"/>
  <br/><br/>
  <img src="screenshots/5.png" alt="Admin Side Support Chat" width="800"/>
  <br/><br/>
  <img src="screenshots/6.png" alt="Session Enrollment" width="800"/>
</div>

---

## Features

| Feature | Description |
|---|---|
| Member management | Add, edit, and track gym members across the full lifecycle |
| Subscription plans | Define membership types, durations, and pricing tiers |
| Trainer management | Assign trainers, manage sessions, and track schedules |
| Payment tracking | Monitor invoices, payments, and membership renewals |
| Reports and analytics | Generate insights on gym activity and revenue trends |
| Authentication | Secure login flows for admins, staff, and members |
| Support chat | Real-time messaging between members and staff via SignalR |
| Session enrollment | Members can browse and enroll in scheduled group sessions |

---

## Architecture

This project follows a clean 3-layer architecture for separation of concerns:

```
Gym-Management-System/
│
├── GymSystem/        # Presentation Layer — UI, forms, user interaction
├── GymSystemBLL/     # Business Logic Layer — rules, validation, workflows
└── GymSystemDAL/     # Data Access Layer — database queries & operations
```

| Layer | Folder | Responsibility |
|---|---|---|
| Presentation | `GymSystem/` | All views, layouts, and client-facing interface components |
| Business Logic | `GymSystemBLL/` | Core application logic, business rules, and data orchestration |
| Data Access | `GymSystemDAL/` | All SQL Server interactions, stored procedures, and data mapping |

---

## Tech Stack

| Technology | Purpose |
|---|---|
| `C#` | Core application logic and server-side processing |
| `ASP.NET` | Web framework and routing |
| `HTML / CSS` | Frontend structure and styling |
| `JavaScript` | Client-side interactivity |
| `SQL Server` | Relational database and persistence |
| `SignalR` | Real-time WebSocket communication (support chat) |

---

## Getting Started

### Prerequisites

- [Visual Studio 2019+](https://visualstudio.microsoft.com/)
- [.NET Framework](https://dotnet.microsoft.com/en-us/download/dotnet-framework)
- SQL Server or SQL Server Express

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/EhabOkashahh/Gym-Management-System.git
```

**2. Open the solution**

```
Open GymSystem.sln in Visual Studio
```

**3. Configure the database**

Set up a SQL Server instance, then update the connection string in the DAL project.

**4. Build and run**

```
Press F5 or click Run in Visual Studio
```

### Demo Account

| Field | Value |
|---|---|
| Email | `Demo@gmail.com` |
| Password | `Tcecaf8a8!` |

---

## Contributing

Contributions are welcome. To get started:

1. Fork the project
2. Create a feature branch — `git checkout -b feature/your-feature`
3. Commit your changes — `git commit -m 'Add your feature'`
4. Push to the branch — `git push origin feature/your-feature`
5. Open a pull request

---

## Author

**Ehab Okasha**

[![GitHub](https://img.shields.io/badge/GitHub-EhabOkashahh-1a3a6b?style=flat-square&logo=github&logoColor=white)](https://github.com/EhabOkashahh)

---

<div align="center">
  <sub>Made with care by Ehab Okasha</sub>
</div>
