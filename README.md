# RaceDay System - Portfolio of Evidence

**Student Name:** Kabuya Tambwe Josue  
**Student Number:** ST10468057  
**Module:** PROG6212 (Programming 2B) - IIE Rosebank College  

---

##  Project Overview
**RaceDay** is an end-to-end event management platform designed specifically for the South African road running, walking, and cycling community. 

Part 2 introduces a fully implemented **RESTful API** built with **C# ASP.NET Core 8 Web API** and **Entity Framework Core**, providing complete backend services, secure authentication, role-based access control, and full data management.

---

##  Supported Roles & Features

###  Organiser
* Create, edit, and manage running/cycling events and distance categories.
* Capture and manage participant finish results and race positions.
* Access complete event enrolment registries.

###  Participant
* Create an account and authenticate securely.
* Browse upcoming race events and distance categories.
* Enrol in events and view personal enrolment history.
* Track individual race results and performance history.

---

## 🛠️ Tech Stack & Backend Architecture
* **Framework:** ASP.NET Core 8 Web API
* **ORM & Database:** Entity Framework Core (In-Memory Database)
* **Security & Authentication:** Custom `SessionAuthMiddleware` with PBKDF2 HMACSHA256 salted password hashing (`PasswordHasher`)
* **Documentation & Testing:** Interactive Swagger / OpenAPI documentation, xUnit Integration Testing
* **CI/CD:** GitHub Actions Automated Build and Test Workflow

---

##  Documentation Included (`/docs`)
* **Entity Relationship Diagram (`erd.png`):** Complete database architecture covering core entities, primary/foreign keys, and cardinalities.
* **API Endpoint Plan (`api-endpoints.md`):** Planning document detailing routes, HTTP methods, role access control, request payloads, and status codes.
* **SQL Database Script (`raceday_schema.sql`):** Fully executable database schema and seed data script written for SQL Server Management Studio (SSMS).

---

##  Running the API Locally

1. Clone the repository:
   ```bash
   git clone [https://github.com/josuekb09/RaceDay.git](https://github.com/josuekb09/RaceDay.git)
