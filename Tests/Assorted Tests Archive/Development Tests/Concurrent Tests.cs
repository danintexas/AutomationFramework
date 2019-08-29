namespace AutomationFramework.Tests.Development  
{
    using NUnit.Framework;

    [Category("Development Tes")] 
    partial class Development : Core 
    {
        [TestCase(TestName = "Concurrent Tests")] 
        [Order(1)] 
        [Repeat(5)]
        [Parallelizable(ParallelScope.Children)]
        public void ConcurrentTests() 
        {
            Logger(Info, "Starting Test Template Test");
            Wait(2);
            Logger(Info, "Waited 2 sec");
        }

        [TestCase(TestName = "Concurrent Tests 2")]
        [Order(1)]
        [Repeat(5)]
        [Parallelizable(ParallelScope.Children)]
        public void ConcurrentTests2()
        {
            Logger(Info, "Starting Test Template Test 2");
            Wait(2);
            Logger(Info, "Waited 2 sec");
        }
    }
}
