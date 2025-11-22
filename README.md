# Product Management API

## Overview
This project implements a **Product Management API** for a retail/e-commerce application (e.g., fashion shop).  
It provides endpoints to manage products with scalability, strong consistency, and easy extensibility for future features.

---

## Table of Contents
- [Overview](#overview)  
- [Requirements](#requirements)  
- [Technology Stack](#technology-stack)  
- [Setup Instructions](#setup-instructions)  
  - [Running Locally](#running-locally)  
  - [Running with Docker](#running-with-docker)  
- [Database Design](#database-design)  
- [API Endpoints](#api-endpoints)  
- [Performance Considerations](#performance-considerations)  
- [Sample API Requests](#sample-api-requests)  

---

## Requirements
- .NET 8 SDK or later  
- Docker & Docker Compose (optional for containerized setup)  
- PostgreSQL (or any SQL database)  

---

## Technology Stack
- **Backend Framework:** ASP.NET Core 8  
- **Database:** SQL (PostgreSQL recommended)  
- **ORM:** Entity Framework Core  
- **Validation:** Data Annotations  
- **API Documentation:** Swagger  

---

## Setup Instructions

### Running Locally
1. **Clone the repository**
```bash
git clone <repo-url>
cd product-management-api
2. **Restore dependencies**
```bash
dotnet restore
3. **Configure database connection**
Edit appsettings.json or set environment variables:
```json
{
  "ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=ProductDb;Username=youruser;Password=yourpassword"
}

4. **Run database migrations (You may run source instead run that)**
```bash
dotnet ef database update
5. **Run the application**
```bash
dotnet run
6. **Access the API**
Open your browser and navigate to `http://localhost:5000/swagger` to view the API
documentation.
### Running with Docker
1. **Build and run the Docker containers**
```bash
docker-compose up --build
2. **Access the API**
Open your browser and navigate to `http://localhost:8080/swagger` to view the API
documentation.

## Database Design

**Database Type:** SQL (PostgreSQL) for strong consistency  

**Tables Example:**
- `Products` — Stores product information (name, description, price, category, etc.)  
- `Attributes` — Stores additional product features for extensibility  

**Extensibility:** The schema allows easy addition of new product attributes without modifying core product tables.

---

## API Endpoints

| Method | Endpoint            | Description           |
|--------|-------------------|----------------------|
| GET    | /catalogproducts          | List all products    |
| POST   | /catalogproducts          | Create a new product |
| GET    | /catalogproducts/{id}     | Get product by ID    |
| PUT    | /catalogproducts/{id}     | Update product       |
| DELETE | /catalogproducts/{id}     | Delete product       |

**Validation & Error Handling:**  
- Inputs validated via Data Annotations  
- Errors return meaningful HTTP status codes and messages  

---

## Performance Considerations
- **Concurrency:** Handled via database transactions and optimistic locking  
- **Scalability:** SQL database design ensures consistency while allowing future features  

---