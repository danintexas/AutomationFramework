namespace AutomationFramework.Tests.Demo
{
    using NUnit.Framework;
    using Pages.Google;

    class QuickTest : Core
    {
        [TestCase(Chrome, Category = "Debug Test", TestName = "Empty Debug Test")]
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