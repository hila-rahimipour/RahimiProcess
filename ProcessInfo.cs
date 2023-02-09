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
        
        public ProcessInfo(int pid)
        {
            Process process = Process.GetProcessById(pid);
            this.pid = pid;
            this.name = process.ProcessName;
            Process[] processes = Process.GetProcessesByName(process.ProcessName);
            int instance=0;
            if (processes.Length != 0)
            {
                for (int i=0; i < processes.Length; i++)
                {
                    if (processes[i].Id == process.Id)
                        instance = i;
                }
            }
            this.instance = instance;
            this.priority = process.BasePriority;
            this.threads = process.Threads;
            this.private_size = process.PrivateMemorySize64;
            this.peak_private = process.PeakWorkingSet64;
            this.virtual_size = process.VirtualMemorySize64;
            this.working_set = process.WorkingSet64;
            this.peak_working = process.PeakWorkingSet64;
            this.writes = 0;
            this.reads = 0;
            this.cpu = 0;
            this.privateWS = 0;
            this.shared = 0;
            /*can find pipelines of the process by:
             * foreach (ProcessModule module in process.Modules)
                {
                    Console.WriteLine("Module Name: " + module.ModuleName);

                    // Check if the module represents a pipe
                    if (module.ModuleName.Contains("pipe"))
                    {
                        Console.WriteLine("Pipe Name: " + module.ModuleName);
                    }
                }
            need to add try and catch
            */
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
