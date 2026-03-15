<div align="center">

# Gym Management System

**A full-featured desktop application for managing gym operations efficiently.**  
Built with **C#** · **ASP.NET** · **3-Layer Architecture**

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![HTML](https://img.shields.io/badge/HTML-E34F26?style=for-the-badge&logo=html5&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/HTML)
[![CSS](https://img.shields.io/badge/CSS-1572B6?style=for-the-badge&logo=css3&logoColor=white)](https://developer.mozilla.org/en-US/docs/Web/CSS)
[![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)

</div>

---

## Screenshots


## Some System Screenshots

<div align="center">
  <img src="screenshots/1.png" alt="Home" width="800"/>
  <br/><br/>
  <img src="screenshots/22.png" alt="Plans" width="800"/>
  <br/><br/>
  <img src="screenshots/3.png" alt="MemberShips" width="800"/>
  <br/><br/>
  <img src="screenshots/4.png" alt="User Side Suppot chat" width="800"/>
  <br/><br/>
  <img src="screenshots/5.png" alt="Admin side suuport chat" width="800"/>
  <br/><br/>
  <img src="screenshots/6.png" alt="Session Enrollment" width="800"/>
</div>

---

## 📖 About

The **Gym Management System** is a comprehensive solution designed to streamline the day-to-day operations of a gym or fitness center. It covers everything from member registration and subscription tracking to trainer management and reporting — all within a clean, organized interface.

---

## Features

- **Member Management** — Add, edit, and track gym members
- **Subscription Plans** — Manage membership types and durations
- **Trainer Management** — Assign trainers and manage schedules
- **Payment Tracking** — Monitor payments and renewals
- **Reports & Analytics** — Generate insightful reports on gym activity
- **User Authentication** — Secure login for admins and staff

---

## Architecture

This project follows a clean **3-Layer Architecture** for separation of concerns:

```
Gym-Management-System/
│
├── GymSystem/          #   Presentation Layer (UI)
├── GymSystemBLL/       #   Business Logic Layer
└── GymSystemDAL/       #   Data Access Layer
```

| Layer | Folder | Responsibility |
|-------|--------|----------------|
| Presentation | `GymSystem` | UI, Forms, User Interaction |
| Business Logic | `GymSystemBLL` | Rules, Validation, Workflows |
| Data Access | `GymSystemDAL` | Database Queries & Operations |

---

## Getting Started

### Prerequisites

- [Visual Studio 2019+](https://visualstudio.microsoft.com/)
- [.NET Framework](https://dotnet.microsoft.com/en-us/download/dotnet-framework)
- SQL Server (or SQL Server Express)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/EhabOkashahh/Gym-Management-System.git
   ```

2. **Open the solution**
   ```
   Open GymSystem.sln in Visual Studio
   ```

3. **Configure the database**
   - Set up your SQL Server instance
   - Update the connection string in the DAL project

4. **Build & Run**
   ```
   Press F5 or click ▶ Run in Visual Studio
   ```

---

## Tech Stack

| Technology | Purpose |
|-----------|---------|
| C# | Core application logic |
| ASP.NET | Web framework |
| HTML / CSS | Frontend structure & styling |
| JavaScript | Client-side interactivity |
| SQL Server | Database |

---

## Contributing

Contributions are welcome! Feel free to:

1. Fork the project
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add your feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

---

## Author

**Ehab Okasha**  
[![GitHub](https://img.shields.io/badge/GitHub-EhabOkashahh-181717?style=flat&logo=github)](https://github.com/EhabOkashahh)

---

<div align="center">
  <sub>Made with ❤️ by Ehab Okasha</sub>
</div>
