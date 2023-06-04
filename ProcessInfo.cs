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
        public ProcessInfo(string name, string pID, string cPU, string wS, string read, string write, string user, string threads, string sTime)
        {
            Name = name;
            PID = pID;
            CPU = cPU;
            WS = wS;
            Read = read;
            Write = write;
            User = user;
            Threads = threads;
            STime = sTime;
        }

        public string Name { get; set; }
        public string PID { get; set; }
        public string CPU { get; set; }
        public string WS { get; set; }
        public string Read { get; set; }
        public string Write { get; set; }
        public string User { get; set; }
        public string Threads { get; set; }
        public string STime { get; set; }
    }

}
