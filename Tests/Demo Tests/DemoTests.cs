namespace AutomationFramework.Tests.Demo
{
    using Core;
    using NUnit.Framework;
    using Pages.RumbleOnClassifieds;

    class DemoTests : BaseTest
    {
        [TestCase(Category = "Demo", TestName = "Validate Homepage - Chrome")]
        [Order(1)]
        public void HomepageChrome()
        {
            UseChrome();
            MaximizeBrowser();

            Url = RumbleOnClassifieds.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Chrome - Homepage");

            Url.ShouldBe(RumbleOnClassifieds.Homepage.homePageURL);
            Title.ShouldBe(RumbleOnClassifieds.Homepage.homePageTitle);
        }

        [TestCase(Category = "Demo", TestName = "Validate HomePage - Firefox")]
        [Order(2)]
        public void homepageFirefox()
        {
            UseFirefox();
            MaximizeBrowser();

            Url = RumbleOnClassifieds.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Firefox - Homepage");

            Url.ShouldBe(RumbleOnClassifieds.Homepage.homePageURL);
            Title.ShouldBe(RumbleOnClassifieds.Homepage.homePageTitle);
        }

        [TestCase(Category = "Demo", TestName = "Validate HomePage - Edge")]
        [Order(3)]
        public void homepageEdge()
        {
            UseEdge();
            MaximizeBrowser();

            Url = RumbleOnClassifieds.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Edge - Homepage");

            Url.ShouldBe(RumbleOnClassifieds.Homepage.homePageURL);
            Title.ShouldBe(RumbleOnClassifieds.Homepage.homePageTitle);
        }
    }
}