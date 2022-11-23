using EpamWebDriver.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
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

            [Test, Order(1)]
            public void LoadMailInBrowser_ClickSignUp_LoadSuccess()
            {
                // Open mail.ru page
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU();

                //Check window is loaded
                Assert.AreEqual("Account Mail.Ru", driver.Title);
            }

            [Test, Order(2)]
            public void Authorization_WithEmptyEmailPassword_AuthorizationError()
            {
                // Open mail.ru page again with empty login data
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("");

                //Take a error of empty username/email address
                autorizationMailru.AssertPopUpEmptyError();
            }

            [Test, Order(3)]
            public void Authorization_WithErrorEmailPassword_AuthorizationError()
            {
                //  Open mail.ru page again for incorrent data
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("123", "123");

                //And catch error of authorization
                autorizationMailru.AssertPopUpInputError();
            }

            [Test, Order(4)]
            public void Authorization_WithCorrectEmailPassword_AuthorizationSuccess()
            {
                // Make full autorization on mail.ru
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("epamtestmail93@mail.ru", "EpamTest185");

                //And success of authorization
                Assert.AreEqual("Авторизация", driver.Title);
            }

            [Test, Order(5)]
            public void SendEmail_WithSomeInformation_SendSuccess()
            {
                // Make full autorization on mail.ru
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("epamtestmail93@mail.ru", "EpamTest185");

                //Fill SendEmailFromMailRU with 1.email receiver 2.Theme of letter 3.letter content and then send email
                MailRuSendMailPageObjects SendMail = new MailRuSendMailPageObjects(driver);
                SendMail.SendEmailFromMailRU("testepammail@yandex.ru", "Test Letter From mail.ru", "Content of test letter");
            }

            // runs once after all tests finished
            [OneTimeTearDown]
            public void Dispose()
            {
                // close down the browser
                //driver.Quit();
                //driver.Dispose();
            }
        }
    }
}