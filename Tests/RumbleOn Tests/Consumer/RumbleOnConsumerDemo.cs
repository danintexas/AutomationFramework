namespace AutomationFramework.Tests
{
    using NUnit.Framework;

    class ConsumerDemo : Core
    {
        [TestCase(Chrome, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Chrome")]
        [TestCase(Firefox, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Firefox")]
        [TestCase(Edge, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Edge")]
        public void HomepageChrome(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = _config["RumbleOnConsumer:Homepage:Url"];

            Wait(2);
            ScreenShot("Consumer Homepage - " + browser);

            ShouldBe(SetUrl, _config["RumbleOnConsumer:Homepage:Url"]);
            ShouldBe(Title, _config["RumbleOnConsumer:Homepage:Title"]);
        }
    }
}