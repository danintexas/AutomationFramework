using System.Configuration;

namespace AutomationFramework.Pages.Consumer
{
    public class Consumer
    {
        public class Homepage
        {
            public static string homePageURL = "https://v3" + ConfigurationManager.AppSettings.Get("Env") + "consumerweb.rumbleon.com/";
            public static string homePageTitle = "Welcome to RumbleOn Vehicle Marketplace";
        }
    }
}
