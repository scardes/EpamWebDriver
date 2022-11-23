using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace EpamWebDriver.PageObjects
{
    class YandexMailMainPageObjects
    {
        private IWebDriver driver;

        string YandexUrl = "https://mail.yandex.ru/";

        //Page objects For MailRUAutorization 
        private readonly By NotReadStatus = By.XPath("//span[@class='mail-Icon mail-Icon-Read js-read toggles-svgicon-on-active is-active']");
        private readonly By TitleOfRecivingMail = By.XPath("//span[@title='epamtestmail93@mail.ru']");
        private readonly By ContentOfRecivingMail = By.XPath("//div[text()='Content of test letter']");
        private readonly By SendResponseLetterButton = By.XPath("//div[@role='button' and contains(@title,'Ответить')]");
        private readonly By ContentOfResponseLetterField = By.XPath("//div[@role='textbox']");
        private readonly By ResponseLetterSendButton = By.XPath("//button[@type = 'button' and contains(@class,'Button2 Button2_pin_circle-circle Button2_view_default Button2_size_l')]");

        public YandexMailMainPageObjects(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CheckNewRecieveMail()
        {
            Assert.IsTrue(driver.FindElement(NotReadStatus).Displayed);
            Assert.IsTrue(driver.FindElement(TitleOfRecivingMail).Displayed);
        }

        public void ReadNewMail()
        {
            driver.FindElement(TitleOfRecivingMail).Click();
        }

        public string ReadContentOfNewMail()
        {
            string reciveContent = driver.FindElement(ContentOfRecivingMail).Text;
            return reciveContent;
        }

        public void ResponceLetterWithName(string newLogin)
        {
            driver.FindElement(SendResponseLetterButton).Click();
            driver.FindElement(ContentOfResponseLetterField, 10).SendKeys(newLogin);
            driver.FindElement(ResponseLetterSendButton, 10).Click();
        }
    }
}
