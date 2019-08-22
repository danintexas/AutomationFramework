﻿namespace AutomationFramework.Tests
{
    using System;
    using System.IO;
    using System.Linq;
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
        // Framework needed variables
        private readonly string _homeDirectory;
        
        // Selenium & Json variables
        protected IWebDriver _driver;
        protected IConfiguration _config;       
        public const string XPath = "xpath", CSS = "css", ID = "ID"; // Const Keywords for Selenium Locators

        // Reporting variables
        protected ExtentReports _extent;
        protected ExtentTest _test;
        Status logstatus;
        public const string Info = "info", Pass = "pass", Fail = "fail"; // Const Keywords for Logger

        // ****RumbleOn Specific variables****
        // Variables needed for account testing
        public string accountEmailUnderTest = "", accountPasswordUnderTest = "", accountFirstNameUnderTest = "", accountLastNameUnderTest = "",
            accountPhoneUnderTest = "", accountStreetAddressUnderTest = "", accountZipCodeUnderTest = "";
        // Variables needed for VIN testing
        public const string Motorcycle = "motorcycle", Car = "car", Truck = "truck", Offroad = "offroad", Random = "random"; 
        public string vinUnderTest = "", makeUnderTest = "", modelUnderTest = "", trimUnderTest = "";
        public int yearUnderTest = 0;

        protected Core()
        {
            _homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Core)).Location);

            var configBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true); 

            foreach (var filename in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Json Repo" , "*.json"))
            {
                if (!filename.StartsWith("appsettings"))
                {
                    configBuilder.AddJsonFile(filename, true, true);
                }
            }

            foreach (var filename in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Json Repo\Json Override", "*.json"))
            {
                if (!filename.StartsWith("appsettings"))
                {
                    configBuilder.AddJsonFile(filename, true, true);
                }
            }

            configBuilder.AddJsonFile(_homeDirectory + "\\appsettings.local.json", true, true);

            _config = configBuilder.Build();            
        }

        // Selenium set the URL to go to
        protected string SetUrl
        {
            get => _driver.Url;
            set => _driver.Url = value;
        }

        // Selenium read the webpages Title text
        protected string Title
        {
            get => _driver.Title;
        }

        //////////////////////////////////////////////////////////////////////////////// NUNIT SECTION ////////////////////////////////////////////////////////////

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

        ///////////////////////////////////////////////////////////////////////////////// Core Commands ///////////////////////////////////////////////////////////

        /// <summary>
        /// Uses Selenium to click on an element on the page
        /// </summary>
        /// <param name="type">Supported: XPath - CSS</param>
        /// <param name="element">Locator path to the item to click</param>
        protected void ClickElement(string rawLocator)
        {
            var type = LocatorCleaner(rawLocator);

            try
            {
                switch (type.Item2)
                {
                    case XPath:
                        _driver.FindElement(By.XPath(type.Item1)).Click();
                        break;
                    case CSS:
                        _driver.FindElement(By.CssSelector(type.Item1)).Click();
                        break;
                    case ID:
                        _driver.FindElement(By.Id(type.Item1)).Click();
                        break;
                    default:
                        Logger(Fail, "Unsupported element type passed to ClickElement. Please report to framework owner. <br />Used " + type.Item2 + " to click on element: <br />" + type.Item1);
                        Assert.Fail($@"Unsupported element type passed to ClickElement. Please report to framework owner." + Environment.NewLine + 
                            "Used " + type.Item2 + " to click on element: " + type.Item1);
                        Environment.Exit(1);
                        break;
                }
            }

            catch (Exception ex_)
            {
                Logger(Fail, $@"Something happened with ClickElement method. Please report to framework owner. <br />Used " 
                    + type.Item2 + " to click on element: <br />" + type.Item1 + "<br />" + ex_);
                Assert.Fail($@"Something happened with ClickElement method. Please report to framework owner." +
                    Environment.NewLine + "Used " + type.Item2 + " to click on element: " + type.Item1 + Environment.NewLine + ex_);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Closes active Selenium controlled browser
        /// </summary>
        protected void CloseBrowser()
        {
            _driver.Close();
        }

        /// <summary>
        /// Closes active Selenium controlled browser then ends all Selenium controlled browser processes
        /// </summary>
        protected void CloseQuitBrowsers()
        {
            _driver?.Close();
            _driver?.Quit();
        }

        /// <summary>
        /// DatabaseCheck method is used to verify a single table entry matches an expected result. 
        /// Please ensure all the server information is filled out in the appsettings.local.template.json file
        /// </summary>
        /// <param name="query">Single Table entry SQL query to run against the database</param>
        /// <param name="expectedResult">The exact result expected</param>
        protected string DatabaseCheck(string query)
        {
            var runDBTests = JsonCall("FrameworkConfiguration:DatabaseTestSteps").ToLower();

            if (runDBTests != "no")
            {
                
                string connectionString = "Server=" + JsonCall("DatabaseServerInformation:Server") +
                    ";Database=" + JsonCall("DatabaseServerInformation:Database") + 
                    ";User Id=" + JsonCall("DatabaseServerInformation:Account") + 
                    ";Password=" + JsonCall("DatabaseServerInformation:Password");
                var resultOfQuery = "";

                resultOfQuery = AdditionalFunctions.QueryDatabase(query, connectionString);

                if (resultOfQuery != "")
                {
                    Logger(Pass, "Checked the database with the following query: <br />'" + query + "' <br />The returned value was: <br />'" + resultOfQuery + "'");
                    return resultOfQuery;
                }
                else
                {
                    Logger(Fail, "Checked the database with the following query: <br />'" + query + "' <br />No result was returned");
                    Assert.Fail("Checked the database with the following query: <br />'" + query + "' <br />No result was returned");
                }
            }

            if (runDBTests != "yes")
            {
                Logger(Info, "Database test step skipped per the setting = 'FrameworkConfiguration:DatabaseTestSteps' set to: <br />'" + runDBTests +
                "'<br />and not being set to 'yes'");
                return "Database Test Step Skipped";
            }

            return "Complete";
        }

        /// <summary>
        /// The GenerateAVIN method reads the VIN Store.json file for VIN known good VINs and will randomly select one. 
        /// This will assign the VIN - Year - Make - Model to the global variables
        /// </summary>
        /// <param name="vinSelection">If not given this method will randomly pick a vehicle type. Other wise you can use the following:
        /// Motorcycle : Car : Truck : Offroad : Random</param>
        /// <returns></returns>
        protected void GenerateAVIN(string vinSelection = "random")
        {
            int motorcyleCounter = 0, truckCounter = 0, carCounter = 0, offroadCounter = 0, randomType = 0;
            int randomVINEnding = (new Random()).Next(100, 1000);

            // Below counts the number of valid VIN templates in the VIN Stor.json file
            for (int x = 0; x < 100; x++)
            {
                if ($"{_config["VINStore:Motorcycles:" + x + ":Vin"]}" != "")
                {
                    motorcyleCounter++;
                }
                if ($"{_config["VINStore:Cars:" + x + ":Vin"]}" != "")
                {
                    carCounter++;
                }
                if ($"{_config["VINStore:Trucks:" + x + ":Vin"]}" != "")
                {
                    truckCounter++;
                }
                if ($"{_config["VINStore:Offroad:" + x + ":Vin"]}" != "")
                {
                    offroadCounter++;
                }
            }

            if (vinSelection == "random")
            {
                randomType = (new Random()).Next(1, 5);
                switch (randomType)
                {
                    case 1:
                        vinSelection = "motorcycle";
                        break;
                    case 2:
                        vinSelection = "car";
                        break;
                    case 3:
                        vinSelection = "truck";
                        break;
                    case 4:
                        vinSelection = "offroad";
                        break;
                }
            }

            if ((vinSelection == "motorcycle" && motorcyleCounter < 1) ||
                (vinSelection == "car" && carCounter < 1) ||
                (vinSelection == "truck" && truckCounter < 1) ||
                (vinSelection == "offroad" && offroadCounter < 1))
            {
                Console.WriteLine("No valid VIN found in the 'VIN Store.json' file for: " + vinSelection);
                Logger(Fail, "No valid VIN found in the 'VIN Store.json' file for: <br />" + vinSelection);
                throw new Exception("No valid VIN found in the 'VIN Store.json' file for: " + vinSelection);
            }
            
            switch (vinSelection)
            {
                case "motorcycle":
                    int randomMotorcycleVin = (new Random()).Next(0, motorcyleCounter);
                    vinUnderTest = $"{_config["VINStore:Motorcycles:" + randomMotorcycleVin + ":Vin"]}";
                    yearUnderTest = Int32.Parse($"{_config["VINStore:Motorcycles:" + randomMotorcycleVin + ":Year"]}");
                    makeUnderTest = $"{_config["VINStore:Motorcycles:" + randomMotorcycleVin + ":Make"]}";
                    modelUnderTest = $"{_config["VINStore:Motorcycles:" + randomMotorcycleVin + ":Model"]}";
                    break;
                case "car":
                    int randomCarVin = (new Random()).Next(0, carCounter);
                    vinUnderTest = $"{_config["VINStore:Cars:" + randomCarVin + ":Vin"]}";
                    yearUnderTest = Int32.Parse($"{_config["VINStore:Cars:" + randomCarVin + ":Year"]}");
                    makeUnderTest = $"{_config["VINStore:Cars:" + randomCarVin + ":Make"]}";
                    modelUnderTest = $"{_config["VINStore:Cars:" + randomCarVin + ":Model"]}";
                    trimUnderTest = $"{_config["VINStore:Cars:" + randomCarVin + ":Trim"]}";
                    break;
                case "truck":
                    int randomTruckVin = (new Random()).Next(0, truckCounter);
                    vinUnderTest = $"{_config["VINStore:Trucks:" + randomTruckVin + ":Vin"]}";
                    yearUnderTest = Int32.Parse($"{_config["VINStore:Trucks:" + randomTruckVin + ":Year"]}");
                    makeUnderTest = $"{_config["VINStore:Trucks:" + randomTruckVin + ":Make"]}";
                    modelUnderTest = $"{_config["VINStore:Trucks:" + randomTruckVin + ":Model"]}";
                    trimUnderTest = $"{_config["VINStore:Trucks:" + randomTruckVin + ":Trim"]}";
                    break;
                case "offroad":
                    int randomOffroadVin = (new Random()).Next(0, offroadCounter);
                    vinUnderTest = $"{_config["VINStore:Offroad:" + randomOffroadVin + ":Vin"]}";
                    yearUnderTest = Int32.Parse($"{_config["VINStore:Offroad:" + randomOffroadVin + ":Year"]}");
                    makeUnderTest = $"{_config["VINStore:Offroad:" + randomOffroadVin + ":Make"]}";
                    modelUnderTest = $"{_config["VINStore:Offroad:" + randomOffroadVin + ":Model"]}";
                    break;
            }

            // Log testing information to Automation Logs
            if (trimUnderTest != "")
            {
                Logger(Info, "Using the following " + (vinSelection.First().ToString().ToUpper() + vinSelection.Substring(1)) + " for testing: <br />VIN: " + vinUnderTest +
                "<br />Year: " + yearUnderTest +
                "<br />Make: " + makeUnderTest +
                "<br />Model: " + modelUnderTest +
                "<br />Trim: " + trimUnderTest);
            }
            else
            {
                Logger(Info, "Using the following " + (vinSelection.First().ToString().ToUpper() + vinSelection.Substring(1)) + " for testing: <br />VIN: " + vinUnderTest +
                "<br />Year: " + yearUnderTest +
                "<br />Make: " + makeUnderTest +
                "<br />Model: " + modelUnderTest);
            }
        }

        /// <summary>
        /// Simple wrapper method to call the 'FetchAllMessages' method which pulls all emails from an email account using POP3. 
        /// Ensure your appsettings.local.JSON file has the following filled out
        /// -EmailInformation:HostName
        /// -EmailInformation:UserName
        /// -EmailInformation:Password
        /// </summary>
        /// <param name="delete">Pass 'true' through the method if you want the emails deleted off the account after saving them</param>
        protected void GetAllEmailsFromAnEmailAccount(bool delete = false)
        {
            if (delete == true)
            {
                AdditionalFunctions.FetchAllMessages(JsonCall("EmailInformation:HostName"), 110, false,
                   JsonCall("EmailInformation:UserName"), JsonCall("EmailInformation:Password"), true);
                Logger(Info, "Checking all emails in the following email account: <br />" + JsonCall("EmailInformation:UserName"));
                Logger(Info, "Deleting all emails in the following email account: <br />" + JsonCall("EmailInformation:UserName"));
            }
            else
            {
                AdditionalFunctions.FetchAllMessages(JsonCall("EmailInformation:HostName"), 110, false,
                    JsonCall("EmailInformation:UserName"), JsonCall("EmailInformation:Password"));
                Logger(Info, "Checking all emails in the following email account: <br />" + JsonCall("EmailInformation:UserName"));
            }
        }

        /// <summary>
        /// GetFieldValue method checks an elements field for its current value
        /// </summary>
        /// <param name="type">Supported: XPath - CSS</param>
        /// <param name="element">Locator path to the item to click</param>
        /// <returns></returns>
        protected string GetFieldValue(string rawLocator)
        {
            var type = LocatorCleaner(rawLocator);
            string returnValue = null;

            try
            {
                switch (type.Item2)
                {
                    case XPath:
                        returnValue = _driver.FindElement(By.XPath(type.Item1)).GetAttribute("value");
                        break;
                    case CSS:
                        returnValue = _driver.FindElement(By.CssSelector(type.Item1)).GetAttribute("value");
                        break;
                    default:
                        Logger(Fail, "Unsupported element type passed to GetFieldValue. Please report to framework owner. <br />Used " + type.Item2 + 
                            " to try and retrieve info in : <br />" + type.Item1);
                        Assert.Fail($@"Unsupported element type passed to GetFieldValue. Please report to framework owner. " + Environment.NewLine + 
                            "Used " + type.Item2 + " to try and retrieve info in : " + type.Item1);
                        Environment.Exit(1);
                        break;
                }
            }

            catch (Exception ex_)
            {
                Logger(Fail, $@"Something happened with GetFieldValue method. Please report to framework owner. <br />Used " 
                    + type.Item2 + " to try and retrieve info in :<br />" + type.Item1 + "<br />" + ex_);
                Assert.Fail($@"Something happened with GetFieldValue method. Please report to framework owner." +
                   Environment.NewLine + "Used " + type.Item2 + " to try and retrieve info in : " + type.Item1 + Environment.NewLine + ex_);
                Environment.Exit(1);
            }

            return returnValue;
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
        /// LocatorCleaner is used to take a locator string and parse out the type of locator and the locator and return both.
        /// </summary>
        /// <param name="rawLocator">String value of the locator formatted the following: 'xpath=this is the locator'</param>
        /// <returns></returns>
        public (string, string) LocatorCleaner(string rawLocator = "")
        {
            string cleanedLocator = "", type = "";

            if (rawLocator.Contains('='))
            {
                type = ((rawLocator.Substring(0, rawLocator.IndexOf('='))).Trim()).ToLower();
                cleanedLocator = rawLocator.Substring(rawLocator.IndexOf('=') + 1);
            }

            return (cleanedLocator, type);
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
        }

        /// <summary>
        /// Quits all Selenium controlled browser processes 
        /// </summary>
        protected void QuitBrowsers()
        {
            _driver.Quit();
        }

        /// <summary>
        /// Method will search in the 'c:\Automation Logs\{Date}\Emails' folder for a specific string in all the emails 
        /// downloaded with the GetAllEmailsFromAnEmailAccount method.
        /// This will fail or pass the current test if the string is not found.
        /// It is recommended to run this after all other tests are completed. 
        /// </summary>
        /// <param name="valueToSearchFor">String to search for.</param>
        protected void ParseAllEmailFilesForAStringValue(string valueToSearchFor)
        {
            DateTime date = DateTime.Today;
            int fileCount   = Directory.GetFiles($@"c:\Automation Logs\{date:MM.dd.yyyy}\Emails\", "Email *" ,SearchOption.TopDirectoryOnly).Length;
            
            if (fileCount > 0)
            {
                bool valueFound = false;

                for (int i = 1; i <= fileCount; i++)
                {
                    StreamReader textFile = new StreamReader($@"c:\Automation Logs\{date:MM.dd.yyyy}\Emails\\Email " + i + ".txt");
                    try
                    {                        
                        string fileContents = textFile.ReadToEnd();

                        if (fileContents.Contains(valueToSearchFor))
                        {
                            Logger(Pass, $@"c:\Automation Logs\{ date: MM.dd.yyyy}\Emails\Email " + i + ".txt - Contained the needed text of:<br />" + valueToSearchFor);
                            valueFound = true;
                            break;
                        }
                    }

                    finally
                    {
                        textFile.Close();
                    }
                }

                if (!valueFound)
                {
                    Logger(Fail, $@"No email files contained in: c:\Automation Logs\{ date: MM.dd.yyyy}\Emails - Contained the needed text of:<br />" + valueToSearchFor);
                    Assert.Fail($@"No email files contained in: c:\Automation Logs\{ date: MM.dd.yyyy}\Emails - Contained the needed text of: " + 
                        Environment.NewLine + valueToSearchFor);
                    Environment.Exit(1);
                }
            }

            else
            {
                Logger(Fail, "No emails were found for ParseAllEmailFilesForAStringValue method to look through.<br />" +
                    "Please ensure your test has a call to the GetAllEmailsFromAnEmailAccount method before this one will work,<br />" + 
                    "otherwise report to framework owner.");
                Assert.Fail("No emails were found for ParseAllEmailFilesForAStringValue method to look through. " + Environment.NewLine + 
                    "Please ensure your test has a call to the GetAllEmailsFromAnEmailAccount method before this one will work, " + 
                    Environment.NewLine + "otherwise report to framework owner.");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Method to take a screen cap of the current browser state
        /// </summary>
        /// <param name="name"></param>
        protected void ScreenShot(string name)
        {
            DateTime date = DateTime.Today;
            var logLocation = $@"c:\Automation Logs\{date:MM.dd.yyyy}\Screenshots";
            var reportLocation = $@"..\Screenshots";

            if (!Directory.Exists(logLocation))
            {
                Directory.CreateDirectory(logLocation);
            }

            Screenshot image = ((ITakesScreenshot)_driver).GetScreenshot();

            var filename = $"{logLocation}\\{name}.png";
            var screenLocation = $"{reportLocation}\\{name}.png";
            int counter = 2;

            while (File.Exists(filename))
            {
                filename = $"{logLocation}\\{name} - {counter}.png";
                screenLocation = $"{reportLocation}\\{name} - {counter}.png";
                counter++;
            }

            image.SaveAsFile(filename);
            _test.Info(name + "<br />", MediaEntityBuilder.CreateScreenCaptureFromPath(screenLocation).Build());
        }

        /// <summary>
        /// SendKeys sends text to a specific element
        /// </summary>
        /// <param name="type">Supported: XPath</param>
        /// <param name="element">Locator path to the item to click</param>
        /// <param name="text">Text to send to the element</param>
        protected void SendKeys(string rawLocator, string text)
        {

            var type = LocatorCleaner(rawLocator);

            IWebElement textbox;
            try
            {
                switch (type.Item2)
                {
                    case XPath:
                        textbox = _driver.FindElement(By.XPath(type.Item1));
                        textbox.SendKeys(text);
                        break;
                    case CSS:
                        textbox = _driver.FindElement(By.CssSelector(type.Item1));
                        textbox.SendKeys(text);
                        break;
                    case ID:
                        textbox = _driver.FindElement(By.Id(type.Item1));
                        textbox.SendKeys(text);
                        break;
                    default:
                        Logger(Fail, $@"Something happened with SendKeys method. Please report to framework owner." +
                            "<br />Used " + type.Item2 + " to send: <br />'" + text + "' to element: <br />" + type.Item1);
                        Assert.Fail($@"Something happened with SendKeys method. Please report to framework owner." + Environment.NewLine +
                            "Used " + type.Item2 + " to send: '" + text + "' to element: " + type.Item1);
                        Environment.Exit(1);
                        break;
                }
            }

            catch (Exception ex_)
            {
                Logger(Fail, $@"Something happened with SendKeys method. Please report to framework owner." +
                   "<br />Used " + type.Item2 + " to send: <br />'" + text + "' to element: <br />" + type.Item1 + "<br />" + ex_);
                Assert.Fail($@"Something happened with SendKeys method. Please report to framework owner." +
                   Environment.NewLine + "Used " + type.Item2 + " to send: '" + text + "' to element: " + type.Item1 + Environment.NewLine + ex_);
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
            }
            catch (Exception ex_)
            {
                Logger(Fail, $@"Two values asked to be the same were not: <br />'" + valueOne + "' <br />and <br />'" + valueTwo + "' <br />" + ex_);
                Assert.Fail($@"Two values asked to be the same were not: '" + valueOne + "' and '" + valueTwo + "' " + Environment.NewLine + ex_);
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
            }
            catch (Exception ex_)
            {
                Logger(Fail, $@"Two values asked to be the different were the same: <br />'" + valueOne + "' <br />and <br />'" + valueTwo + "' <br />" + ex_);
                Assert.Fail($@"Two values asked to be the different were the same: '" + valueOne + "' and '" + valueTwo + "' " + Environment.NewLine + ex_);
            }
        }

        /// <summary>
        /// StripEndingURL is a custom method that will remove everything after the last '/' in a URL
        /// </summary>
        /// <param name="urlToStrip">URL to strip everything after the last '/'</param>
        /// <returns></returns>
        protected string StripEndingUrl(string urlToStrip)
        {
            if (urlToStrip.Contains('/'))
                urlToStrip = urlToStrip.Substring(0, urlToStrip.LastIndexOf('/'));

            return urlToStrip;
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
                    break;

                case "firefox":
                    _driver = new FirefoxDriver($"{_homeDirectory}\\Support");
                    break;

                case "edge":
                    _driver = new EdgeDriver($"{_homeDirectory}\\Support");
                    break;

                default:
                    logstatus = Status.Fail;
                    Logger(Fail, "Browser type passed is not supported on test. Used: <br />" + browserType);
                    Assert.Fail("Browser type passed is not supported on test Used: " + Environment.NewLine + browserType);
                    break;
            }

            Logger(Info, "Starting test with " + browserType);
        }

        /// <summary>
        /// Forced Wait: Forces Selenium to wait a certain amound of seconds before doing anything &#8211; 
        /// Expects input in seconds
        /// </summary>
        /// <param name="wait"></param>
        protected void Wait(int wait)
        {
            wait *= 1000; // Converts from milliseconds to seconds
            Thread.Sleep(wait);
        }

        /// <summary>
        /// Waits for an element to exist for 30 seconds
        /// </summary>
        /// <param name="type">Supported: XPath</param>
        /// <param name="element">Locator path to the item to wait for</param>
        protected void WaitForElement(string rawLocator, int time = 30)
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, time));
            var type = LocatorCleaner(rawLocator);

            try
            {
                switch (type.Item2)
                {
                    case XPath:
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(type.Item1)));
                        break;
                    case CSS:
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(type.Item1)));
                        break;
                    case ID:
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(type.Item1)));
                        break;
                    default:
                        Logger(Fail, $@"Unsupported element type passed to WaitForElement. Please report to framework owner." + 
                            "<br />Used " + type.Item2 + " to wait on element: <br />" + type.Item1);
                        Assert.Fail($@"Unsupported element type passed to WaitForElement. Please report to framework owner." + Environment.NewLine +
                            "Used " + type.Item2 + " to wait on element: " + type.Item1);
                        Environment.Exit(1);
                        break;
                }
            }

            catch (Exception ex_)
            {
                Logger(Fail, $@"Something happened with WaitForElement method. Please report to framework owner." +
                    "<br />Used " + type.Item2 + " to wait on element: <br />" + type.Item1 + "<br />" + ex_);
                Assert.Fail($@"Something happened with WaitForElement method. Please report to framework owner." +
                    Environment.NewLine + "Used " + type.Item2 + " to wait on element: " + type.Item1 + Environment.NewLine + ex_);
                Environment.Exit(1);
            }
        }
    }
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////