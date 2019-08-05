﻿namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    partial class SmokeTest : Core
    {
        [TestCase(TestName = "CLS-27 Normal Account Signup")]
        [Order(2)]
        public void AccountCreation()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            Logger(Info, "Starting Account Creation test");
            SetUrl = JsonCall("RumbleOnClassifieds:Url:Homepage");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Header:Signup"));
            ScreenShot("Classifieds Homepage");
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:Homepage"));
            ShouldBe(Title, JsonCall("RumbleOnClassifieds:Homepage:Title"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Header:Signup"));

            WaitForElement(ID, JsonCall("RumbleOnClassifieds:Signup:FirstName"));
            ScreenShot("Classifieds Signup Page");
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:FirstName"), "Test Name");
            Wait(1);
            ScreenShot("Classifieds Signup Page - After Fill in");

        }
    }
}