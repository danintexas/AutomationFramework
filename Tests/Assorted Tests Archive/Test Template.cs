﻿namespace AutomationFramework.Tests.Test_Template
{
    using NUnit.Framework;
    using System;

    [Category("Category Name")]
    partial class TestName : Core
    {
        [TestCase(TestName = "Test Name")]
        [Order(1)]
        public void TestMethod()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            Logger(Info, "Starting Test Template Test");
            SetUrl = "https://www.google.com";
            ScreenShot("Google Homepage");
            ShouldBe(SetUrl, "https://www.google.com/");
            ShouldBe(Title, "Google");
        }
    }
}
