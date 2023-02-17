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
        public double cpu;
        public double private_size;
        public double peak_private;
        public double virtual_size;
        public double privateWS;
        public double working_set;
        public double shared;
        public double peak_working;
        public double reads;
        public double writes;
        public List<string> pipes = new List<string>();

        public ProcessInfo(int pid, string name, int priority, ProcessThreadCollection threads)
        {
            Process process = Process.GetProcessById(pid);
            this.pid = pid;
            this.name = name;
            this.priority = priority;
            this.threads = threads;
            this.writes = 0;
            this.reads = 0;
            this.cpu = 0;
            this.privateWS = 0;
            this.shared = 0;

            
        
        }

        public void SetInstance()
        {
            Process process = Process.GetProcessById(this.pid);
            Process[] processes = Process.GetProcessesByName(this.name);
            int instance = 0;
            if (processes.Length != 0)
            {
                for (int i = 0; i < processes.Length; i++)
                {
                    if (processes[i].Id == this.pid)
                        instance = i;
                }
            }
            this.instance = instance;
            try
            {
                this.private_size = process.PrivateMemorySize64;
                this.peak_private = process.PeakWorkingSet64;
                this.virtual_size = process.VirtualMemorySize64;
                this.working_set = process.WorkingSet64;
                this.peak_working = process.PeakWorkingSet64;
                foreach (ProcessModule module in process.Modules)
                {
                    // Check if the module represents a pipe
                    if (module.ModuleName.Contains("pipe"))
                    {
                        this.pipes.Add(module.ModuleName);
                    }
                }
            }
            catch
            {
                this.private_size = -1;
                this.peak_private = -1;
                this.virtual_size = -1;
                this.working_set = -1;
                this.peak_working = -1;

            }
        }
        public void SetWrites(double writes)
        {
            this.writes = writes;
        }
        public void SetReads(double reads)
        {
            this.reads = reads;
        }
        public void SetPrivateWS(double privateWS)
        {
            this.privateWS = privateWS;
        }
        public void SetShared(double shared)
        {
            this.shared = shared;
        }
        public void SetCPU(double cpu)
        {
            this.cpu = cpu;
        }
        public int GetInstance()
        {
            return this.instance;
        }
        public string GetName()
        {
            return this.name;
        }
        public double GetWS()
        {
            return this.working_set;
        }
        public int GetPID()
        {
            return this.pid;
        }
        public double GetCPU()
        {
            return this.cpu;
        }

        public int ThreadCount()
        {
            return this.threads.Count;
        }
        public double GetReads()
        {
            return this.reads;
        }
        public double GetWrites()
        {
            return this.writes;
        }
    }
}
