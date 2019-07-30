namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    [Category("Classifieds Smoketest")]
    partial class SmokeTest : Core
    {
        [TestCase(Chrome, TestName = "Validate Classifieds Homepage")]
        [Order(1)]
        public void HomepageVerification(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = JsonCall("RumbleOnClassifieds:Url:Homepage");
            ScreenShot("Classifieds Homepage - " + browser);
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:Homepage"));
            ShouldBe(Title, JsonCall("RumbleOnClassifieds:Homepage:Title"));
        }
    }
}
