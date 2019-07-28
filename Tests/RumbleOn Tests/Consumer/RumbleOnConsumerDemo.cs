namespace AutomationFramework.Tests.Consumer.Demo
{
    using NUnit.Framework;

    [Category("RumbleOn Consumer")]
    class ConsumerDemo : Core
    {
        [TestCase(Chrome, TestName = "Validate Consumer Homepage - Chrome")]
        [TestCase(Firefox, TestName = "Validate Consumer Homepage - Firefox")]
        [TestCase(Edge, TestName = "Validate Consumer Homepage - Edge")]
        [Order(1)]
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