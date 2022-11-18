using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.ObjectModel;
using System.IO;


// Example usage for MailSlurp email API plugin
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
                // open the dummy authentication app and assert it is loaded
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