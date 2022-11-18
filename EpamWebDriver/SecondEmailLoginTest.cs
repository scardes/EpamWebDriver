using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.ObjectModel;
using System.IO;

/// <summary>
/// Selenium for mail on https://account.mail.ru/
/// </summary>
namespace ExampleService.Tests
{
    [TestFixture]
    public class SecondEmailAuthorization
    {
        public class NunitSetup
        {
            IWebDriver driver;

            [OneTimeSetUp]
            public void Setup()
            {
                //Below code is to get the drivers folder path dynamically.
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

                //Creates the ChomeDriver object, Executes tests on Google Chrome
                driver = new ChromeDriver(path + @"\drivers\");
            }

            [Test, Order(1)]
            public void LoadMailInBrowser_ClickSignUp_LoadSuccess()
            {
                // open the mail url
                driver.Navigate().GoToUrl("https://account.mail.ru/");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(By.Id("login-content")).Displayed);
            }

            [Test, Order(2)]
            public void Authorization_WithEmptyEmailPassword_AuthorizationError()
            {
                // inbox has a empty username/email address
                var username = "";

                // next fill out the sign-up form with email address and try to go in page with password
                driver.FindElement(By.Name("username")).SendKeys(username);

                // try to go in page with password
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.CssSelector("[data-test-id=next-button]")).Click();

                //Take a error of empty username/email address
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(By.CssSelector("[data-test-id=required]")).Displayed);
            }

            [Test, Order(3)]
            public void Authorization_WithErrorEmailPassword_AuthorizationError()
            {
                // inbox has a empty email address
                var username = "123";
                var emailPassword = "123";

                // next fill out the sign-up form with email address and a password
                driver.FindElement(By.Name("username")).SendKeys(username);

                // Go to on page with password
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.CssSelector("[data-test-id=next-button]")).Click();

                //enter wrong password and try to enter
                driver.FindElement(By.Name("password")).SendKeys(emailPassword);
                driver.FindElement(By.CssSelector("[data-test-id=submit-button]")).Click();

                //And catch error of authorization
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(By.CssSelector("[data-test-id=password-input-error]")).Displayed);
            }

            [Test, Order(4)]
            public void Authorization_WithCorrectEmailPassword_AuthorizationSuccess()
            {
                // open the mail url again for corrent data
                driver.Navigate().GoToUrl("https://account.mail.ru/");

                // inbox has a empty email address
                var username = "epamtestmail93@mail.ru";
                var emailPassword = "EpamTest185";


                // next fill out the sign-up form with email address and a password
                driver.FindElement(By.Name("username")).SendKeys(username);

                // Go to on page with password
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.CssSelector("[data-test-id=next-button]")).Click();

                //enter wrong password and try to enter
                driver.FindElement(By.Name("password")).SendKeys(emailPassword);
                driver.FindElement(By.CssSelector("[data-test-id=submit-button]")).Click();

                //And success of authorization
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.AreEqual("Авторизация", driver.Title);
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