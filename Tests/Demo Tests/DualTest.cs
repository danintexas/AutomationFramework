namespace AutomationFramework.Tests.Demo
{
    using NUnit.Framework;
    using Pages.Google;

    class DualTest : Core
    {
        [TestCase(Chrome, Category = "Dual Test", TestName = "Validate Classifieds Homepage - Chrome")]
        [Order(1)]
        public void ClassifiedsHomePage(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();

            SetUrl = _config["RumbleOnClassifieds:Homepage:Url"];

            Wait(2);
            ScreenShot("Dual Test - Classifieds");

            ShouldBe(SetUrl, _config["RumbleOnClassifieds:Homepage:Url"]);
        }

        [TestCase(Category = "Dual Test", TestName = "Validate Google Homepage - Chrome")]
        [Order(2)]
        public void GoogleHomePage()
        {
            SetUrl = Google.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Dual Test - Google");

            ShouldBe(SetUrl, Google.Homepage.homePageURL);
        }
    }
}