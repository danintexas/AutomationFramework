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
            Console.WriteLine(DatabaseCheck("select Vin from ClsListing where ListingId = 318"));

            GenerateAVIN();
            Console.WriteLine(DatabaseCheck("RumbleOnClassifieds:DatabaseQueries:CheckVINIfAlreadyListed"));
        }
    }
}