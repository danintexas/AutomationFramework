namespace AutomationFramework.Tests.Classifieds.Demo
{
    using NUnit.Framework;

    [Category("RumbleOn Classifieds")]
    class RumbleOnClassifiedsDemo : Core
    {
        [TestCase(Chrome, TestName = "Validate Classifieds Homepage - Chrome")]
        [TestCase(Firefox, TestName = "Validate Classifieds Homepage - Firefox")]
        [TestCase(Edge, TestName = "Validate Classifieds Homepage - Edge")]
        [Order(1)]
        public void Homepage(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = JsonCall("RumbleOnClassifieds:Homepage:Url");

            Wait(1);
            ScreenShot("Classifieds Homepage - " + browser);

            ShouldBe(SetUrl, _config["RumbleOnClassifieds:Homepage:Url"]);
            ShouldBe(Title, _config["RumbleOnClassifieds:Homepage:Title"]);
        }
    }
}