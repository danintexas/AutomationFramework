namespace AutomationFramework.Tests.Debug
{
    using NUnit.Framework;
    using Pages.Google;

    [Category("Debug Test")]
    class QuickTest : Core
    {
        [TestCase(Chrome, TestName = "Empty Debug Test")]
        [Order(1)]
        public void HomepageChrome(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = Google.Homepage.homePageURL;
            ScreenShot("Homepage - " + browser);
        }
    }
}