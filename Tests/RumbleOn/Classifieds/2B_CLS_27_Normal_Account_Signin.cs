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
            ClickElement(JsonCall("RumbleOnClassifieds:Header:LoginButton"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:LoginPage"));
            WaitForElement(JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            ScreenShot("After clicking Login on Homepage");

            ClickElement(JsonCall("RumbleOnClassifieds:Loginpage:Email"));
            SendKeys(JsonCall("RumbleOnClassifieds:Loginpage:Email"), JsonCall("RumbleOnClassifieds:Account:User"));
            SendKeys(JsonCall("RumbleOnClassifieds:Loginpage:Password"), JsonCall("RumbleOnClassifieds:Account:Password"));
            ScreenShot("Filled in email and password");

            ClickElement(JsonCall("RumbleOnClassifieds:Loginpage:LoginButton"));
            WaitForElement(JsonCall("RumbleOnClassifieds:Header:MyGarage"));
            ScreenShot("Homepage being logged in");
        }
    }
}
