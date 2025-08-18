using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace IngameScript
{
    public static class TestingExtensions
    {
        public static SEApplicationBuilder AddTesting(this SEApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ITestReporter>(sp => new LogReporter(sp.GetService<ILogger>()));
            builder.Services.AddSingleton<ITestRunner>(sp => new TestRunner(sp.GetService<ITestReporter>()));
            return builder;
        }

        public static IServiceCollection AddTestProvider<T>(this IServiceCollection services, Func<IServiceProvider, T> factory)
            where T : class, ITestProvider
        {
            services.AddSingleton<T>(factory);
            services.AddSingleton<ITestProvider>(sp => sp.GetService<T>());
            return services;
        }
    }

    public class TestCommandModule : ICommandModule
    {
        private readonly IServiceProvider _serviceProvider;

        public TestCommandModule(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<ICommand> GetCommands()
        {
            yield return new Command("run_tests", "Usage: run_tests", RunTestsAction);
        }

        private void RunTestsAction(string[] args)
        {
            var scheduler = _serviceProvider.GetService<ISchedulerService>();
            var testRunner = _serviceProvider.GetService<ITestRunner>();
            //TODO: Test it better
            var testProvider = ((ServiceProvider)_serviceProvider).GetService<ITestProvider>();//maybe could cause some issues if multiple providers are registered 
            var logger = _serviceProvider.GetService<ILogger>();

            if (scheduler == null)
            {
                logger.Error("TaskScheduler service is not registered. Cannot run tests.");
                return;
            }

            logger.Info("Scheduling test run as a new task...");
            
            testRunner.AddProvider(testProvider);
            
            var testEnumerator = testRunner.RunAll();
            scheduler.AddTask(TaskType.Sequential, testEnumerator, TaskPriority.High);
        }
    }
}