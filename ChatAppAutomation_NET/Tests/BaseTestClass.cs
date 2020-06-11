using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ChatAppAutomation.Tests
{
    public class BaseTestClass
    {
        //This object will contain both drivers so we can 
        public IList<IWebDriver> drivers = new List<IWebDriver>();

        [TearDown]
        public void TearDown()
        {
            foreach (var driver in drivers)
            {
                driver.Quit();
            }
        }
    }
}