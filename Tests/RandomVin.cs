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
            for (int i = 0; i < 10; i++)
            {
                GenerateAVIN();
                Console.WriteLine(vinUnderTest);
                Console.WriteLine(yearUnderTest);
                Console.WriteLine(makeUnderTest);
                Console.WriteLine(modelUnderTest);
                Console.WriteLine("========================");
            }
        }
        
    }
}