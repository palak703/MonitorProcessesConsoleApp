using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using MonitorProcessesConsoleApp.Interface;

namespace MonitorProcessesConsoleApp
{
    public class ProcessMonitor : IDisposable
    {
        public string? ProcessName { get; set; }
        public TimeSpan MaximumLifetime { get; set; }
        public TimeSpan MonitoringFrequency { get; set; }

        private System.Timers.Timer? _processTimer;

        private bool _disposedValue;

        private ILogger _logger;
        public ProcessMonitor(ILogger logger)
        {
            _logger = logger;
        }

        public void StartMonitoring()
        {
            _processTimer = new System.Timers.Timer(MonitoringFrequency.TotalMilliseconds);
            _processTimer.Elapsed += ProcessTimer_Elapsed;
            _processTimer.Enabled = true;
            _processTimer.AutoReset = true;
            _processTimer.Start();
        }

        private void ProcessTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                _logger.WriteLog($"Process Monitor runs at: {DateTime.Now}");

                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(ProcessName);

                if (!processes.Any())
                {
                    _logger.WriteLog($"No process running at: {DateTime.Now}");
                    return;
                }

                foreach (var process in processes)
                {
                    try
                    {
                        var runtime = DateTime.Now - process.StartTime;
                        string? log = null;
                        if (runtime > MaximumLifetime)
                        {
                            DateTime dt = new DateTime(runtime.Ticks);
                            process.Kill();
                            log = $"ProcessName: {process.ProcessName} ProcessID: {process.Id} ProcessRuntime: {Math.Round(runtime.TotalMinutes,2).ToString()}mins ProcessStatus: Is Killed";
                            _logger.WriteLog(log);
                        }
                        else
                        {
                            log = $"ProcessName: {process.ProcessName} ProcessID: {process.Id} ProcessRuntime: {Math.Round(runtime.TotalMinutes,2).ToString()}mins ProcessStatus: Is Alive";
                            _logger.WriteLog(log);
                        }
                    }
                    catch (Win32Exception ex)
                    {
                        // Ignore processes that give "access denied" error.
                        if (ex.NativeErrorCode == 5)
                            continue;
                        else
                            _logger.WriteLog("Some Exception Occured: "+ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("Some Exception Occured: " + ex.ToString());
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                _processTimer?.Dispose();
                _processTimer = null;
                // TODO: set large fields to null
                _disposedValue = true;
            }
        }

        //TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~ProcessMonitor()
        {
            Dispose(disposing: false);
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
