namespace AutomationFramework.Tests.Demo
{
    using NUnit.Framework;
    using Pages.Google;

    class QuickTest : BaseTest
    {
        [TestCase(Category = "Debug Test", TestName = "Empty Debug Test")]
        [Order(1)]
        public void homepageChrome()
        {
            UseChrome();
            MaximizeBrowser();
            Url = Google.Homepage.homePageURL;
            ScreenShot("Chrome - Homepage");
        }
    }
}