namespace AutomationFramework.Tests.Demo_Tests
{
    using NUnit.Framework;
    using Pages.Google;

    class BrowserSupportDemo : BaseTest
    {
        [TestCase("chrome", Category = "Multiple Browser Demo", TestName = "Chrome Test")]
        [TestCase("edge", Category = "Multiple Browser Demo", TestName = "Edge Test")]
        [TestCase("firefox", Category = "Multiple Browser Demo", TestName = "Firefox Test")]
        public void MultipleBrowsers(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            Url = Google.Homepage.homePageURL;
            Wait(1);
            ScreenShot("Homepage - " + browser);
        }
    }
}
