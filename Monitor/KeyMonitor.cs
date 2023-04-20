using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonitorProcessesConsoleApp.Interface;

namespace MonitorProcessesConsoleApp
{
    public static class KeyMonitor
    {
        public static bool VerifyIfQisPressed(IConsoleWrapper consoleWrapper)
        {
            bool Is_q_pressed;
            do
            {
                while (!consoleWrapper.IsConsoleKeyAvalialbe())
                {

                }
                var key = consoleWrapper.ReadKey();
                if (key.KeyChar == 'q' || key.KeyChar == 'Q')
                {
                    Is_q_pressed = true;
                    break;
                }

            } while (true);
            return Is_q_pressed;
        }
    }
}
