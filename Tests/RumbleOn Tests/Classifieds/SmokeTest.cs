namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;
    using OpenQA.Selenium;

    [Category("Classifieds Smoketest")]
    class SmokeTest : Core
    {
        [TestCase(Chrome, TestName = "Validate Classifieds Homepage")]
        [Order(1)]
        public void HomepageVerification(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = JsonCall("RumbleOnClassifieds:Homepage:Url");
            ScreenShot("Classifieds Homepage - " + browser);
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Homepage:Url"));
            ShouldBe(Title, JsonCall("RumbleOnClassifieds:Homepage:Title"));
        }

        [TestCase(Chrome, TestName = "CLS-27 Normal Account Signup/Signin")]
        [Order(2)]
        public void StepOne(string browser)
        {
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:Header:LoginButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Loginpage:Url"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            ScreenShot("After clicking Login - " + browser);

            ClickButton(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"), JsonCall("RumbleOnClassifieds:Account:User"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Password"), JsonCall("RumbleOnClassifieds:Account:Password"));
            ScreenShot("Filled in email and password - " + browser);

            ClickButton(XPath, JsonCall("RumbleOnClassifieds:Loginpage:LoginButton"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Header:MyGarage"));
            ScreenShot("Homepage being logged in - " + browser);
        }

        [TestCase(Chrome, TestName = "CLS-28 Create Listing Normal Flow")]
        [Order(3)]
        public void StepTwo(string browser)
        {
            // First page of listing flow
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:Header:ListMyVehicle"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlLandingPage"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));
            ScreenShot("Listing Landing Page - " + browser);
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ContinueButton"));

            // VIN entry page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlListingPage"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinEntry"), JsonCall("RumbleOnClassifieds:Account:VIN")); // Eventually roll in VIN gen into framework
            Wait(1);
            ScreenShot("Vin Entered - " + browser);
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SubmitButton"));

            // VIN Found page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlVinFound"));
            ScreenShot("VIN Found Page - " + browser);
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VinFoundButton"));

            // Additional Information page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlAdditionalInfo"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MileageEntry"), "12345");  // Eventually need to randomize this
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ColorPicker"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ColorBlack"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ListingDescription"), "This is created using automation technology");
            ScreenShot("Additional Information - " + browser);
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalInfoNext"));

            // Additional Questions page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlAdditionalQuestions"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsDamage")); 
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:DamageNo"));
            ClickButton(XPath, JsonCall(@"RumbleOnClassifieds:ListingFlow:VehicleOperated"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:VehicleOperatedYes"));
            Wait(1);
            ScreenShot("Additional Questions - " + browser);
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:AdditionalQuestionsNext"));

            // Rate Your Vehicle page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlRateYourVehicle"));
            ScreenShot("Rate Your Vehicle - " + browser);
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:RateNextButton"));

            // Show Your Ride Off page
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ShowYourRideNextButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlShowOffYourRide"));
            ScreenShot("Show Off Your Ride - " + browser);
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ShowYourRideNextButton"));

            // Key Featurs of Your Ride
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MajorFactoryOptions"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:ListingFlow:UrlKeyFeatures"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:MajorFactoryOptions"), "This is created using automation technology");

            ScreenShot("Key Features Part I - " + browser);

            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBlack"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:SecondaryColorBlack"));

            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInSelectState"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:StateTitledInSelectState"));
            
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ABSBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ABSBoxYes"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:ABSBoxYes"));

            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBoxYes"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:WarrantyBoxYes"));

            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusBox"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusClean"));
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:TitleStatusClean"));

            ScreenShot("Key Features Part II - " + browser);
            ClickButton(XPath, JsonCall("RumbleOnClassifieds:ListingFlow:KeyFeaturesNextButton"));
        }
    }
}
