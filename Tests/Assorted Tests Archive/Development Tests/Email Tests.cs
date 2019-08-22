using NUnit.Framework;
namespace AutomationFramework.Tests.Development_Tests
{
    [Category("Dev Tests")]
    partial class CheckPOP3EmailTests : Core
    {
        [TestCase(TestName = "Check Email")]
        [Order(99)]
        
        public void Test()
        {
            GetAllEmailsFromAnEmailAccount();
            ParseAllEmailFilesForAStringValue("This is a Classifieds report test");
            ParseAllEmailFilesForAStringValue("This is a Classifieds Message test");
            ParseAllEmailFilesForAStringValue("This is a Classifieds Listing post email");
            ParseAllEmailFilesForAStringValue("This will fail");
        }       
    }
}