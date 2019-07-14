// Kindergarden Automation Framework
// Main Creator: Daniel Gail
// For RumbleOn

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Selenium;
using System;
using System.Collections;
using System.IO;

namespace AutomationFramework
{
    public class FrameworkCore
    {

        // Prep the Selenium driver for usage
        public static IWebDriver driver;

        // Needed for reporting
        protected ExtentReports _extent;
        protected ExtentTest _test;

        // Run before every suite
        [OneTimeSetUp]
        protected void Setup()
        {
            SeleniumCommands.LogCleaner();

            DateTime date = DateTime.Today;
            var dir = @"c:\Automation Logs\" + date.ToString("MM.dd.yyyy")  + @"\Reports\";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var fileName = this.GetType().ToString() + ".html";
            var htmlReporter = new ExtentHtmlReporter(dir + fileName);

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        // Run before every test
        [SetUp]
        public void SetUp()
        {
            _test = _extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        // Run after every test
        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                    ? ""
                    : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            _test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            _extent.Flush();
        }

        // Run after every suite
        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            _extent.Flush();
        }
    }
}

