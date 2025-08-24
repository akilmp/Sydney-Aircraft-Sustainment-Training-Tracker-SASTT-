# Sydney Aircraft Sustainment & Training Tracker (SASTT)

## README (Executive Summary)

**Project:** Sydney Aircraft Sustainment & Training Tracker (SASTT)
**Stack:** C# / ASP.NET Core 8, Oracle DB (ODP.NET + EF Core), TypeScript + Razor (or Blazor Server), xUnit, Docker
**Scope:** A production-style web app that simulates how aircraft maintenance and pilot training are planned, executed, and audited at **Sydney (YSSY)**, with an optional profile for **RAAF Base Williamtown (YWLM)**.
**Why I built it:** I live in Sydney and grew up around the airport’s flight paths. I wanted to learn the discipline behind aviation sustainment software while practicing Microsoft-centric development and Oracle data modeling. This is a personal project—data is **synthetic** and **unclassified**.

**Highlights**

* Role-aligned: C#, Microsoft stack, Oracle DB, SDLC rigor, documentation, CI tests.
* Aviation-realistic: maintenance work orders, training hours, airfield weather feeds, ops dashboards.
* Enterprise touches: RBAC, audit log, health checks, seed scripts, migration strategy.

**Quick Start**

1. `docker compose up -d` (spins up Oracle XE and the web app)
2. Copy `.env.example` to `.env` and set `ORACLE__CONNECTIONSTRING`, `WEATHER__APIKEY`, and `APP__DEFAULTBASE` (see [Configuration](#configuration)).
3. The web container entrypoint runs `dotnet ef database update` and then `dotnet run -- --seed --base $APP__DEFAULTBASE`.
4. Login as `admin@sastt.local` / `Passw0rd!` (dev)

### Configuration

Create a `.env` file with the following values:

```
ORACLE__CONNECTIONSTRING=User Id=USER;Password=PASSWORD;Data Source=HOST:PORT/SERVICE;
WEATHER__APIKEY=YOUR_WEATHER_API_KEY
APP__DEFAULTBASE=YSSY
```

### Documentation

- [Architecture overview](docs/architecture.md)
- [API specification](docs/api-spec.md)
- [Database schema](docs/db-schema.sql)

To seed manually outside of Docker:

```bash
dotnet run --project src/Sastt.Web -- --seed --base YSSY
```

Replace `YSSY` with `YWLM` to load Williamtown data.

---

## 1. Product Overview

### 1.1 Problem Statement

Coordinating aircraft maintenance and training is complex: you’re juggling airworthiness, parts, technicians, pilots, weather, and schedules. SASTT simulates this environment for **Sydney**, demonstrating full-stack engineering and SDLC fluency.

### 1.2 Goals

* Model **maintenance workflows** (plan → execute → QA → close).
* Track **training events** and **pilot hours** with currency rules.
* Provide a **single dashboard** for ops status at YSSY (and optional YWLM).
* Show clean, testable, Microsoft-centric code with Oracle data persistence.

### 1.3 Non-Goals

* Real-time ops; classified data; integration with live Defence systems.
* Production SSO or live Oracle RAC; this uses Oracle XE for simplicity.

---

## 2. Users & Use Cases

### 2.1 Personas

* **Maintenance Planner (Ava)** – creates work orders; assigns technicians and parts.
* **Technician (Hemi)** – executes tasks; records defects; uploads photos/notes.
* **Training Officer (Mia)** – schedules sorties/sims; validates pilot currencies.
* **Ops Lead (Luca)** – watches dashboards; approves deferrals; reads reports.
* **Auditor (Sofia)** – views immutable audit log of changes.

### 2.2 Primary Use Cases

* Create aircraft, link to base (YSSY/YWLM).
* Create maintenance plan → generate work orders → track task status → QA close.
* Schedule training sessions; accrue pilot hours; enforce recurrence (e.g., “IFR currency within last 60 days”).
* See weather snapshot for the base; highlight potential weather-driven risk.
* Export weekly sustainment & training readiness report (PDF/CSV).

---

## 3. Requirements

### 3.1 Functional

* CRUD for **Aircraft**, **Work Orders**, **Tasks**, **Defects**, **Pilots**, **Training Sessions**.
* Status workflow: *Draft → Planned → In-Progress → QA Review → Closed*; with deferral path.
* Calendar views and list views with filters (base, aircraft type, status, date).
* **RBAC**: Admin, Planner, Technician, TrainingOfficer, Auditor, Viewer.
* Audit trail for every create/update/delete (who/when/what).
* Weather widget per base (simple API fetch; no secrets committed).
* Reports: weekly CSV & PDF export, and a printable view.

### 3.2 Non-Functional

* **Performance:** P95 API < 300ms for core reads on dev data sets.
* **Reliability:** Graceful retry around external APIs; health checks at `/health`.
* **Security:** ASP.NET Identity, per-role authorization, parameterized SQL; secrets via env vars.
* **Compliance Awareness:** No export-controlled data; synthetic data only.
* **Observability:** Structured logs (Serilog), correlation IDs, minimal metrics.

---

## 4. Architecture

### 4.1 High-Level

* **Frontend:** Razor/Blazor Server for simplicity; TypeScript for widgets.
* **Backend:** ASP.NET Core 8, Clean Architecture (Domain, Application, Infrastructure, Web).
* **Persistence:** Oracle XE with EF Core (Oracle.EntityFrameworkCore) and ODP.NET (Oracle.ManagedDataAccess.Core).
* **Integrations:** Weather provider (e.g., OpenWeather) via simple HttpClient.
* **Containerization:** Docker Compose for app + Oracle XE.

```
Browser
  ↕ HTTPS
Web (ASP.NET Core)
  ├─ Application (CQRS: MediatR handlers, validation)
  ├─ Domain (entities, value objects, events)
  └─ Infrastructure
       ├─ Oracle (EF Core + migrations)
       ├─ Auth (ASP.NET Identity)
       ├─ WeatherClient (HttpClient)
       └─ Logging (Serilog)
```

### 4.2 Solution Layout

```
src/
  Sastt.Web/          // UI, controllers/pages, DI
  Sastt.Application/  // CQRS, validators, DTOs
  Sastt.Domain/       // Entities, enums, VOs, domain events
  Sastt.Infrastructure// EF Core, Oracle, Identity, Repos
tests/
  Sastt.UnitTests/
  Sastt.IntegrationTests/
docs/
  README.md
  architecture.md
  api-spec.md
  db-schema.sql
  ops-runbook.md
```

---

## 5. Data Model

### 5.1 Entities (selected)

* **Base**(Id, ICAO, Name, Lat, Lon, Timezone)
* **Aircraft**(Id, TailNumber, Type, BaseId, Status)
* **WorkOrder**(Id, AircraftId, Title, Priority, Status, PlannedStart, PlannedEnd, CreatedBy, CreatedAt)
* **Task**(Id, WorkOrderId, StepNo, Description, Status, TechnicianId, StartedAt, CompletedAt)
* **Defect**(Id, AircraftId, ReportedBy, Severity, Description, Status)
* **Pilot**(Id, StaffNo, Name, Rank, Qualifications\[])
* **TrainingSession**(Id, PilotId, AircraftId?, Type\[SIM|SORTIE], Start, End, Result, Notes)
* **PilotCurrency**(Id, PilotId, RuleCode, SatisfiedUntil)
* **User**(Id, Email, PasswordHash, Role)
* **AuditLog**(Id, UserId, EntityType, EntityId, Action, DataJson, Timestamp)

### 5.2 Oracle DDL (excerpt)

```sql
CREATE TABLE BASE (
  ID NUMBER GENERATED BY DEFAULT ON NULL AS IDENTITY PRIMARY KEY,
  ICAO VARCHAR2(4) NOT NULL UNIQUE,
  NAME VARCHAR2(100) NOT NULL,
  LAT NUMBER(9,6),
  LON NUMBER(9,6),
  TIMEZONE VARCHAR2(50) NOT NULL
);
```

(Additional schema definitions omitted here for brevity, but included in full documentation.)

### 5.3 Seed Data (Sydney-flavoured)

* Bases: `YSSY` (Sydney Kingsford Smith), `YWLM` (RAAF Base Williamtown).
* Aircraft types: `B738`, `A330`, `Hawk 127`, `PC-21`.
* Tail numbers: `SY-001`, `SY-002`, `WL-H127-01`…
* Pilots: fictional names with local touches; technician teams “Mascot Maint A/B”.

---

## 6. API Spec (REST)

Base URL: `/api/v1`

### GET `/aircraft?base=YSSY&status=AVAILABLE`

**200**

```json
[
  {"id": 12, "tailNumber": "SY-001", "type": "B738", "base": "YSSY", "status": "AVAILABLE"}
]
```

### POST `/workorders`

```json
{
  "aircraftId": 12,
  "title": "A-check cycle 4",
  "priority": "HIGH",
  "plannedStart": "2025-08-21T08:00:00+10:00"
}
```

---

## 7. UI/UX

### 7.1 Screens

* **Dashboard** – Maintenance backlog, training events, weather, defects.
* **Maintenance** – Work orders list/detail.
* **Training** – Calendar of sessions.
* **Aircraft** – Fleet roster and status.
* **Reports** – Export readiness.
* **Admin** – Manage users/roles.

### 7.2 Personalization

* Default landing shows **Sydney (YSSY)** runway map.
* Weather panel captioned: *“Local to Sydney (AEST/UTC+10, DST aware)”*.

---

## 8. SDLC & Quality

### 8.1 Process

* Branching: `main`, `develop`, `feat/*`, `fix/*`.
* PR template includes risks, screenshots, tests.

### 8.2 Testing

* Unit: xUnit domain rules.
* Integration: Oracle Testcontainers.
* UI smoke: Playwright.

---

## 9. Security

* ASP.NET Identity, RBAC roles.
* Parametrized queries.
* Secrets in env vars, not code.

---

## 10. Configuration

The web application requires the following environment variables. Copy `.env.example` to `.env` and adjust the values for your environment.

| Setting               | Env Var                    | Example                                                         |
| --------------------- | -------------------------- | --------------------------------------------------------------- |
| Oracle connect string | `ORACLE__CONNECTIONSTRING` | `User Id=SASTT;Password=...;Data Source=localhost:1521/XEPDB1;` |
| Weather API key       | `WEATHER__APIKEY`          | `xxxxx`                                                         |
| Default base          | `APP__DEFAULTBASE`         | `YSSY`                                                          |

---

## 11. Implementation Notes

* Work order closure blocked by open defects.
* Training session updates pilot currencies.
* Weather cached 15 minutes.

---

## 12. Compliance & Ethics

* All data fictional/unclassified.
* Repo contains `COMPLIANCE.md`.

---

## 13. Telemetry & Ops

* Structured logs (Serilog).
* `/health` endpoint.

---

## 14. Reports

* Weekly sustainment CSV/PDF.
* Audit export CSV.

---

## 15. Manual Test Cases

1. Create Work Order → tasks incomplete → block closure.
2. Add High defect → block closure.
3. Pilot currency updated on training pass.
4. Role enforcement.

---

## 16. Roadmap

* Parts & inventory.
* Digital signatures.
* Advanced scheduling.


