﻿using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace AutomationFramework.Tests.Development_Tests
{
    [Category("Dev Tests")]
    partial class DevTest : Core
    {
        [TestCase(TestName = "Check Digit")]
        [Order(99)]

        public void Test()
        {
            for(int i = 0; i <25; i++)
            Console.WriteLine(GenerateAVIN(Car));
        }
        
    }
}