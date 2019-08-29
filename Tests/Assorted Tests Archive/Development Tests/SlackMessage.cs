namespace AutomationFramework.Tests.DevTests
{
    using NUnit.Framework;

    [Category("Dev Tests")] 
    partial class SlackMessageTest : Core
    {
        [TestCase(TestName = "Slack message")] 
        [Order(1)] 
        [Repeat(1)]
        public void SlackMessage() 
        {
            Logger(Info, "Starting Slack Message Test");
            Wait(1);
            ShouldBe("a", "b");
        }
    }
}