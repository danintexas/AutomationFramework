namespace AutomationFramework.Tests
{
    using Core;
    using NUnit.Framework;
    using Pages.RumbleOnConsumer;

    class ConsumerDemo : BaseTest
    {
        [TestCase(Category = "RumbleOn Consumer (Chrome)", TestName = "Validate Consumer Homepage")]
        public void HomepageChrome()
        {
            UseChrome();
            MaximizeBrowser();
            Url = RumbleOnConsumer.Homepage.homePageURL;

            Wait(2);
            ScreenShot("Consumer Homepage - Chrome");

            Url.ShouldBe(RumbleOnConsumer.Homepage.homePageURL);
            Title.ShouldBe(RumbleOnConsumer.Homepage.homePageTitle);
        }
    }
}