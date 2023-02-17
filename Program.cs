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


namespace POC_NEW
{
    class Program
    {
        static int tableWidth = 170;
        [DllImport("Kernel32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int GetCurrentProcessorNumber();

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
            collected_info[9] = GetThreads(process);
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
        public static string GetThreads(Process process)
        {
            string data = "";
            ProcessThreadCollection threads = process.Threads;
            
            //get cpu of each thread
            foreach (ProcessThread thread in threads)
            {
                long currentTick = (DateTime.Now).Ticks;
                double currentcpu = thread.TotalProcessorTime.TotalMilliseconds;
                Thread.Sleep(100);
                long newTick = (DateTime.Now).Ticks;
                double newcpu = thread.TotalProcessorTime.TotalMilliseconds;
                double totalcpu = 100000 * (double)((newcpu - currentcpu)) / (double)(newTick - currentTick);

            }
            return data;
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
        public static void ToList(List<ProcessInfo> procs)
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process proc in processes)
            {
                ProcessInfo process = new ProcessInfo(proc.Id, 
                    proc.ProcessName, proc.BasePriority, proc.Threads);
                if (!procs.Contains(process))
                {
                    procs.Add(process);
                }
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
        public static void GetAllCpu(PerformanceCounter[] cpus, List<ProcessInfo> procs)
        {
            for (int a = 0; a < cpus.Length; a++)
            {
                try
                {
                    double cpu = cpus[a].NextValue();
                    procs[a].SetCPU(cpu);
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
        public static void GetAllReads(PerformanceCounter[] reads, List<ProcessInfo> procs)
        {
            for (int q = 0; q < reads.Length; q++)
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
        public static void GetAllWrites(PerformanceCounter[] writes, List<ProcessInfo> procs)
        {
            for (int m = 0; m < writes.Length; m++)
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
        public static void GetAllWS(PerformanceCounter[] workingSet, List<ProcessInfo> procs)
        {
            for (int l = 0; l < workingSet.Length; l++)
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
        static IPEndPoint[] GetActiveUdpListeners()
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
        public static void AllInstance(List<ProcessInfo> procs)
        {
            foreach (ProcessInfo proc in procs)
                proc.SetInstance();
        }


        static void Main(string[] args)
        {
            Console.WriteLine("start");
            List<ProcessInfo> procs = new List<ProcessInfo>();
            ToList(procs);

            Console.WriteLine("list");

            PerformanceCounter[] cpus = new PerformanceCounter[procs.Count];
            PerformanceCounter[] reads = new PerformanceCounter[procs.Count];
            PerformanceCounter[] writes = new PerformanceCounter[procs.Count];
            PerformanceCounter[] workingSet = new PerformanceCounter[procs.Count];




            Thread cpu = new Thread(() => AllCpu(cpus, procs));
            Thread read = new Thread(() => AllReads(reads, procs));
            Thread write = new Thread(() => AllWrites(writes, procs));
            Thread ws = new Thread(() => AllWS(workingSet, procs));
            Thread instance = new Thread(() => AllInstance(procs));

            instance.Start();
            instance.Join();
            Console.WriteLine("start cpu");
            cpu.Start();
            read.Start();
            write.Start();
            ws.Start();

            //cpu.Join();
            //read.Join();
            //write.Join();
            //ws.Join();
            Thread.Sleep(10);


            Thread Getcpu = new Thread(() => GetAllCpu(cpus, procs));
            Thread Getread = new Thread(() => GetAllReads(reads, procs));
            Thread Getwrite = new Thread(() => GetAllWrites(writes, procs));
            Thread Getws = new Thread(() => GetAllWS(workingSet, procs));
            Getcpu.Start();
            Getread.Start();
            Getwrite.Start();
            Getws.Start();

            Console.WriteLine("end");
            Console.WriteLine(procs.Count);
            Console.ReadKey();

        }
    }
}
