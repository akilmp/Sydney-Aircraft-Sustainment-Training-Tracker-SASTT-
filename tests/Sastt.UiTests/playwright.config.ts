import { defineConfig } from '@playwright/test';

export default defineConfig({
  webServer: {
    command: 'dotnet run --project ../../src/Sastt.Web/Sastt.Web.csproj --urls http://localhost:5000',
    url: 'http://localhost:5000',
    reuseExistingServer: !process.env.CI,
  },
  use: {
    baseURL: 'http://localhost:5000'
  }
});
