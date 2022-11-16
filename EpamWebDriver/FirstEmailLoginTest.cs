using System;
using System.Text.RegularExpressions;
using mailslurp.Api;
using mailslurp.Client;
using mailslurp.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

// Example usage for MailSlurp email API plugin
namespace ExampleService.Tests
{

    [TestFixture]
    public class ExampleTest
    {
        private static IWebDriver _webdriver;
        private static Configuration _mailslurpConfig;

        // get a MailSlurp API Key free at https://app.mailslurp.com
        private static readonly string YourApiKey = "your-api-key-here";

        private static readonly long TimeoutMillis = 30_000L;

        [SetUpFixture]
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