using AutomationFramework.Pages.Consumer;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class ConsumerDemo : FrameworkCore
    {
        [TestCase(Category = "Consumer (Chrome)", TestName = "Validate Consumer Homepage")]
        public void HomepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();
            seleniumDriver.Url = Consumer.Homepage.homePageURL;
            
            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Consumer Homepage - Chrome");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, Consumer.Homepage.homePageURL);
            SeleniumCommands.AssertEqual(seleniumDriver.Title, Consumer.Homepage.homePageTitle);

            SeleniumCommands.CloseQuitBrowsers();
        }
    }
}
