namespace AutomationFramework.Tests
{
    using NUnit.Framework;

    class RumbleOnClassifiedsDemo : Core
    {
        [TestCase(Chrome, Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Chrome")]
        [Order(1)]
        [TestCase(Firefox, Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Firefox")]
        [TestCase(Edge, Category = "RumbleOn Classifieds", TestName = "Validate Classifieds Homepage - Edge")]
        public void Homepage(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = _config["RumbleOnClassifieds:Homepage:Url"];

            Wait(1);
            ScreenShot("Classifieds Homepage - " + browser);

            ShouldBe(SetUrl, _config["RumbleOnClassifieds:Homepage:Url"]);
            ShouldBe(Title, _config["RumbleOnClassifieds:Homepage:Title"]);
        }
    }
}