namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;
    partial class SmokeTest : Core
    {
        [TestCase(TestName = "Delete Listing")]
        [Order(99)]
        [Repeat(1)]
        public void DeleteListing()
        {
            Logger(Info, "Delete Listing Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:MyGarage:DeleteButton"));
            ScreenShot("My Garage Before Delete");
            Wait(1);
            ClickElement(JsonCall("RumbleOnClassifieds:MyGarage:DeleteButton"));
            WaitForElement(JsonCall("RumbleOnClassifieds:MyGarage:ConfirmDeleteListing"));
            ClickElement(JsonCall("RumbleOnClassifieds:MyGarage:ConfirmDeleteListing"));
            Wait(2);
            ScreenShot("My Garage After Delete");
        }
    }
}
