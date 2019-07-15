using AutomationFramework.Pages.Classifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests.Demo_Tests
{
    class NegitiveTests : FrameworkCore
    {

        [TestCase(Category = "Negative Test", TestName = "Validate Homepage - Chrome - Negative Test")]
        [Order(1)]
        public void HomepageChromeNeg()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();

            seleniumDriver.Url = Classifieds.Homepage.homePageURL;

            SeleniumCommands.AssertNotEqual(seleniumDriver.Url, "This will fail");
            SeleniumCommands.AssertEqual(seleniumDriver.Title, Classifieds.Homepage.homePageTitle);

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Homepage Negative Test");

            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
