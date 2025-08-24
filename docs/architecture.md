# System Architecture

The Sydney Aircraft Sustainment Training Tracker (SASTT) is built as an ASP.NET Core web API.
It integrates an Oracle database for persistence and an external weather service for planning
flight-related training. The application is containerized and orchestrated with Docker Compose
for local development.

Components:
- **API Service**: Exposes training management endpoints and coordinates data access.
- **Oracle Database**: Stores trainees, sessions, and attendance records.
- **Weather Provider**: Third-party REST API used to retrieve forecast data.
- **Background Jobs**: Periodically synchronize weather data and clean up expired sessions.

Data Flow:
1. Client requests are processed by the API service.
2. The service queries or updates the Oracle database.
3. Weather information is fetched when needed for scheduling or debriefing sessions.

Deployment assumes the services run within a secure network and communicate via environment
variables configured in `.env`.
