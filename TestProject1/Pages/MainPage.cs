using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support;
using TestProject1.WebDriverExtension;

namespace TestProject1.Pages
{
    class MainPage
    {
        private IWebDriver _webDriver;
        private readonly By _cargoSearchButton = By.XPath("//span[contains(@class, 'f15i6y93')]");

        public MainPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public CargoPage ClickCargoSearchButton()
        {
            IWebElement p = WebDriverExtensions.FindElement(_webDriver, _cargoSearchButton, 5);
            p.Click();
            return new CargoPage(_webDriver);
        }
    }
}
