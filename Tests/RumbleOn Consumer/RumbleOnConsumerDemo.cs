namespace AutomationFramework.Tests
{
    using Core;
    using NUnit.Framework;
    using Pages.RumbleOnConsumer;

    class ConsumerDemo : BaseTest
    {
        [TestCase(Chrome, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Chrome")]
        [TestCase(Firefox, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Firefox")]
        [TestCase(Edge, Category = "RumbleOn Consumer", TestName = "Validate Consumer Homepage - Edge")]
        public void HomepageChrome(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            Url = RumbleOnConsumer.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Consumer Homepage - " + browser);

            Url.ShouldBe(RumbleOnConsumer.Homepage.homePageURL);
            Title.ShouldBe(RumbleOnConsumer.Homepage.homePageTitle);
        }
    }
}