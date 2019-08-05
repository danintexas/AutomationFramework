namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;

    partial class SmokeTest : Core
    {
        [TestCase(TestName = "CLS-27 Normal Account Signup")]
        [Order(2)]
        public void AccountCreation()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            Logger(Info, "Starting Account Creation test");
            SetUrl = JsonCall("RumbleOnClassifieds:Url:Homepage");
            WaitForElement(XPath, JsonCall("RumbleOnClassifieds:Header:Signup"));
            ScreenShot("Classifieds Homepage");
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:Homepage"));
            ShouldBe(Title, JsonCall("RumbleOnClassifieds:Homepage:Title"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Header:Signup"));

            WaitForElement(ID, JsonCall("RumbleOnClassifieds:Signup:FirstName"));
            ShouldBe(SetUrl, JsonCall("RumbleOnClassifieds:Url:CreateAnAccountPage"));
            ScreenShot("Classifieds Signup Page");
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:FirstName"), JsonCall("RumbleOnClassifieds:NewAccount:FirstName"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:LastName"), JsonCall("RumbleOnClassifieds:NewAccount:LastName"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:Email"), JsonCall("RumbleOnClassifieds:NewAccount:Email"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:ConfirmEmail"), JsonCall("RumbleOnClassifieds:NewAccount:ConfirmEmail"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:PhoneNumber"), JsonCall("RumbleOnClassifieds:NewAccount:PhoneNumber"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:StreetAddress"), JsonCall("RumbleOnClassifieds:NewAccount:StreetAddress"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:ZipCode"), JsonCall("RumbleOnClassifieds:NewAccount:ZipCode"));
            ClickElement(CSS, JsonCall("RumbleOnClassifieds:Signup:StateDropdown"));
            ClickElement(XPath, JsonCall("RumbleOnClassifieds:Signup:SelectStateAL"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:Password"), JsonCall("RumbleOnClassifieds:NewAccount:Password"));
            SendKeys(ID, JsonCall("RumbleOnClassifieds:Signup:ConfirmPassword"), JsonCall("RumbleOnClassifieds:NewAccount:ConfirmPassword"));


            Wait(3);
            ScreenShot("Classifieds Signup Page - After Fill in");

        }
    }
}
