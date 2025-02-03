# Base runtime image for running the app in production
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage - compiles the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file and restore dependencies
COPY ["Dashboard_Management.csproj", "."]
RUN dotnet restore "./Dashboard_Management.csproj"

# Copy all files and build the project
COPY . .
RUN dotnet build "./Dashboard_Management.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage - prepares the app for deployment
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Dashboard_Management.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage - runs the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Ensure the correct user permissions (especially for Railway)
USER root

ENTRYPOINT ["dotnet", "Dashboard_Management.dll"]
