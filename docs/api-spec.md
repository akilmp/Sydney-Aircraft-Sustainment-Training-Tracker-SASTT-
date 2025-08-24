# API Specification

The SASTT API exposes RESTful endpoints for managing aircraft, maintenance work orders, and pilot training. All endpoints require authentication and respond with JSON.

## Aircraft
- `GET /api/aircraft` – list all aircraft.
- `POST /api/aircraft` – register a new aircraft.
- `GET /api/aircraft/{id}` – retrieve aircraft details.

## Maintenance
- `GET /api/workorders` – list maintenance work orders.
- `POST /api/workorders` – create a work order.
- `PATCH /api/workorders/{id}/status` – update the status of a work order.
- `POST /api/workorders/{id}/tasks` – add a task to a work order.
- `PATCH /api/tasks/{id}` – update a task's status.
- `POST /api/defects` – record a defect against an aircraft.

## Training
- `GET /api/pilots` – list pilots.
- `POST /api/pilots` – register a new pilot.
- `GET /api/training-sessions` – list scheduled training sessions.
- `POST /api/training-sessions` – schedule a new training session.

## Weather
- `GET /api/weather/{base}` – fetch weather forecast for an air base.

Responses use JSON and standard HTTP status codes.
