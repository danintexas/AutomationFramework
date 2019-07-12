using AventStack.ExtentReports.Reporter;
using NUnit.Framework;

namespace AutomationFramework.Tests
{
    class DemoTests : FrameworkCore
    {
        [TestCase (Category = "Demo", TestName = "1 - Validate Homepage - Chrome")]
        [Order(1)]
        public void homepageChrome()
        {
            SetBrowser("chrome");

            driver.Url = testEnv;

            Assert.AreEqual(driver.Url, testEnv);
            Assert.AreEqual(driver.Title, "Welcome to RumbleOn Classifieds Motorcycle Listing Site");

            //var reporter = new ExtentHtmlReporter("path/to/directory/");
        }
 
        [TestCase(Category = "Demo", TestName = "2 - Validate HomePage - Firefox")]
        [Order(2)]
        public void homepageFirefox()
        {
            SetBrowser("firefox");

            driver.Url = testEnv;

            Assert.AreEqual(driver.Url, testEnv);
            Assert.AreEqual(driver.Title, "Welcome to RumbleOn Classifieds Motorcycle Listing Site");

            //var reporter = new ExtentHtmlReporter("path/to/directory/");
        }

        [TestCase(Category = "Demo", TestName = "3 - Validate HomePage - Edge")]
        [Order(3)]
        public void homepageEdge()
        {
            SetBrowser("edge");

            driver.Url = testEnv;

            Assert.AreEqual(driver.Url, testEnv);
            Assert.AreEqual(driver.Title, "Welcome to RumbleOn Classifieds Motorcycle Listing Site");

            //var reporter = new ExtentHtmlReporter("path/to/directory/");
        }

        [TestCase(Category = "Negative Test", TestName = "Validate Homepage - Chrome - Negative Test")]
        [Order(1)]
        public void homepageChromeNeg()
        {
            SetBrowser("chrome");

            driver.Url = testEnv;

            Assert.AreEqual(driver.Url, "Fail");
            Assert.AreEqual(driver.Title, "Welcome to RumbleOn Classifieds Motorcycle Listing Site");

            var reporter = new ExtentHtmlReporter("path/to/directory/");
        }
    }
}
