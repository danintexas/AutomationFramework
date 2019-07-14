using AutomationFramework.Pages.Consumer;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class ConsumerDemo : FrameworkCore
    {
        [TestCase(Category = "Consumer (Chrome)", TestName = "Validate Consumer Homepage")]
        public void homepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();
            driver.Url = Consumer.Homepage.homePageURL;
            
            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Consumer Homepage - Chrome");

            Assert.AreEqual(driver.Url, Consumer.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Consumer.Homepage.homePageTitle);

            SeleniumCommands.closeQuitBrowsers();
        }
    }
}
