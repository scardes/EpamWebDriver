using EpamWebDriver.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

/// <summary>
/// Selenium test project with two emails 
/// send email from https://account.mail.ru/
/// </summary>
namespace ExampleService.Tests
{
    [TestFixture]
    public class YandexMail_RecieveAndSendMail
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
            public void Authorization_OnYandexMail_AuthorizationSuccess()
            {
                YandexAutorizationPageObjects EnterInYandex = new YandexAutorizationPageObjects(driver);
                EnterInYandex.AutorizationInYandexMail("testepammail@yandex.ru", "EpamTest185");
            }

            [Test, Order(2)]
            public void MailStatusCheck_OnYandexMail_NotReadSuccess()
            {

            }


            // Runs once after all tests finished
            [OneTimeTearDown]
            public void Dispose()
            {
                // Close down the browser
                //driver.Quit();
                //driver.Dispose();
            }
        }
    }
}