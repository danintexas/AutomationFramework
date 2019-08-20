namespace AutomationFramework.Tests.Test_Template  // Good idea to change the namespace from Test_Template to something appropriate
{
    using NUnit.Framework;

    [Category("Category Name")] // Change the Category Name to something more appropriate
    partial class TestName : Core // Instead of TestName name the class something more descriptive. Make sure to leave the ': Core' otherwise the test won't be linked to the Core
    {
        [TestCase(TestName = "Test Name")] // Change the Test Name to something appropriate
        [Order(1)] // You can have multiple TestCases. Use this entry to run them in a specific order.
        public void TestMethod() // Method can also be renamed to something more descriptive
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser")); // Looks at the 'FrameworkConfiguration:Browser' setting in the appsettings.json for what browser to use
            MaximizeBrowser(); // Put the browser into fullscreen
            Logger(Info, "Starting Test Template Test"); // Logs the text into Extent reports as an Info entry. 
            SetUrl = "https://www.google.com"; // Tells the controlled browser to go to a specific url. You can use the JsonCall method to not hardcode your tests.
            ShouldBe(SetUrl, "https://www.google.com/"); // Calls a method to ensure the browser is where it should be. You can use the JsonCall method to not hardcode your tests.
            ShouldBe(Title, "Google"); // Ensures that the browsers Title is as expected. You can use the JsonCall method to not hardcode your tests.
            ScreenShot("Google Homepage"); // Takes a screenshot of the controlled browser and saves it as the text in the string field. This will also be added inline to Extent reports
        }
    }
}
