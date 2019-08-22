namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    [Category("Classifieds Smoketest")]
    partial class SmokeTest : Core
    {
        [TestCase(TestName = "Validate Classifieds Homepage")]
        [Order(1)]
        public void HomepageVerification()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            Logger(Info, "Starting valadiation test of Classifieds Homepage");
            SetUrl = JsonCall("RumbleOnClassifieds:Url:Homepage");
            ScreenShot("Classifieds Homepage");
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:Homepage"));
            ShouldBe(Title, JsonCall("RumbleOnClassifieds:Homepage:Title"));
        }
    }
}
