using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.ObjectModel;
using System.IO;

/// <summary>
/// Selenium test project with two emails 
/// first email on https://account.mail.ru/
/// second email on 
/// </summary>
namespace ExampleService.Tests
{
    [TestFixture]
    public class FullEmailTestBySelenium
    {
        public class NunitSetup
        {
            IWebDriver driver;

            string MailRuUrl = "https://account.mail.ru/";
            string username = "epamtestmail93@mail.ru";
            string emailPassword = "EpamTest185";

            //Page objects For MailRUAutorization() and [Test, Order(2)]; [Test, Order(3)]
            By NextButton = By.CssSelector("[data-test-id=next-button]");
            By SubmitButton = By.CssSelector("[data-test-id=submit-button]");
            By PopUpEmptyError = By.CssSelector("[data-test-id=required]");
            By PopUpInputError = By.CssSelector("[data-test-id=password-input-error]");

            //Page objects For send email from mail.ru [Test, Order(5)]
            By WriteLetterButton = By.XPath("//span[@class='compose-button__txt']");
            By LetterReciverField = By.XPath("//input[@class='container--H9L5q size_s--3_M-_']");
            By LetterThemeField = By.XPath("//input[@name='Subject']");
            By LetterContendField = By.XPath("//div[@role='textbox']/div");
            By SendButton = By.CssSelector("[data-test-id=send]");

            [OneTimeSetUp]
            public void Setup()
            {
                //Below code is to get the drivers folder path dynamically.
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

                //Creates the ChomeDriver object, Executes tests on Google Chrome
                driver = new ChromeDriver(path + @"\drivers\");
            }

            //Open mail.ru page
            public void GotoMailRU()
            {
                driver.Navigate().GoToUrl(MailRuUrl);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

            // Make full autorization on mail.ru
            public void MailRUAutorization()
            {
                // Open mail.ru page
                GotoMailRU();
                
                // Next fill out the sign-up form with email address and a password
                driver.FindElement(By.Name("username")).SendKeys(username);

                // Go to on page with password
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(NextButton).Click();

                //enter wrong password and try to enter
                driver.FindElement(By.Name("password")).SendKeys(emailPassword);
                driver.FindElement(SubmitButton).Click();
            }

            [Test, Order(1)]
            public void LoadMailInBrowser_ClickSignUp_LoadSuccess()
            {
                // Open mail.ru page
                GotoMailRU();

                //Check window is loaded
                Assert.IsTrue(driver.FindElement(By.Id("login-content")).Displayed);
            }

            [Test, Order(2)]
            public void Authorization_WithEmptyEmailPassword_AuthorizationError()
            {
                // Open mail.ru page again for empty data
                GotoMailRU();

                // next fill out the sign-up form with email address and try to go in page with password
                driver.FindElement(By.Name("username")).SendKeys("");

                // try to go in page with password
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(NextButton).Click();

                //Take a error of empty username/email address
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(PopUpEmptyError).Displayed);
            }

            [Test, Order(3)]
            public void Authorization_WithErrorEmailPassword_AuthorizationError()
            {
                //  Open mail.ru page again for incorrent data
                GotoMailRU();

                // next fill out the sign-up form with email address and a password
                driver.FindElement(By.Name("username")).SendKeys("123");

                // Go to on page with password
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(NextButton).Click();

                //enter wrong password and try to enter
                driver.FindElement(By.Name("password")).SendKeys("123");
                driver.FindElement(SubmitButton).Click();

                //And catch error of authorization
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(PopUpInputError).Displayed);
            }

            [Test, Order(4)]
            public void Authorization_WithCorrectEmailPassword_AuthorizationSuccess()
            {
                // Make full autorization on mail.ru
                MailRUAutorization();

                //And success of authorization
                Assert.AreEqual("Авторизация", driver.Title);
            }

            [Test, Order(5)]
            public void SendEmail_WithSomeInformation_SendSuccess()
            {
                // Make full autorization on mail.ru
                MailRUAutorization();
                                
                // Go to the page with sending email
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(WriteLetterButton).Click();

                //Fill a letter content and send email
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                driver.FindElement(LetterReciverField).SendKeys("testepammail@yandex.ru");
                driver.FindElement(LetterThemeField).SendKeys("Test Letter From mail.ru");
                //Fill content of letter
                driver.FindElement(LetterContendField).SendKeys("Content of test letter");
                //And send letter
                driver.FindElement(SendButton).Click();
            }



            // runs once after all tests finished

            [OneTimeTearDown]
            public void Dispose()
            {
                // close down the browser
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}