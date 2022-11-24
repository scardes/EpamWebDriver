using EpamWebDriver.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

/// <summary>
/// Selenium test project with two emails 
/// first email on https://account.mail.ru/
/// </summary>
namespace ExampleService.Tests
{
    [TestFixture]
    public class ChangeNickNameOnMailRu
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
            public void ReadEmail_TekeNewNickInformation_ReadSuccess()
            {
                // Make full autorization on mail.ru
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("epamtestmail93@mail.ru", "EpamTest185");

                //Read the letter and take new nick name
                MailRuSendMailPageObjects ReadMail = new MailRuSendMailPageObjects(driver);

                //Start UnitTest for Assert of Mail content 
                //Arrange
                string expectedStr = "EpamTestLogin";

                //Act
                string readedStr = ReadMail.ReadLetterAndTakeNickName();

                //Assert
                Assert.AreEqual(expectedStr, readedStr);
            }

            [Test, Order(2)]
            public void EnterInPersonalData_ChangeNickName_ChangeSuccess()
            {
                string NickNaneFromLetter = "EpamTestLogin";

                // Make full autorization on mail.ru
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("epamtestmail93@mail.ru", "EpamTest185");

                //Read the letter and take new nick name
                MailRuSendMailPageObjects EnterSettings = new MailRuSendMailPageObjects(driver);
                EnterSettings.EnterInSetting();

                MailRuSetPersonalDataPageObjects ChangeNickname = new MailRuSetPersonalDataPageObjects(driver);
                ChangeNickname.WriteNewNickName(NickNaneFromLetter);
                ChangeNickname.SavePersonData();

                ////Start UnitTest for Assert of Mail content 
                ////Arrange
                //string expectedStr = "EpamTestLogin";

                ////Act
                //string readedStr = ReadMail.ReadLetterAndTakeNickName();

                ////Assert
                //Assert.AreEqual(expectedStr, readedStr);
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