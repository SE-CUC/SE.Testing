using System.Collections.Generic;

namespace IngameScript
{
    public interface ITestReporter
    {
        void ReportSuiteStart(string suiteName);
        void ReportTestResult(ITestResult result);
        void ReportSuiteEnd(string suiteName, IEnumerable<ITestResult> results);
        void ReportFinalResults(IEnumerable<ITestResult> allResults);
    }
}