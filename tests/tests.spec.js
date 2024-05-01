const { test, expect } = require('@playwright/test')

test.beforeEach('', async ({ page }) => {
  await page.goto('https://localhost:7288/')
})

test('Test: page nagavation', async ({ page }) => {

  await page.getByLabel('Username').fill('s215779@dtu.dk');
  await page.getByLabel('Password').fill('Kv08-650b');
  await page.getByRole('button', { name: 'Login' }).click();

  await page.getByRole('link', { name: 'Privacy' }).click();
  await page.getByRole('link', { name: 'Home' }).click();
  await page.getByRole('link', { name: ' View Quizzes' }).click();
  await page.getByRole('link', { name: '' }).click();
  await page.getByRole('link', { name: ' Create new quiz' }).click();
  await expect(page.getByRole('heading', { name: 'Create New Quiz' })).toBeVisible()

});

test('Test: test creation', async ({ page }) => {

  await page.getByLabel('Username').fill('s215779@dtu.dk');
  await page.getByLabel('Password').fill('Kv08-650b');
  await page.getByRole('button', { name: 'Login' }).click();

  await page.getByRole('link', { name: ' Create new quiz' }).click();

  await page.getByLabel('Course ID').fill('62413');

  await page.getByLabel('Question').click();
  await page.getByLabel('Question').fill('1');
  await page.getByLabel('Correct Answer', { exact: true }).click();
  await page.getByLabel('Correct Answer', { exact: true }).fill('2');
  await page.getByLabel('Incorrect Answer 1').fill('3');
  await page.getByLabel('Incorrect Answer 1').click();
  await page.getByLabel('Incorrect Answer 2').click();
  await page.getByLabel('Incorrect Answer 2').fill('4');
  await page.getByLabel('Incorrect Answer 3').click();
  await page.getByLabel('Incorrect Answer 3').fill('5');

  await page.getByRole('button', { name: 'Add Another Question' }).click();

  await page.locator('input[name="Input\\.Questions\\[1\\]\\.QuizQuestion"]').click();
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.QuizQuestion"]').fill('6');
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.CorrectAnswer"]').click();
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.CorrectAnswer"]').fill('7');
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.IncorrectAnswer1"]').click();
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.IncorrectAnswer1"]').fill('8');
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.IncorrectAnswer2"]').click();
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.IncorrectAnswer2"]').fill('9');
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.IncorrectAnswer3"]').click();
  await page.locator('input[name="Input\\.Questions\\[1\\]\\.IncorrectAnswer3"]').fill('0');

  await page.getByRole('button', { name: 'Create Quiz' }).click();

});

