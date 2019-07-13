using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Selenium;
using System;

namespace AutomationFramework
{
    public class FrameworkCore
    {
        // Prep the Selenium driver for usage
        public static IWebDriver driver;

        // Print out results of the test
        public void ResultPrint()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.Name);
            Console.WriteLine(TestContext.CurrentContext.Result.Outcome.Status);
            Console.WriteLine(TestContext.CurrentContext.Result.Message);
        }

        // Place holder in case I need this to run before every test
        [SetUp]
        public void SetUp()
        {

        }

        // Section that is executed after every test
        [TearDown]
        public void TearDown()
        {
            // These are seperate for now for future features
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                ResultPrint();          
            }

            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                ResultPrint();
            }

            else
            {
                ResultPrint();
            }
        }
    }
}
