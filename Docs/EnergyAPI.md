# Energy Controller API Documentation

## Overview
The Energy Controller API provides endpoints to retrieve energy consumption data and metrics for buildings.

## Base URL
```
http://localhost:5050/api/energy
```

## Endpoints

### 1. Get Energy Consumption

#### Description
Fetches energy consumption data based on user-defined parameters.

#### Endpoint:
```
POST /api/energy/energy-consumption
```

#### Request:
```sh
curl -X POST "http://localhost:5050/api/energy/energy-consumption" \
-H "Authorization: Bearer <your_token>" \
-H "Content-Type: application/json" \
-d '{
    "startDate": "2025-01-22",
    "endDate": "2025-02-01",
    "buildingId": 1,
    "floorId": 10
}'
```

#### Request Body (JSON):
```json
{
    "startDate": "2025-01-22",
    "endDate": "2025-02-01",
    "buildingId": 1,
    "floorId": 10
}
```

#### Response (JSON):
```json
{
    "success": true,
    "message": "Data retrieved successfully",
    "data": [
        {
            "floorId": 10,
            "floorNumber": 2,
            "energyConsumedKwh": 500.75,
            "floorDetails": [
                {
                    "consumptionId": 101,
                    "floorId": 10,
                    "buildingId": 1,
                    "timestamp": "2025-01-22T10:00:00Z",
                    "energyConsumedKwh": 250.5,
                    "peakLoadKw": 75.5,
                    "avgTemperatureC": 22.5,
                    "co2EmissionsKg": 10.5,
                    "costPerUnit": 23,
                    "totalCost": 5761.5
                }
            ]
        }
    ]
}
```

---

### 2. Get Metrics

#### Description
Retrieves energy metrics for buildings.

#### Endpoint:
```
POST /api/energy/GetMetrics
```

#### Request:
```sh
curl -X POST "http://localhost:5050/api/energy/GetMetrics" \
-H "Authorization: Bearer <your_token>" \
-H "Content-Type: application/json" \
-d '{
    "startDate": "2024-01-20",
    "endDate": "2024-02-01",
    "metricType": "persft",
    "buildingId": 1
}'
```

#### Request Body (JSON):
```json
{
    "startDate": "2024-01-20",
    "endDate": "2024-02-01",
    "metricType": "persft",
    "buildingId": 1
}
```

#### Response (JSON):
```json
{
    "success": true,
    "message": "Data retrieved successfully",
    "data": [
        {
            "buildingId": 1,
            "floorId": 10,
            "floorNumber": 2,
            "metricType": "Energy Efficiency",
            "metricValue": 85.3
        }
    ]
}
```

---

## Authentication
All endpoints require authentication using a Bearer Token. Include the token in the Authorization header as follows:
```sh
-H "Authorization: Bearer <your_token>"
```

