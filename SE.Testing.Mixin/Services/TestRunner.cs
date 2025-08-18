using System;
using System.Collections.Generic;

namespace IngameScript
{
    /// <remarks>
    /// Version: 1.0
    /// Author: @AIBaster & Gemini
    /// Repository: https://github.com/SE-CUC/SE.Testing
    /// </remarks>
    public class TestRunner : ITestRunner
    {
        private readonly ITestReporter _reporter;
        private readonly List<ITestProvider> _providers = new List<ITestProvider>();

        public TestRunner(ITestReporter reporter)
        {
            if (reporter == null)
                throw new ArgumentNullException(nameof(reporter));

            _reporter = reporter;
        }

        public void AddProvider(ITestProvider provider)
        {
            if (provider != null)
                _providers.Add(provider);
        }

        public void AddProvider(params ITestProvider[] providers)
        {
            foreach (var provider in providers)
            {
                AddProvider(provider);
            }
        }

        public IEnumerator<bool> RunAll()
        {
            var allResults = new List<ITestResult>();
            foreach (var provider in _providers)
            {
                var providerName = provider.GetType().Name;
                _reporter.ReportSuiteStart(providerName);
                var providerResults = new List<ITestResult>();

                foreach (var test in provider.GetTests())
                {
                    var result = RunSingleTest(test.Name, test.Body);
                    providerResults.Add(result);
                    allResults.Add(result);
                    _reporter.ReportTestResult(result);
                    yield return true;
                }
                _reporter.ReportSuiteEnd(providerName, providerResults);
            }
            _reporter.ReportFinalResults(allResults);
        }
        
        private ITestResult RunSingleTest(string testName, Action testAction)
        {
            var result = new TestResult(testName);
            try
            {
                testAction();
            }
            catch (Exception ex)
            {
                var innerEx = ex.InnerException ?? ex;
                if (innerEx is AssertionException)
                {
                    result.FailureMessage = innerEx.Message;
                }
                else
                {
                    result.FailureMessage = $"Unexpected exception: {innerEx.GetType().Name} - {innerEx.Message}";
                }
            }
            return result;
        }
    }
}