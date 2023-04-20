using MonitorProcessesConsoleApp.Interface;

namespace MonitorProcessesConsoleApp.ConsoleWrappers
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey(true);
        }
        public bool IsConsoleKeyAvalialbe()
        {
            return true;
        }
    }
}
