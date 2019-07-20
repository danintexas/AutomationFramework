namespace AutomationFramework.Tests.Demo_Tests
{
    using Core;
    using NUnit.Framework;
    using Pages.Google;

    class BrowserSupportDemo : BaseTest
    {
        [TestCase(Chrome, Category = "Multiple Browser Demo", TestName = "Chrome Test")]
        [TestCase(Edge, Category = "Multiple Browser Demo", TestName = "Edge Test")]
        [TestCase(Firefox, Category = "Multiple Browser Demo", TestName = "Firefox Test")]
        public void MultipleBrowsers(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            Url = Google.Homepage.homePageURL;
            Url.ShouldBe(Google.Homepage.homePageURL);
            Wait(1);
            ScreenShot("Homepage - " + browser);
        }
    }
}
