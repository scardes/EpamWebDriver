using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpamWebDriver.PageObjects
{
    class MailRuSendMailPageObjects
    {
        private IWebDriver driver;

        //Page objects For send email from mail.ru [Test, Order(5)]
        private readonly By WriteLetterButton = By.XPath("//a[@title='Написать письмо']");
        private readonly By LetterReciverField = By.XPath("//input[@type='text' and @tabindex='100']");
        private readonly By LetterThemeField = By.XPath("//input[@name='Subject']");
        private readonly By LetterContendField = By.XPath("//div[@role='textbox']/div[1]");
        private readonly By SendButton = By.XPath("//button[@data-test-id='send']");

        private readonly By ReadLetterField = By.XPath("//span[@class='ll-sp__normal']");
        private readonly By ContentOfRecivingMailRu = By.XPath("//*[@id='style_16692022190831093165_BODY']/div/div[1]");
        private readonly By EnterInUserSettingButton = By.XPath("//div[@aria-label='epamtestmail93@mail.ru']");
        private readonly By EnterInPersonalDataButton = By.XPath("//div[text()='Личные данные']");

        public MailRuSendMailPageObjects (IWebDriver driver)
        {
            this.driver = driver;
        }

        // SendEmail_WithSomeInformation_SendSuccess
        public void SendEmailFromMailRU(string email, string theme, string content)
        {
            // Go to the page with sending email
            driver.Manage().Window.Maximize();
            driver.FindElement(WriteLetterButton, 20).Click();

            //Fill a letter content and send email
            driver.FindElement(LetterReciverField, 20).SendKeys(email);
            driver.FindElement(LetterThemeField).SendKeys(theme);
            //Fill content of letter
            driver.FindElement(LetterContendField).SendKeys(content);
            //And send letter
            driver.FindElement(SendButton).Click();
            WebDriverExtensions.WaitSomeInterval(5); 
        }

        public string ReadLetterAndTakeNickName()
        {
            //Enter in Letter
            driver.FindElement(ReadLetterField, 20).Click();

            string reciveContent = driver.FindElement(ContentOfRecivingMailRu).Text;
            return reciveContent;
        }

        public void EnterInSetting()
        {
            driver.FindElement(EnterInUserSettingButton, 20).Click();
            driver.FindElement(EnterInPersonalDataButton, 20).Click();
        }
    }
}
