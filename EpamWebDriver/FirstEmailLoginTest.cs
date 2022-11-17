using System;
using System.Text.RegularExpressions;
using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

// Example usage for MailSlurp email API plugin
namespace ExampleService.Tests
{
       
    [TestFixture]
    public class ExampleTest
    {
        private static IWebDriver _webdriver;
        private static Configuration _mailslurpConfig;

        // get a MailSlurp API Key free at https://app.mailslurp.com
        private static readonly string YourApiKey = "d78376137b1b7f5fa1de7480c363bb7f31edc47415306a77b5f763b5775f8f70";
        private static readonly long TimeoutMillis = 30_000L;
        private static InboxDto _inbox;

        public class NunitSetup
        {
            // runs once before any tests
            [OneTimeSetUp]
            public void SetUp()
            {
                // set up the webdriver for selenium
                var timeout = TimeSpan.FromMilliseconds(TimeoutMillis);
                var service = FirefoxDriverService.CreateDefaultService();
                _webdriver = new FirefoxDriver(service, new FirefoxOptions(), timeout);
                _webdriver.Manage().Timeouts().ImplicitWait = timeout;

                // configure mailslurp with API Key
                Assert.NotNull(YourApiKey);
                _mailslurpConfig = new Configuration();
                _mailslurpConfig.ApiKey.Add("x-api-key", YourApiKey);
            }


            [Test, Order(1)]
            public void LoadMailInBrowser_ClickSignUp()
            {
                // open the dummy authentication app and assert it is loaded
                _webdriver.Navigate().GoToUrl("https://playground.mailslurp.com/");
                Assert.AreEqual("React App", _webdriver.Title);

                // can click the signup button
                _webdriver.FindElement(By.CssSelector("[data-test=sign-in-create-account-link]")).Click();
            }

            [Test, Order(2)]
            public void Authorization_WithEmptyEmailPassword_AuthenticatorError()
            {
                // inbox has a real email address
                var emailAddress = "";
                var emailPassword = "";

                // next fill out the sign-up form with email address and a password
                _webdriver.FindElement(By.Name("email")).SendKeys(emailAddress);
                _webdriver.FindElement(By.Name("password")).SendKeys(emailPassword);

                // submit form
                _webdriver.FindElement(By.CssSelector("[data-test=sign-up-create-account-button]")).Click();

                _webdriver.FindElement(By.CssSelector("[data-test=authenticator-error]"));
            }


           // runs once after all tests finished
           [OneTimeTearDown]
            public void Dispose()
            {
                // close down the browser
                _webdriver.Quit();
                _webdriver.Dispose();
            }
        }
    }
}