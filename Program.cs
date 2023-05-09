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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


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


        public static void AllCpu(Dictionary<int, double[]> cpus, Process[] procs)
        {
            Parallel.For(0, procs.Length, c =>
            {
            try
            {
                    
                    double lastProcessor = procs[c].TotalProcessorTime.Ticks;
                    double lastTime = DateTime.Now.Ticks;
                    double[] data = {lastProcessor, lastTime,0 };
                    cpus[procs[c].Id]=data;
                    
                }
                catch
                {
                    Console.WriteLine("hello");
                    double[] data = { 0,0,0 };
                    cpus[procs[c].Id] = data;
                    // do nothing and continue to the next CPU
                }
            });
        }

        public static void GetAllCpu1(Dictionary<int, double[]> cpus, Process[] procs)
        {
            Parallel.For(0, cpus.Count / 2, a =>
            {
                try
                {
                    double newProcessor = procs[a].TotalProcessorTime.Ticks;
                    double newTime = DateTime.Now.Ticks;
                    cpus[procs[a].Id][2]=((newProcessor-cpus[procs[a].Id][0] ) / (newTime-cpus[procs[a].Id][2]));
                }
                catch
                {
                    //procs[a].SetCPU(0);
                    // do nothing and continue to the next index
                }
            });
        }

        public static void GetAllCpu2(Dictionary<int, double[]> cpus, Process[] procs)
        {
            Parallel.For(cpus.Count / 2, cpus.Count, b =>
            {
                try
                {
                    double newProcessor = procs[b].TotalProcessorTime.Ticks;
                    double newTime = DateTime.Now.Ticks;
                    cpus[procs[b].Id][2]=((newProcessor-cpus[procs[b].Id][0]) / (newTime-cpus[procs[b].Id][1]));
                }
                catch
                {
                    //procs[b].SetCPU(0);
                    // do nothing and continue to the next index
                }
            });
        }

        public static List<string> tcpSockets = new List<string>();
        public static List<string> udpSockets = new List<string>();
        public static List<string> openPipes = new List<string>();
        public static void GetTcpSockets()
        {
            //IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            //TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            //string info = "";
            //Console.WriteLine("Active TCP Connections:");
            //foreach (TcpConnectionInformation connection in connections)
            //{
            //    info+="Local endpoint: " + connection.LocalEndPoint.Address+":"+ connection.LocalEndPoint.Port+"\n";
            //    info+="Remote endpoint: "+ connection.RemoteEndPoint.Address+":"+ connection.RemoteEndPoint.Port+"\n";
            //    info+="State: "+ connection.State;
            //    tcpSockets.Add(info);
            //    info = "";
            //}
            
        }
        public static void GetUdpSockets()
        {
            //IPEndPoint[] endpoints = GetActiveUdpListeners();

            //Console.WriteLine("Active UDP Listeners:");
            //string info = "";
            //foreach (IPEndPoint endpoint in endpoints)
            //{
            //    info+="Endpoint: "+ endpoint.Address+":"+ endpoint.Port;
            //    udpSockets.Add(info);
            //    info = "";
            //}
            
           
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

        public static List<string> runProcs=new List<string>();
        public static bool isDone=false;
        public static Dictionary<int, double[]> cpus = new Dictionary<int, double[]>();
        public static void UpdateRun()
        {
            foreach (string fileName in Directory.GetFiles(@"C:\Program Files (x86)", "*.exe", SearchOption.AllDirectories))
            {
                string name = fileName.Split('\\')[fileName.Split('\\').Length-1].ToLower();
                runProcs.Add(name);
            }
            foreach (string fileName in Directory.GetFiles(@"C:\WINDOWS\system32", "*.exe"))
            {
                string name = fileName.Split('\\')[fileName.Split('\\').Length - 1].ToLower(); ;
                runProcs.Add(name);
            }
            isDone = true;
            


        }
        
        public static void UpdateProcsForm(home form)
        {
            Process[] procs = Process.GetProcesses();
            Thread updaterun = new Thread(() => UpdateRun());
            updaterun.Start();

            Thread cpu1;

            Thread Getcpu1;

            Thread Getcpu2;
            bool isFirst = true;

          
            while (true)
            {
                tcpSockets.Clear();
                udpSockets.Clear();
                openPipes.Clear();
                Console.WriteLine("start list");
                
                Thread tcp = new Thread(() => GetTcpSockets());
                Thread udp = new Thread(() => GetUdpSockets());
                Thread pipes = new Thread(() => GetPipes());
                tcp.Start();
                udp.Start();
                pipes.Start();

                Console.WriteLine("end list");


                
                //cpus = new Dictionary<int, double[]>();
                


                
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

                try {  tempItem = list.TopItem.Index; }

                catch { }
                System.Windows.Forms.TextBox search = (System.Windows.Forms.TextBox)form.Controls.Find("searchBox", false)[0];
                list.SuspendLayout();
                list.BeginUpdate();
                list.Items.Clear();
                typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
    .SetValue(list, true);
                
                if (search.Text == "Search")
                {
                    
                    foreach (Process proc in procs)
                    {
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

                        try {
                            if (proc.Threads[0].ThreadState.ToString() == "Suspended")
                            {
                                string[] data = {proc.ProcessName, proc.Id.ToString(), "Suspended",
                     proc.WorkingSet64.ToString(),reads.ToString(), writes.ToString(),
                     proc.StartTime.ToString()};
                                var ListViewItemData = new ListViewItem(data);
                                list.Items.Add(ListViewItemData);
                            }
                            else
                            {
                                string[] data = {proc.ProcessName, proc.Id.ToString(), Math.Round(cpus[proc.Id][2],2).ToString(),
                     proc.WorkingSet64.ToString(),reads.ToString(), writes.ToString(),
                     proc.StartTime.ToString()};
                                var ListViewItemData = new ListViewItem(data);
                                list.Items.Add(ListViewItemData);
                                
                            }
                        }
                        catch
                        {
                            try
                            {
                                if (proc.Threads[0].ThreadState.ToString() == "Suspended")
                                {
                                    string[] data = {proc.ProcessName, proc.Id.ToString(), "Suspended",
                     proc.WorkingSet64.ToString(), reads.ToString(), writes.ToString(),
                     ""};
                                    var ListViewItemData = new ListViewItem(data);
                                    list.Items.Add(ListViewItemData);
                                }
                                else
                                {
                                    string[] data = {proc.ProcessName, proc.Id.ToString(), Math.Round(cpus[proc.Id][2],2).ToString(),
                     proc.WorkingSet64.ToString(), reads.ToString(), writes.ToString(),
                     ""};
                                    var ListViewItemData = new ListViewItem(data);
                                    list.Items.Add(ListViewItemData);
                                    
                                }
                            }
                            catch { }
                        }
                        
                        
                    }
                    try { list.TopItem = list.Items[tempItem]; }
                    catch { }

                    
                }
                else
                {
                    System.Windows.Forms.ComboBox filter = (System.Windows.Forms.ComboBox)form.Controls.Find("selectBy", false)[0];
                    if (filter.Text == "PID")
                    {
                        foreach (ListViewItem item in list.Items)
                        {

                            if (!item.SubItems[1].Text.ToUpper().Contains(search.Text.ToUpper()))
                            {
                                list.Items[item.Index].Remove();
                            }
                        }
                            
                    }
                    else if (filter.Text == "Name")
                    {
                        foreach (ListViewItem item in list.Items)
                        {

                            if (!item.SubItems[0].Text.ToUpper().Contains(search.Text.ToUpper()))
                            {
                                list.Items[item.Index].Remove();
                            }
                        }
                    }
                        

                }
                if (isFirst)
                {
                    // Call the sort method to manually sort.
                    list.Sort();
                    // Set the ListViewItemSorter property to a new ListViewItemComparer
                    // object.
                    list.ListViewItemSorter = new ListViewItemComparer(0, list.Sorting);
                    isFirst = false;
                }
                if (home.isSort)
                {
                    list.Sort();
                    list.ListViewItemSorter = new ListViewItemComparer(home.column, home.sort);
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

        public static Process[] procs;

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            home form = new home();
            Thread runProcUpdate = new Thread(() => UpdateProcsForm(form));
            runProcUpdate.Start();
            Application.Run(form);

            Console.ReadKey();



        }
    }
}
