using AutomationFramework;
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
        /// Maximizes the browser window
        /// </summary>
        public static void maximizeBrowser()
        {
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Closes active Selenium controlled browser
        /// </summary>
        public static void closeBrowser()
        {
            driver.Close();
        }

        /// <summary>
        /// Quits all Selenium controlled browser processes 
        /// </summary>
        public static void quitBrowsers()
        {
            driver.Quit();
        }

        /// <summary>
        /// Closes active Selenum controlled browser then ends all Selenium controlled browser processes
        /// </summary>
        public static void closeQuitBrowsers()
        {
            driver.Close();
            driver.Quit();
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
                driver = new ChromeDriver($"{homeDirectory}\\Support");
            }

            // Sets Selenium to use Firefox
            if (browser == "firefox")
            {
                driver = new FirefoxDriver($"{homeDirectory}\\Support");
            }

            // Sets Selenium to use Edge
            if (browser == "edge")
            {
                driver = new EdgeDriver($"{homeDirectory}\\Support");
            }
        }

        /// <summary>
        /// Forced Wait: Forces Selenium to wait a certain amound of seconds before doing anything &#8211; 
        /// Expects input in seconds
        /// </summary>
        /// <param name="wait"></param>
        public static void ForcedWait (int wait)
        {
            wait = wait * 1000; // Converts from milliseconds to seconds
            Thread.Sleep(wait);
        }

        /// <summary>
        /// Method to take a screen cap of the current browser state
        /// </summary>
        /// <param name="name"></param>
        public static void ScreenShot (string name)
        {
            DateTime date = DateTime.Today;               
            var logLocation = @"c:\Automation Logs\" + date.ToString("MM.dd.yyyy") + @"\Screenshots";

            if (!Directory.Exists(logLocation))
            {
                Directory.CreateDirectory(logLocation);
            }

            Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
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
    }
}