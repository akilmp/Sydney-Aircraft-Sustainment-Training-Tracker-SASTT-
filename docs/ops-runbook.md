# Operations Runbook

## Health Checks

The application exposes health information at `/health`.

### Oracle DB
- **Name:** `oracle-db`
- Opens a connection using `ORACLE__CONNECTIONSTRING`.
- Reports unhealthy if the database cannot be reached.

### Weather API
- **Name:** `weather-api`
- Performs an HTTP request to WeatherAPI using `WEATHER__APIKEY` and the default base code from `APP__DEFAULTBASE`.
- Fails when the API responds with a non-success status.

## Verification
To check the status locally:

```bash
curl http://localhost:5000/health
```

Adjust the port if the web app listens on a different one.

