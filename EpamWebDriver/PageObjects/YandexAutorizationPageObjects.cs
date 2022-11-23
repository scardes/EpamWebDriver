using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace EpamWebDriver.PageObjects
{
    class YandexAutorizationPageObjects
    {
        private IWebDriver driver;

        string YandexUrl = "https://passport.yandex.ru/";

        //Page objects For MailRUAutorization 
        private readonly By NextButton = By.CssSelector("[data-test-id=next-button]");
        private readonly By SubmitButton = By.CssSelector("[data-test-id=submit-button]");
        private readonly By UsernameField = By.Name("username");
        private readonly By PasswordField = By.Name("password");

        public YandexAutorizationPageObjects(IWebDriver driver)
        {
            this.driver = driver;
        }


        //Open mail.ru page
        private void YandexMail()
        {
            driver.Navigate().GoToUrl(YandexUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        // Make full autorization on mail.ru
        public void AutorizationInYandexMail(string username, string password)
        {
            YandexMail();
            //Fill Username(Login) information
            driver.FindElement(NextButton).Click();
            driver.FindElement(UsernameField).SendKeys(username);

            // Go to the next step
            driver.FindElement(NextButton, 10).Click();

            //Now fill the password
            driver.FindElement(PasswordField).SendKeys(password);
            driver.FindElement(SubmitButton).Click();
        }
    }
}
