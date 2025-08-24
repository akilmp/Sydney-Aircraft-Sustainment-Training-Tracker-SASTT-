# API Specification

The API exposes RESTful endpoints secured behind application authentication.

## Endpoints

- `GET /api/trainees` – list all trainees.
- `POST /api/trainees` – register a new trainee.
- `GET /api/sessions` – retrieve scheduled training sessions.
- `POST /api/sessions` – schedule a new session.
- `GET /api/weather/{base}` – fetch weather forecast for a training base.

Responses use JSON and standard HTTP status codes.
