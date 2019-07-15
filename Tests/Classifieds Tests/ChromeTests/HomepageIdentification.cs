using AutomationFramework.Pages.Classifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class HomepageIdentification : FrameworkCore
    {
        [TestCase(Category = "Classifieds (Chrome)", TestName = "Validate Classifieds Homepage")]
        public void Homepage()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();
            seleniumDriver.Url = Classifieds.Homepage.homePageURL;
            
            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Classifieds Homepage - Chrome");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, Classifieds.Homepage.homePageURL);
            SeleniumCommands.AssertEqual(seleniumDriver.Title, Classifieds.Homepage.homePageTitle);

            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
