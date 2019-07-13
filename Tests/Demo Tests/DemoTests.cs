using AutomationFramework.Pages.Classifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class DemoTests : FrameworkCore
    {
        [TestCase (Category = "Demo", TestName = "1 - Validate Homepage - Chrome")][Order(1)]

        public void homepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);
        }
 
        [TestCase(Category = "Demo", TestName = "2 - Validate HomePage - Firefox")][Order(2)]
        public void homepageFirefox()
        {
            SeleniumCommands.SetBrowser("firefox");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);
        }

        [TestCase(Category = "Demo", TestName = "3 - Validate HomePage - Edge")][Order(3)]
        public void homepageEdge()
        {
            SeleniumCommands.SetBrowser("edge");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);
        }

        [TestCase(Category = "Negative Test", TestName = "Validate Homepage - Chrome - Negative Test")][Order(4)]
        public void homepageChromeNeg()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();

            driver.Url = Classifieds.Homepage.homePageURL;

            Assert.AreNotEqual(driver.Url, "This will fail");
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);
        }
    }
}
