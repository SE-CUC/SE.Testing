using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
    public static class TestingExtension
    {
        public static SEApplicationBuilder AddTesting(this SEApplicationBuilder builder, IEnumerable<ITestProvider> tests = null, ITestReporter reporter = null)
        {          
            builder.Services.AddSingleton<ITestReporter>(sp => 
            {
                if(reporter != null)
                    return reporter;

                return new LogReporter(sp.GetService<ILogger>());            
            });
            builder.Services.AddSingleton<ITestRunner>(sp => 
            {
                var runner = new TestRunner(sp.GetService<ITestReporter>());
                if (tests != null)
                {
                    foreach (var provider in tests)
                    {
                        runner.AddProvider(provider);
                    }
                }

                return runner;
            });

            return builder;
        }

        public static SEApplicationBuilder AddTesting(this SEApplicationBuilder builder, Func<SEApplicationBuilder, SEApplicationBuilder> func)
            => func(builder);
    }
}
