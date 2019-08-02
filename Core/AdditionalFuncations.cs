using NUnit.Framework;
using System;
using System.IO;

static class AdditionalFunctions
{
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
}

