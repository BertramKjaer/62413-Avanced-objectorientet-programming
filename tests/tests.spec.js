// @ts-check
const { test, expect } = require('@playwright/test')

test.beforeEach('', async ({ page }) => {
  await page.goto('https://localhost:7288/')
})

test('Test: page nagavation', async ({ page }) => {

  await expect(page.getByRole('heading')).toHaveText('Login')

  await page.getByRole('button', { name: 'Login' }).click()
  await expect(page.getByRole('heading')).toHaveText('Home Page')

  await page.locator('.nav-link', {hasText: 'Privacy'}).click()
  await expect(page.getByRole('heading').nth(0)).toHaveText('Privacy Policy')

  await page.locator('.nav-link', {hasText: 'Home'}).click()
  await expect(page.getByRole('heading').nth(0)).toHaveText('Home Page')

  await page.locator('.nav-link', {hasText: 'Logout'}).click()
  await expect(page.getByRole('heading').nth(0)).toHaveText('Login')

});

test('Test: test creation', async ({ page }) => {

  await page.getByRole('button', { name: 'Login' }).click()
  await page.locator('.icon-link', {hasText: 'Create new quiz'}).click() 
  
  await expect(page.getByRole('heading').nth(0)).toHaveText('Create New Quiz')
  await expect( page.getByText('Question 1')).toBeVisible()

  await page.getByRole('button', { name: 'Add Another Question' }).click()
  await expect( page.getByText('Question 2')).toBeVisible()

  await page.getByRole('button', { name: 'Add Another Question' }).click()
  await expect( page.getByText('Question 3')).toBeVisible()
});

