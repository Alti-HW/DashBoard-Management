# Occupancy Controller API Documentation

## Overview
The Occupancy Controller API provides endpoints to retrieve occupancy data for buildings.

## Base URL
```
http://localhost:5050/api/occupancy
```

## Endpoints

### 1. Get Occupancy Data

#### Description
Fetches occupancy data for a specified building and time range.

#### Endpoint:
```
POST /api/occupancy/get
```

#### Request:
```sh
curl -X POST "http://localhost:5050/api/occupancy/get" \
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
            "averageOccupancy": 45,
            "peakOccupancy": 80
        }
    ]
}
```

## Authentication
All endpoints require authentication using a Bearer Token. Include the token in the Authorization header as follows:
```sh
-H "Authorization: Bearer <your_token>"
```

