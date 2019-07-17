namespace AutomationFramework.Tests.Demo
{
    using Core;
    using NUnit.Framework;
    using Pages.Google;

    class NegativeTests : BaseTest
    {
        [TestCase(Category = "Negative Test", TestName = "Validate Google Homepage - Chrome - Negative Test")]
        [Order(1)]
        public void HomepageChromeNeg()
        {
            UseChrome();
            MaximizeBrowser();

            Url = Google.Homepage.homePageURL;

            Url.ShouldNotBe("This will fail");

            Wait(2);
            ScreenShot("Homepage Negative Test");
        }
    }
}