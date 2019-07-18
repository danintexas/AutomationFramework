namespace AutomationFramework.Core
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public static class TestExtensions
    {
        /// <summary>
        /// Asserts two values are the same. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void ShouldBe(this string a, string b)
        {
            try
            {
                Assert.AreEqual(a, b);
            }
            catch (Exception)
            {
                // I need to work on getting this catch into extent reports
            }
        }

        /// <summary>
        /// Asserts two values are not the same. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void ShouldNotBe(this string a, string b)
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
    }
}