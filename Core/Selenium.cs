using AutomationFramework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Selenium
{
    public class SeleniumCommands : FrameworkCore
    {
        /// <summary>
        /// Asserts two values are the same. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void AssertEqual(string a, string b)
        {
            try
            {
                Assert.AreEqual(a, b);
            }
            catch
            {
                // I need to work on getting this catch into extent reports
            }
        }

        /// <summary>
        /// Asserts two values are not the same. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void AssertNotEqual(string a, string b)
        {
            try
            {
                Assert.AreNotEqual(a, b);
            }
            catch
            {
                // I need to work on getting this catch into extent reports
            }
        }

        /// <summary>
        /// Closes active Selenium controlled browser
        /// </summary>
        public static void CloseBrowser()
        {
            seleniumDriver.Close();
        }

        /// <summary>
        /// Closes active Selenum controlled browser then ends all Selenium controlled browser processes
        /// </summary>
        public static void CloseQuitBrowsers()
        {
            seleniumDriver.Close();
            seleniumDriver.Quit();
        }

        /// <summary>
        /// Forced Wait: Forces Selenium to wait a certain amound of seconds before doing anything &#8211; 
        /// Expects input in seconds
        /// </summary>
        /// <param name="wait"></param>
        public static void ForcedWait(int wait)
        {
            wait = wait * 1000; // Converts from milliseconds to seconds
            Thread.Sleep(wait);
        }

        /// <summary>
        /// Method that will rename the core log folder if it exists for archival purposes
        /// </summary>
        public static void LogCleaner()
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
        public static void MaximizeBrowser()
        {
            seleniumDriver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Quits all Selenium controlled browser processes 
        /// </summary>
        public static void QuitBrowsers()
        {
            seleniumDriver.Quit();
        }

        /// <summary>
        /// Method to set the current browser to test &#8211;
        /// Accepted values are &#8211; chrome &#8211; firefox &#8211; edge
        /// </summary>
        /// <param name="browser"></param>
        public static void SetBrowser(String browser)
        {
            // This sets the home directory for the automation framework so the Selenium drivers can be used
            // with in the framework package
            string homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(FrameworkCore)).Location);

            // Sets Selenium to use Chrome
            if (browser == "chrome")
            {
                seleniumDriver = new ChromeDriver($"{homeDirectory}\\Support");
            }

            // Sets Selenium to use Firefox
            if (browser == "firefox")
            {
                seleniumDriver = new FirefoxDriver($"{homeDirectory}\\Support");
            }

            // Sets Selenium to use Edge
            if (browser == "edge")
            {
                seleniumDriver = new EdgeDriver($"{homeDirectory}\\Support");
            }
        }
        
        /// <summary>
        /// Method to take a screen cap of the current browser state
        /// </summary>
        /// <param name="name"></param>
        public static void ScreenShot(string name)
        {
            DateTime date = DateTime.Today;
            var logLocation = @"c:\Automation Logs\" + date.ToString("MM.dd.yyyy") + @"\Screenshots";

            if (!Directory.Exists(logLocation))
            {
                Directory.CreateDirectory(logLocation);
            }

            Screenshot image = ((ITakesScreenshot)seleniumDriver).GetScreenshot();
            DateTime timeStamp = DateTime.Now;

            var filename = logLocation + "\\" + name + ".png";
            int counter = 2;

            while (File.Exists(filename))
            {
                filename = logLocation + "\\" + name + " - " + counter + ".png";
                counter++;
            }

            image.SaveAsFile(filename);
        }
    }
}