﻿using System;
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
    public class FirstEmailAuthorization
    {
        private static IWebDriver _webdriver;
        private static Configuration _mailslurpConfig;

        // get a MailSlurp API Key free at https://app.mailslurp.com
        private static readonly string YourApiKey = "d78376137b1b7f5fa1de7480c363bb7f31edc47415306a77b5f763b5775f8f70";
        private static readonly long TimeoutMillis = 30_000L;

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
            public void LoadMailInBrowser_ClickSignUp_LoadSuccess()
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
                // inbox has a empty email address
                var emailAddress = "";
                var emailPassword = "";

                // next fill out the sign-up form with email address and a password
                _webdriver.FindElement(By.Name("email")).SendKeys(emailAddress);
                _webdriver.FindElement(By.Name("password")).SendKeys(emailPassword);

                // submit form
                _webdriver.FindElement(By.CssSelector("[data-test=sign-up-create-account-button]")).Click();
                
                _webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _webdriver.FindElement(By.CssSelector("[data-test=authenticator-error]"));
            }

            [Test, Order(3)]
            public void Authorization_WithWrongEmailPassword_AuthorizationError()
            {
                // inbox has a fake email address
                var emailAddress = "132@mail.com";
                var emailPassword = "123456";

                // next fill out the sign-up form with email address and a password
                _webdriver.FindElement(By.Name("email")).SendKeys(emailAddress);
                _webdriver.FindElement(By.Name("password")).SendKeys(emailPassword);

                // submit form
                _webdriver.FindElement(By.CssSelector("[data-test=sign-up-create-account-button]")).Click();

                _webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _webdriver.FindElement(By.CssSelector("[data-test=authenticator-error]"));
            }

            [Test, Order(4)]
            public void Authorization_WithCorrectEmailPassword_AuthorizationSuccess()
            {
                // inbox has a real email address
                var emailAddress = "ac58d572-8f4f-4ca8-817e-b3bfb1e9f2e8@mailslurp.com";
                var emailPassword = "Test-password-793";

                // next fill out the sign-up form with email address and a password
                _webdriver.FindElement(By.Name("email")).SendKeys(emailAddress);
                _webdriver.FindElement(By.Name("password")).SendKeys(emailPassword);

                // submit form
                _webdriver.FindElement(By.CssSelector("[data-test=sign-up-create-account-button]")).Click();
                _webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                // verify that user can see authenticated content
                Assert.IsTrue(_webdriver.FindElement(By.TagName("h1")).Text.Contains("Welcome"));
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