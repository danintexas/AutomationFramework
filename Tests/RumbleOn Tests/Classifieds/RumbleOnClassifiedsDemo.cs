namespace AutomationFramework.Tests
{
    using AutomationFramework.Pages.RumbleOn.Classifieds;
    using Core;
    using NUnit.Framework;

    class RumbleOnClassifiedsDemo : BaseTest
    {
        [TestCase(Chrome, Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Chrome")]
        [Order(1)]
        [TestCase(Firefox, Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Firefox")]
        [TestCase(Edge, Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Edge")]
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