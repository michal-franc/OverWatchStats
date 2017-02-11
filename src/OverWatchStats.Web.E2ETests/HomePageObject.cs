using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace OverWatchStats.Web.E2ETests
{
    public class HomePageObject : IDisposable
    {
        private readonly IWebDriver _driver;

        public HomePageObject(Uri webDriverUri)
        {
            var capabilities = DesiredCapabilities.Chrome();
            _driver = new RemoteWebDriver(webDriverUri, capabilities);
        }

        public void PerformSearchFor(string outCode)
        {
            _driver.Navigate().GoToUrl("http://localhost:22222/");

            InputBattleTag(outCode);
            PushSearchButton();
        }

        public bool ValidationMessageVisible()
        {
            return GetMainContent().Contains("Invalid battle-net tag value.");
        }

        public bool ThereIsNoProfileFoundMessage()
        {
            return GetMainContent().Contains("No profile found.");
        }

        // I know that this is very crude :D
        public bool ProfileStatsAreVisible()
        {
            var content = GetMainContent();
            return content.Contains("Gold Medals");
        }

        private string GetMainContent()
        {
            var query = _driver.FindElement(By.Id("main"));
            return query.Text;
        }

        private void InputBattleTag(string battleTag)
        {
            var query = _driver.FindElement(By.Id("battlenettag"));
            query.SendKeys(battleTag);
        }

        private void PushSearchButton()
        {
            var query = _driver.FindElement(By.Id("btnSearch"));
            query.Click();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
