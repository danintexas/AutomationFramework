using NUnit.Framework;
using System;

namespace AutomationFramework.Tests.Development_Tests
{
    [Category("Dev Tests")]
    partial class SmokeTest : Core
    {
        [TestCase(TestName = "Check Digit")]
        [Order(99)]

        public void Test()
        {
            //Console.WriteLine(GenerateAVIN(Motorcycle));

            
        }
        
    }
}