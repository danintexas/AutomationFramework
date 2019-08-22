using NUnit.Framework;
using System;

namespace AutomationFramework.Tests.Development_Tests
{
    [Category("Dev Tests")]
    partial class CheckDataBaseTestExample : Core
    {
        [TestCase(TestName = "Check Database")]
        [Order(99)]

        public void Test()
        {
            GenerateAVIN();
            //Console.WriteLine(DatabaseCheck("RumbleOnClassifieds:DatabaseQueries:CheckVINIfAlreadyListed"));
            Console.WriteLine(JsonCall("RumbleOnClassifieds:DatabaseQueries:CheckVINIfAlreadyListed"));
            Console.WriteLine($"select count(ListingStatusId) from clslisting where vin = '{vinUnderTest}' and(ListingStatusId = 2 OR ListingStatusId = 9 OR ListingStatusId = 8)");
        }
    }
}