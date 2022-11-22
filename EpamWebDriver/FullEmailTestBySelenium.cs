using EpamWebDriver.PageObjects;
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


            // Mail.ru Autorization data
            

            //Page objects For MailRUAutorization() and [Test, Order(2)]; [Test, Order(3)]
            //private readonly By NextButton = By.CssSelector("[data-test-id=next-button]");
            //private readonly By SubmitButton = By.CssSelector("[data-test-id=submit-button]");
            private readonly By PopUpEmptyError = By.CssSelector("[data-test-id=required]");
            private readonly By PopUpInputError = By.CssSelector("[data-test-id=password-input-error]");

            //Page objects For send email from mail.ru [Test, Order(5)]
            private readonly By WriteLetterButton = By.XPath("//span[@class='compose-button__txt']");
            private readonly By LetterReciverField = By.XPath("//input[@class='container--H9L5q size_s--3_M-_']");
            private readonly By LetterThemeField = By.XPath("//input[@name='Subject']");
            private readonly By LetterContendField = By.XPath("//div[@role='textbox']/div");
            private readonly By SendButton = By.CssSelector("[data-test-id=send]");

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
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU();
                // Open mail.ru page

                //Check window is loaded
                Assert.IsTrue(driver.FindElement(By.Id("login-content")).Displayed);
            }

            [Test, Order(2)]
            public void Authorization_WithEmptyEmailPassword_AuthorizationError()
            {
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                
                // Open mail.ru page again for empty data
                autorizationMailru.AutorizationInMailRU("");

                //Take a error of empty username/email address
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(PopUpEmptyError).Displayed);
            }

            [Test, Order(3)]
            public void Authorization_WithErrorEmailPassword_AuthorizationError()
            {
                //  Open mail.ru page again for incorrent data
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("123", "123");

                //And catch error of authorization
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                Assert.IsTrue(driver.FindElement(PopUpInputError).Displayed);
            }

            [Test, Order(4)]
            public void Authorization_WithCorrectEmailPassword_AuthorizationSuccess()
            {
                // Make full autorization on mail.ru
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                
                autorizationMailru.AutorizationInMailRU("epamtestmail93@mail.ru", "EpamTest185");

                //And success of authorization
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                Assert.AreEqual("Авторизация", driver.Title);
            }

            [Test, Order(5)]
            public void SendEmail_WithSomeInformation_SendSuccess()
            {
                // Make full autorization on mail.ru
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);

                autorizationMailru.AutorizationInMailRU("epamtestmail93@mail.ru", "EpamTest185");

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

            //[OneTimeTearDown]
            //public void Dispose()
            //{
            //    // close down the browser
            //    driver.Quit();
            //    driver.Dispose();
            //}
        }
    }
}