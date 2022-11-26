using EpamWebDriver;
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

                // Make full autorization on mail.ru
                var autorizationMailru = new MailRuAutorizationPageObjects(driver);
                autorizationMailru.AutorizationInMailRU("epamtestmail93@mail.ru", "EpamTest185");
            }

            [Test, Order(1)]
            public void ReadEmail_TakeNewNickInformation_ReadSuccess()
            {
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
            public void EnterInPersonalData_ChangeNickName_SaveSuccess()
            {
                string NickNameFromLetter = "EpamTestLogin";
                
                WebDriverExtensions.WaitSomeInterval(3);

                //Read the letter and take new nick name
                MailRuSendMailPageObjects EnterSettings = new MailRuSendMailPageObjects(driver);
                EnterSettings.EnterInSetting();

                //Set new Nick name and save changes
                MailRuSetPersonalDataPageObjects ChangeNickname = new MailRuSetPersonalDataPageObjects(driver);
                ChangeNickname.WriteNewNickName(NickNameFromLetter);
                ChangeNickname.SavePersonData();
            }

            [Test, Order(3)]
            public void ChangePersonalData_ChangeNickName_ChangeSuccess()
            {
                // Make full autorization on mail.ru
                WebDriverExtensions.WaitSomeInterval(3);
                
                //Read the letter and take new nick name
                MailRuSendMailPageObjects EnterSettings = new MailRuSendMailPageObjects(driver);
                EnterSettings.EnterInSetting();

                MailRuSetPersonalDataPageObjects ChangeNickname = new MailRuSetPersonalDataPageObjects(driver);
                
                //Start UnitTest for Assert New Nick Name
                //Arrange
                string expectedStr = "EpamTestLogin";

                //Act
                string readedStr = ChangeNickname.ReadNewNickName();

                //Assert
                Assert.AreEqual(expectedStr, readedStr);
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