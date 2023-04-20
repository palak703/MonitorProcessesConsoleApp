using MonitorProcessesConsoleApp.Interface;
using System.Diagnostics;

namespace MonitorProcessesConsoleApp.ConsoleWrappers
{
    public class ConsoleLogger : ILogger //ToDo rename consoleLogger
    {
        private TextWriter _textWriter;

        public ConsoleLogger() : this(Console.Out)
        {

        }
        public ConsoleLogger(TextWriter textWriter) // ToDo:make it protected
        {
            _textWriter = textWriter;
        }

        public void WriteLog(string log)
        {
            _textWriter.WriteLine(log);
        }
    }
}
