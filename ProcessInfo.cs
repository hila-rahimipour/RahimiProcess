using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace POC_NEW
{
    class ProcessInfo
    {
        public int pid;
        public string name;
        public int instance;
        public int priority;
        public ProcessThreadCollection threads;
        public double private_size;
        public double peak_private;
        public double virtual_size;
        public double privateWS;
        public double working_set;
        public double shared;
        public double peak_working;
        public double reads;
        public double writes;
        
        public ProcessInfo(int pid)
        {
            Process process = Process.GetProcessById(pid);
            this.pid = pid;
            this.name = process.ProcessName;
            Process[] processes = Process.GetProcessesByName(process.ProcessName);
            int instance;
            if (processes.Length != 0)
            {
                for (int i=0; i < processes.Length; i++)
                {
                    if (processes[i].Id == process.Id)
                        instance = i;
                }
            }

        }

    }
}
