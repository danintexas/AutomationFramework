namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    [Category("Classifieds Smoketest")]
    class SmokeTest : Core
    {
        [TestCase(Chrome, TestName = "Validate Classifieds Homepage")]
        [Order(1)]
        public void HomepageVerification(string browser)
        {
            UseBrowser(browser);
            MaximizeBrowser();
            SetUrl = _config["RumbleOnClassifieds:Homepage:Url"];
            ScreenShot("Classifieds Homepage - " + browser);
            ShouldBe(SetUrl, _config["RumbleOnClassifieds:Homepage:Url"]);
            ShouldBe(Title, _config["RumbleOnClassifieds:Homepage:Title"]);
        }

        [TestCase(Chrome, TestName = "CLS-27 Normal Account Signup/Signin")]
        [Order(2)]
        public void StepOne(string browser)
        {
            ClickButton(XPath, _config["RumbleOnClassifieds:Header:LoginButton"]);
            ShouldBe(SetUrl, _config["RumbleOnClassifieds:Loginpage:Url"]);
            WaitForElement(XPath, _config["RumbleOnClassifieds:Loginpage:Email"]);
            ScreenShot("After clicking Login - " + browser);

            ClickButton(XPath, _config["RumbleOnClassifieds:Loginpage:Email"]);
            SendKeys(XPath, _config["RumbleOnClassifieds:Loginpage:Email"], _config["RumbleOnClassifieds:Account:User"]);
            SendKeys(XPath, _config["RumbleOnClassifieds:Loginpage:Password"], _config["RumbleOnClassifieds:Account:Password"]);
            ScreenShot("Filled in email and password - " + browser);

            ClickButton(XPath, _config["RumbleOnClassifieds:Loginpage:LoginButton"]);
            WaitForElement(XPath, _config["RumbleOnClassifieds:Header:MyGarage"]);
            ScreenShot("Homepage being logged in - " + browser);
        }

        [TestCase(Chrome, TestName = "CLS-28 Create Listing Normal Flow")]
        [Order(3)]
        public void StepTwo(string browser)
        {
            ClickButton(XPath, _config["RumbleOnClassifieds:Header:ListMyVehicle"]);
            ShouldBe(SetUrl, _config["RumbleOnClassifieds:ListingFlow:UrlLandingPage"]);
            WaitForElement(XPath, _config["RumbleOnClassifieds:ListingFlow:ContinueButton"]);
            ScreenShot("Listing Landing Page - " + browser);
        }
    }
}
