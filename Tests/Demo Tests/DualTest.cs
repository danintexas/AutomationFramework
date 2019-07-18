namespace AutomationFramework.Tests.Demo
{
    using Core;
    using NUnit.Framework;
    using Pages.Google;
    using Pages.RumbleOnClassifieds;

    class DualTest : BaseTest
    {
        [TestCase(Category = "Dual Test", TestName = "Validate Classifieds Homepage - Chrome")]
        [Order(1)]
        public void ClassifiedsHomePage()
        {
            UseBrowser("chrome");
            MaximizeBrowser();

            Url = RumbleOnClassifieds.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Dual Test - Classifieds");

            Url.ShouldBe(RumbleOnClassifieds.Homepage.homePageURL);
        }

        [TestCase(Category = "Dual Test", TestName = "Validate Google Homepage - Chrome")]
        [Order(2)]
        public void GoogleHomePage()
        {
            Url = Google.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Dual Test - Google");

            Url.ShouldBe(Google.Homepage.homePageURL);
        }
    }
}