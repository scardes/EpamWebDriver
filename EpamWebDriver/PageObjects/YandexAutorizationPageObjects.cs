using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace EpamWebDriver.PageObjects
{
    class YandexAutorizationPageObjects
    {
        private IWebDriver driver;

        string YandexUrl = "https://mail.yandex.ru/";

        //Page objects For MailRUAutorization 
        private readonly By InputInMailButton = By.XPath("//button[@type='button']");
        private readonly By LoginField = By.XPath("//input[@name='login']");
        private readonly By SubmitButton = By.XPath("//button[@type='submit']");
        private readonly By PasswordField = By.XPath("//input[@type='password']");
        private readonly By EnterButton = By.XPath("//button[@type='submit']");
        
        // private readonly By PasswordField = By.Name("password");

        public YandexAutorizationPageObjects(IWebDriver driver)
        {
            this.driver = driver;
        }


        // Make full autorization on mail.ru
        public void AutorizationInYandexMail(string login, string password)
        {
            // Go to Yandex.mail page
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(YandexUrl);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //
            driver.FindElement(InputInMailButton, 20).Click();
            driver.FindElement(LoginField, 10).SendKeys(login);
            driver.FindElement(SubmitButton, 30).Click();

            //Fill password information
            WebDriverExtensions.WaitSomeInterval(3);
            driver.FindElement(PasswordField, 30).SendKeys(password);

            // Go to the next step
            driver.FindElement(EnterButton, 10).Click();
        }
    }
}
