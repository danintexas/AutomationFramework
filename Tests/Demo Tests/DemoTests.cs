using AutomationFramework.Pages.RumbleOnClassifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests.Demo
{
    class DemoTests : FrameworkCore
    {
        [TestCase (Category = "Demo", TestName = "Validate Homepage - Chrome")][Order(1)]

        public void HomepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();

            seleniumDriver.Url = RumbleOnClassifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Chrome - Homepage");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, RumbleOnClassifieds.Homepage.homePageURL);
            SeleniumCommands.AssertEqual(seleniumDriver.Title, RumbleOnClassifieds.Homepage.homePageTitle);

            SeleniumCommands.CloseQuitBrowsers();
        }
 
        [TestCase(Category = "Demo", TestName = "Validate HomePage - Firefox")][Order(2)]
        public void homepageFirefox()
        {
            SeleniumCommands.SetBrowser("firefox");
            SeleniumCommands.MaximizeBrowser();

            seleniumDriver.Url = RumbleOnClassifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Firefox - Homepage");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, RumbleOnClassifieds.Homepage.homePageURL);
            SeleniumCommands.AssertEqual(seleniumDriver.Title, RumbleOnClassifieds.Homepage.homePageTitle);

            SeleniumCommands.CloseQuitBrowsers();
        }

        [TestCase(Category = "Demo", TestName = "Validate HomePage - Edge")][Order(3)]
        public void homepageEdge()
        {
            SeleniumCommands.SetBrowser("edge");
            SeleniumCommands.MaximizeBrowser();

            seleniumDriver.Url = RumbleOnClassifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Edge - Homepage");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, RumbleOnClassifieds.Homepage.homePageURL);
            SeleniumCommands.AssertEqual(seleniumDriver.Title, RumbleOnClassifieds.Homepage.homePageTitle);

            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
