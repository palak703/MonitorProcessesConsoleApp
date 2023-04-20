using MonitorProcessesConsoleApp;
using MonitorProcessesConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessesConsoleAppTests
{
    public class KeyMonitorTest
    {
        [Test]
        public void Verify_keyPresses_Is_Q()
        {
            // Arrange
            var stub = new ConsoleWrapperStub(ConsoleKey.Q);
            var expectedResult = true;

            // Act
            var actualResult = KeyMonitor.VerifyIfQisPressed(stub);

            //Assert 
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        public class ConsoleWrapperStub : IConsoleWrapper
        {
            private ConsoleKey _key;
            private int keyIndex = 0;

            public ConsoleWrapperStub(ConsoleKey key)
            {
                this._key = key;
            }

            public string Output = string.Empty;

            public ConsoleKeyInfo ReadKey()
            {
                return new ConsoleKeyInfo((char)_key, _key, false, false, false);
            }
            public bool IsConsoleKeyAvalialbe()
            {
                return true;
            }
        }
    }
}
