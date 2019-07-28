﻿namespace AutomationFramework.Tests
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using AventStack.ExtentReports;
    using AventStack.ExtentReports.Reporter;
    using Microsoft.Extensions.Configuration;
    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Edge;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Support.UI;

    public abstract class Core
    {
        protected IWebDriver _driver;
        protected IConfiguration _config;
        protected string[] supportedBrowsers = { Chrome, Firefox, Edge };
        
        // Needed for reporting
        protected ExtentReports _extent;
        protected ExtentTest _test;
        Status logstatus;

        private readonly string _homeDirectory;

        public const string Chrome = "chrome", Firefox = "firefox", Edge = "edge"; // Const Keywords for UseBrowser
        public const string Info = "info", Pass = "pass", Fail = "fail"; // Const Keywords for Logger
        public const string XPath = "xpath", CSS = "css"; // Const Keywords for Selenium Locators

        protected Core()
        {
            _homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Core)).Location);

            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true) // load base settings
                .AddJsonFile("appsettings.local.json", true, true); // load local settings

            _config = configBuilder.Build();
        }

        protected string SetUrl
        {
            get => _driver.Url;
            set => _driver.Url = value;
        }

        protected string Title
        {
            get => _driver.Title;
        }

        // Run before every suite
        [OneTimeSetUp]
        protected void Setup()
        {
            LogCleaner();

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
            _extent.AddSystemInfo("Tester: ", _config["ReportInformation:TesterName"]);
            _extent.AddSystemInfo("Project", _config["ReportInformation:Project"]);
            _extent.AddSystemInfo("Environment", _config["ReportInformation:Environment"]);
            _extent.AddSystemInfo("Build", _config["ReportInformation:Build"]);
        }

        // Run after every suite
        [OneTimeTearDown]
        protected void OneTimeTearDown()
        {
            CloseQuitBrowsers();
            _extent.Flush();
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

        /// <summary>
        /// Uses Selenium to click on an element on the page
        /// </summary>
        /// <param name="type">Supported: XPath</param>
        /// <param name="element">Locator path to the item to click</param>
        protected void ClickButton(string type, string element)
        {
            try
            {
                switch (type)
                {
                    case XPath:
                        _driver.FindElement(By.XPath(element)).Click();
                        break;
                    case CSS:
                        _driver.FindElement(By.CssSelector(element)).Click();
                        break;
                    default:
                        Logger(Info, "Unsupported element type passed to ClickButton. Please report to framework owner.");
                        Logger(Fail, "Used " + type + " to click on element: " + element);
                        Assert.Fail($@"Something happened with ClickButton method. Please report to framework owner.");
                        Environment.Exit(1);
                        break;
                }

                Logger(Info, "Used " + type + " to click on element: " + element);
            }

            catch
            {
                Logger(Fail, "Used " + type + " to click on element: " + element);
                Assert.Fail($@"Something happened with ClickButton method. Please report to framework owner.");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Closes active Selenium controlled browser
        /// </summary>
        protected void CloseBrowser()
        {
            _driver.Close();
            Logger(Info, "Closing active Selenium controlled browser");
        }

        /// <summary>
        /// Closes active Selenum controlled browser then ends all Selenium controlled browser processes
        /// </summary>
        protected void CloseQuitBrowsers()
        {
            _driver?.Close();
            _driver?.Quit();
            Logger(Info, "Closing and quitting all active Selenium controlled browsers");
        }

        /// <summary>
        /// This method just tells the framework to read the JSON file - appsettings
        /// </summary>
        /// <param name="call">JSON entry to use</param>
        /// <returns></returns>
        protected string JsonCall(string call)
        {
            string returnedCall = _config[call];

            return returnedCall;
        }

        /// <summary>
        /// Method that will rename the core log folder if it exists for archival purposes
        /// </summary>
        public static void LogCleaner()
        {
            DateTime date = DateTime.Today;
            var logLocation = @"c:\Automation Logs\" + date.ToString("MM.dd.yyyy");
            string newLocation;

            if (Directory.Exists(logLocation))
            {
                DateTime dt = Directory.GetCreationTime(logLocation);
                newLocation = logLocation + " - " + dt.ToString("hh.mm.ss tt");

                try
                {
                    Directory.Move(logLocation, newLocation);
                }
                catch
                {
                    Assert.Fail($@"Please ensure no file located in c:\Automation Logs\{date.ToString("MM.dd.yyyy")} is open!!");
                    Environment.Exit(1);
                }
            }
        }

        /// <summary>
        /// Method determines what gets written to the extent logs. 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        protected void Logger(string type, string text)
        {
            switch (type)
            {
                case "info":
                    logstatus = Status.Info;
                    break;
                case "pass":
                    logstatus = Status.Pass;
                    break;
                case "fail":
                    logstatus = Status.Fail;
                    break;
                default:
                    break;
            }
            _test.Log(logstatus, text);
        }

        /// <summary>
        /// Maximizes the browser window
        /// </summary>
        protected void MaximizeBrowser()
        {
            _driver.Manage().Window.Maximize();
            Logger(Info, "Maximized controlled browser");
        }

        /// <summary>
        /// Quits all Selenium controlled browser processes 
        /// </summary>
        protected void QuitBrowsers()
        {
            _driver.Quit();
            Logger(Info, "Quitting all active Selenium controlled browsers");
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
            Logger(Info, "Screenshot saved at: " + filename);
        }

        /// <summary>
        /// SendKeys sends text to a specific element
        /// </summary>
        /// <param name="type">Supported: XPath</param>
        /// <param name="element">Locator path to the item to click</param>
        /// <param name="text">Text to send to the element</param>
        protected void SendKeys(string type, string element, string text)
        {
            IWebElement textbox = _driver.FindElement(By.XPath(element));
            try
            {
                switch (type)
                {
                    case XPath:
                        break;
                    default:
                        Logger(Info, "Unsupported element type passed to SendKeys. Please report to framework owner.");
                        Logger(Fail, "Used " + type + " to send: '" + text + "' to element: " + element);
                        Assert.Fail($@"Something happened with SendKeys method. Please report to framework owner.");
                        Environment.Exit(1);
                        break;
                }

                textbox.SendKeys(text);
                Logger(Info, "Sent the following: '" + text + "'" + " to the element: '" + element + "'");
            }

            catch
            {
                Logger(Info, "Possible bad element sent to SendKeys.");
                Logger(Fail, "Used " + type + " to send: '" + text + "' to element: " + element);
                Assert.Fail($@"Something happened with SendKeys method. Please report to framework owner.");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Asserts two values are the same. 
        /// </summary>
        /// <param name="valueOne"></param>
        /// <param name="valueTwo"></param>
        protected void ShouldBe(string valueOne, string valueTwo)
        {
            try
            {
                Assert.AreEqual(valueOne, valueTwo);
                Logger(Info, "Verified the two values were the same: '" + valueOne + "' and '" + valueTwo + "'");
            }
            catch (Exception)
            {
                Logger(Fail, "Two values asked to be the same were not: '" + valueOne + "' and '" + valueTwo + "'");
            }
        }

        /// <summary>
        /// Asserts two values are not the same. 
        /// </summary>
        /// <param name="valueOne"></param>
        /// <param name="valueTwo"></param>
        protected void ShouldNotBe(string valueOne, string valueTwo)
        {
            try
            {
                Assert.AreNotEqual(valueOne, valueTwo);
                Logger(Info, "Verified the two values were not the same: '" + valueOne + "' and '" + valueTwo + "'");
            }
            catch
            {
                Logger(Fail, "Two values asked to be the different were the same: '" + valueOne + "' and '" + valueTwo + "'");
            }
        }

        /// <summary>
        /// Method to start up the Selenium driver
        /// </summary>
        /// <param name="browserType"></param>
        protected void UseBrowser(string browserType)
        {
            CloseQuitBrowsers();

            switch (browserType)
            {
                case "chrome":
                    _driver = new ChromeDriver($"{_homeDirectory}\\Support");
                    Logger(Info, "Chrome started");
                    break;

                case "firefox":
                    _driver = new FirefoxDriver($"{_homeDirectory}\\Support");
                    logstatus = Status.Pass;
                    Logger(Info, "Firefox started");
                    break;

                case "edge":
                    _driver = new EdgeDriver($"{_homeDirectory}\\Support");
                    logstatus = Status.Pass;
                    Logger(Info, "Edge started");
                    break;

                default:
                    logstatus = Status.Fail;
                    Logger(Fail, "Browser type passed is not supported on test");
                    Assert.Fail("Browser type passed is not supported on test");
                    break;
            }
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
            Logger(Info, "Waited " + (wait / 1000) + " seconds");
        }

        /// <summary>
        /// Waits for an element to exist for 30 seconds
        /// </summary>
        /// <param name="type">Supported: XPath</param>
        /// <param name="element">Locator path to the item to wait for</param>
        protected void WaitForElement(string type, string element)
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 30));
            try
            {
                switch (type)
                {
                    case XPath:
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(element)));
                        break;
                    default:
                        Logger(Info, "Unsupported element type passed to WaitForElement. Please report to framework owner.");
                        Logger(Fail, "Used " + type + " to wait on element: " + element);
                        Assert.Fail($@"Something happened with WaitForElement method. Please report to framework owner.");
                        Environment.Exit(1);
                        break;
                }

                Logger(Info, "Used " + type + " to wait on element: " + element);
            }

            catch
            {
                Logger(Fail, "Used " + type + " to wait on element: " + element);
                Assert.Fail($@"Something happened with WaitForElement method. Please report to framework owner.");
                Environment.Exit(1);
            }
        }
    }
}