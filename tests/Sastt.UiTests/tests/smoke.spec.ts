import { test, expect } from '@playwright/test';

test('home page has title', async ({ page }) => {
  await page.goto('/');
  await expect(page).toHaveTitle(/SASTT/);
});

test('training page requires auth', async ({ page }) => {
  const response = await page.goto('/training');
  expect(response?.status()).toBe(401);
});
