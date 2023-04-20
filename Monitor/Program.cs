// See https://aka.ms/new-console-template for more information
using MonitorProcessesConsoleApp;
using MonitorProcessesConsoleApp.ConsoleWrappers;

string processName = args[0];
double processmaximumLifetime;
double.TryParse(args[1], out processmaximumLifetime);
double processMonitoringFrequency;
double.TryParse(args[2], out processMonitoringFrequency);

if (processmaximumLifetime == 0.0 || processMonitoringFrequency == 0.0)
{
    Console.Error.WriteLine("Invalid Arguments");
    Console.Error.WriteLine("Order of Arguments should be :ProcessName, maximumLifetime, monitoring frequency.");
    Console.Error.WriteLine("maximumLifetime (in minutes) and monitoring frequency(in minutes) should be greater then 0.");
    return;
}

MonitorProcess(processName, processmaximumLifetime, processMonitoringFrequency);

return;
static void MonitorProcess(string processName, double processmaximumLifetime, double processMonitoringFrequency)
{
    using (var processMonitor = new ProcessMonitor(new ConsoleLogger()))
    {
        processMonitor.ProcessName = processName;
        processMonitor.MaximumLifetime = TimeSpan.FromMinutes(processmaximumLifetime);
        processMonitor.MonitoringFrequency = TimeSpan.FromMinutes(processMonitoringFrequency);
        processMonitor.StartMonitoring();
        
        if(KeyMonitor.VerifyIfQisPressed(new ConsoleWrapper()))
        {
            Console.WriteLine("User Presses Q. Process Monitor Will stop");
        }
    }
}
