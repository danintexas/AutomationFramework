using AutomationFramework.Pages.Classifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests.Demo_Tests
{
    class NegitiveTests : FrameworkCore
    {

        [TestCase(Category = "Negative Test", TestName = "Validate Homepage - Chrome - Negative Test")]
        [Order(1)]
        public void homepageChromeNeg()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            Assert.AreNotEqual(driver.Url, "This will fail");
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Homepage Negative Test");

            SeleniumCommands.closeQuitBrowsers();
        }
    }
}
