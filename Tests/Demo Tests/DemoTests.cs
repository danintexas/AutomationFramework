using AutomationFramework.Pages.Classifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class DemoTests : FrameworkCore
    {
        [TestCase (Category = "Demo", TestName = "Validate Homepage - Chrome")][Order(1)]

        public void homepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Chrome - Homepage");

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);

            SeleniumCommands.closeQuitBrowsers();
        }
 
        [TestCase(Category = "Demo", TestName = "Validate HomePage - Firefox")][Order(2)]
        public void homepageFirefox()
        {
            SeleniumCommands.SetBrowser("firefox");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Firefox - Homepage");

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);

            SeleniumCommands.closeQuitBrowsers();
        }

        [TestCase(Category = "Demo", TestName = "Validate HomePage - Edge")][Order(3)]
        public void homepageEdge()
        {
            SeleniumCommands.SetBrowser("edge");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Edge - Homepage");

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);

            SeleniumCommands.closeQuitBrowsers();
        }
    }
}
