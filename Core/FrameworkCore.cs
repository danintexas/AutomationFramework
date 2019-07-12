using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System;
using System.Configuration;

namespace AutomationFramework
{
    using System.IO;
    using System.Reflection;

    public class FrameworkCore
    {
        // Set the driver
        public IWebDriver driver;

        public string testEnv = "https://" + ConfigurationManager.AppSettings.Get("Env") + ".rumbleonclassifieds.com/";
        
        // Print out results of the test
        public void resultPrint()
        {
            Console.WriteLine(TestContext.CurrentContext.Test.Name);
            Console.WriteLine(TestContext.CurrentContext.Result.Outcome.Status);
            Console.WriteLine(TestContext.CurrentContext.Result.Message);
        }

        // Method to set the current browser to test
        public void SetBrowser(String browser)
        {
            // Runs test for Chrome 
            if (browser == "chrome")
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetAssembly(typeof(FrameworkCore)).Location) + "\\Support");
            }

            // Runs test for Firefox
            if (browser == "firefox")
            {
                driver = new FirefoxDriver(System.IO.Directory.GetCurrentDirectory() + "\\Support");
            }

            // Runs test for Edge
            if (browser == "edge")
            {
                driver = new EdgeDriver(System.IO.Directory.GetCurrentDirectory() + "\\Support");
            }


            driver.Manage().Window.Maximize();
        }

        // Place holder in case I need this to run before every test
        [SetUp]
        public void setUp()
        {

        }

        // Section that is executed after every test
        [TearDown]
        public void TearDown()
        {
            // Close the webdriver
            driver.Close();
            driver.Quit();

            // These are seperate for now for future features
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                resultPrint();          
            }

            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                resultPrint();
            }

            else
            {
                resultPrint();
            }
        }
    }
}
