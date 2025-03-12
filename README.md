# DashBoard-Management

## Overview
DashBoard Management is a .NET Core-based web API designed to manage building, energy, and occupancy data. It integrates with Keycloak for authentication and uses PostgreSQL as the database.

## Pre-requisites
- **.NET SDK 6 or later**  
- **Visual Studio or Visual Studio Code** (for development and debugging)  
- **PostgreSQL database**  
- **Keycloak authentication server**  
- **Aiven Cloud PostgreSQL** (for cloud database hosting)  

## Setting Up the Application

### Install Dependencies
1. Install .NET SDK from [Microsoft .NET](https://dotnet.microsoft.com/download)
2. Install PostgreSQL  
3. Install **Visual Studio** or **Visual Studio Code**:
   - **Visual Studio**: Download from [Visual Studio](https://visualstudio.microsoft.com/)
   - **Visual Studio Code**: Download from [VS Code](https://code.visualstudio.com/)
4. Open the project in Visual Studio and restore dependencies:
   ```sh
   dotnet restore
## Configure the Database

The application uses **Aiven Cloud PostgreSQL** for database hosting. The connection string for Aiven is stored in the `appsettings.json` file.

### Update Connection String
Open `appsettings.json` and update the **PostgreSQL connection string** with the credentials from your **Aiven Cloud Console**:
    
## Connecting to Aiven Cloud PostgreSQL

You can connect to **Aiven's PostgreSQL database** using **pgAdmin** or any SQL client with the connection details provided in `appsettings.json`.

## Viewing Databases and Tables in Aiven Cloud Console

1. Log in to your **Aiven** account.  
2. Select your **PostgreSQL service**.  
3. Navigate to the **Database** section to explore the tables. 


## References
[Technical Documentation](https://o365altimetrik-my.sharepoint.com/:w:/g/personal/bdileep_altimetrik_com/EWaLz3ceQPhDr7sOnucyyhUBnXI6el3--F4PjTQhm6Pp0Q?e=h3mdet)




 
