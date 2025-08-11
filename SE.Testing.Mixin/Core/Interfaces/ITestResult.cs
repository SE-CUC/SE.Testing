namespace IngameScript
{
    public interface ITestResult
    {
        string TestName { get; }
        bool Passed { get; }
        string FailureMessage { get; }
    }
}