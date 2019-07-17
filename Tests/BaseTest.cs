namespace AutomationFramework.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Reporter;
    using Core;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;

    public abstract class BaseTest
    {
        protected IWebDriver _driver;

        // Needed for reporting
        protected ExtentReports _extent;
        protected ExtentTest _test;

        private readonly string _homeDirectory;

        protected BaseTest()
        {
            _homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(BaseTest)).Location);
        }

        protected string Url
        {
            get => _driver.Url;
            set => _driver.Url = value;
        }

        protected string Title
        {
            get => _driver.Title;
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
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
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

        // Run before every suite
        [OneTimeSetUp]
        protected void Setup()
        {
            TestExtensions.LogCleaner();

            DateTime date = DateTime.Today;
            var dir = $@"c:\Automation Logs\{date:MM.dd.yyyy}\Reports\";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var fileName = GetType() + ".html";
            var htmlReporter = new ExtentHtmlReporter(dir + fileName);

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
        }

        // Run after every suite
        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            CloseQuitBrowsers();
            _extent.Flush();
        }

        protected void UseChrome()
        {
            CloseQuitBrowsers();
            _driver = new ChromeDriver($"{_homeDirectory}\\Support");
        }

        protected void UseFirefox()
        {
            CloseQuitBrowsers();
            _driver = new FirefoxDriver($"{_homeDirectory}\\Support");
        }

        protected void UseEdge()
        {
            CloseQuitBrowsers();
            _driver = new EdgeDriver($"{_homeDirectory}\\Support");
        }


        /// <summary>
        /// Closes active Selenium controlled browser
        /// </summary>
        protected void CloseBrowser()
        {
            _driver.Close();
        }

        /// <summary>
        /// Closes active Selenum controlled browser then ends all Selenium controlled browser processes
        /// </summary>
        protected void CloseQuitBrowsers()
        {
            _driver?.Close();
            _driver?.Quit();
        }

        /// <summary>
        /// Forced Wait: Forces Selenium to wait a certain amound of seconds before doing anything &#8211; 
        /// Expects input in seconds
        /// </summary>
        /// <param name="wait"></param>
        protected void Wait(int wait)
        {
            wait = wait * 1000; // Converts from milliseconds to seconds
            Thread.Sleep(wait);
        }

        /// <summary>
        /// Method that will rename the core log folder if it exists for archival purposes
        /// </summary>
        protected void LogCleaner()
        {
            DateTime date = DateTime.Today;
            var logLocation = @"c:\Automation Logs\" + date.ToString("MM.dd.yyyy");
            string newLocation = logLocation;

            if (Directory.Exists(logLocation))
            {
                int counter = 1;

                while (Directory.Exists(newLocation))
                {
                    newLocation = logLocation + " - " + "Archive " + counter;
                    counter++;
                }

                Directory.Move(logLocation, newLocation);
            }
        }
  
        /// <summary>
        /// Maximizes the browser window
        /// </summary>
        protected void MaximizeBrowser()
        {
            _driver.Manage().Window.Maximize();
        }


        /// <summary>
        /// Quits all Selenium controlled browser processes 
        /// </summary>
        protected void QuitBrowsers()
        {
            _driver.Quit();
        }


        /// <summary>
        /// Method to take a screen cap of the current browser state
        /// </summary>
        /// <param name="name"></param>
        protected void ScreenShot(string name)
        {
            DateTime date = DateTime.Today;
            var logLocation = $@"c:\Automation Logs\{date:MM.dd.yyyy}\Screenshots";

            if (!Directory.Exists(logLocation))
            {
                Directory.CreateDirectory(logLocation);
            }

            Screenshot image = ((ITakesScreenshot)_driver).GetScreenshot();
            DateTime timeStamp = DateTime.Now;

            var filename = $"{logLocation}\\{name}.png";
            int counter = 2;

            while (File.Exists(filename))
            {
                filename = $"{logLocation}\\{name} - {counter}.png";
                counter++;
            }

            image.SaveAsFile(filename);
        }
    }
}