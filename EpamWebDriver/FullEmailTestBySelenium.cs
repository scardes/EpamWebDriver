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

            [OneTimeSetUp]
            public void Setup()
            {
                //Below code is to get the drivers folder path dynamically.
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

                //Creates the ChomeDriver object, Executes tests on Google Chrome
                driver = new ChromeDriver(path + @"\drivers\");
            }


            public void GotoMailRU()
            {
                driver.Navigate().GoToUrl("https://account.mail.ru/");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

            public void MailRUAutorization()
            {
                GotoMailRU();

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
            }

            [Test, Order(1)]
            public void LoadMailInBrowser_ClickSignUp_LoadSuccess()
            {
                // open the mail url
                GotoMailRU();

                //Check window is loaded
                Assert.IsTrue(driver.FindElement(By.Id("login-content")).Displayed);
            }

            [Test, Order(2)]
            public void Authorization_WithEmptyEmailPassword_AuthorizationError()
            {
                // open the mail url again for empty data
                GotoMailRU();

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
                // open the mail url again for incorrent data
                GotoMailRU();

                // next fill out the sign-up form with email address and a password
                driver.FindElement(By.Name("username")).SendKeys("123");

                // Go to on page with password
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.CssSelector("[data-test-id=next-button]")).Click();

                //enter wrong password and try to enter
                driver.FindElement(By.Name("password")).SendKeys("123");
                driver.FindElement(By.CssSelector("[data-test-id=submit-button]")).Click();

                //And catch error of authorization
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(By.CssSelector("[data-test-id=password-input-error]")).Displayed);
            }

            [Test, Order(4)]
            public void Authorization_WithCorrectEmailPassword_AuthorizationSuccess()
            {
                MailRUAutorization();

                //And success of authorization
                
                Assert.AreEqual("Авторизация", driver.Title);
            }

            [Test, Order(5)]
            public void SendEmail_WithSomeInformation_SendSuccess()
            {
                MailRUAutorization();

                // Go to the page with sending email
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.XPath("//*[@id='app-canvas']/div/div[1]/div[1]/div/div[2]/span/div[1]/div[1]/div/div/div/div[1]/div/div/a")).Click();

                //Fill info one to receive a letter
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/div[3]/div[2]/div/div/div[1]/div/div[2]/div/div/label/div/div/input"))
                    .SendKeys("ac58d572-8f4f-4ca8-817e-b3bfb1e9f2e8@mailslurp.com");
                //Fill the theme of letter
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/div[3]/div[3]/div[1]/div[2]/div/input"))
                    .SendKeys("Test Letter From mail.ru");
                //Fill content of letter
                driver.FindElement(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/div[3]/div[5]/div/div/div[2]/div[1]"))
                    .SendKeys("Content of test letter");
                //And send letter
                driver.FindElement(By.CssSelector("[data-test-id=send]")).Click();
                //By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[3]/div[1]/div[1]/div/button/span")
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