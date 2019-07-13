using AutomationFramework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Reflection;

namespace Selenium
{
    public class SeleniumCommands : AutomationFramework.FrameworkCore
    {
        // Maximizes the browser window
        public static void maximizeBrowser()
        {
            driver.Manage().Window.Maximize();
        }

        // Closes active Selenium controlled browser
        public static void closeBrowser()
        {
            driver.Close();
        }

        // Quits all Selenium controlled browser processes 
        public static void quitBrowsers()
        {
            driver.Quit();
        }

        // Closes active Selenum controlled browser then ends all Selenium controlled browser processes
        public static void closeQuitBrowsers()
        {
            driver.Close();
            driver.Quit();
        }

        // Method to set the current browser to test
        public static void SetBrowser(String browser)
        {
            // This sets the home directory for the automation framework so the Selenium drivers can be used
            // with in the framework package
            var homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(FrameworkCore)).Location);

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
    }
}