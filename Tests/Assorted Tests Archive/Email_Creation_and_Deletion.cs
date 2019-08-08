namespace AutomationFramework.Tests.Classifieds.Smoke
{
    using NUnit.Framework;
    [Category("Web Server Tests")]
    partial class SmokeTest : Core
    {
        [TestCase(TestName = "Create Email Account on Personal Webserver")]
        [Order(1)]
        public void CreateEmail()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            SetUrl = JsonCall("HostGator:Homepage:URL");
            WaitForElement(ID, JsonCall("HostGator:Homepage:UserName"));
            SendKeys(ID, JsonCall("HostGator:Homepage:UserName"), JsonCall("HostGator:Account:User"));
            SendKeys(ID, JsonCall("HostGator:Homepage:Password"), JsonCall("HostGator:Account:Password"));
            ClickElement(XPath, JsonCall("HostGator:Homepage:LoginButton"));

            Wait(1);
            ClickElement(CSS, JsonCall("HostGator:EmailPage:EmailButton"));
            Wait(1);
            SendKeys(ID, JsonCall("HostGator:EmailPage:EmailAccount"), "123456");
            Wait(1);
            SendKeys(XPath, JsonCall("HostGator:EmailPage:Password"), "Rumbleon12");
            Wait(1);
            SendKeys(XPath, JsonCall("HostGator:EmailPage:ReenterPassword"), "Rumbleon12");
            Wait(2);
            ClickElement(ID, JsonCall("HostGator:EmailPage:CreateButton"));
        }

        [TestCase(TestName = "Delete Email Account on Personal Webserver")]
        [Order(2)]
        public void DeleteEmail()
        {
            UseBrowser(JsonCall("FrameworkConfiguration:Browser"));
            MaximizeBrowser();
            SetUrl = JsonCall("HostGator:Homepage:URL");
            WaitForElement(ID, JsonCall("HostGator:Homepage:UserName"));
            SendKeys(ID, JsonCall("HostGator:Homepage:UserName"), JsonCall("HostGator:Account:User"));
            SendKeys(ID, JsonCall("HostGator:Homepage:Password"), JsonCall("HostGator:Account:Password"));
            ClickElement(XPath, JsonCall("HostGator:Homepage:LoginButton"));

            Wait(1);
            ClickElement(CSS, JsonCall("HostGator:EmailPage:EmailButton"));
            Wait(1);
            SendKeys(ID, JsonCall("HostGator:EmailPage:SearchField"), "123456@danintexas.com");
            Wait(1);
            ClickElement(ID, JsonCall("HostGator:EmailPage:SearchButton"));
            Wait(1);
            ClickElement(XPath, JsonCall("HostGator:EmailPage:DeleteButton"));
            Wait(2);
            ClickElement(ID, JsonCall("HostGator:EmailPage:DeleteConfirmationButton"));
        }
    }
}
