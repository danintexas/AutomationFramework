namespace AutomationFramework.Tests.Test_Template  // Good idea to change the namespace from Test_Template to something appropriate
{
    using NUnit.Framework;

    [Category("Category Name")] // Change the Category Name to something more appropriate
    partial class TestName : Core // Instead of TestName name the class something more descriptive
    {
        [TestCase(TestName = "Test Name")] // Change the Test Name to something appropriate
        [Order(1)]
        public void TestMethod() // Method can also be renamed to something more descriptive
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
