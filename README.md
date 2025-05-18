
# Crudify Solution Structure

This document outlines the folder structure of the **Crudify** solution, organized by layer and responsibility.

---

## 📁 Solution Items (.NET Core Backend)

### 📁 src
Contains the main application source code.

#### 📁 Crudify.Api
The entry point of the application (Web API).

- 📁 ActionFilters
- 📁 Controllers
- 📁 Extensions
- 📁 Middlewares

#### 📁 Crudify.Application
Application-level logic such as DTOs, interfaces, and services.

- 📁 Dependencies
- 📁 Dtos
- 📁 Interfaces
- 📁 Services

#### 📁 Crudify.Domain
Contains domain entities and core business models.

- 📁 Entities

#### 📁 Crudify.Infrastructure
Manages persistence, logging, and other infrastructure-level concerns.

- 📁 Database
- 📁 Extensions
- 📁 FluentConfigurations
- 📁 Interfaces
- 📁 Migrations

---

## 🧪 Tests

### 📁 unit
Contains unit test projects.

- 📁 Crudify.Api.Tests
- 📁 Crudify.Application.Tests

---

## 📝 Notes

- The solution follows a Clean Architecture pattern.
- Each project is structured with a clear separation of concerns.
- All configurations are organized logically for maintainability and scalability.

---

## 📁 Solution Items (Next.js with TypeScript)

### 📁 src

#### 📁 app

- 📁 auth
  - 📁 jwt
- 📁 students
  - 📁 add
  - 📁 edit

---

## ⚙️ Environment Setup

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- Optional: Postman or any HTTP client for testing endpoints

---

## ▶️ Running the Project

### .NET Core Project

#### 1. Configure the Database Connection

- Update the `appsettings.json` file with the appropriate database connection string.

#### 2. Run the Application in HTTPS Mode

- Ensure the project runs over HTTPS to maintain secure communication.

#### 3. Database Initialization

On first run, the application will:
- Create a new database named **Crudify**.
- Automatically apply all Entity Framework migrations.
- Execute configured SQL scripts for seeding and initialization.

#### 4. API Testing via Swagger

Swagger UI is integrated for API testing:
- First, authorize Swagger by making a `POST` request to `/auth/login` using one of the credentials listed below.
- Copy the JWT token from the response.
- Click Swagger's **Authorize** button and paste the token.
  - Swagger is pre-configured to use the Bearer scheme.
  - Example usage is shown in the Swagger authorization modal.

#### 5. SQL Script Seeding (For any new script)

To enable SQL script seeding:
- Navigate to the `seedings/sqlscripts` folder.
- Set each SQL script file’s **Build Action** to **Embedded Resource** in the file properties.
- These scripts will be executed automatically on application startup.
- The application maintains script execution history in the database to prevent duplication.
Note: You don't need to change anything in the existing sql files in the mentioned path.

#### 6. Login Credentials

- **Admin**
  - Email: `sjafri@calrom.com`
  - Password: `@Password123`
  - Role: `Admin`

- **User**
  - Email: `sdjafri@calrom.com`
  - Password: `@Password123`
  - Role: `User`

#### 7. Activity Monitor Middleware

- Logs all user activities with relevant details for audit and tracking.

#### 8. Exception Monitor Middleware

- Captures and logs all unhandled exceptions including stack traces and context information for easier debugging.

---

### Next.js Project

#### 1. Install Dependencies

Run the following command to install all required packages:

```bash
npm install
```

#### 2. Start the Development Server

Use the following command to start the application in development mode:

```bash
npm run dev
```

#### 3. Sign In

- Log in using one of the credentials provided in the **.NET Core Project Setup** section.
- The JWT token is securely stored in the Next.js session after successful authentication.

#### 4. Student Management

After logging in, you will be redirected to the **Student List** page. You can:

- Edit student details.
- Delete a student entry.

These actions are available via buttons located in each row of the grid.

---

### Challenges Faced
- Took the understanding of Next.js with typescript. It's more like a extended version of react with SSR.
- Splitting the components while keeping the essence of when to use 'use client' and when not to.
- Adding Protected Route was tricky bit.
- Adding AuthContext and privder was tricky in here.
- Tried to make the application scalable for future use.
- In short, I faced challenges in front end side.