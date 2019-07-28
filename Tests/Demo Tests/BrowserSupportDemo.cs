namespace AutomationFramework.Tests.FrameworkDemo
{
    using NUnit.Framework;
    using Pages.Google;

    [Category("Multiple Browser Demo")]
    class BrowserSupportDemo : Core
    {
        [TestCase(Chrome, TestName = "Chrome Test")]
        [TestCase(Edge, TestName = "Edge Test")]
        [TestCase(Firefox, TestName = "Firefox Test")]
       
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
