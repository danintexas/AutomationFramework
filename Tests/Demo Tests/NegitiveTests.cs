using AutomationFramework.Pages.Google;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests.Demo
{
    class NegitiveTests : FrameworkCore
    {

        [TestCase(Category = "Negative Test", TestName = "Validate Google Homepage - Chrome - Negative Test")]
        [Order(1)]
        public void HomepageChromeNeg()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();

            seleniumDriver.Url = Google.Homepage.homePageURL;

            SeleniumCommands.AssertNotEqual(seleniumDriver.Url, "This will fail");

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Homepage Negative Test");

            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
