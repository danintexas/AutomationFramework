namespace AutomationFramework.Tests.LocatorCorrection
{
    using NUnit.Framework;
    using System;

    [Category("Dev Test")] 
    partial class LocatorCorrection : Core 
    {
        [TestCase(TestName = "Locator Correction")] 
        [Order(1)] 
        public void DevTest() 
        {
            var thing = LocatorCleaner(JsonCall("FrameworkConfiguration:TestThing"));
            var thing2 = LocatorCleaner(JsonCall("FrameworkConfiguration:TestThing2"));
            var thing3 = LocatorCleaner(JsonCall("FrameworkConfiguration:TestThing3"));

            Console.WriteLine("Thing1 Locator: '" + thing.Item1 + "' Thing1 Type: '" + thing.Item2 + "'");
            Console.WriteLine("Thing2 Locator: '" + thing2.Item1 + "' Thing1 Type: '" + thing2.Item2 + "'");
            Console.WriteLine("Thing3 Locator: '" + thing3.Item1 + "' Thing1 Type: '" + thing3.Item2 + "'");
        }
    }
}
