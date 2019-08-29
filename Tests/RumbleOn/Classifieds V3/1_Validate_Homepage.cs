namespace AutomationFramework.Tests.Classifieds.V3_Smoke.Page_Validation
{
    using NUnit.Framework;

    [Category("Classifieds Smoketest")]
    partial class ClassifiedsPageValidation : Core
    {
        [TestCase(TestName = "Classifieds Page Validation")]
        [Order(1)]
        [Repeat(1)]
        public void PageValidation()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            Logger(Info, "Starting valadiation test of Classifieds Pages");
            SetUrl = JsonCall("RumbleOnClassifieds:Url:Homepage");
            Wait(1);
            ScreenShot("Classifieds Homepage");
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:Homepage"));
            ShouldBe(Title, JsonCall("RumbleOnClassifieds:Homepage:Title"));
            SetUrl = JsonCall("RumbleOnClassifieds:Url:ViewListings");
            Wait(1);
            ScreenShot("Classifieds ViewListings Page");
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ViewListings"));
            ShouldBe(Title, JsonCall("RumbleOnClassifieds:Homepage:Title"));
        }
    }
}
