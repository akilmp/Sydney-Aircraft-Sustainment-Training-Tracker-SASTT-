# System Architecture

SASTT is an ASP.NET Core web API built with a clean architecture that models aircraft inventory, maintenance workflows, and pilot training. Data is stored in an Oracle database and weather data is retrieved from an external service to help plan operations.

## Components
- **API Service** – exposes endpoints for aircraft, work orders, tasks, defects, pilots, and training sessions.
- **Oracle Database** – persists domain entities and audit logs via Entity Framework Core.
- **Weather Provider** – third‑party REST API queried for base forecasts.
- **Background Jobs** – synchronize weather snapshots and perform routine cleanup.

## Data Flow
1. Clients send requests to the API service.
2. The service reads or writes data through EF Core to Oracle.
3. Weather information is fetched on demand when planning maintenance or training.

Services are containerized with Docker Compose and configured through environment variables in `.env`.
