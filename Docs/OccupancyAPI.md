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
            "buildingId": 1,
            "buildingName": "Building A",
            "floors": [
                {
                    "floorId": 10,
                    "floorNumber": 2,
                    "totalOccupancyCount": 150,
                    "avgOccupancyRatio": 0.75,
                    "occupancyDetails": [
                        {
                            "occupancyId": 101,
                            "floorId": 10,
                            "timestamp": "2025-01-22T10:00:00Z",
                            "occupancyCount": 50,
                            "avgOccupancyRatio": 0.85
                        },
                        {
                            "occupancyId": 102,
                            "floorId": 10,
                            "timestamp": "2025-01-23T10:00:00Z",
                            "occupancyCount": 60,
                            "avgOccupancyRatio": 0.80
                        }
                    ]
                }
            ]
        }
    ]
}
```

## Authentication
All endpoints require authentication using a Bearer Token. Include the token in the Authorization header as follows:
```sh
-H "Authorization: Bearer <your_token>"
```

