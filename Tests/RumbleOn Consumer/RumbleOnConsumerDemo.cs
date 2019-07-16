using AutomationFramework.Pages.RumbleOnConsumer;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class ConsumerDemo : FrameworkCore
    {
        [TestCase(Category = "RumbleOn Consumer (Chrome)", TestName = "Validate Consumer Homepage")]
        public void HomepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();
            seleniumDriver.Url = RumbleOnConsumer.Homepage.homePageURL;
            
            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Consumer Homepage - Chrome");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, RumbleOnConsumer.Homepage.homePageURL);
            SeleniumCommands.AssertEqual(seleniumDriver.Title, RumbleOnConsumer.Homepage.homePageTitle);

            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
