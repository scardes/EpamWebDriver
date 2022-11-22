using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpamWebDriver.PageObjects
{
    class MailRuAutorizationPageObjects
    {
        private IWebDriver driver;

        string MailRuUrl = "https://account.mail.ru/";

        //Page objects For MailRUAutorization 
        private readonly By NextButton = By.CssSelector("[data-test-id=next-button]");
        private readonly By SubmitButton = By.CssSelector("[data-test-id=submit-button]");
        private readonly By UsernameField = By.Name("username");
        private readonly By PasswordField = By.Name("password");

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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(NextButton).Click();
        }

        // Make full autorization on mail.ru
        public void AutorizationInMailRU(string username, string password) 
        {
            GotoMailRU();
            //Fill Username(Login) information
            driver.FindElement(NextButton).Click();
            driver.FindElement(UsernameField).SendKeys(username);

            // Go to the nest step
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(NextButton).Click();

            //Now fill the password
            driver.FindElement(PasswordField).SendKeys(password);
            driver.FindElement(SubmitButton).Click();
        }
    }
}
