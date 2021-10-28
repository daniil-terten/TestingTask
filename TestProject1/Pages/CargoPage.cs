using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support;
using TestProject1.WebDriverExtension;

namespace TestProject1.Pages
{
    class CargoPage
    {
        private IWebDriver _webDriver;
        private readonly By _auctionButton = By.XPath("//label[normalize-space() = 'Аукцион']");
        private readonly By _countCargo = By.XPath("//div[@class = 'fqwty63 f19fmarb']");
        private readonly By _listCargo = By.XPath("//tr[@data-element-id = 'cargo-table-row']");
        private readonly By _pagesCount = By.XPath("//span[contains(@class , 'f1u04dns')]");
        private readonly By _load = By.XPath("//table[@class = 'ftbij2m']");
        private readonly By _afterLoad = By.XPath("//div[@class = 'fcdpsbx']");


        public CargoPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void ClickAuctionButton()
        {
            IWebElement p = WebDriverExtensions.FindElement(_webDriver, _auctionButton, 7);
            p.Click();
            Loading();
        }

        public int GetCountCargo()
        {
            IWebElement p = WebDriverExtensions.FindElement(_webDriver, _countCargo, 7);
            string a = p.Text;
            int value;
            int.TryParse(string.Join("", a.Where(c => char.IsDigit(c))), out value);
            return value;
        }

        public int GetCargoListCount()
        {
            List<IWebElement> pagesList = _webDriver.FindElements(_pagesCount).ToList();
            int pagesCount = int.Parse(pagesList.Last().Text);
            List<IWebElement> CargoList;

            if (pagesCount > 1)
            {
                pagesList.Last().Click();
                Loading();
                CargoList = _webDriver.FindElements(_listCargo).ToList();
                return 50 * (pagesCount - 1) + CargoList.Count;
            }
            else
            {
                CargoList = _webDriver.FindElements(_listCargo).ToList();
                return CargoList.Count;
            }
        }

        public void Loading()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromMilliseconds(10);
            wait.Until(d => {
                try
                {
                    return !d.FindElement(_load).Displayed;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
            wait.Until(d => d.FindElement(_afterLoad));
        }
    }
}
