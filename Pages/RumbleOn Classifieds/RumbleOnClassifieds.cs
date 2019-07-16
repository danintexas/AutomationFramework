using System.Configuration;

namespace AutomationFramework.Pages.RumbleOnClassifieds
{
    public class RumbleOnClassifieds
    {
        public class Homepage
        {
            public static string homePageURL = "https://" + ConfigurationManager.AppSettings.Get("Env") + ".rumbleonclassifieds.com/";
            public static string homePageTitle = "Welcome to RumbleOn Classifieds Motorcycle Listing Site";
        }        
    }
}
