﻿namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    [Category("Classifieds Smoketest")]
    partial class SmokeTest : Core
    {
        [TestCase(Chrome, TestName = "CLS-28 Create Listing Normal Flow")]
        [Order(3)]
        public void StepTwo(string browser)
        {
            // First page of listing flow
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Header:ListMyVehicle"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:LandingPage"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));
            ScreenShot("Listing Landing Page - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));

            // VIN entry page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ListingPage"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"), JsonCall("RumbleOnClassifieds:Account:VIN")); // Eventually roll in VIN gen into framework
            Wait(1);
            ScreenShot("Vin Entered - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SubmitButton"));
            
            // VIN Found page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:VinFound"));
            ScreenShot("VIN Found Page - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));

            // Additional Information page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:AdditionalInfo"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"), "12345");  // Eventually need to randomize this
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ColorPicker"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ColorBlack"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ListingDescription"), "This is created using automation technology");
            ScreenShot("Additional Information - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalInfoNext"));

            // Additional Questions page
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:AdditionalQuestions"));
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickElement(CSS, JsonCall(@"RumbleOnClassifieds:ListingFlow:VehicleOperated"));
            WaitForElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            Wait(1);
            ScreenShot("Additional Questions - " + browser);
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));
            Wait(2);

            // Rate Your Vehicle page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:RateYourVehicle"));
            ScreenShot("Rate Your Vehicle - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));
            Wait(2);

            // Show Your Ride Off page
            // Need to add in photo upload - this can not be done with Selenium but can with C#
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ShowYourRideNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ShowOffYourRide"));
            ScreenShot("Show Off Your Ride - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ShowYourRideNextButton"));

            // Key Featurs of Your Ride
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MajorFactoryOptions"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:KeyFeatures"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MajorFactoryOptions"), "This is created using automation technology");

            ScreenShot("Key Features Part I - " + browser);

            ClickElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBlack"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBlack"));

            ClickElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInSelectStateTX"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInSelectStateTX"));

            ClickElement(CSS, JsonCall("RumbleOnClassifieds:ListingFlow:ABSBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ABSBoxYes"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ABSBoxYes"));

            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBoxYes"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBoxYes"));

            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusClean"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusClean"));

            ScreenShot("Key Features Part II - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:KeyFeaturesNextButton"));

            // Confirm Email Page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SendNotificationsButton"));
            Wait(1);
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:ConfirmEmail"));
            ScreenShot("Please Confirm Your Email - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SendNotificationsButton"));

            // Set Price Page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceBox"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:SetPrice"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceBox"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceBox"), "5000");
            ScreenShot("Price Set Page - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SetPriceButton"));

            // Editing & Review Page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:EditVehiclePricingButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:EditReview"));
            ShouldBe(GetFieldValue(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:EditPageMileageBox")), "12,345");
            ScreenShot("Editing and Review Part I - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:EditPageDescription"));
            ScreenShot("Editing and Review Part II - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:EditPageSaveContinueButton"));

            // Listing Preview Page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ListingPreviewPublishButton"));
            ShouldBe(StripEndingUrl(SetUrl), JsonCall("RumbleOnClassifieds:Url:YourListingPreview"));
            ScreenShot("Your Listing Preview - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ListingPreviewPublishButton"));

            // Congratulations Page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:GoToMyGarageButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:CongratulationsPage"));
            ScreenShot("Congratulations Page - " + browser);
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:GoToMyGarageButton"));
        }
    }
}
