using AventStack.ExtentReports.Reporter;
using NUnit.Framework;

namespace AutomationFramework.Tests.ChromeTests 
{
    class HomepageIdentification : FrameworkCore
    {
        [TestCase(Category = "Classifieds (Chrome)", TestName = "Validate Homepage")]
        public void homepageChrome()
        {
            SetBrowser("chrome");

            driver.Url = testEnv;

            Assert.AreEqual(driver.Url, testEnv);
            Assert.AreEqual(driver.Title, "Welcome to RumbleOn Classifieds Motorcycle Listing Site");
        }
    }
}
