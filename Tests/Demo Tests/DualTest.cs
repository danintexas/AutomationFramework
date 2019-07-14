using AutomationFramework.Pages.Classifieds;
using AutomationFramework.Pages.Google;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests.Demo_Tests
{
    class DualTest : FrameworkCore
    {
        [TestCase(Category = "Dual Test", TestName = "Validate Classifieds Homepage - Chrome")]
        [Order(1)]
        public void classifiedsHomePage()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Dual Test - Classifieds");

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
        }

        [TestCase(Category = "Dual Test", TestName = "Validate Google Homepage - Chrome")]
        [Order(2)]
        public void googleHomePage()
        {
            driver.Url = Google.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Dual Test - Google");

            Assert.AreEqual(driver.Url, Google.Homepage.homePageURL);

            SeleniumCommands.closeQuitBrowsers();
        }

    }
}
