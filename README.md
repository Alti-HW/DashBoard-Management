# Consolidated README - DashBoard Management

## Overview
DashBoard Management is a .NET Core-based web API designed to handle building, energy, and occupancy data. It integrates with **PostgreSQL** for database storage and **Keycloak** for authentication.

## Pre-requisites
Ensure you have the following installed before proceeding:

- **.NET SDK 6 or later** - Required to run the .NET services.
- **PostgreSQL Database** - Used for data storage.
- **Keycloak Authentication Server** - Used for securing API endpoints.
- **Docker** (Optional) - For running Keycloak or PostgreSQL containers.

## Database Configuration
The application uses **Aiven Cloud PostgreSQL** as the database. The connection details are stored in `appsettings.json`.

### Connection Strings:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=pg-1c707a6d-basamdileepkumar-9fe8.d.aivencloud.com;Port=26900;Username=avnadmin;Password=AVNS__6UJ9rVOAAkzZi6LUJ6;Database=EMS_HoneyWell;SSL Mode=Require"
}
```

### Connecting to PostgreSQL
You can connect to this database using **pgAdmin** or any SQL client with the credentials provided in `appsettings.json`.

## Running the Application
1. **Set up PostgreSQL** with the given connection string.
2. **Restore dependencies**:
   ```sh
   dotnet restore
   ```
3. **Run the application**:
   ```sh
   dotnet run
   ```
   The API will be available at:
   ```
   http://localhost:5050
   ```

## Keycloak Authentication Setup
The application uses **Keycloak** for authentication. The configuration is stored in `appsettings.json`.

### Keycloak Configuration:
```json
"KeycloakService": {
    "Realm": "Alti-EMS",
    "ClientId": "EMS",
    "ClientSecret": "q66B31Pde4NfC9LNM6EULSGOzfRJj0Re",
    "TokenUrl": "http://localhost:8080/realms/Alti-EMS/protocol/openid-connect/token",
    "ServerUrl": "http://localhost:8080",
    "Username": "mvgokul@altimetrik.com",
    "Password": "123"
}
```

### Running Keycloak:
If running Keycloak locally, start the server with Docker:
```sh
docker run -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:latest start-dev
```
Access Keycloak Admin Console at:
```
http://localhost:8080/admin
```

## Summary
1. Set up **PostgreSQL** with the provided credentials.
2. Ensure **Keycloak** is running and properly configured.
3. Start the **.NET API** on port **5050**.
4. The application will be ready for authentication and data management.

This setup ensures seamless integration between database storage, authentication, and API functionalities.

