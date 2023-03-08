using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace POC_NEW
{
    public class ProcessInfo
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
        public string affinity;
        public List<string> pipes = new List<string>();

        public int handleCount;

        public ProcessInfo(int pid, string name, int priority, 
            ProcessThreadCollection threads, int instance, double private_size, 
             double virtual_size, double working_set, double peak_working, List<string> pipes, string affinity,
             int handleCount)
        {
          
            this.pid = pid;
            this.name = name;
            this.priority = priority;
            this.threads = threads;
            this.instance = instance;
            this.writes = 0;
            this.reads = 0;
            this.cpu = 0;
            this.privateWS = 0;
            this.shared = 0;

            //peak private working set
            this.private_size = private_size;
           
            this.virtual_size = virtual_size;
            this.working_set = working_set;
            this.peak_working = peak_working;
            this.pipes = pipes;
            this.affinity = affinity;
            this.handleCount = handleCount;

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

        public void SetThreads(ProcessThreadCollection threads)
        {
            this.threads = threads;
        }
        public void SetBase(int basePriority)
        {
            this.priority = basePriority;
        }
        public void SetPrivateMemory(double privateMemory)
        {
            this.private_size = privateMemory;
        }
        public void SetVirtual(double virtualMemory)
        {
            this.virtual_size = virtualMemory;
        }
        public void SetWS(double ws)
        {
            this.working_set = ws;
        }
        public void SetPeakWS(double peakWS)
        {
            this.peak_working = peakWS;
        }
        public void SetPipes(List<string> pipes)
        {
            this.pipes = pipes;
        }
        public void SetAffinity(string affinity)
        {
            this.affinity = affinity;
        }
        public void SetHandle(int handleCount)
        {
            this.handleCount = handleCount;
        }
        public void SetInstance(int instance)
        {
            this.instance = instance;
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
        public ProcessThreadCollection GetThreads()
        {
            return this.threads;
        }

        public string GetAffinity()
        {
            return this.affinity;
        }
        public int GetPriority()
        {
            return this.priority;
        }

        public double GetPrivateSize()
        {
            return this.private_size;
        }
        public double GetVirtualSize()
        {
            return this.virtual_size;
        }

        public double GetShared()
        {
            return this.shared;
        }

        public double GetPeakWs()
        {
            return this.peak_working;
        }

        public int GetHandleCount()
        {
            return this.handleCount;
        }
        public double GetPrivateWS()
        {
            return this.privateWS;
        }
        

    }


}
