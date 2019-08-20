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
            GenerateAVIN(Motorcycle);
            Console.WriteLine(vinUnderTest);
            Console.WriteLine(yearUnderTest);
            Console.WriteLine(makeUnderTest);
            Console.WriteLine(modelUnderTest);
            Console.WriteLine("========================");
            trimUnderTest = "";
            GenerateAVIN(Car);
            Console.WriteLine(vinUnderTest);
            Console.WriteLine(yearUnderTest);
            Console.WriteLine(makeUnderTest);
            Console.WriteLine(modelUnderTest);
            Console.WriteLine("========================");
            trimUnderTest = "";
            GenerateAVIN(Truck);
            Console.WriteLine(vinUnderTest);
            Console.WriteLine(yearUnderTest);
            Console.WriteLine(makeUnderTest);
            Console.WriteLine(modelUnderTest);
            Console.WriteLine("========================");
            trimUnderTest = "";
            GenerateAVIN(Offroad);
            Console.WriteLine(vinUnderTest);
            Console.WriteLine(yearUnderTest);
            Console.WriteLine(makeUnderTest);
            Console.WriteLine(modelUnderTest);
            Console.WriteLine("========================");
        }
        
    }
}