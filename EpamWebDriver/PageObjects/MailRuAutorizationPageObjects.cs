using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace EpamWebDriver.PageObjects
{
    class MailRuAutorizationPageObjects
    {
        private IWebDriver driver;

        string MailRuUrl = "https://account.mail.ru/";

        //Page objects For MailRUAutorization 
        private readonly By UsernameField = By.Name("username");
        private readonly By PasswordField = By.Name("password");
        private readonly By NextButton = By.CssSelector("[data-test-id=next-button]");
        //private readonly By NextButton = By.Xpath("//button[@data-test-id ='next-button']");
        //private readonly By NextButton = By.Xpath("//span[text()='Ввести пароль']");
        private readonly By SubmitButton = By.CssSelector("[data-test-id=submit-button]");
        private readonly By PopUpEmptyError = By.CssSelector("[data-test-id=required]");
        private readonly By PopUpInputError = By.CssSelector("[data-test-id=password-input-error]");

        public MailRuAutorizationPageObjects(IWebDriver driver)
        {
            this.driver = driver;
        }


        //Open mail.ru page
        private void GotoMailRU()
        {
            driver.Navigate().GoToUrl(MailRuUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        // Just open window autorization on mail.ru
        public void AutorizationInMailRU()
        {
            GotoMailRU();
        }

        // Make error login input on mail.ru
        public void AutorizationInMailRU(string username)
        {
            GotoMailRU();
            //Fill Username(Login) information
            driver.FindElement(NextButton).Click();
            driver.FindElement(UsernameField).SendKeys(username);

            // Go to the nest step
            driver.FindElement(NextButton, 10).Click();
        }

        // Make full autorization on mail.ru
        public void AutorizationInMailRU(string username, string password) 
        {
            GotoMailRU();
            driver.Manage().Window.Maximize();
            //Fill Username(Login) information
            driver.FindElement(NextButton).Click();
            driver.FindElement(UsernameField).SendKeys(username);

            // Go to the next step
            driver.FindElement(NextButton, 10).Click();

            //Now fill the password
            driver.FindElement(PasswordField).SendKeys(password);
            driver.FindElement(SubmitButton).Click();
        }

        public void AssertPopUpEmptyError()
        {
            Assert.IsTrue(driver.FindElement(PopUpEmptyError, 30).Displayed);
        }

        public void AssertPopUpInputError()
        {
            Assert.IsTrue(driver.FindElement(PopUpInputError, 30).Displayed);
        }

    }
}
