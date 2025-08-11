using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    public partial class Program : MyGridProgram
    {
        private SystemMonitorService _systemMonitor;
        private IConfigurationManager _manager;
        private readonly ITestRunner _testRunner;
        private IEnumerator<bool> _testEnumerator;
        private ILogger _logger;

        public Program()
        {
            _systemMonitor = new SystemMonitorService(this);

            IConfigStorage storage = new ProgrammableBlockStorage(Me);
            _manager = new ConfigurationManager(storage);

            _manager.Register(new LoggerConfig());

            _manager.Load();

            _logger = new Logger(_manager.GetOptions<LoggerConfig>().Level);

            _logger.AddTarget(new BufferedSurfaceTarget(
                Me.GetSurface(0),
                new TimeBasedFlushStrategy(_manager.GetOptions<LoggerConfig>().FlushIntervalSeconds)));
            _logger.AddTarget(new EchoTarget(Echo));

            _logger.ClearAllTargets();

            _testRunner = new TestRunner(new LogReporter(_logger));
            _testRunner.AddProvider(new MyFinalTestProvider());

            Runtime.UpdateFrequency = UpdateFrequency.Update10;
            Echo("Test framework ready. Use 'run_tests'.");
        }

        public void Main(string argument, UpdateType updateSource)
        {
            if ((updateSource & (UpdateType.Terminal | UpdateType.Trigger)) != 0 && argument.ToLower() == "run_tests")
            {
                Echo("Running tests...");
                _testEnumerator = _testRunner.RunAll();
            }

            if ((updateSource & (UpdateType.Terminal | UpdateType.Trigger | UpdateType.Mod)) != 0 && argument.ToLower() == "save_default")
            {
                Echo("Saving default configuration...");
                _manager.SaveDefaults();
            }

            if (_testEnumerator != null && !_testEnumerator.MoveNext())
            {
                Echo("Tests completed.");   
                _testEnumerator.Dispose();
                _testEnumerator = null;
            }

            _logger.Flush();
            _systemMonitor.PrintStatsToEcho();
        }
    }
}
