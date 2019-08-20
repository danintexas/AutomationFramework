using NUnit.Framework;
namespace AutomationFramework.Tests.Development_Tests
{
    [Category("Dev Tests")]
    partial class CheckDataBaseTestExample : Core
    {
        [TestCase(TestName = "Check Database")]
        [Order(99)]

        public void Test()
        {
            DatabaseCheck("select Vin from ClsListing where ListingId = 318", "1HD1BXB106Y053007");
        }
    }
}