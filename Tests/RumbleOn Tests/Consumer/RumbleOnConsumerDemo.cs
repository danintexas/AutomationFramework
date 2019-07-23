namespace AutomationFramework.Tests
{
    using Core;
    using NUnit.Framework;

    class ConsumerDemo : BaseTest
    {
        [TestCase(Chrome, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Chrome")]
        [TestCase(Firefox, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Firefox")]
        [TestCase(Edge, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Edge")]
        public void HomepageChrome(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            Url = _config["RumbleOnConsumer:Homepage:Url"];

            Wait(2);
            ScreenShot("Consumer Homepage - " + browser);

            Url.ShouldBe(_config["RumbleOnConsumer:Homepage:Url"]);
            Title.ShouldBe(_config["RumbleOnConsumer:Homepage:Title"]);
        }
    }
}