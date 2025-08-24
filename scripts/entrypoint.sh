#!/usr/bin/env bash
set -e

# Apply migrations
if command -v dotnet >/dev/null 2>&1; then
  echo "Applying database migrations..."
  dotnet ef database update

  BASE=${APP__DEFAULTBASE:-YSSY}
  echo "Seeding database for ${BASE}..."
  dotnet run -- --seed --base ${BASE}
else
  echo "dotnet command not found" >&2
  exit 1
fi
