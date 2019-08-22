namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;
    using System;

    [Category("Classifieds Smoketest")]
    partial class SmokeTest : Core
    {
        [TestCase(TestName = "CLS-28 Create Listing Normal Flow")]
        [Order(4)]
        public void NormalListingflow()
        {
            // First page of listing flow
            Logger(Info, "Normal Listing Flow Test");
            ClickElement(JsonCall("RumbleOnClassifieds:Header:ListMyVehicle"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:LandingPage"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));
            ScreenShot("Listing Landing Page");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));

            // VIN entry page
            Logger(Info, "VIN entry page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ListingPage"));

            GenerateAVIN(Motorcycle);
            SendKeys(JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"), vinUnderTest);
            Wait(1);
            ScreenShot("Vin Entered");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:SubmitButton"));

            // VIN Found page
            Logger(Info, "VIN Found page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:VinFound"));
            ScreenShot("VIN Found Page");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));

            // Additional Information page
            Logger(Info, "Additional Information page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:AdditionalInfo"));
            SendKeys(JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"), "12345");  // Eventually need to randomize this
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:ColorPicker"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:ColorBlack"));
            SendKeys(JsonCall("RumbleOnClassifieds:ListingFlow:ListingDescription"), "This is created using automation technology");
            ScreenShot("Additional Information");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalInfoNext"));

            // Additional Questions page
            Logger(Info, "Additional Questions page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:AdditionalQuestions"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickElement(JsonCall(@"RumbleOnClassifieds:ListingFlow:VehicleOperated"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            Wait(1);
            ScreenShot("Additional Questions");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));
            Wait(2);

            // Rate Your Vehicle page
            Logger(Info, "Rate Your Vehicle page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:RateYourVehicle"));
            ScreenShot("Rate Your Vehicle");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));
            Wait(2);

            // Show Your Ride Off page
            // Need to add in photo upload - this can not be done with Selenium but can with C#
            Logger(Info, "Show Your Ride Off page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:ShowYourRideNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ShowOffYourRide"));

            // Below is proof of concept. This generates an error dialog with SendKeys. 
            // It will be corrected with Net Core 3.0 which supports SendKeys
            //////////////////////////////////////////////////////////////////////////////////////
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:RightSidePic"));
            Wait(5);
            AdditionalFunctions.PhotoSelection(0);
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:LeftSidePic"));
            Wait(1);
            AdditionalFunctions.PhotoSelection(0);
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:FrontSidePic"));
            Wait(1);
            AdditionalFunctions.PhotoSelection(0);
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:BackSidePic"));
            Wait(1);
            AdditionalFunctions.PhotoSelection(0);
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:RearTirePic"));
            Wait(1);
            AdditionalFunctions.PhotoSelection(0);
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:OdometerPic"));
            Wait(1);
            AdditionalFunctions.PhotoSelection(0);
            Wait(3);
            ScreenShot("Show Off Your Ride");
            //////////////////////////////////////////////////////////////////////////////////////

            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:ShowYourRideNextButton"));

            // Key Features of Your Ride page
            Logger(Info, "Key Features of Your Ride page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:MajorFactoryOptions"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:KeyFeatures"));
            SendKeys(JsonCall("RumbleOnClassifieds:ListingFlow:MajorFactoryOptions"), "This is created using automation technology");

            ScreenShot("Key Features Part I");

            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBox"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBlack"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBlack"));

            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInBox"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInSelectStateTX"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInSelectStateTX"));

            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:ABSBox"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:ABSBoxYes"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:ABSBoxYes"));

            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBox"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBoxYes"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBoxYes"));

            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusBox"));
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusClean"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusClean"));

            ScreenShot("Key Features Part II");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:KeyFeaturesNextButton"));

            // Confirm Email Page
            Logger(Info, "Confirm Email page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:SendNotificationsButton"));
            Wait(1);
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ConfirmEmail"));
            ScreenShot("Please Confirm Your Email");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:SendNotificationsButton"));

            // Set Price Page
            Logger(Info, "Set Price page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceBox"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:SetPrice"));
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceBox"));
            SendKeys(JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceBox"), "5000");
            ScreenShot("Price Set Page");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceButton"));

            // Editing & Review Page
            Logger(Info, "Editiing & Review page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:EditVehiclePricingButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:EditReview"));
            ShouldBe(GetFieldValue(JsonCall("RumbleOnClassifieds:ListingFlow:EditPageMileageBox")), "12,345");
            ScreenShot("Editing and Review Part I");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:EditPageDescription"));
            ScreenShot("Editing and Review Part II");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:EditPageSaveContinueButton"));

            // Listing Preview Page
            Logger(Info, "Listing Preview page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:ListingPreviewPublishButton"));
            ShouldBe(StripEndingUrl(SetUrl), JsonCall("RumbleOnClassifieds:Url:YourListingPreview"));
            Wait(1);
            ScreenShot("Your Listing Preview");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:ListingPreviewPublishButton"));

            // Congratulations Page
            Logger(Info, "Congratulations page Test");
            WaitForElement(JsonCall("RumbleOnClassifieds:ListingFlow:GoToMyGarageButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:CongratulationsPage"));
            ScreenShot("Congratulations Page");
            ClickElement(JsonCall("RumbleOnClassifieds:ListingFlow:GoToMyGarageButton"));
        }
    }
}
