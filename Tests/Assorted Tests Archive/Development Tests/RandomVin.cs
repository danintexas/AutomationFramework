using NUnit.Framework;
using System;

namespace AutomationFramework.Tests.Development_Tests
{
    [Category("Dev Tests")]
    partial class RandomVin : Core
    {
        [TestCase(TestName = "Random VIN")]
        [Order(99)]

        public void Test()
        {
            for (int i = 1; i < 10; i++)
            {
                GenerateAVIN();
            }
        }
        
    }
}