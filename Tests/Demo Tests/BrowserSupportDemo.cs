namespace AutomationFramework.Tests.Demo_Tests
{
    using NUnit.Framework;
    using Pages.Google;

    class BrowserSupportDemo : Core
    {
        [TestCase(Chrome, Category = "Multiple Browser Demo", TestName = "Chrome Test")]
        [TestCase(Edge, Category = "Multiple Browser Demo", TestName = "Edge Test")]
        [TestCase(Firefox, Category = "Multiple Browser Demo", TestName = "Firefox Test")]
        public void MultipleBrowsers(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = Google.Homepage.homePageURL;

            ShouldBe(SetUrl, Google.Homepage.homePageURL);
            ShouldBe(Title, Google.Homepage.homePageTitle);

            Wait(1);

            ScreenShot("Homepage - " + browser);
        }
    }
}
