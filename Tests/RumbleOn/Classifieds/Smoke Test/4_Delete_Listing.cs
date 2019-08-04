namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;
    partial class SmokeTest : Core
    {
        [TestCase(Chrome, TestName = "Delete Listing")]
        [Order(5)]
        public void Delete(string browser)
        {
            Logger(Info, "Delete Listing Test");
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:DeleteButton"));
            ScreenShot("My Garage Before Delete");
            Wait(1);
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:DeleteButton"));
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:ConfirmDeleteListing"));
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:ConfirmDeleteListing"));
            Wait(1);
            ScreenShot("My Garage After Delete");

        }
    }
}
