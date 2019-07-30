namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;
    partial class SmokeTest : Core
    {
        [TestCase(Chrome, TestName = "CLS-27 Normal Account Signup/Signin")]
        [Order(2)]
        public void StepOne(string browser)
        {
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Header:LoginButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:LoginPage"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            ScreenShot("After clicking Login - " + browser);

            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"), JsonCall("RumbleOnClassifieds:Account:User"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Password"), JsonCall("RumbleOnClassifieds:Account:Password"));
            ScreenShot("Filled in email and password - " + browser);

            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Loginpage:LoginButton"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Header:MyGarage"));
            ScreenShot("Homepage being logged in - " + browser);
        }
    }
}
