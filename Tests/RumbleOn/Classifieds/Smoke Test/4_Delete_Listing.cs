namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;
    partial class SmokeTest : Core
    {
        [TestCase(TestName = "Delete Listing")]
        [Order(5)]
        public void Delete()
        {
            Logger(Info, "Delete Listing Test");
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:DeleteButton"));
            ScreenShot("My Garage Before Delete");
            Wait(1);
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:DeleteButton"));
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:ConfirmDeleteListing"));
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:ConfirmDeleteListing"));
            Wait(2);
            ScreenShot("My Garage After Delete");

        }
    }
}
