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
using System.Security.Cryptography;
using System.Reflection;
using System.Drawing;


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

        struct IO_COUNTERS
        {
            public ulong ReadOperationCount;
            public ulong WriteOperationCount;
            public ulong OtherOperationCount;
            public ulong ReadTransferCount;
            public ulong WriteTransferCount;
            public ulong OtherTransferCount;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct WIN32_FIND_DATA
        {
            public uint dwFileAttributes;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool FindNextFile(IntPtr hFindFile, out WIN32_FIND_DATA
           lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FindClose(IntPtr hFindFile);
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        static extern bool GetThreadIdealProcessorEx(IntPtr hThread, out PROCESSOR_NUMBER lpIdealProcessor);

        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool GetProcessIoCounters(IntPtr hProcess, out IO_COUNTERS counters);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

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
            Parallel.ForEach(processes, proc =>
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

                string affinity;
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
                double reads = 0; ;
                double writes = 0;
                try
                {
                    if (GetProcessIoCounters(proc.Handle, out IO_COUNTERS counters))
                    {
                        reads = counters.ReadTransferCount;
                        writes = counters.WriteTransferCount;
                    }
                }
                catch
                {
                    
                }
                ProcessInfo process;
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ProcessId = {proc.Id}");
                    long virtualMemorySize = Convert.ToInt64(-1);
                    long ws = Convert.ToInt64(-1);
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        virtualMemorySize = Convert.ToInt64(obj["VirtualSize"]);
                        virtualMemorySize=(Convert.ToInt64(obj["WorkingSetSize"]));
                        ws=(Convert.ToInt64(obj["PeakWorkingSetSize"]));
                    }

                    process = new ProcessInfo(proc.Id,
                    proc.ProcessName, proc.BasePriority, proc.Threads, instance,
                    proc.PrivateMemorySize64, virtualMemorySize, ws,
                    proc.PeakWorkingSet64, pipes, affinity, proc.HandleCount, reads, writes);
                }


                catch
                {
                    string query = string.Format("SELECT VirtualSize FROM Win32_Process WHERE ProcessId = {0}", proc.Id);
                    long virtualMemorySize = Convert.ToInt64(-1);
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                    {
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            virtualMemorySize = Convert.ToInt64(obj["VirtualSize"]);
                        }
                    }
                    process = new ProcessInfo(proc.Id,
                       proc.ProcessName, proc.BasePriority, proc.Threads, instance,
                       proc.PrivateMemorySize, virtualMemorySize, proc.WorkingSet,
                       proc.PeakWorkingSet, pipes, affinity, proc.HandleCount, reads, writes);
                }
                


                    procs.Add(process);
                
            });
        }


        public static void AllCpu(Dictionary<int, double[]> cpus, List<ProcessInfo> procs)
        {
            Parallel.For(0, procs.Count, c =>
            {
            try
            {
                    Console.WriteLine("add");
                    double lastProcessor = Process.GetProcessById(procs[c].GetPID()).TotalProcessorTime.Ticks;
                    double lastTime = DateTime.Now.Ticks;
                    double[] data = {lastProcessor, lastTime };
                    cpus[procs[c].GetPID()]=data;
                    
                }
                catch
                {
                    Console.WriteLine("hello");
                    double[] data = { 0,0 };
                    cpus[procs[c].GetPID()] = data;
                    // do nothing and continue to the next CPU
                }
            });
        }

        public static void GetAllCpu1(Dictionary<int, double[]> cpus, List<ProcessInfo> procs)
        {
            Parallel.For(0, cpus.Count / 2, a =>
            {
                try
                {
                    double newProcessor = Process.GetProcessById(procs[a].GetPID()).TotalProcessorTime.Ticks;
                    double newTime = DateTime.Now.Ticks;
                    procs[a].SetCPU((newProcessor-cpus[procs[a].GetPID()][0] ) / (newTime-cpus[procs[a].GetPID()][1]));
                }
                catch
                {
                    procs[a].SetCPU(0);
                    // do nothing and continue to the next index
                }
            });
        }

        public static void GetAllCpu2(Dictionary<int, double[]> cpus, List<ProcessInfo> procs)
        {
            Parallel.For(cpus.Count / 2, cpus.Count, b =>
            {
                try
                {
                    double newProcessor = Process.GetProcessById(procs[b].GetPID()).TotalProcessorTime.Ticks;
                    double newTime = DateTime.Now.Ticks;
                    procs[b].SetCPU((newProcessor-cpus[procs[b].GetPID()][0]) / (newTime-cpus[procs[b].GetPID()][1]));
                }
                catch
                {
                    procs[b].SetCPU(0);
                    // do nothing and continue to the next index
                }
            });
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


        public static List<string> tcpSockets = new List<string>();
        public static List<string> udpSockets = new List<string>();
        public static List<string> openPipes = new List<string>();
        public static void GetTcpSockets()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            string info = "";
            Console.WriteLine("Active TCP Connections:");
            foreach (TcpConnectionInformation connection in connections)
            {
                info+="Local endpoint: " + connection.LocalEndPoint.Address+":"+ connection.LocalEndPoint.Port+"\n";
                info+="Remote endpoint: "+ connection.RemoteEndPoint.Address+":"+ connection.RemoteEndPoint.Port+"\n";
                info+="State: "+ connection.State;
                tcpSockets.Add(info);
                info = "";
            }
            
        }
        public static void GetUdpSockets()
        {
            IPEndPoint[] endpoints = GetActiveUdpListeners();

            Console.WriteLine("Active UDP Listeners:");
            string info = "";
            foreach (IPEndPoint endpoint in endpoints)
            {
                info+="Endpoint: "+ endpoint.Address+":"+ endpoint.Port;
                udpSockets.Add(info);
                info = "";
            }
            
           
        }
        public static void GetPipes()
        {
            WIN32_FIND_DATA lpFindFileData;

            var ptr = FindFirstFile(@"\\.\pipe\*", out lpFindFileData);
            openPipes.Add(lpFindFileData.cFileName);
            while (FindNextFile(ptr, out lpFindFileData))
            {
                openPipes.Add(lpFindFileData.cFileName);
            }
            FindClose(ptr);

            openPipes.Sort();
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
            procs.Clear();


            Dictionary<int, double[]> cpus=new Dictionary<int, double[]>();

            Thread cpu1;

            Thread Getcpu1;

            Thread Getcpu2;


          
            while (true)
            {
                tcpSockets.Clear();
                udpSockets.Clear();
                openPipes.Clear();
                Console.WriteLine("start list");
                Thread listThread = new Thread(() => ToList(procs));
                Thread tcp = new Thread(() => GetTcpSockets());
                Thread udp = new Thread(() => GetUdpSockets());
                Thread pipes = new Thread(() => GetPipes());
                tcp.Start();
                udp.Start();
                pipes.Start();

                listThread.Start();
                listThread.Join();

                Console.WriteLine(procs.Count);
                Console.WriteLine(Process.GetProcesses().Length);
                Console.WriteLine("end list");


                Console.WriteLine("array");
                //cpus = new Dictionary<int, double[]>();
                Console.WriteLine("Array 2");


                
                cpu1 = new Thread(() => AllCpu(cpus, procs));




                Console.WriteLine("start cpu");
                cpu1.Start();



                cpu1.Join();

                
                Console.WriteLine("end cpu");
                Thread.Sleep(500);



                Console.WriteLine("start get");
                Console.WriteLine($"dict length: {cpus.Count}");
                Getcpu1 = new Thread(() => GetAllCpu1(cpus, procs));
                Getcpu2 = new Thread(() => GetAllCpu2(cpus, procs));



                Getcpu1.Start();


                Getcpu2.Start();
   
               


                Getcpu1.Join();
                
                Getcpu2.Join();
                Console.WriteLine("end get");
                int tempItem = 0;
                DoubleBufferedListView list = (DoubleBufferedListView)form.Controls.Find("listView1", false)[0];

                try {  tempItem = list.TopItem.Index + 1; }

                catch { }
                TextBox search = (TextBox)form.Controls.Find("searchBox", false)[0];
                list.SuspendLayout();
                list.BeginUpdate();
                list.Items.Clear();
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
    .SetValue(list, true);
                
                if (search.Text == "Search")
                {
                    foreach (ProcessInfo proc in procs)
                    {
                        string[] data = {proc.GetName(), proc.GetPID().ToString(), proc.GetCPU().ToString(),
                     proc.GetWS().ToString(), proc.GetReads().ToString(), proc.GetWrites().ToString(),
                     proc.GetThreads().Count.ToString(), proc.GetHandleCount().ToString()};
                        var ListViewItemData = new ListViewItem(data);
                        list.Items.Add(ListViewItemData);
                    }
                    try { list.TopItem = list.Items[tempItem]; }
                    catch { }

                    
                }
                else
                {
                    ComboBox filter = (ComboBox)form.Controls.Find("selectBy", false)[0];
                    if (filter.Text == "PID")
                    {
                        foreach (ProcessInfo process in procs)
                        {
                            if (process.GetPID().ToString().Contains(search.Text))
                            {
                                string[] data = {process.GetName(), process.GetPID().ToString(), process.GetCPU().ToString(),
                                 process.GetWS().ToString(), process.GetReads().ToString(), process.GetWrites().ToString(),
                                 process.GetThreads().Count.ToString(), process.GetHandleCount().ToString()};
                                var ListViewItemData = new ListViewItem(data);
                                list.Items.Add(ListViewItemData);
                            }
                        }
                            
                    }
                    else if (filter.Text == "Name")
                    {
                        foreach (ProcessInfo process in procs)
                            if (process.GetName().ToUpper().Contains(search.Text.ToUpper()))
                            {
                                string[] data = {process.GetName(), process.GetPID().ToString(), process.GetCPU().ToString(),
                                 process.GetWS().ToString(), process.GetReads().ToString(), process.GetWrites().ToString(),
                                 process.GetThreads().Count.ToString(), process.GetHandleCount().ToString()};
                                var ListViewItemData = new ListViewItem(data);
                                list.Items.Add(ListViewItemData);
                            }
                    }
                        

                    

                }
                list.EndUpdate();
                    
                    list.Update();
                list.ResumeLayout();

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


            procs.Clear();
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
