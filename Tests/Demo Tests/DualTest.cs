﻿using AutomationFramework.Pages.RumbleOnClassifieds;
using AutomationFramework.Pages.Google;
using NUnit.Framework;
using Selenium;

namespace AutomationFramework.Tests.Demo
{
    class DualTest : FrameworkCore
    {
        [TestCase(Category = "Dual Test", TestName = "Validate Classifieds Homepage - Chrome")]
        [Order(1)]
        public void classifiedsHomePage()
        {
            SeleniumCommands.SetBrowser("chrome");
            SeleniumCommands.MaximizeBrowser();

            seleniumDriver.Url = RumbleOnClassifieds.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Dual Test - Classifieds");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, RumbleOnClassifieds.Homepage.homePageURL);
        }

        [TestCase(Category = "Dual Test", TestName = "Validate Google Homepage - Chrome")]
        [Order(2)]
        public void googleHomePage()
        {
            seleniumDriver.Url = Google.Homepage.homePageURL;

            SeleniumCommands.ForcedWait(2);
            SeleniumCommands.ScreenShot("Dual Test - Google");

            SeleniumCommands.AssertEqual(seleniumDriver.Url, Google.Homepage.homePageURL);

            SeleniumCommands.CloseQuitBrowsers();
        }

    }
}
