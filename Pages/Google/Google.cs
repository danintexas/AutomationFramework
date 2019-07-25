// Example here is of having URLs and locators for each page in a seperate file. 
// Traditionally I would run everything through the appsettings.json file however. 
// This way you could have seperate config files for every page/project/environment.

namespace AutomationFramework.Pages.Google
{
    class Google
    {
        public class Homepage
        {
            public static string homePageURL = "https://www.google.com/";
            public static string homePageTitle = "Google";
        }
    }
}
