# Clean Architecture ASP.NET Core API

Welcome to the **Clean Architecture ASP.NET Core API** repository! This project follows the principles of Clean Architecture and is built using ASP.NET Core. It is designed to be scalable, maintainable, and easy to understand. The API serves as a robust foundation for building complex applications with a clear separation of concerns.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
  - [API Layer](#api-layer)
  - [Application Layer](#application-layer)
  - [Infrastructure Layer](#infrastructure-layer)
  - [Domain Layer](#domain-layer)
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Running the Application](#running-the-application)
- [Authentication and Authorization](#authentication-and-authorization)


## Overview

This project is a Clean Architecture implementation of an ASP.NET Core API. The solution is structured into four distinct layers:

- **API Layer:** Responsible for handling HTTP requests and responses.
- **Application Layer:** Contains business logic, CQRS (Command Query Responsibility Segregation) with MediatR, services, middleware, behaviors, and external service integrations.
- **Infrastructure Layer:** Includes database context, repository patterns, migrations.
- **Domain Layer:** Contains core entities, exceptions, and helper classes.

## Architecture

### API Layer

The **API Layer** is responsible for handling HTTP requests and returning appropriate responses. It includes:

- **Controllers:** The entry point for handling incoming API requests.
- **Routing & Versioning:** Manages routing and API versioning.

### Application Layer

The **Application Layer** contains the core business logic and application flow, including:

- **CQRS with MediatR:** Implements the Command Query Responsibility Segregation pattern using MediatR for handling commands and queries.
- **Services:** Encapsulate business logic and operations.
- **Middleware:** Custom middleware, including error handling.
- **Filters & Middleware:** Custom error handling, authentication, and authorization are managed here.
- **Behaviors:** Built with FluentValidation for validation, logging, and other cross-cutting concerns.
- **External Services:** Integrations with external services (e.g., email, logging).
  
### Infrastructure Layer

The **Infrastructure Layer** handles data access and external service interactions, including:

- **Database Context:** Manages the database connection and entity configurations.
- **Repositories:** Implements the repository pattern with specific and generic repositories.
- **Migrations:** Manages database schema changes.


### Domain Layer

The **Domain Layer** is the core of the application and contains:

- **Entities:** Represent the tables in the database.
- **Exceptions:** Custom exceptions.
- **Helpers:** Utility classes and Confiuration class.

## Features

- **Clean Architecture:** Clear separation of concerns between layers.
- **CQRS with MediatR:** Decouples the execution of commands/queries from the infrastructure.
- **FluentValidation:** For powerful and flexible validation.
- **Custom Middleware:** For global error handling, authentication, and authorization.
- **JWT Authentication:** Secure authentication using JSON Web Tokens.
- **Repository Pattern:** For clean and maintainable data access.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any other supported database)
- [Postman](https://www.postman.com/downloads/) (for API testing)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/akotb14/clean-architecture-aspnet-core-api.git
   cd clean-architecture-aspnet-core-api

## Configuration

1. Update the `appsettings.json` file with your database connection string and JWT settings.

2. Apply migrations to the database:

   ```bash
   dotnet ef database update

## Running the Application

1. Run the application:

   ```bash
   dotnet run
## Authentication and Authorization

The API uses JWT (JSON Web Tokens) for authentication and authorization. This allows you to secure your endpoints and ensure that only authenticated users can access certain parts of the API.

### Generating a JWT Token

1. **Login to Obtain Token:**
   - Use the `/api/auth/login` endpoint to authenticate the user.
   - Provide the necessary credentials (e.g., username and password) in the request body.
   - Upon successful authentication, a JWT token will be returned in the response.

   Example using `curl`:

   ```bash
   curl -X POST https://localhost:5001/api/auth/login -H "Content-Type: application/json" -d '{"email": "your-email", "password": "your-password"}'
2. **Include Token in Requests**
    - To access secured endpoints, include the JWT token in the Authorization header. Format the header as follows:
    ```bash
      authorization: Bearer your-jwt-token
### Token Expiration and Refresh
  JWT tokens have an expiration time, after which they become invalid. To handle token expiration and allow users to obtain a new token without re-authenticating, you should implement a refresh token mechanism.

1. **Implement Token Refresh:**
    - Create an endpoint to handle token refresh requests.
    - Validate the refresh token and issue a new JWT token if the refresh token is valid.
2. **Configure Token Settings:**
    - Ensure that your appsettings.json file is configured with JWT settings such as the secret key, issuer, and audience.
