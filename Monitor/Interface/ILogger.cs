﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorProcessesConsoleApp.Interface
{
    public interface ILogger
    {
        void WriteLog(string log);
    }
}
