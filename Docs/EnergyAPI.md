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
    "buildingId": 1,
    "startDate": "2024-03-01",
    "endDate": "2024-03-10"
}'
```

#### Response (JSON):
```json
{
    "success": true,
    "message": "Data retrieved successfully",
    "data": [
        {
            "buildingId": 1,
            "energyConsumed": 500.75,
            "peakLoad": 75.5
        }
    ]
}
```

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
    "buildingId": 1
}'
```

#### Response (JSON):
```json
{
    "success": true,
    "message": "Data retrieved successfully",
    "data": [
        {
            "metricType": "Energy Efficiency",
            "value": 85.3
        }
    ]
}
```

## Authentication
All endpoints require authentication using a Bearer Token. Include the token in the Authorization header as follows:
```sh
-H "Authorization: Bearer <your_token>"
```

