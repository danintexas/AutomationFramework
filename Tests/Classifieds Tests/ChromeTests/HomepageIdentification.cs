using AutomationFramework.Pages.Classifieds;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests
{
    class HomepageIdentification : FrameworkCore
    {
        [TestCase(Category = "Classifieds (Chrome)", TestName = "Validate Homepage")]
        public void homepageChrome()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.maximizeBrowser();
            driver.Url = Classifieds.Homepage.homePageURL;

            Assert.AreEqual(driver.Url, Classifieds.Homepage.homePageURL);
            Assert.AreEqual(driver.Title, Classifieds.Homepage.homePageTitle);
        }
    }
}
