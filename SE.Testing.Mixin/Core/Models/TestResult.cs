namespace IngameScript
{
    public class TestResult : ITestResult
    {
        public string TestName { get; }
        public bool Passed => string.IsNullOrEmpty(FailureMessage);
        public string FailureMessage { get; set; }

        public TestResult(string testName)
        {
            TestName = testName;
        }
    }
}