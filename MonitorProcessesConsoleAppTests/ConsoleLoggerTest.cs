using MonitorProcessesConsoleApp;
using MonitorProcessesConsoleApp.ConsoleWrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessesConsoleAppTests
{
    public class ConsoleLoggerTest 
    {
        [Test]
        public void ConsoleLogger_Should_Write_Log_ToConsole()
        {
            var stringwriter = new StringWriter();
            var consoleProcessStatus = new ConsoleLogger(stringwriter);
            consoleProcessStatus.WriteLog("xyz");
            var result = stringwriter.ToString();
            Assert.That(result,Is.EqualTo("xyz\r\n"));
        }
    }
}
