using AutomationFramework.Pages.Google;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests.Demo
{
    class QuickTest : FrameworkCore
    {
        [TestCase(Category = "Debug Test", TestName = "Empty Debug Test")]
        [Order(1)]

        public void homepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();
            seleniumDriver.Url = Google.Homepage.homePageURL;
            SeleniumCommands.ScreenShot("Chrome - Homepage");
            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
