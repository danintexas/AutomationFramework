using System.Configuration;

namespace AutomationFramework.Pages.Classifieds
{
    public class Classifieds
    {
        public class Homepage
        {
            public static string homePageURL = "https://" + ConfigurationManager.AppSettings.Get("Env") + ".rumbleonclassifieds.com/";
            public static string homePageTitle = "Welcome to RumbleOn Classifieds Motorcycle Listing Site";
        }        
    }
}
