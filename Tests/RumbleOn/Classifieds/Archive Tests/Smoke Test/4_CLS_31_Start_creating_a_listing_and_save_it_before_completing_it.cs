namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    partial class SmokeTest : Core
    {
        [TestCase(TestName = "CLS-31 Start creating a listing and save it before completing it")]
        [Order(5)]
        public void PartialListingFlow()
        {
            // First page of listing flow
            Logger(Info, "Normal Listing Flow Test");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Header:ListMyVehicle"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:LandingPage"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));
            ScreenShot("Listing Landing Page");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));

            // VIN entry page
            Logger(Info, "VIN entry page Test");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ListingPage"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"), JsonCall("RumbleOnClassifieds:Account:VIN2")); // Eventually roll in VIN gen into framework
            Wait(1);
            ScreenShot("Vin Entered");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SubmitButton"));

            // VIN Found page
            Logger(Info, "VIN Found page Test");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:VinFound"));
            ScreenShot("VIN Found Page");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));

            // Additional Information page
            Logger(Info, "Additional Information page Test");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:AdditionalInfo"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"), "12345");  // Eventually need to randomize this
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ColorPicker"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ColorBlack"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ListingDescription"), "This is created using automation technology");
            ScreenShot("Additional Information");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalInfoNext"));

            // Additional Questions page
            Logger(Info, "Additional Questions page Test");
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:AdditionalQuestions"));
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickElement(CSS, JsonCall(@"RumbleOnClassifieds:ListingFlow:VehicleOperated"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            Wait(1);
            ScreenShot("Additional Questions");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));
            Wait(2);

            // Rate Your Vehicle page
            Logger(Info, "Rate Your Vehicle page Test");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:RateYourVehicle"));
            ScreenShot("Rate Your Vehicle");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));
            Wait(2);

            // Show Your Ride Off page
            Logger(Info, "Show Your Ride Off page Test");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ShowYourRideNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ShowOffYourRide"));
            Wait(1);

            // Kill flow and move to My Garage
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Header:MyGarage"));
            Logger(Info, "Ending Normal Flow and checking My Garage");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Header:MyGarage"));
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:MyGarage:DeleteButton"));
            ScreenShot("Current My Garage Page");
            Wait(1);
        }
    }
}
