using MonitorProcessesConsoleApp;
using MonitorProcessesConsoleApp.ConsoleWrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Process = System.Diagnostics.Process;

namespace MonitorProcessesConsoleAppTests
{
    public class ProcessMonitorTest
    {
        private TimeSpan _testProcessLifetime;
        private TimeSpan _testMonitoringInterval;
        private TimeSpan _taskDelayTimeSpan;
        private bool disposedValue;

        [Test]
        public async Task Monitorprocessor_should_kill_program_after_elapsedTimeInterval_and_timespan_greater_than_lifetime()
        {
            _testMonitoringInterval = TimeSpan.FromMinutes(0.4);
            _testProcessLifetime = TimeSpan.FromMinutes(0.3);
            _taskDelayTimeSpan = TimeSpan.FromMinutes(0.6);
            using (var processor = new ProcessMonitor(new ConsoleLogger()))
            {
                processor.MonitoringFrequency = _testMonitoringInterval;
                processor.MaximumLifetime = _testProcessLifetime;
                processor.ProcessName = "notepad";
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", @".\TestData\TestNotepad.txt");
                p.StartInfo = psi;
                p.Start();
                var processId = p.Id;
                processor.StartMonitoring();
                await Task.Delay((int)_taskDelayTimeSpan.TotalMilliseconds);
                var result = Process.GetProcessById(processId);
                Assert.IsTrue(result.HasExited);
            }

        }
        [Test]
        public async Task Monitorprocessor_should_not_kill_program_after_elapsedTimeInterval_and_timespan_less_than_lifetime()
        {
            _testMonitoringInterval = TimeSpan.FromMinutes(0.4);
            _testProcessLifetime = TimeSpan.FromMinutes(1);
            _taskDelayTimeSpan = TimeSpan.FromMinutes(0.6);
            using (var processor = new ProcessMonitor(new ConsoleLogger()))
            {
                processor.MonitoringFrequency = _testMonitoringInterval;
                processor.MaximumLifetime = _testProcessLifetime;
                processor.ProcessName = "notepad";
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", @".\TestData\TestNotepad.txt");
                p.StartInfo = psi;
                p.Start();
                var processId = p.Id;
                processor.StartMonitoring();
                await Task.Delay((int)_taskDelayTimeSpan.TotalMilliseconds);
                var result = Process.GetProcessById(processId);
                Assert.IsFalse(result.HasExited);
                p.Kill();
            }

        }
        [Test]
        public async Task Monitorprocessor_should_not_kill_program_before_elapsedTimeInterval()
        {
            _testMonitoringInterval = TimeSpan.FromMinutes(0.6);
            _testProcessLifetime = TimeSpan.FromMinutes(1);
            _taskDelayTimeSpan = TimeSpan.FromMinutes(0.4);
            using (var processor = new ProcessMonitor(new ConsoleLogger()))
            {
                processor.MonitoringFrequency = _testMonitoringInterval;
                processor.MaximumLifetime = _testProcessLifetime;
                processor.ProcessName = "notepad";
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo("Notepad.Exe", @".\TestData\TestNotepad.txt");
                p.StartInfo = psi;
                p.Start();
                var processId = p.Id;
                processor.StartMonitoring();
                await Task.Delay((int)_taskDelayTimeSpan.TotalMilliseconds);
                var result = Process.GetProcessById(processId);
                Assert.IsFalse(result.HasExited);
                p.Kill();
            }
        }

    }
}

