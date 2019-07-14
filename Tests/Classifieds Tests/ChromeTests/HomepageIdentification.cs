using AutomationFramework.Pages.Classifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class HomepageIdentification : FrameworkCore
    {
        [TestCase(Category = "Classifieds (Chrome)", TestName = "Validate Classifieds Homepage")]
        public void homepage()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();
            driver.Url = Classifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Classifieds Homepage - Chrome");

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);

            SeleniumCommands.closeQuitBrowsers();
        }
    }
}
