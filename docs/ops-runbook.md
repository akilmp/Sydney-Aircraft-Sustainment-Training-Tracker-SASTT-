# Operations Runbook

This runbook outlines common operational tasks for SASTT.

## Setup
1. Copy `.env.example` to `.env` and supply real values.
2. Ensure Docker and docker-compose are installed.
3. Start services with:
   ```
   docker-compose up --build
   ```

## Routine Tasks
- **Database migrations**: handled automatically on startup. For manual runs:
  ```
  dotnet ef database update
  ```
- **Logs**: application logs are written to standard output and collected by the host system.
- **Backup**: export Oracle data using native tools or snapshots.

## Incident Response
1. Check container logs for errors.
2. If the database is unavailable, restart the `oracle` service.
3. Escalate unresolved issues to the platform team.
