using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Wait = OpenQA.Selenium.Support.UI;

namespace ChatAppAutomation.Pages
{
    public class BasePageClass
    {
        private readonly IWebDriver _driver;
        public Wait.WebDriverWait wait;

        public BasePageClass(IWebDriver driver)
        {
            _driver = driver;
            wait = new Wait.WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public bool IsAt(By by)
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
