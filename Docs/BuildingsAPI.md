# Building Controller API Documentation

## Overview
The Building Controller API provides endpoints for managing buildings and retrieving their associated floor data.

## Base URL
```
http://localhost:5050/api/building
```

## Endpoints

### 1. Get All Buildings with Floors

#### Description
Retrieves all buildings along with their associated floors.

#### Endpoint:
```
GET /api/building/GetAllBuildingsWithFloors
```

#### Request:
```sh
curl -X GET "http://localhost:5050/api/building/GetAllBuildingsWithFloors" \
-H "Authorization: Bearer <your_token>"
```

#### Response (JSON):
```json
{
    "success": true,
    "message": "Data retrieved successfully",
    "data": [
        {
            "buildingId": 1,
            "name": "Building A",
            "floors": [
                { "floorId": 101, "floorNumber": 1 },
                { "floorId": 102, "floorNumber": 2 }
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

