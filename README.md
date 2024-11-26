# Fixsy Services Backend

## 📖 Overview

**Fixsy Services** is the backend part of the Fixsy platform, built with **.NET Core 8** and using **PostgreSQL 16** for data storage. This service provides API endpoints for managing users, job postings, and job assignments for various home services (e.g., plumbing, carpentry, remodeling).

---

## ✨ Features

- **User Management**: Handles user authentication, registration, and profile management.
- **Job Management**: Manages job postings, including job status updates, assignment to service providers, and notifications.
- **Admin Panel**: Allows administrators to manage service providers, jobs, and update job statuses.
- **Migrations**: Uses Entity Framework Core migrations to set up and update the database schema.

---

## 🚀 Prerequisites

Before running the backend service locally, ensure you have the following installed:

- **.NET Core 8** (for running the backend service)
- **PostgreSQL 16** (for database management)
- **EF Core Tools** (for running migrations)

---

## 📥 Installation Steps

### 1. Clone the Repository
Clone the repository to your local machine:
```bash
git clone https://github.com/dtercero89/fixsy-services.git
```

### 2. Navigate to the Project Directory
Change into the project directory:
```bash
cd fixsy-services
```

### 3. Install Dependencie
Run the following command to restore the necessary dependencies:
```bash
cd fixsy-services
```

### 4. Configure Connection String
In the root directory of the backend project, locate the `appSettings.json` file. Add the following `ConnectionStrings` section with the `FixsyDatabase` connection string:

```json
{
  "ConnectionStrings": {
    "FixsyDatabase": "Host=localhost;Database=fixsy;Username=your_username;Password=your_password"
  }
}
```

### 5. Run Migrations
Use Entity Framework Core to apply the database migrations:

```bash
dotnet ef database update
```

### 6. Start the Application

```bash
dotnet run
```

## ⚙️ Technologies Used
**.NET Core 8:** Backend framework for building the API services.
<br/>
**Entity Framework Core:** ORM for database access and migration management.
<br/>
**PostgreSQL 16:** Relational database used for storing data.

## 🌐 Deployment
The Fixsy backend services are deployed on **Azure App Service**. You can access the live API at the following URI:

**API URL:** [https://fixsy-services-bneqgbfbf5c4gpep.canadacentral-01.azurewebsites.net/](https://fixsy-services-bneqgbfbf5c4gpep.canadacentral-01.azurewebsites.net/)

Additionally, the PostgreSQL database used by the application is hosted on **Azure Database for PostgreSQL**, ensuring high availability and scalability for production workloads.

## 📧 Contact

**Denis Tercero**
<br/>*Software Developer*
<br/>**GitHub** [dtercero89](https://github.com/dtercero89) 
<br/>**Email:** dennistercero@hotmail.com

