using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Data;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;


namespace POC_NEW
{
    class Program
    {
        static int tableWidth = 170;
        const uint THREAD_QUERY_INFORMATION = 0x0040;

        [StructLayout(LayoutKind.Sequential)]
        struct PROCESSOR_RELATIONSHIP
        {
            public byte Flags;
            public byte EfficiencyClass;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] Reserved;
            public ushort GroupCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public GROUP_AFFINITY[] GroupMask;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct GROUP_AFFINITY
        {
            [FieldOffset(0)]
            public ulong Mask;
            [FieldOffset(0)]
            public ushort Group;
            [FieldOffset(2)]
            public ushort Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct PROCESSOR_NUMBER
        {
            public ushort Group;
            public byte Number;
            public byte Reserved;
        }
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        static extern bool GetThreadIdealProcessorEx(IntPtr hThread, out PROCESSOR_NUMBER lpIdealProcessor);

        public static void PrintLine()
        {
            Console.WriteLine(new string('-', tableWidth));
        }
        public static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }
        public static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        public static double GetCpuProcess(Process process, int instance)
        {
            PerformanceCounter myAppCpu;
            if (instance == 0)
                myAppCpu = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            else
                myAppCpu = new PerformanceCounter("Process", "% Processor Time", process.ProcessName + "#" + instance);
            myAppCpu.NextValue();
            Thread.Sleep(10);
            double cpu = myAppCpu.NextValue();
            return cpu;
        }
        public static double GetWS (Process process, int instance)
        {
            PerformanceCounter WS;
            if (instance==0)
                WS = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName);
            else
                WS = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName+"#"+instance);
            WS.InstanceName = process.ProcessName;
            WS.NextValue();
            Thread.Sleep(10);
            double ram_usage = WS.NextValue() / 1024;
            return ram_usage;
        }
        public static double GetInput (Process process, int instance)
        {

            PerformanceCounter net;
            if (instance==0)
                net = new PerformanceCounter("Process", "IO Read Bytes/sec", process.ProcessName);
            else
                net = new PerformanceCounter("Process", "IO Read Bytes/sec", process.ProcessName+"#"+instance);
            net.NextValue();
            Thread.Sleep(10);
            double network = net.NextValue();
            return network;
        }
        public static double GetOutput(Process process, int instance)
        {

            PerformanceCounter net;
            if (instance == 0)
                net = new PerformanceCounter("Process", "IO Write Bytes/sec", process.ProcessName);
            else
                net = new PerformanceCounter("Process", "IO Write Bytes/sec", process.ProcessName + "#" + instance);
            net.NextValue();
            Thread.Sleep(10);
            double network = net.NextValue();
            return network;
        }
        public static void OneProcess(Dictionary<int, object[]> info, Process process)
        {
            Console.Clear();

            object[] collected_info;
            //0- id, 1- cpu, 2- physical memory, 3- ram, 4- network, 5- total cpu, 6- total memory, 7- total ram, 8- total network, 9- thread
            try
            {
                collected_info = info[process.Id];
            }
            catch
            {
                collected_info = new object[10];
            }

            //get cpu info
            PerformanceCounter myAppCpu = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            myAppCpu.NextValue();
            Thread.Sleep(10);
            double cpu = myAppCpu.NextValue();

            //get private working set memory info
            PerformanceCounter PC = new PerformanceCounter();
            PC.CategoryName = "Process";
            PC.CounterName = "Working Set - Private";
            PC.InstanceName = process.ProcessName;
            PC.NextValue();
            Thread.Sleep(10);
            float ram_usage = PC.NextValue() / 1024;
            PC.Close();
            PC.Dispose();

            //get private memory info
            long memory = process.PrivateMemorySize64;

            //get netork info
            PerformanceCounter net = new PerformanceCounter("Process", "IO Read Bytes/sec", process.ProcessName);
            net.NextValue();
            Thread.Sleep(10);
            double network = net.NextValue();

            //updating data
            collected_info[0] = process.ProcessName;
            collected_info[1] = cpu / 10;
            collected_info[2] = memory;
            collected_info[3] = ram_usage;
            collected_info[4] = network;

            if (collected_info[5] == null)
                collected_info[5] = (double)0;
            else
            {
                double total_cpu = (double)collected_info[5] + cpu;
                collected_info[5] = total_cpu;
            }

            if (collected_info[6] == null)
                collected_info[6] = (long)0;
            else
            {
                long total_memory = (long)collected_info[6] + memory;
                collected_info[6] = total_memory;
            }

            if (collected_info[7] == null)
                collected_info[7] = (float)0;
            else
            {
                float total_ram = (float)collected_info[7] + ram_usage;
                collected_info[7] = total_ram;
            }
            if (collected_info[8] == null)
                collected_info[8] = (double)0;
            else
            {
                double total_net = (double)collected_info[8] + network;
                collected_info[8] = total_net;
            }


            //getting information of threads       
            //collected_info[9] = GetThreads(process);
            string[] all_threads = ((string)collected_info[9]).Split('|');
            //updating dict
            info[process.Id] = collected_info;
            PrintLine();
            PrintRow("pid", "process name", "cpu", "private memory", "private working set",
                "network", "total cpu", "total private memory", "total working set", "total network",
                "threads");
            PrintLine();
            PrintRow(process.Id.ToString(), collected_info[0].ToString(), collected_info[1].ToString(),
                collected_info[2].ToString(), collected_info[3].ToString(), collected_info[4].ToString(),
                collected_info[5].ToString(), collected_info[6].ToString(), collected_info[7].ToString(),
                collected_info[8].ToString(), all_threads[0].ToString());
            for (int i = 1; i < all_threads.Length; i++)
                PrintRow(" ", " ", " ", " ", " ", " ", " ", " ", " ", " ", all_threads[i].ToString());
            PrintLine();


        }
        public static void GetProcessors()
        {
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                Console.WriteLine("Number Of Physical Processors: {0} ", item["NumberOfProcessors"]);
            }
        }
        public static int GetCores()
        {
            int coreCount = 0;
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }
            //Console.WriteLine("Number Of Cores: {0}", coreCount);
            return coreCount;
        }
        public static int GetLogical()
        {
            return Environment.ProcessorCount;
        }
        public static double CpuThread(ProcessThread thread)
        {

            //get cpu of each thread


            long currentTick = (DateTime.Now).Ticks;
            double currentcpu = thread.TotalProcessorTime.TotalMilliseconds;
            Thread.Sleep(100);
            long newTick = (DateTime.Now).Ticks;
            double newcpu = thread.TotalProcessorTime.TotalMilliseconds;
            double totalcpu = 100000 * (double)((newcpu - currentcpu)) / (double)(newTick - currentTick);


            return totalcpu;
        }
        public static double GetCpuThread(ProcessThread thread)
        {
            long currentTick = (DateTime.Now).Ticks;
            double currentcpu = thread.TotalProcessorTime.TotalMilliseconds;
            Thread.Sleep(500);
            long newTick = (DateTime.Now).Ticks;
            double newcpu = thread.TotalProcessorTime.TotalMilliseconds;
            double totalcpu = 100000 * (double)((newcpu - currentcpu)) / (double)(newTick - currentTick);
            return totalcpu;
        }
        public static void GetLogicalCpu()
        {
            int cores = GetLogical();
            for (int i=0; i<cores; i++)
            {
                PerformanceCounter pc = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
                pc.NextValue();
                Thread.Sleep(10);
                Console.WriteLine("core {0}: {1}", i.ToString(), pc.NextValue());
            }
            
        }
        public static double GetCpuProcess(string name, int instance)
        {
            PerformanceCounter cpu;
            if (instance == 0)
            {
                cpu = new PerformanceCounter("Process", "% Processor Time", name, true);
            }
            else
            {
                cpu = new PerformanceCounter("Process", "% Processor Time",name + "#" + instance, true);
            }
            cpu.NextValue();
            Thread.Sleep(1000);
            return cpu.NextValue();
        }

        public static double[] GetProc(string name, int instance)
        {
            PerformanceCounter[] values = new PerformanceCounter[3];
            double[] output = new double[3];
            if (instance == 0)
            {
                values[0] = new PerformanceCounter("Process", "IO Read Bytes/sec", name, true);
                values[1] = new PerformanceCounter("Process", "IO Write Bytes/sec", name, true);
                values[2] = new PerformanceCounter("Process", "Working Set - Private", name, true);
                
            }
            else
            {
                values[0] = new PerformanceCounter("Process", "IO Read Bytes/sec", name + "#" + instance, true);
                values[1] = new PerformanceCounter("Process", "IO Write Bytes/sec", name + "#" + instance, true);
                values[2] = new PerformanceCounter("Process", "Working Set - Private", name + "#" + instance, true);
            }
            values[0].NextValue();
            values[1].NextValue();
            values[2].NextValue();

            Thread.Sleep(1000);
            for (int i=0; i < values.Length; i++)
            {
                output[i] = values[i].NextValue();
            }
            return output;
        }
        public static void ToList(List<ProcessInfo> procs)
        {
            procs.Clear();
            Process[] processes = Process.GetProcesses();
            foreach (Process proc in processes)
            {
                string name = proc.ProcessName;
                Process[] process_name = Process.GetProcessesByName(name);
                List<string> pipes = new List<string>();

                try
                {
                    foreach (ProcessModule module in proc.Modules)
                    {
                        // Check if the module represents a pipe
                        if (module.ModuleName.Contains("pipe"))
                        {
                            pipes.Add(module.ModuleName);
                        }
                    }
                }
                catch
                {
                    pipes.Add("-1");
                }

                string affinity = "";
                try
                {
                    affinity = Convert.ToString((int)proc.ProcessorAffinity, 2);
                }
                catch
                {
                    affinity = "-1";
                }

                int instance = 0;
                if (process_name.Length != 0)
                {
                    for (int i = 0; i < process_name.Length; i++)
                    {
                        if (process_name[i].Id == proc.Id)
                            instance = i;
                    }
                }





                ProcessInfo process = new ProcessInfo(proc.Id, 
                    proc.ProcessName, proc.BasePriority, proc.Threads, instance,
                    proc.PrivateMemorySize64, proc.VirtualMemorySize64, proc.WorkingSet64, 
                    proc.PeakWorkingSet64, pipes, affinity, proc.HandleCount);
                

                foreach(ProcessInfo find_proc in procs)
                {
                    if (find_proc.GetPID() == proc.Id)
                        procs.Remove(find_proc);
                }
                procs.Add(process);
            }
                
        }

        public static void AllCpu(PerformanceCounter[] cpus, List<ProcessInfo> procs)
        {
            for (int c = 0; c < cpus.Length; c++)
            {
                try
                {
                    if (procs[c].GetInstance() == 0)
                    {
                        cpus[c] = new PerformanceCounter("Process", "% Processor Time", procs[c].GetName(), true);
                    }
                    else
                    {
                        cpus[c] = new PerformanceCounter("Process", "% Processor Time", procs[c].GetName() + "#" + procs[c].GetInstance(), true);
                    }
                    cpus[c].NextValue();
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void GetAllCpu1(PerformanceCounter[] cpus, List<ProcessInfo> procs)
        {
            for (int a = 0; a < cpus.Length/2; a++)
            {
                try
                {
                    double cpu = cpus[a].NextValue();
                    procs[a].SetCPU(cpu/GetLogical());
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void GetAllCpu2(PerformanceCounter[] cpus, List<ProcessInfo> procs)
        {
            for (int z = cpus.Length / 2; z < cpus.Length; z++)
            {
                try
                {
                    double cpu = cpus[z].NextValue();
                    procs[z].SetCPU(cpu/GetLogical());
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void AllReads(PerformanceCounter[] reads, List<ProcessInfo> procs)
        {
            for (int n = 0; n < reads.Length; n++)
            {
                try
                {
                    if (procs[n].GetInstance() == 0)
                    {
                        reads[n] = new PerformanceCounter("Process", "IO Read Bytes/sec", procs[n].GetName(), true);
                    }
                    else
                    {
                        reads[n] = new PerformanceCounter("Process", "IO Read Bytes/sec", procs[n].GetName() + "#" + procs[n].GetInstance(), true);
                    }
                    reads[n].NextValue();
                }
                catch
                {
                    continue;
                }
            }  
        }
        public static void GetAllReads1(PerformanceCounter[] reads, List<ProcessInfo> procs)
        {
            for (int q = 0; q < reads.Length/2; q++)
            {
                try
                {
                    double read = reads[q].NextValue();
                    procs[q].SetReads(read);
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void GetAllReads2(PerformanceCounter[] reads, List<ProcessInfo> procs)
        {
            for (int y = reads.Length / 2; y < reads.Length; y++)
            {
                try
                {
                    double read = reads[y].NextValue();
                    procs[y].SetReads(read);
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void AllWrites(PerformanceCounter[] writes, List<ProcessInfo> procs)
        {
            for (int j = 0; j < writes.Length; j++)
            {
                try
                {
                    if (procs[j].GetInstance() == 0)
                    {
                        writes[j] = new PerformanceCounter("Process", "IO Write Bytes/sec", procs[j].GetName(), true);
                    }
                    else
                    {
                        writes[j] = new PerformanceCounter("Process", "IO Write Bytes/sec", procs[j].GetName() + "#" + procs[j].GetInstance(), true);
                    }
                    writes[j].NextValue();
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void GetAllWrites1(PerformanceCounter[] writes, List<ProcessInfo> procs)
        {
            for (int m = 0; m < writes.Length/2; m++)
            {
                try
                {
                    double write = writes[m].NextValue();
                    procs[m].SetWrites(write);
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void GetAllWrites2(PerformanceCounter[] writes, List<ProcessInfo> procs)
        {
            for (int s = writes.Length / 2; s < writes.Length; s++)
            {
                try
                {
                    double write = writes[s].NextValue();
                    procs[s].SetWrites(write);
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void AllWS(PerformanceCounter[] workingSet, List<ProcessInfo> procs)
        {
            for (int i = 0; i < workingSet.Length; i++)
            {
                try
                {
                    if (procs[i].GetInstance() == 0)
                    {

                        workingSet[i] = new PerformanceCounter("Process", "Working Set - Private", procs[i].GetName());
                    }
                    else
                    {
                        workingSet[i] = new PerformanceCounter("Process", "Working Set - Private", procs[i].GetName() + "#" + procs[i].GetInstance());
                    }
                    workingSet[i].NextValue();
                }
                catch
                {
                    continue;
                }
            }
        }
        public static void GetAllWS1(PerformanceCounter[] workingSet, List<ProcessInfo> procs)
        {
            for (int l = 0; l < workingSet.Length/2; l++)
            {
                try
                {
                    double privateWS = workingSet[l].NextValue();
                    procs[l].SetPrivateWS(privateWS);
                    procs[l].SetShared(procs[l].GetWS() - privateWS);
                }
                catch
                {
                    continue;
                }

            }
        }
        public static void GetAllWS2(PerformanceCounter[] workingSet, List<ProcessInfo> procs)
        {
            for (int f = workingSet.Length / 2; f < workingSet.Length; f++)
            {
                try
                {
                    double privateWS = workingSet[f].NextValue();
                    procs[f].SetPrivateWS(privateWS);
                    procs[f].SetShared(procs[f].GetWS() - privateWS);
                }
                catch
                {
                    continue;
                }

            }
        }
        public static void GetTcpSockets()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            Console.WriteLine("Active TCP Connections:");
            foreach (TcpConnectionInformation connection in connections)
            {
                Console.WriteLine("Local endpoint: {0}:{1}", connection.LocalEndPoint.Address, connection.LocalEndPoint.Port);
                Console.WriteLine("Remote endpoint: {0}:{1}", connection.RemoteEndPoint.Address, connection.RemoteEndPoint.Port);
                Console.WriteLine("State: {0}", connection.State);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public static void GetUdpSockets()
        {
            IPEndPoint[] endpoints = GetActiveUdpListeners();

            Console.WriteLine("Active UDP Listeners:");
            foreach (IPEndPoint endpoint in endpoints)
            {
                Console.WriteLine("Endpoint: {0}:{1}", endpoint.Address, endpoint.Port);
            }

            Console.ReadKey();
        }
        public static IPEndPoint[] GetActiveUdpListeners()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] endpoints = new IPEndPoint[0];

            foreach (UnicastIPAddressInformation address in properties.GetUnicastAddresses())
            {
                if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    try
                    {
                        IPEndPoint endpoint = new IPEndPoint(address.Address, 0);
                        using (UdpClient client = new UdpClient(endpoint))
                        {
                            IPEndPoint[] localEndpoints = { client.Client.LocalEndPoint as IPEndPoint };
                            Array.Resize(ref endpoints, endpoints.Length + localEndpoints.Length);
                            Array.Copy(localEndpoints, 0, endpoints, endpoints.Length - localEndpoints.Length, localEndpoints.Length);
                        }
                    }
                    catch (SocketException)
                    {
                        // ignore exceptions for this example
                    }
                }
            }

            return endpoints;
        }
        public static void UpdateProcsForm(home form)
        {
            Console.WriteLine("start");


            Console.WriteLine("list");

            PerformanceCounter[] cpus = new PerformanceCounter[procs.Count];
            PerformanceCounter[] reads = new PerformanceCounter[procs.Count];
            PerformanceCounter[] writes = new PerformanceCounter[procs.Count];
            PerformanceCounter[] workingSet = new PerformanceCounter[procs.Count];




            // ThreadPool.QueueUserWorkItem(new WaitCallback(() => AllCpu(cpus, procs)));
            Thread cpu;
            Thread read;
            Thread write;
            Thread ws;



            //ThreadPool.QueueUserWorkItem(state => GetAllCpu(cpus, procs));
            //ThreadPool.QueueUserWorkItem(state => GetAllReads(cpus, procs));
            //ThreadPool.QueueUserWorkItem(state => GetAllWrites(cpus, procs));
            //ThreadPool.QueueUserWorkItem(state => GetAllWS(cpus, procs));

            Thread Getcpu1;
            Thread Getread1;
            Thread Getwrite1;
            Thread Getws1;

            Thread Getcpu2;
            Thread Getread2;
            Thread Getwrite2;
            Thread Getws2;


          
            while (true)
            {
                ToList(procs);


                cpus = new PerformanceCounter[procs.Count];
                reads = new PerformanceCounter[procs.Count];
                writes = new PerformanceCounter[procs.Count];
                workingSet = new PerformanceCounter[procs.Count];




                // ThreadPool.QueueUserWorkItem(new WaitCallback(() => AllCpu(cpus, procs)));
                cpu = new Thread(() => AllCpu(cpus, procs));
                read = new Thread(() => AllReads(reads, procs));
                write = new Thread(() => AllWrites(writes, procs));
                ws = new Thread(() => AllWS(workingSet, procs));



                Console.WriteLine("start cpu");
                cpu.Start();
                read.Start();
                write.Start();
                ws.Start();


                cpu.Join();
                read.Join();
                write.Join();
                ws.Join();
                Console.WriteLine("end cpu");
                Thread.Sleep(1000);


                //ThreadPool.QueueUserWorkItem(state => GetAllCpu(cpus, procs));
                //ThreadPool.QueueUserWorkItem(state => GetAllReads(cpus, procs));
                //ThreadPool.QueueUserWorkItem(state => GetAllWrites(cpus, procs));
                //ThreadPool.QueueUserWorkItem(state => GetAllWS(cpus, procs));

                Console.WriteLine("start get");
                Getcpu1 = new Thread(() => GetAllCpu1(cpus, procs));
                Getread1 = new Thread(() => GetAllReads1(reads, procs));
                Getwrite1 = new Thread(() => GetAllWrites1(writes, procs));
                Getws1 = new Thread(() => GetAllWS1(workingSet, procs));

                Getcpu2 = new Thread(() => GetAllCpu2(cpus, procs));
                Getread2 = new Thread(() => GetAllReads2(reads, procs));
                Getwrite2 = new Thread(() => GetAllWrites2(writes, procs));
                Getws2 = new Thread(() => GetAllWS2(workingSet, procs));


                Getcpu1.Start();
                Getread1.Start();
                Getwrite1.Start();
                Getws1.Start();

                Getcpu2.Start();
                Getread2.Start();
                Getwrite2.Start();
                Getws2.Start();


                Getcpu1.Join();
                Getread1.Join();
                Getwrite1.Join();
                Getws1.Join();
                Getcpu2.Join();
                Getread2.Join();
                Getwrite2.Join();
                Getws2.Join();
                //Thread sendInfo = new Thread(()=>form.SetProcs());
                //sendInfo.Start();
                //form.SetProcs();
                Console.WriteLine("end get");

                ListView list = (ListView)form.Controls.Find("listView1", false)[0];
                list.Items.Clear();

                list.BeginUpdate();

                foreach (ProcessInfo proc in procs)
                {
                    string[] data = {proc.GetName(), proc.GetPID().ToString(), proc.GetCPU().ToString(),
                     proc.GetWS().ToString(), proc.GetReads().ToString(), proc.GetWrites().ToString(),
                     proc.GetThreads().Count.ToString(), proc.GetHandleCount().ToString()};
                    var ListViewItemData = new ListViewItem(data);
                    list.Items.Add(ListViewItemData);
                }

                list.EndUpdate();
                list.Update();

            }
        }

        public static string GetIdealProcessor(uint threadId)
        {
            // Thread ID of the thread you want to get the logical processor number for
            IntPtr thread = OpenThread(THREAD_QUERY_INFORMATION, false, threadId);
            PROCESSOR_NUMBER idealProcessor;
            if (thread != IntPtr.Zero)
            {
                if (GetThreadIdealProcessorEx(thread, out idealProcessor))
                {
                    byte processor = idealProcessor.Number;
                    string byteString = string.Format("{0:X2}", processor);
                    return byteString;
                }
            }
            return "";
        }

        public static EventWaitHandle waitHandle = new AutoResetEvent(false);



        public static List<ProcessInfo> procs = new List<ProcessInfo>();
        static void Main(string[] args)
        {

            //Console.WriteLine("start");

            //ToList(procs);

            //Console.WriteLine("list");

            //PerformanceCounter[] cpus = new PerformanceCounter[procs.Count];
            //PerformanceCounter[] reads = new PerformanceCounter[procs.Count];
            //PerformanceCounter[] writes = new PerformanceCounter[procs.Count];
            //PerformanceCounter[] workingSet = new PerformanceCounter[procs.Count];

            //PerformanceCounter[] totalcpu = new PerformanceCounter[GetLogical()];
            //Console.WriteLine(totalcpu.Length);



            //// ThreadPool.QueueUserWorkItem(new WaitCallback(() => AllCpu(cpus, procs)));
            //Thread cpu = new Thread(() => AllCpu(cpus, procs));
            //Thread read = new Thread(() => AllReads(reads, procs));
            //Thread write = new Thread(() => AllWrites(writes, procs));
            //Thread ws = new Thread(() => AllWS(workingSet, procs));



            //Console.WriteLine("start cpu");
            //cpu.Start();
            //read.Start();
            //write.Start();
            //ws.Start();


            //cpu.Join();
            //read.Join();
            //write.Join();
            //ws.Join();
            //Console.WriteLine("end cpu");
            //Thread.Sleep(1000);


            ////ThreadPool.QueueUserWorkItem(state => GetAllCpu(cpus, procs));
            ////ThreadPool.QueueUserWorkItem(state => GetAllReads(cpus, procs));
            ////ThreadPool.QueueUserWorkItem(state => GetAllWrites(cpus, procs));
            ////ThreadPool.QueueUserWorkItem(state => GetAllWS(cpus, procs));

            //Console.WriteLine("start get");
            //Thread Getcpu1 = new Thread(() => GetAllCpu1(cpus, procs));
            //Thread Getread1= new Thread(() => GetAllReads1(reads, procs));
            //Thread Getwrite1 = new Thread(() => GetAllWrites1(writes, procs));
            //Thread Getws1 = new Thread(() => GetAllWS1(workingSet, procs));

            //Thread Getcpu2 = new Thread(() => GetAllCpu2(cpus, procs));
            //Thread Getread2 = new Thread(() => GetAllReads2(reads, procs));
            //Thread Getwrite2 = new Thread(() => GetAllWrites2(writes, procs));
            //Thread Getws2 = new Thread(() => GetAllWS2(workingSet, procs));


            //Getcpu1.Start();
            //Getread1.Start();
            //Getwrite1.Start();
            //Getws1.Start();

            //Getcpu2.Start();
            //Getread2.Start();
            //Getwrite2.Start();
            //Getws2.Start();


            //Getcpu1.Join();
            //Getread1.Join();
            //Getwrite1.Join();
            //Getws1.Join();
            //Getcpu2.Join();
            //Getread2.Join();
            //Getwrite2.Join();
            //Getws2.Join();

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            home form = new home();
            Thread runProcUpdate = new Thread(() => UpdateProcsForm(form));
            runProcUpdate.Start();
            Application.Run(form);

            


            Console.WriteLine("end get");
            PrintRow("name", "pid", "cpu", "ws", "read", "write");
            PrintLine();
            foreach (ProcessInfo proc in procs)
            {
                PrintRow($"{proc.GetName()}", $"{proc.GetPID()}", $"{(proc.GetCPU()/GetLogical())}",
                    $"{proc.GetWS()}", $"{proc.GetReads()}", $"{proc.GetWrites()}");
            }


            //proc info
            Console.WriteLine("enter process id");
            int procId = int.Parse(Console.ReadLine());
            int count = 0;

            ////proc part 1
            //foreach (ProcessInfo proc in procs)
            //{
            //    count++;
            //    if (procId == proc.GetPID())
            //    {
            //        Console.Clear();
            //        Console.WriteLine($"------- Process {proc.GetPID()} part 1 -------");
            //        PrintLine();
            //        PrintRow("priority", "cpu", "d. cpu", "p. bytes", "d. p. bytes", "virtual size", "affinity", "threads");
            //        double old_cpu = proc.GetCPU();
            //        double old_private_size = proc.GetPrivateSize();

            //        while (true)
            //        {
            //            PrintRow($"{proc.GetPriority()}", $"{proc.GetCPU()}", $"{proc.GetCPU() - old_cpu}", $"{proc.GetPrivateSize()}", $"{proc.GetPrivateSize() - old_private_size}",
            //                    $"{proc.GetVirtualSize()}", $"{proc.GetAffinity()}", $"{proc.GetThreads().Count}");
            //            //PerformanceCounter cpuCounter= new PerformanceCounter("Process", "% Processor Time", proc.GetName(), true);
            //            //cpuCounter.NextValue();

            //            List<string> pipes = new List<string>();

            //            Process newInfo = Process.GetProcessById(proc.GetPID());
            //            Process[] process_name = Process.GetProcessesByName(proc.GetName());

            //            try
            //            {
            //                foreach (ProcessModule module in newInfo.Modules)
            //                {
            //                    // Check if the module represents a pipe
            //                    if (module.ModuleName.Contains("pipe"))
            //                    {
            //                        pipes.Add(module.ModuleName);
            //                    }
            //                }
            //            }
            //            catch
            //            {
            //                pipes.Add("-1");
            //            }

            //            string affinity = "";
            //            try
            //            {
            //                affinity = Convert.ToString((int)newInfo.ProcessorAffinity, 2);
            //            }
            //            catch
            //            {
            //                affinity = "-1";
            //            }

            //            int instance = 0;
            //            if (process_name.Length != 0)
            //            {
            //                for (int i = 0; i < process_name.Length; i++)
            //                {
            //                    if (process_name[i].Id == newInfo.Id)
            //                        instance = i;
            //                }
            //            }

            //            old_cpu = proc.GetCPU();
            //            old_private_size = proc.GetPrivateSize();

            //            double cpuValue = GetCpuProcess(proc.name, instance);


            //            procs[count].SetBase(newInfo.BasePriority);
            //            procs[count].SetThreads(newInfo.Threads);
            //            procs[count].SetInstance(instance);
            //            procs[count].SetPrivateMemory(newInfo.PrivateMemorySize64);
            //            procs[count].SetVirtual(newInfo.VirtualMemorySize64);
            //            procs[count].SetWS(newInfo.WorkingSet64);
            //            procs[count].SetPeakWS(newInfo.PeakWorkingSet64);
            //            procs[count].SetPipes(pipes);
            //            procs[count].SetAffinity(affinity);
            //            procs[count].SetHandle(newInfo.HandleCount);
            //            procs[count].SetCPU(cpuValue / GetLogical());


            //            proc.SetCPU(cpuValue / GetLogical());
            //            proc.SetBase(newInfo.BasePriority);
            //            proc.SetThreads(newInfo.Threads);
            //            proc.SetInstance(instance);
            //            proc.SetPrivateMemory(newInfo.PrivateMemorySize64);
            //            proc.SetVirtual(newInfo.VirtualMemorySize64);
            //            proc.SetWS(newInfo.WorkingSet64);
            //            proc.SetPeakWS(newInfo.PeakWorkingSet64);
            //            proc.SetPipes(pipes);
            //            proc.SetAffinity(affinity);
            //            proc.SetHandle(newInfo.HandleCount);



            //        }


            //    }
            //}

            ////proc part 2
            //foreach (ProcessInfo proc in procs)
            //{
            //    count++;
            //    if (procId == proc.GetPID())
            //    {
            //        Console.Clear();
            //        Console.WriteLine($"------- Process {proc.GetPID()} part 2 -------");
            //        PrintLine();
            //        PrintRow("WS", "d. WS", "private WS", "shared WS", "peak WS", "read", "write", "handle count");
            //        double old_ws = proc.GetWS();

            //        while (true)
            //        {
            //            PrintRow($"{proc.GetWS()}", $"{proc.GetWS() - old_ws}",$"{proc.GetPrivateWS()}" ,$"{proc.GetShared()}", $"{proc.GetPeakWs()}", $"{proc.GetReads()}",
            //                    $"{proc.GetWrites()}", $"{proc.GetHandleCount()}");

            //            List<string> pipes = new List<string>();

            //            Process newInfo = Process.GetProcessById(proc.GetPID());
            //            Process[] process_name = Process.GetProcessesByName(proc.GetName());

            //            try
            //            {
            //                foreach (ProcessModule module in newInfo.Modules)
            //                {
            //                    // Check if the module represents a pipe
            //                    if (module.ModuleName.Contains("pipe"))
            //                    {
            //                        pipes.Add(module.ModuleName);
            //                    }
            //                }
            //            }
            //            catch
            //            {
            //                pipes.Add("-1");
            //            }

            //            string affinity = "";
            //            try
            //            {
            //                affinity = Convert.ToString((int)newInfo.ProcessorAffinity, 2);
            //            }
            //            catch
            //            {
            //                affinity = "-1";
            //            }

            //            int instance = 0;
            //            if (process_name.Length != 0)
            //            {
            //                for (int i = 0; i < process_name.Length; i++)
            //                {
            //                    if (process_name[i].Id == newInfo.Id)
            //                        instance = i;
            //                }
            //            }

            //            old_ws = proc.GetWS();
            //            double[] values = GetProc(proc.GetName(), instance);


            //            procs[count].SetBase(newInfo.BasePriority);
            //            procs[count].SetThreads(newInfo.Threads);
            //            procs[count].SetInstance(instance);
            //            procs[count].SetPrivateMemory(newInfo.PrivateMemorySize64);
            //            procs[count].SetVirtual(newInfo.VirtualMemorySize64);
            //            procs[count].SetWS(newInfo.WorkingSet64);
            //            procs[count].SetPeakWS(newInfo.PeakWorkingSet64);
            //            procs[count].SetPipes(pipes);
            //            procs[count].SetAffinity(affinity);
            //            procs[count].SetHandle(newInfo.HandleCount);
            //            procs[count].SetPrivateWS(values[2]);
            //            procs[count].SetWrites(values[1]);
            //            procs[count].SetReads(values[0]);
            //            procs[count].SetShared(proc.GetWS() - values[2]);



            //            proc.SetBase(newInfo.BasePriority);
            //            proc.SetThreads(newInfo.Threads);
            //            proc.SetInstance(instance);
            //            proc.SetPrivateMemory(newInfo.PrivateMemorySize64);
            //            proc.SetVirtual(newInfo.VirtualMemorySize64);
            //            proc.SetWS(newInfo.WorkingSet64);
            //            proc.SetPeakWS(newInfo.PeakWorkingSet64);
            //            proc.SetPipes(pipes);
            //            proc.SetAffinity(affinity);
            //            proc.SetHandle(newInfo.HandleCount);

            //            proc.SetPrivateWS(values[2]);
            //            proc.SetWrites(values[1]);
            //            proc.SetReads(values[0]);
            //            proc.SetShared(proc.GetWS()-values[2]);
            //        }


            //    }
            //}


            //thread info
            Console.WriteLine("enter thread id: ");
            int threadId = int.Parse(Console.ReadLine());
            foreach (ProcessInfo proc in procs)
            {
                ProcessThreadCollection threads = proc.GetThreads();
                foreach (ProcessThread thread in threads)
                    if (thread.Id == threadId)
                    {
                        Console.Clear();
                        Console.WriteLine($"----------- Process {proc.GetPID()} | Thread {threadId} -----------");

                        PrintLine();
                        if (thread.ThreadState.ToString() == "Wait")
                            PrintRow("b. priority", "d. priority", "ideal processor", "State", "Wait Reason", "CPU");
                        else
                            PrintRow("b. priority", "d. priority", "ideal processor", "State", "CPU");
                        PrintLine();

                        while (true)
                        {
                            System.Diagnostics.ThreadState state = (System.Diagnostics.ThreadState)2;
                            if (thread.ThreadState == state)
                                Console.WriteLine("running");
                                //PrintRow($"{thread.BasePriority}", $"{thread.CurrentPriority}", $"{GetIdealProcessor((uint)threadId)}",
                                //$"{thread.ThreadState}", $"{thread.WaitReason}", $"{GetCpuThread(thread)}");
                            else
                                PrintRow($"{thread.BasePriority}", $"{thread.CurrentPriority}", $"{GetIdealProcessor((uint)threadId)}",
                               $"{thread.ThreadState}", $"{GetCpuThread(thread)}");
                            //Thread.Sleep(1000);
                        }
                    }

            }




            //network info

            //Console.Clear();
            Console.WriteLine("----- Network Information -----");
            Console.WriteLine();
            Console.WriteLine("TCP sockets");
            GetTcpSockets();
            Console.WriteLine();
            Console.WriteLine("UDP sockets");
            GetUdpSockets();




            Console.ReadKey();



        }
    }
}
