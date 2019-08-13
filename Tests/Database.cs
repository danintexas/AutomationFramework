using NUnit.Framework;
namespace AutomationFramework.Tests.Development_Tests
{
    [Category("Dev Tests")]
    partial class SmokeTest : Core
    {
        [TestCase(TestName = "Check Database")]
        [Order(99)]

        public void Test()
        {
            
        }
    }
}