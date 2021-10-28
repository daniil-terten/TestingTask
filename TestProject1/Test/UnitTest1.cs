using NUnit.Framework;
using TestProject1;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using TestProject1.Pages;
using System;
using System.Linq;

namespace TestProject1.Test
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Navigate().GoToUrl("https://monopoly.online/");
        }

        [Test]
        public void Test1()
        {
            var Main = new MainPage(_webDriver);
            var Cargo = Main.ClickCargoSearchButton();
            Cargo.ClickAuctionButton();
            Assert.AreEqual(Cargo.GetCargoListCount(),Cargo.GetCountCargo());
        }
    }
}