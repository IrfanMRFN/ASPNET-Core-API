# ASP.NET Core 10 Minimal API - Bank Backend 🏦

This repository contains Phase 3 of my comprehensive C# and .NET 10 learning journey. It is a fully functional RESTful Web API built using modern Minimal API architecture, designed to securely bridge a frontend client with a SQL Server database.

## 🚀 Features
* **Full CRUD Operations:** Complete endpoint lifecycle (GET, POST, PUT, DELETE) for managing bank accounts and transaction histories.
* **Data Transfer Objects (DTOs):** Implemented strict input and output DTOs to secure database entities, prevent over-posting vulnerabilities, and permanently resolve JSON Object Cycle exceptions.
* **Dependency Injection (DI):** Utilizes the ASP.NET Core DI container to manage the Entity Framework Core `DbContext` lifecycle safely and efficiently.
* **Asynchronous Architecture:** All database queries and commands utilize `async/await` and non-blocking threads to ensure high performance under load.
* **Cascade Deletion:** Properly configured relational database deletion to ensure orphaned transaction records are safely removed when an account is closed.

## 🛠️ Tech Stack
* **Language:** C# 14
* **Framework:** ASP.NET Core 10 (Minimal APIs)
* **ORM:** Entity Framework Core 10
* **Database:** Microsoft SQL Server 2025 Developer Edition
* **Testing:** `.http` REST Client (VS Code)

## 📡 API Endpoints
| Method | Endpoint | Description | Payload |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/accounts` | Retrieve all accounts | None |
| `GET` | `/api/accounts/{id}` | Retrieve a specific account & transaction history | None |
| `POST` | `/api/accounts` | Open a new account | `CreateAccountDto` |
| `PUT` | `/api/accounts/{id}/deposit`| Deposit funds into an account | `DepositDto` |
| `DELETE` | `/api/accounts/{id}` | Close an account | None |

## ⚙️ How to Run Locally

1. Ensure you have the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) installed.
2. Clone this repository:
   ```bash
   git clone [https://github.com/IrfanMRFN/ASPNET-Core-API.git](https://github.com/IrfanMRFN/ASPNET-Core-API.git)