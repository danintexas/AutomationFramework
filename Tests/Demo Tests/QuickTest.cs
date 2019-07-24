namespace AutomationFramework.Tests.Demo
{
    using NUnit.Framework;
    using Pages.Google;

    class QuickTest : BaseTest
    {
        [TestCase(Chrome, Category = "Debug Test", TestName = "Empty Debug Test")]
        [Order(1)]
        public void HomepageChrome(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            Url = Google.Homepage.homePageURL;
            ScreenShot("Homepage - " + browser);
        }
    }
}