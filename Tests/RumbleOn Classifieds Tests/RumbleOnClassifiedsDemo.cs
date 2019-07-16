using AutomationFramework.Pages.RumbleOnClassifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class RumbleOnClassifiedsDemo : FrameworkCore
    {
        [TestCase(Category = "RumbleOn Classifieds (Chrome)", TestName = "Validate Classifieds Homepage")]
        public void Homepage()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();
            seleniumDriver.Url = RumbleOnClassifieds.Homepage.homePageURL;
            
            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Classifieds Homepage - Chrome");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, RumbleOnClassifieds.Homepage.homePageURL);
            SeleniumCommands.AssertEqual(seleniumDriver.Title, RumbleOnClassifieds.Homepage.homePageTitle);

            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
