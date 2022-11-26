using EpamWebDriver.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

/// <summary>
/// Selenium test project with two emails 
/// second email is https://mail.yandex.ru/
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

                //Autorization on Yandex mail
                YandexAutorizationPageObjects EnterInYandex = new YandexAutorizationPageObjects(driver);
                EnterInYandex.AutorizationInYandexMail("testepammail@yandex.ru", "EpamTest185");
            }

            [Test, Order(1)]
            public void Authorization_OnYandexMail_AuthorizationSuccess()
            {
                //Autorization on Yandex mail
                YandexAutorizationPageObjects EnterInYandex = new YandexAutorizationPageObjects(driver);
                EnterInYandex.AutorizationInYandexMail("testepammail@yandex.ru", "EpamTest185");
            }

            [Test, Order(2)]
            public void MailStatusCheck_OnYandexMail_NotReadSuccess()
            {
                //Chech the letter is not read yet and have correct sender
                YandexMailMainPageObjects YandexMain = new YandexMailMainPageObjects(driver);
                YandexMain.CheckNewRecieveMail();
            }

            [Test, Order(3)]
            public void ReadNewMail_AssertContent_CheckSuccess()
            {
                //Read the letter and check mail content
                YandexMailMainPageObjects YandexMain = new YandexMailMainPageObjects(driver);
                YandexMain.ReadNewMail();

                //Start UnitTest for Assert of Mail content
                //Arrange
                string expectedStr = "Content of test letter";

                //Act
                string readedStr = YandexMain.ReadContentOfNewMail();

                //Assert
                Assert.AreEqual(expectedStr, readedStr);
            }

            [Test, Order(4)]
            public void ResponseToMail_WithNewLogin_SendSuccess()
            {
                //Read the letter and check mail content
                YandexMailMainPageObjects YandexMain = new YandexMailMainPageObjects(driver);
                YandexMain.ReadNewMail();

                YandexMain.ResponceLetterWithName("EpamTestLogin");
            }

            // Runs once after all tests finished
            [OneTimeTearDown]
            public void Dispose()
            {
                // Close down the browser
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}