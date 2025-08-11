using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IngameScript
{
    public class LogReporter : ITestReporter
    {
        private readonly ILogger _logger;
        private readonly StringBuilder _reportBuilder = new StringBuilder();

        public LogReporter(ILogger logger)
        {
            if(logger == null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        public void ReportSuiteStart(string suiteName)
        {
            _reportBuilder.AppendLine($"\n--- Running Suite: {suiteName} ---");
        }

        public void ReportTestResult(ITestResult result)
        {
            if (result.Passed)
            {
                _reportBuilder.AppendLine($"  [PASS] {result.TestName}");
            }
            else
            {
                _reportBuilder.AppendLine($"  [FAIL] {result.TestName}");
                _reportBuilder.AppendLine($"    - {result.FailureMessage}");
            }
        }

        public void ReportSuiteEnd(string suiteName, IEnumerable<ITestResult> results)
        {
            var passedCount = results.Count(r => r.Passed);
            var failedCount = results.Count() - passedCount;
            _reportBuilder.AppendLine($"--- Suite Finished: {passedCount} passed, {failedCount} failed ---");
        }

        public void ReportFinalResults(IEnumerable<ITestResult> allResults)
        {
            var passedCount = allResults.Count(r => r.Passed);
            var failedCount = allResults.Count() - passedCount;
            var total = allResults.Count();

            _reportBuilder.Insert(0, $"Test Run Finished\nTotal: {total}, Passed: {passedCount}, Failed: {failedCount}\n");

            _logger.Debug(_reportBuilder.ToString());

            _reportBuilder.Clear();
        }
    }
}