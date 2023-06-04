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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Principal;
using System.Data.SQLite;

using System.Windows.Forms.PropertyGridInternal;
using static System.Net.WebRequestMethods;


namespace RahimiProcess
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
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);


        public static List<string> tcpSockets = new List<string>();
        public static List<string> udpSockets = new List<string>();
        public static List<string> openPipes = new List<string>();
        public static List<string> runProcs = new List<string>();
        public static bool isDone = false;
        public static Dictionary<int, double[]> cpus = new Dictionary<int, double[]>();
        public static EventWaitHandle waitHandle = new AutoResetEvent(false);
        public static Process[] procs;
        public static Dictionary<int, List<string[]>> dict = new Dictionary<int, List<string[]>>();



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
        public static void AllCpu(Dictionary<int, double[]> cpus, Process[] procs)
        {
            Parallel.For(0, procs.Length, m =>
            {
                try
                {

                    double lastProcessor = procs[m].TotalProcessorTime.Ticks;
                    double lastTime = DateTime.Now.Ticks;
                    double[] data = { lastProcessor, lastTime, 0 };
                    cpus[procs[m].Id] = data;

                }
                catch
                {
                    //Console.WriteLine("hello");
                    double[] data = { 0, 0, 0 };
                    cpus[procs[m].Id] = data;
                    // do nothing and continue to the next CPU
                }
            });
        }
        public static void GetAllCpu1(Dictionary<int, double[]> cpus, Process[] procs)
        {
            Parallel.For(0, cpus.Count / 2, d =>
            {
                try
                {
                    double newProcessor = procs[d].TotalProcessorTime.Ticks;
                    double newTime = DateTime.Now.Ticks;
                    cpus[procs[d].Id][2] = (100 * (newProcessor - cpus[procs[d].Id][0])) / ((newTime - cpus[procs[d].Id][1]) * Environment.ProcessorCount);
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
                    cpus[procs[b].Id][2] = (100 * (newProcessor - cpus[procs[b].Id][0])) / ((newTime - cpus[procs[b].Id][1]) * Environment.ProcessorCount);
                }
                catch
                {
                    //procs[b].SetCPU(0);
                    // do nothing and continue to the next index
                }
            });
        }
        public static void GetTcpSockets()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

            string info = "";
            //Console.WriteLine("Active TCP Connections:");
            foreach (TcpConnectionInformation connection in connections)
            {
                info += "Local endpoint: " + connection.LocalEndPoint.Address + ":" + connection.LocalEndPoint.Port + "\n";
                info += "Remote endpoint: " + connection.RemoteEndPoint.Address + ":" + connection.RemoteEndPoint.Port + "\n";
                info += "State: " + connection.State;
                tcpSockets.Add(info);
                info = "";
            }

        }
        public static void GetUdpSockets()
        {
            IPEndPoint[] endpoints = GetActiveUdpListeners();
            string info = "";
            foreach (IPEndPoint endpoint in endpoints)
            {
                info += "Endpoint: " + endpoint.Address + ":" + endpoint.Port;
                udpSockets.Add(info);
                info = "";
            }
        }
        public static IPEndPoint[] GetActiveUdpListeners()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            List<IPEndPoint> endpointList = new List<IPEndPoint>(); // Use a list instead of an array

            foreach (UnicastIPAddressInformation address in properties.GetUnicastAddresses())
            {
                if (address.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    try
                    {
                        IPEndPoint endpoint = new IPEndPoint(address.Address, 0);
                        using (UdpClient client = new UdpClient(endpoint))
                        {
                            IPEndPoint localEndpoint = client.Client.LocalEndPoint as IPEndPoint; // Use a single endpoint variable
                            endpointList.Add(localEndpoint);
                        }
                    }
                    catch (SocketException)
                    {
                        // ignore exceptions for this example
                    }
                }
            }

            return endpointList.ToArray(); // Convert the list to an array before returning
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

        public static void UpdateRun()
        {
            foreach (string fileName in Directory.GetFiles(@"C:\Program Files (x86)", "*.exe", SearchOption.AllDirectories))
            {
                string name = fileName.Split('\\')[fileName.Split('\\').Length - 1].ToLower();
                if (!runProcs.Contains(name))
                    runProcs.Add(name);
            }
            foreach (string fileName in Directory.GetFiles(@"C:\WINDOWS\system32", "*.exe"))
            {
                string name = fileName.Split('\\')[fileName.Split('\\').Length - 1].ToLower(); ;
                if (!runProcs.Contains(name))
                    runProcs.Add(name);
            }
            isDone = true;
        }
        private static string GetProcessUser(Process process)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                OpenProcessToken(process.Handle, 8, out processHandle);
                WindowsIdentity wi = new WindowsIdentity(processHandle);
                string user = wi.Name;
                return user.Contains(@"\") ? user.Substring(user.IndexOf(@"\") + 1) : user;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    CloseHandle(processHandle);
                }
            }
        }
        public static Dictionary<string, List<string>> existAlert = new Dictionary<string, List<string>>();
        public static bool isChanged = false;
        public static void Getalerts(Dictionary<string, List<string[]>> alerts)
        {
            string alertData = "";
            string path = "alerts.db";
            string cs = @"URI=file:" + Application.StartupPath + "\\alerts.db";
            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            //cpu
            try
            {
                int count = 0;
                foreach (string[] data in alerts["CPU"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //Console.WriteLine($"VALUE {value}");
                    //Console.WriteLine($"DATE: {date}");
                    //Console.WriteLine(date);
                    DateTime now = DateTime.Now;


                    foreach (Process process in procs)
                    {
                        if (cpus[process.Id][2] > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                //     Console.WriteLine("CPUS" + cpus[process.Id][2]);
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"CPU above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"CPU above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"CPU above {value}"] = new List<string>();
                                        existAlert[$"CPU above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"CPU above {value}"] = new List<string>();
                                existAlert[$"CPU above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }
                        }

                        else
                        {
                            try
                            {
                                if (existAlert[$"CPU above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"CPU above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "CPU");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            //             Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //        Console.WriteLine("created new key");
                            //      Console.WriteLine(alerts.Count);
                        }

                    }


                    if (isAdded)
                    {
                        alertData += $"with CPU above {value}\n";
                    }
                }
            }
            catch
            {

            }
            //Working Set
            try
            {
                int count = 0;
                foreach (string[] data in alerts["Working Set"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //      Console.WriteLine($"VALUE {value}");
                    //    Console.WriteLine($"DATE: {date}");
                    //  Console.WriteLine(date);
                    DateTime now = DateTime.Now;





                    foreach (Process process in Process.GetProcesses())
                    {

                        if (process.WorkingSet64 > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"Working Set above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"Working Set above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"Working Set above {value}"] = new List<string>();
                                        existAlert[$"Working Set above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"Working Set above {value}"] = new List<string>();
                                existAlert[$"Working Set above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }

                        }
                        else
                        {
                            try
                            {
                                if (existAlert[$"Working Set above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"Working Set above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "Working Set");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            //  Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //         Console.WriteLine("created new key");
                            //       Console.WriteLine(alerts.Count);
                        }

                    }
                    if (isAdded)
                    {
                        alertData += $"with Working Set above {value}\n";
                    }
                }
            }
            catch { }
            //Private Bytes
            try
            {
                int count = 0;
                foreach (string[] data in alerts["Private Bytes"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //       Console.WriteLine($"VALUE {value}");
                    //      Console.WriteLine($"DATE: {date}");
                    //      Console.WriteLine(date);
                    DateTime now = DateTime.Now;





                    foreach (Process process in Process.GetProcesses())
                    {

                        if (process.PrivateMemorySize64 > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"Private Bytes above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"Private Bytes above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"Private Bytes above {value}"] = new List<string>();
                                        existAlert[$"Private Bytes above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"Private Bytes above {value}"] = new List<string>();
                                existAlert[$"Private Bytes above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }


                        }
                        else
                        {
                            try
                            {
                                if (existAlert[$"Private Bytes above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"Private Bytes above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "Private Bytes");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            //  Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //    Console.WriteLine("created new key");
                            //    Console.WriteLine(alerts.Count);
                        }

                    }

                    if (isAdded)
                    {
                        alertData += $"with Private Bytes above {value}\n";
                    }
                }
            }
            catch { }
            //Virtual Memory
            try
            {
                int count = 0;
                foreach (string[] data in alerts["Virtual Memory"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //    Console.WriteLine($"VALUE {value}");
                    //    Console.WriteLine($"DATE: {date}");
                    //    Console.WriteLine(date);
                    DateTime now = DateTime.Now;



                    foreach (Process process in Process.GetProcesses())
                    {

                        if (process.VirtualMemorySize64 > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"Virtual Memory above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"Virtual Memory above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"Virtual Memory above {value}"] = new List<string>();
                                        existAlert[$"Virtual Memory above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"Virtual Memory above {value}"] = new List<string>();
                                existAlert[$"Virtual Memory above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }

                        }
                        else
                        {
                            try
                            {
                                if (existAlert[$"Virtual Memory above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"Virtual Memory above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "Virtual Memory");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //Console.WriteLine("created new key");
                            //Console.WriteLine(alerts.Count);
                        }

                    }


                    if (isAdded)
                    {
                        alertData += $"with Virtual Memory above {value}\n";
                    }
                }

            }

            catch { }
            //Reads
            try
            {
                int count = 0;
                foreach (string[] data in alerts["Reads"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //Console.WriteLine($"VALUE {value}");
                    //Console.WriteLine($"DATE: {date}");
                    //Console.WriteLine(date);
                    DateTime now = DateTime.Now;





                    foreach (Process process in Process.GetProcesses())
                    {

                        double reads = 0; ;

                        try
                        {
                            if (GetProcessIoCounters(process.Handle, out IO_COUNTERS counters))
                            {
                                reads = counters.ReadOperationCount;

                            }
                        }
                        catch
                        {

                        }
                        if (reads > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"Reads above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"Reads above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"Reads above {value}"] = new List<string>();
                                        existAlert[$"Reads above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"Reads above {value}"] = new List<string>();
                                existAlert[$"Reads above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }

                        }
                        else
                        {
                            try
                            {
                                if (existAlert[$"Reads above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"Reads above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "Reads");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            //Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //Console.WriteLine("created new key");
                            //Console.WriteLine(alerts.Count);
                        }

                    }


                    if (isAdded)
                    {
                        alertData += $"with Reads above {value}\n";
                    }
                }

            }

            catch { }
            //Writes
            try
            {
                int count = 0;
                foreach (string[] data in alerts["Writes"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //Console.WriteLine($"VALUE {value}");
                    //Console.WriteLine($"DATE: {date}");
                    //Console.WriteLine(date);
                    DateTime now = DateTime.Now;





                    foreach (Process process in Process.GetProcesses())
                    {

                        double writes = 0; ;

                        try
                        {
                            if (GetProcessIoCounters(process.Handle, out IO_COUNTERS counters))
                            {
                                writes = counters.WriteOperationCount;

                            }
                        }
                        catch
                        {

                        }
                        if (writes > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"Writes above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"Writes above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"Writes above {value}"] = new List<string>();
                                        existAlert[$"Writes above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"Writes above {value}"] = new List<string>();
                                existAlert[$"Writes above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }

                        }
                        else
                        {
                            try
                            {
                                if (existAlert[$"Writes above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"Writes above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "Writes");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            //Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //Console.WriteLine("created new key");
                            //Console.WriteLine(alerts.Count);
                        }

                    }

                    if (isAdded)
                    {
                        alertData += $"with Writes above {value}\n";
                    }
                }
            }

            catch { }
            //Handle Count
            try
            {
                int count = 0;
                foreach (string[] data in alerts["Handle Count"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //Console.WriteLine($"VALUE {value}");
                    //Console.WriteLine($"DATE: {date}");
                    //Console.WriteLine(date);
                    DateTime now = DateTime.Now;

                    //Console.WriteLine("HEREERERER");



                    foreach (Process process in Process.GetProcesses())
                    {

                        if (process.HandleCount > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"Handle Count above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"Handle Count above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"Handle Count above {value}"] = new List<string>();
                                        existAlert[$"Handle Count above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"Handle Count above {value}"] = new List<string>();
                                existAlert[$"Handle Count above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }

                        }
                        else
                        {
                            try
                            {
                                if (existAlert[$"Handle Count above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"Handle Count above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "Handle Count");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            //Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //Console.WriteLine("created new key");
                            //Console.WriteLine(alerts.Count);
                        }

                    }


                    if (isAdded)
                    {
                        alertData += $"with Handle Count above {value}\n";
                    }
                }
            }

            catch { }
            //Thread Count
            try
            {
                int count = 0;
                foreach (string[] data in alerts["Thread Count"])
                {
                    count++;
                    double value = double.Parse(data[0]);
                    DateTime date = DateTime.Parse(data[1]);
                    bool isAdded = false;
                    //Console.WriteLine($"VALUE {value}");
                    //Console.WriteLine($"DATE: {date}");
                    //Console.WriteLine(date);
                    DateTime now = DateTime.Now;

                    //Console.WriteLine("HEREERERER");



                    foreach (Process process in Process.GetProcesses())
                    {

                        if (process.Threads.Count > value)
                        {
                            if (date.AddHours(2) < now)
                            {
                                alertData += $"{process.ProcessName} ({process.Id}), ";

                                isAdded = true;
                            }
                            try
                            {
                                if (!existAlert[$"Thread Count above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {

                                    try
                                    {
                                        existAlert[$"Thread Count above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    catch
                                    {
                                        existAlert[$"Thread Count above {value}"] = new List<string>();
                                        existAlert[$"Thread Count above {value}"].Add($"{process.ProcessName} ({process.Id})");
                                    }
                                    isChanged = true;
                                }
                            }
                            catch
                            {
                                existAlert[$"Thread Count above {value}"] = new List<string>();
                                existAlert[$"Thread Count above {value}"].Add($"{process.ProcessName} ({process.Id})");
                            }
                        }
                        else
                        {
                            try
                            {
                                if (existAlert[$"Thread Count above {value}"].Contains($"{process.ProcessName} ({process.Id})"))
                                {
                                    existAlert[$"Thread Count above {value}"].Remove($"{process.ProcessName} ({process.Id})");
                                    isChanged = true;
                                }
                            }
                            catch { }
                        }
                    }
                    con = new SQLiteConnection(cs);
                    con.Open();
                    cmd = new SQLiteCommand(con);
                    cmd.CommandText = $"UPDATE info SET date=@date WHERE field=@fieldold AND value=@valueold";

                    cmd.Parameters.AddWithValue("@fieldold", "Thread Count");
                    cmd.Parameters.AddWithValue("@valueold", value.ToString());
                    cmd.Parameters.AddWithValue("@date", now.ToString());
                    cmd.ExecuteNonQuery();

                    string stm = "SELECT * FROM info";
                    SQLiteCommand command = new SQLiteCommand(stm, con);
                    dr = command.ExecuteReader();
                    alerts.Clear();
                    while (dr.Read())
                    {
                        string field = dr.GetString(0);
                        string[] datanew = new string[2] { dr.GetString(1), dr.GetString(2) };
                        try
                        {
                            alerts[field].Add(datanew);
                            //Console.WriteLine("add to existing list");
                        }
                        catch
                        {
                            alerts[field] = new List<string[]> { datanew };
                            //Console.WriteLine("created new key");
                            //Console.WriteLine(alerts.Count);

                        }

                    }
                    if (isAdded)
                    {
                        alertData += $"with Thread Count above {value}\n";
                    }
                }
            }

            catch { }


        }
        public static void GetConnections()
        {

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "netstat",
                Arguments = $"-noa",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                Verb="runas"
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            string[] outputNew = output.Split('\n');
            for (int i = 4; i < outputNew.Length - 1; i++)
            {

                string proto = outputNew[i].Substring(2, 5);
                string local = outputNew[i].Substring(9, 23).Trim();
                string forign = outputNew[i].Substring(31, 23).Trim();
                string state = outputNew[i].Substring(54, 17).Trim();
                string pid = outputNew[i].Substring(66).Trim();
                string[] info = new string[4] { proto, local, forign, state };
                try
                {
                    bool exists = false;
                    foreach (string[] data in dict[int.Parse(pid)])
                    {
                        if (data[0] == proto && data[1] == local && data[2] == forign)
                            exists = true;
                    }
                    if (!exists)
                        dict[int.Parse(pid)].Add(info);
                    
                }
                catch
                {
                    dict[int.Parse(pid)] = new List<string[]>();
                }
            }

        }
        public static Dictionary<string, int> locations = new Dictionary<string, int>();
        public static List<string> activeId = new List<string>();
        public static void InsertAllProcs(DoubleBufferedListView list)
        {
            foreach (Process proc in Process.GetProcesses())
            {

                string reads = "0"; ;
                string writes = "0";
                string cpuVal = "0";
                string date = "";
                try
                {
                    if (GetProcessIoCounters(proc.Handle, out IO_COUNTERS counters))
                    {
                        reads = counters.ReadOperationCount.ToString();
                        writes = counters.WriteOperationCount.ToString();
                    }
                }
                catch { }
                try
                {
                    if (proc.Threads[0].WaitReason.ToString() == "Suspended")
                        cpuVal = "Suspended";
                    else
                    {
                        try
                        {
                            cpuVal = Math.Round(cpus[proc.Id][2], 2).ToString();
                        }
                        catch { }
                    }

                }
                catch { }
                try
                {
                    date = proc.StartTime.ToString();
                }
                catch { }

                string[] data = new string[]{proc.ProcessName, proc.Id.ToString(), cpuVal,
                            proc.WorkingSet64.ToString(),reads, writes, GetProcessUser(proc), proc.Threads.Count.ToString(),
                            date};
                list.Items.Add(new ListViewItem(data));
            }
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
            string path = "alerts.db";
            string cs = @"URI=file:" + Application.StartupPath + "\\alerts.db";
            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;


            

            DoubleBufferedListView list = (DoubleBufferedListView)form.Controls.Find("listView1", false)[0];


            System.Windows.Forms.TextBox search = (System.Windows.Forms.TextBox)form.Controls.Find("searchBox", false)[0];

            int tempItem = 0;
            int selected = -1;
            list.BeginUpdate();
            if (search.Text == "Search")
            {

                InsertAllProcs(list);
               
            }
            list.EndUpdate();
            list.Update();

            while (true)
            {
                con = new SQLiteConnection(cs);
                con.Open();
                string stm = "SELECT * FROM info";
                cmd = new SQLiteCommand(stm, con);
                dr = cmd.ExecuteReader();

                Dictionary<string, List<string[]>> alerts = new Dictionary<string, List<string[]>>();



                while (dr.Read())
                {
                    string field = dr.GetString(0);
                    string[] data = new string[2] { dr.GetString(1), dr.GetString(2) };
                    try
                    {
                        alerts[field].Add(data);
                        //Console.WriteLine("add to existing list");
                    }
                    catch
                    {
                        alerts[field] = new List<string[]> { data };
                        //Console.WriteLine("created new key");
                        //Console.WriteLine(alerts.Count);
                    }

                }
                tcpSockets.Clear();
                udpSockets.Clear();
                openPipes.Clear();
                //Console.WriteLine("start list");

                Thread tcp = new Thread(() => GetTcpSockets());
                Thread udp = new Thread(() => GetUdpSockets());
                Thread pipes = new Thread(() => GetPipes());
                Thread connections = new Thread(() => GetConnections());
                Thread alertsThread = new Thread(() => Getalerts(alerts));

                alertsThread.Start();
                tcp.Start();
                udp.Start();
                pipes.Start();
                connections.Start();

                //Console.WriteLine("end list");
                //cpus = new Dictionary<int, double[]>();
                cpu1 = new Thread(() => AllCpu(cpus, procs));

                //Console.WriteLine("start cpu");
                cpu1.Start();
                cpu1.Join();

                //Console.WriteLine("end cpu");
                Thread.Sleep(500);
                //Console.WriteLine("start get");
                //Console.WriteLine($"dict length: {cpus.Count}");

                Getcpu1 = new Thread(() => GetAllCpu1(cpus, procs));
                Getcpu2 = new Thread(() => GetAllCpu2(cpus, procs));
                procs = Process.GetProcesses();
                Getcpu1.Start();
                Getcpu2.Start();

                Getcpu1.Join();
                Getcpu2.Join();



                List<ListViewItem> toAdd = new List<ListViewItem>();
                locations = new Dictionary<string, int>();
                activeId = new List<string>();
                
                if (search.Text == "Search")
                {
                    if(procs.Length-50>list.Items.Count)
                    {
                        list.BeginUpdate();
                        list.Items.Clear();
                        InsertAllProcs(list);
                        list.EndUpdate();
                        list.Update();
                    }    

                    for (int i = 0; i < list.Items.Count; i++)
                    {
                        try
                        {
                            locations.Add(list.Items[i].SubItems[1].Text, i);
                        }
                        catch { }
                    }

                    Parallel.For(0, procs.Length, i =>
                    {
                        Process proc = procs[i];
                        

                        try
                        {
                            activeId.Add(proc.Id.ToString());
                        }
                        catch { }
                        string reads = "0"; ;
                        string writes = "0";
                        string cpuVal = "0";
                        string date = "";
                        try
                        {
                            if (GetProcessIoCounters(proc.Handle, out IO_COUNTERS counters))
                            {
                                reads = counters.ReadOperationCount.ToString();
                                writes = counters.WriteOperationCount.ToString();
                            }
                        }
                        catch { }
                        try
                        {
                            if (proc.Threads[0].WaitReason == ThreadWaitReason.Suspended)
                                cpuVal = "Suspended";
                            else
                            {
                                try
                                {
                                    cpuVal = Math.Round(cpus[proc.Id][2], 2).ToString();

                                }
                                catch { }
                            }

                        }
                        catch { }
                        try
                        {
                            date = proc.StartTime.ToString();
                        }
                        catch { }
                        string[] data = new string[]{proc.ProcessName, proc.Id.ToString(), cpuVal,
                            proc.WorkingSet64.ToString(),reads, writes, GetProcessUser(proc), proc.Threads.Count.ToString(),
                            date};

                        try
                        {


                            
                                if (!locations.ContainsKey(proc.Id.ToString()))
                                {
                                    ListViewItem item = new ListViewItem(data);
                                    toAdd.Add(item);
                                }
                            if (locations.ContainsKey(proc.Id.ToString()))
                            {

                                int location = locations[proc.Id.ToString()];
                                list.Items[location].SubItems[2].Text = cpuVal;
                                list.Items[location].SubItems[3].Text = proc.WorkingSet64.ToString();
                                list.Items[location].SubItems[4].Text = reads;
                                list.Items[location].SubItems[5].Text = writes;
                                list.Items[location].SubItems[7].Text = proc.Threads.Count.ToString();


                            }
                        }
                        catch { }

                    });
                    foreach (ListViewItem item in toAdd)
                        list.Items.Add(item);
                    toAdd.Clear();

                    try { tempItem = list.TopItem.Index; }

                    catch { }
                    try { selected = list.SelectedItems[0].Index; }
                    catch { }
                    try { list.TopItem = list.Items[tempItem]; }
                    catch { }
                    if (selected != -1)
                    {
                        try { list.Items[selected].Selected = true; list.Items[selected].Checked = true; }
                        catch { }
                    }


                }
                else
                {
                    try { tempItem = list.TopItem.Index; }

                    catch { }
                    try { selected = list.SelectedItems[0].Index; }
                    catch { }
                    list.BeginUpdate();
                    list.Items.Clear();
                    System.Windows.Forms.ComboBox filter = (System.Windows.Forms.ComboBox)form.Controls.Find("selectBy", false)[0];
                    if (filter.Text == "PID")
                    {

                        foreach (Process proc in procs)
                        {
                            if (proc.Id.ToString().Contains(search.Text))
                            {
                                string reads = "0"; ;
                                string writes = "0";
                                string cpuVal = "0";
                                string date = "";
                                try
                                {
                                    if (GetProcessIoCounters(proc.Handle, out IO_COUNTERS counters))
                                    {
                                        reads = counters.ReadOperationCount.ToString();
                                        writes = counters.WriteOperationCount.ToString();
                                    }
                                }
                                catch
                                {

                                }
                                try
                                {
                                    if (proc.Threads[0].WaitReason.ToString() == "Suspended")
                                        cpuVal = "Suspended";
                                    else
                                    {
                                        try
                                        {
                                            cpuVal = Math.Round(cpus[proc.Id][2], 2).ToString();
                                        }
                                        catch { }
                                    }

                                }
                                catch { }
                                try
                                {
                                    date = proc.StartTime.ToString();
                                }
                                catch { }

                                string[] data = new string[]{proc.ProcessName, proc.Id.ToString(), cpuVal,
                     proc.WorkingSet64.ToString(),reads, writes, GetProcessUser(proc), proc.Threads.Count.ToString(),
                     date};
                                list.Items.Add(new ListViewItem(data));


                            }
                        }

                    }
                    else if (filter.Text == "Name")
                    {
                        
                        foreach (Process proc in procs)
                            if (proc.ProcessName.ToUpper().Contains(search.Text.ToUpper()))
                            {
                                string reads = "0"; ;
                                string writes = "0";
                                string cpuVal = "0";
                                string date = "";
                                try
                                {
                                    if (GetProcessIoCounters(proc.Handle, out IO_COUNTERS counters))
                                    {
                                        reads = counters.ReadOperationCount.ToString();
                                        writes = counters.WriteOperationCount.ToString();
                                    }
                                }
                                catch
                                {

                                }
                                try
                                {
                                    if (proc.Threads[0].WaitReason.ToString() == "Suspended")
                                        cpuVal = "Suspended";
                                    else
                                    {
                                        try
                                        {
                                            cpuVal = Math.Round(cpus[proc.Id][2], 2).ToString();
                                        }
                                        catch { }
                                    }

                                }
                                catch { }
                                try
                                {
                                    date = proc.StartTime.ToString();
                                }
                                catch { }

                                string[] data = new string[]{proc.ProcessName, proc.Id.ToString(), cpuVal,
                     proc.WorkingSet64.ToString(),reads, writes, GetProcessUser(proc), proc.Threads.Count.ToString(),
                     date};
                                list.Items.Add(new ListViewItem(data));
                                
                            }
                    }
                    try { list.TopItem = list.Items[tempItem]; }
                    catch { }
                    if (selected != -1)
                    {
                        try { list.Items[selected].Selected = true; list.Items[selected].Checked = true; }
                        catch { }
                    }
                    list.EndUpdate();
                    list.Update();
                }

                


               
                
                
                

            }
        }
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


