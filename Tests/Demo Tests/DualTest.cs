namespace AutomationFramework.Tests.Demo
{
    using Core;
    using NUnit.Framework;
    using Pages.Google;

    class DualTest : BaseTest
    {
        [TestCase(Chrome, Category = "Dual Test", TestName = "Validate Classifieds Homepage - Chrome")]
        [Order(1)]
        public void ClassifiedsHomePage(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();

            Url = _config["RumbleOnClassifieds:Homepage:Url"];

            Wait(2);
            ScreenShot("Dual Test - Classifieds");

            Url.ShouldBe(_config["RumbleOnClassifieds:Homepage:Url"]);
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