using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace EpamWebDriver.PageObjects
{
    class MailRuSetPersonalDataPageObjects
    {
        private IWebDriver driver;

        //Page objects For MailRu Set Personal Data
        private readonly By NickNameField = By.XPath("//input[@id='nickname']");
        private readonly By SaveDataButton = By.XPath("//button[@data-test-id='save-button']");

        public MailRuSetPersonalDataPageObjects(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WriteNewNickName(string newNickName)
        {
            driver.FindElement(NickNameField).Clear();
            driver.FindElement(NickNameField).SendKeys(newNickName);
        }

        public string ReadNewNickName()
        {
            string reciveContent = driver.FindElement(NickNameField).GetAttribute("value");
            return reciveContent;
        }

        public void SavePersonData()
        {
            driver.FindElement(SaveDataButton, 20).Click();
        }

    }
}
