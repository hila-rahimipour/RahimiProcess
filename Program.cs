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
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
        }

        //static void Main(string[] args)
        //{
        //    Process[] processes = Process.GetProcesses();
        //    Dictionary<int, object[]> info = new Dictionary<int, object[]>();
        //    int pid = 1376;
        //    Process process=Process.GetProcessById(pid);
        //    Console.WriteLine("would you like to recieve info about process or thread? (p/t):");
        //    char selection = char.Parse(Console.ReadLine());
        //    if (selection == 'p')
        //    {
        //        Console.WriteLine("enter process id: ");
        //        int process_id = int.Parse(Console.ReadLine());
        //        try
        //        {
        //            Process selected_process = Process.GetProcessById(process_id);
        //            Process[] same_name = Process.GetProcessesByName(selected_process.ProcessName);
        //            int instance = 0;
        //            if (same_name.Length != 0)
        //            {
        //                for (int i=0; i < same_name.Length; i++)
        //                {
        //                    if (same_name[i].Id == selected_process.Id)
        //                        instance = i;
        //                }
        //            }
        //            Console.WriteLine("would you like to get part 1/2 (1/2)?");
        //            int select = int.Parse(Console.ReadLine());
        //            Console.Clear();
        //            PrintRow($"information about process {selected_process.ProcessName}, PID of {selected_process.Id}- PART 1");
        //            PrintLine();
        //            if (select == 1)
        //            {
        //                string data = "|";
        //                int count = 0;
        //                foreach (ProcessThread thread in selected_process.Threads)
        //                {
        //                    data += thread.Id + "," + thread.ThreadState + "|";
        //                    count++;
        //                    if (count == 5)
        //                    {
        //                        Console.WriteLine(data);
        //                        data = "|";
        //                        count = 0;
        //                    }
        //                }
        //                double cpu_old = 0;
        //                double old_private = 0;
        //                PrintRow("priority","cpu", "d. cpu", "p. bytes", "d. p. bytes", "peak private", "virtual size", "affinity");
        //                while (true)
        //                {
        //                    double cpu = GetCpuProcess(selected_process, instance);



        //                    double private_size = selected_process.PrivateMemorySize64;
        //                    double peak_private = selected_process.PeakVirtualMemorySize64;
        //                    double virtual_size = selected_process.VirtualMemorySize64;
        //                    //0- cpu, 1- d. cpu, 2- private bytes, 3- d. private bytes, 4- peak private,
        //                    // 5- virtual size, 6- working set, 7- d. working set, 8- ws private, 9- ws shareable
        //                    //10- peak working set, 11- read, 12- write, 13- handle
        //                    PrintRow($"{selected_process.BasePriority}", $"{cpu}", $"{cpu - cpu_old}", $"{private_size}", $"{private_size - old_private}",
        //                        $"{peak_private}", $"{virtual_size}", $"{Convert.ToString((int)selected_process.ProcessorAffinity, 2)}");
        //                    cpu_old = GetCpuProcess(selected_process, instance);
        //                    old_private = selected_process.PrivateMemorySize64;
        //                    Thread.Sleep(1000);
        //                }
        //            }
        //            if (select == 2)
        //            {
        //                PrintRow("WS", "d. WS", "private WS", "shared WS", "peak WS", "read", "write", "handle count");
        //                double old_ws = 0;
        //                while (true)
        //                {
        //                    double privateWS = GetWS(selected_process, instance);
        //                    double working_set = selected_process.WorkingSet64;
        //                    double shared = selected_process.WorkingSet64 - selected_process.PagedMemorySize64;
        //                    double peak_working = selected_process.PeakWorkingSet64;
        //                    double reads = GetOutput(selected_process, instance);
        //                    double writes = GetOutput(selected_process, instance);
        //                    PrintRow($"{working_set}", $"{working_set - old_ws}", $"{privateWS}", $"{shared}",
        //                        $"{peak_working}", $"{reads}", $"{writes}", $"{selected_process.HandleCount}");
        //                    Thread.Sleep(1000);
        //                }
        //                //0- cpu, 1- d. cpu, 2- private bytes, 3- d. private bytes, 4- peak private,
        //                // 5- virtual size, 6- working set, 7- d. working set, 8- ws private, 9- ws shareable
        //                //10- peak working set, 11- read, 12- write, 13- handle
        //            }
        //            //give process info
        //        }
        //        catch
        //        {
        //            Console.WriteLine("process doesnt exist, try to run the program again");
        //        }
        //    }
        //    if (selection == 't')
        //    {
        //        Console.WriteLine("enter process id: ");
        //        int process_id = int.Parse(Console.ReadLine());
        //        try
        //        {
        //            Process selected_process = Process.GetProcessById(process_id);
        //            foreach (ProcessThread thread in selected_process.Threads)
        //            {
        //                Console.WriteLine($"Thread Id: {thread.Id}");
        //            }
        //            Console.WriteLine("select thread Id: ");
        //            int thread_id = int.Parse(Console.ReadLine());
        //            bool is_exist = false;
        //            ProcessThread selected_thread=selected_process.Threads[0];
        //            foreach (ProcessThread thread in selected_process.Threads)
        //            {
        //                if (thread_id == thread.Id)
        //                {
        //                    is_exist = true;
        //                    selected_thread = thread;
        //                    break;

        //                }
        //            }
        //            if (!is_exist)
        //                Console.WriteLine("thread doesnt exist");
        //            else
        //            {
        //                Console.Clear();
        //                //give thread info

        //                PrintRow($"information about thread {selected_thread.Id} from process" +
        //                    $" {selected_process.ProcessName}, {selected_process.Id}");
        //                PrintLine();
        //                if (selected_thread.ThreadState.ToString()=="Wait")
        //                    PrintRow("b. priority", "d. priority", "ideal processor", "State", "Wait Reason", "CPU");
        //                else
        //                    PrintRow("b. priority", "d. priority", "ideal processor", "State", "CPU");
        //                PrintLine();
        //                while (true)
        //                {
        //                    if (selected_thread.ThreadState.ToString() == "Wait")
        //                        PrintRow($"{selected_thread.BasePriority}", $"{selected_thread.CurrentPriority}", $"{ideal}",
        //                        $"{selected_thread.ThreadState}", $"{selected_thread.WaitReason}", $"{GetCpuThread(selected_thread)}");
        //                    else
        //                        PrintRow($"{selected_thread.BasePriority}", $"{selected_thread.CurrentPriority}", $"{ideal}",
        //                       $"{selected_thread.ThreadState}", $"{GetCpuThread(selected_thread)}");
        //                    Thread.Sleep(1000);
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            Console.WriteLine("process doesnt exist, try to run the program again");
        //        }
        //    }


        //    //      Parallel.For(0, 1000000, state => Console.WriteLine("Thread Id = {0}, CoreId = {1}",
        //    //Thread.CurrentThread.ManagedThreadId,
        //    //GetCurrentProcessorNumber()));

        //    //while (true)
        //    //{
        //    //    OneProcess(info, process);
        //    //    Thread.Sleep(2000);
        //    //}
        //    Console.ReadKey();

        //}
    }
}
