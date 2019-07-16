namespace AutomationFramework.Tests
{
    using Core;
    using NUnit.Framework;
    using Pages.RumbleOnClassifieds;

    class RumbleOnClassifiedsDemo : BaseTest
    {
        [TestCase(Category = "RumbleOn Classifieds (Chrome)", TestName = "Validate Classifieds Homepage")]
        public void Homepage()
        {
            UseChrome();
            MaximizeBrowser();
            Url = RumbleOnClassifieds.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Classifieds Homepage - Chrome");

            Url.ShouldBe(RumbleOnClassifieds.Homepage.homePageURL);
            Title.ShouldBe(RumbleOnClassifieds.Homepage.homePageTitle);
        }
    }
}