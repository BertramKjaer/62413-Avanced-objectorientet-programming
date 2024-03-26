// @ts-check
const { test, expect } = require('@playwright/test');

test('Index page title', async ({ page }) => {
  await page.goto('https://localhost:7288/');

  // Expect a title "to contain" a substring.
  await expect(page.locator('.navbar-brand')).toHaveText('QuizProgram')
});


