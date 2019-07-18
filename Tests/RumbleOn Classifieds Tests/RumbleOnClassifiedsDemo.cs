namespace AutomationFramework.Tests
{
    using Core;
    using NUnit.Framework;
    using Pages.RumbleOnClassifieds;

    class RumbleOnClassifiedsDemo : BaseTest
    {
        [TestCase("chrome", Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Chrome")] [Order(1)]
        [TestCase("firefox", Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Firefox")]
        [TestCase("edge", Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Edge")]
        public void Homepage(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            Url = RumbleOnClassifieds.Homepage.homePageURL;

            Wait(1);
            ScreenShot("Classifieds Homepage - " + browser);

            Url.ShouldBe(RumbleOnClassifieds.Homepage.homePageURL);
            Title.ShouldBe(RumbleOnClassifieds.Homepage.homePageTitle);
        }
    }
}