namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    partial class SmokeTest : Core
    {
        [TestCase(TestName = "CLS-27 Normal Account Signin")]
        [Order(3)]
        public void NormalAccountLogin()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            Logger(Info, "Normal Account Login Test");
            SetUrl = JsonCall("RumbleOnClassifieds:Url:Homepage");
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Header:LoginButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:LoginPage"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            ScreenShot("After clicking Login on Homepage");

            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Email"), JsonCall("RumbleOnClassifieds:Account:User"));
            SendKeys(XPath, JsonCall("RumbleOnClassifieds:Loginpage:Password"), JsonCall("RumbleOnClassifieds:Account:Password"));
            ScreenShot("Filled in email and password");

            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Loginpage:LoginButton"));
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Header:MyGarage"));
            ScreenShot("Homepage being logged in");
        }
    }
}
