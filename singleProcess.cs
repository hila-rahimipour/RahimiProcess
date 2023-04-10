using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Net.NetworkInformation;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.ObjectModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace POC_NEW
{ 
    public partial class singleProcess : Form
    {
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
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool GetProcessIoCounters(IntPtr hProcess, out IO_COUNTERS counters);
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        static extern bool GetThreadIdealProcessorEx(IntPtr hThread, out PROCESSOR_NUMBER lpIdealProcessor);
        const uint THREAD_QUERY_INFORMATION = 0x0040;

        public int pid;
        public static ProcessInfo process;
        int count = 0;
        public singleProcess(int pid)
        {
            this.pid = pid;
            this.Text = $"Process {pid}";
            InitializeComponent();
            process = new ProcessInfo(-1, "-1", -1, null, -1, -1, -1, -1, -1, null, "-1", -1, -1, -1);
            cpuGraph.ChartAreas[0].AxisY.Maximum = 100;
            cpuGraph.ChartAreas[0].AxisY.Minimum = 0;
            cpuGraph.ChartAreas[0].AxisX.Maximum = 30;
            cpuGraph.ChartAreas[0].AxisX.Minimum = 0;

            ioGraph.ChartAreas[0].AxisY.Maximum = 1000;
            ioGraph.ChartAreas[0].AxisY.Minimum = 0;
            ioGraph.ChartAreas[0].AxisX.Maximum = 30;
            ioGraph.ChartAreas[0].AxisX.Minimum = 0;




        }


        public string GetIdealProcessor(uint threadId)
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
        public void SingleProcess(int pid)
        {

            //need to add try & catch in case of process doesnt exists
            Process proc = Process.GetProcessById(pid);

            this.Text = $"{proc.ProcessName}: {proc.Id}";


            Process[] sameName = Process.GetProcessesByName(proc.ProcessName);
            int instance = 0;
            for (int i = 0; i < sameName.Length; i++)
            {
                if (sameName[i].Id == proc.Id)
                    instance = i;
            }
            List<string> pipes = new List<string>();

            string pipeText = "";
            RichTextBox tempRichTextBox1 = new RichTextBox();
            tempRichTextBox1.Font = new Font("Consolas", 10);
            tempRichTextBox1.BackColor = Color.White;
            tempRichTextBox1.BorderStyle = BorderStyle.None;
            tempRichTextBox1.WordWrap = false;
            try
            {
                foreach (ProcessModule module in proc.Modules)
                {
                    //Check if the module represents a pipe
                    if (module.ModuleName.Contains("pipe"))
                    {
                        pipes.Add(module.ModuleName);
                        pipeText += module.ModuleName + "\n";
                    }
                }
            }
            catch
            {
                pipeText = "Can't access";
                pipes.Add("-1");
            }
            tempRichTextBox1.AppendText(pipeText);
            try
            {

                BeginInvoke(new Action(() =>
                {
                    dlls.SuspendLayout();
                    dlls.Text = pipeText;
                    dlls.ResumeLayout();

                }));
                start.Text = proc.StartTime.ToString();
            }
            catch { }
            

            string affinity;
            try
            {
                affinity = Convert.ToString((int)proc.ProcessorAffinity, 2);
            }
            catch
            {
                affinity = "-1";
            }
            double reads = 0; 
            double writes = 0;
            try
            {
                if (GetProcessIoCounters(proc.Handle, out IO_COUNTERS counters))
                {
                    reads = counters.ReadOperationCount;
                    writes = counters.WriteOperationCount;
                }
            }
            catch
            {

            }
            
            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ProcessId = {proc.Id}");
            long virtualMemorySize = Convert.ToInt64(-1);
            foreach (ManagementObject obj in searcher.Get())
            {
                virtualMemorySize = Convert.ToInt64(obj["VirtualSize"]);
                process.SetWS(Convert.ToInt64(obj["WorkingSetSize"]));
                process.SetWS(Convert.ToInt64(obj["PeakWorkingSetSize"]));
                Console.WriteLine(process.GetPeakWs());
                
                command.Text = obj["CommandLine"].ToString();
                path.Text = obj["ExecutablePath"].ToString();
                parent.Text = obj["ParentProcessId"].ToString();

            }
            if (process.GetPeakWs() == -1)
                process.SetWS(proc.PeakWorkingSet64);
            process.SetPrivateMemory(proc.PrivateMemorySize64);

            
            process.SetName(proc.ProcessName);
            process.SetVirtual(virtualMemorySize);
            process.SetThreads(proc.Threads);
            process.SetInstance(instance);
            
            process.SetPipes(pipes);
            if (affinity == "-1")
                process.SetAffinity("unavailable");
            else
                process.SetAffinity(affinity);
                process.SetPriority(proc.BasePriority);
                process.SetHandle(proc.HandleCount);
                process.SetReads(reads);
                process.SetWrites(writes);

                
                Console.WriteLine("done processing");

            double[] cpu = new double[2] { 0, 0 };
            PerformanceCounter workingSet;
            PerformanceCounter readBytes;
            PerformanceCounter writeBytes;
            double read = 0;
            double write = 0;

            if (instance == 0)
            {
                workingSet = new PerformanceCounter("Process", "Working Set - Private", proc.ProcessName);
                readBytes = new PerformanceCounter("Process", "IO Read Bytes/sec", proc.ProcessName);
                writeBytes = new PerformanceCounter("Process", "IO Write Bytes/sec", proc.ProcessName);
                Console.WriteLine("in cpu");
            }
            else
            {
                workingSet = new PerformanceCounter("Process", "Working Set - Private", proc.ProcessName + "#" + instance);
                readBytes = new PerformanceCounter("Process", "IO Read Bytes/sec", proc.ProcessName + "#" + instance);
                writeBytes = new PerformanceCounter("Process", "IO Write Bytes/sec", proc.ProcessName + "#" + instance);
                Console.WriteLine("in cpu");
            }
            try
            {
                cpu[0] = proc.TotalProcessorTime.Ticks;
                cpu[1] = DateTime.Now.Ticks;
            }
            catch
            {
                cpu[0] = 0;
                cpu[1] = 0;
            }
            
            workingSet.NextValue();
            readBytes.NextValue();
            writeBytes.NextValue();
            Thread.Sleep(500);
            Console.WriteLine("here");
            reads=readBytes.NextValue();
            writes = writeBytes.NextValue();
            try
            {
                process.SetCPU(((DateTime.Now.Ticks - cpu[1]) / (proc.TotalProcessorTime.Ticks - cpu[0])) / Environment.ProcessorCount);
            }
            catch
            {
                process.SetCPU(0);
            }
            
            double wsValue = workingSet.NextValue();
            process.SetPrivateWS(wsValue);
            process.SetShared(process.GetWS() - wsValue);
            Console.WriteLine("done cpu single");

            try
            {
                if (process.GetCPU() < 0.5)
                    cpuGraph.ChartAreas[0].AxisY.Maximum = 0.5;
                else if (process.GetCPU() < 7)
                    cpuGraph.ChartAreas[0].AxisY.Maximum = 7;
                else if (process.GetCPU() < 10)
                    cpuGraph.ChartAreas[0].AxisY.Maximum = 10;
                else if (process.GetCPU() < 20)
                    cpuGraph.ChartAreas[0].AxisY.Maximum = 20;

                

            }
            catch { }


            RichTextBox tempRichTextBox = new RichTextBox();
            tempRichTextBox.Font = new Font("Consolas", 10);
            tempRichTextBox.BackColor = Color.White;
            tempRichTextBox.BorderStyle = BorderStyle.None;
            tempRichTextBox.WordWrap = false;


            string moduleText = "";
            try
            {

                foreach (ProcessModule module in proc.Modules)
                    moduleText += module.ModuleName + "\n";
            }
            catch
            {
                moduleText = "Can't Access";
            }
            tempRichTextBox.AppendText(moduleText);
            try
            {

                BeginInvoke(new Action(() =>
                {
                    dlls.SuspendLayout();
                    dlls.Text = moduleText;
                    dlls.ResumeLayout();

                }));
                start.Text = proc.StartTime.ToString();
            }
            catch { }

            try
            {
                
                cpuGraph.Series[0].Points.AddXY(count, process.GetCPU());
                ioGraph.Series[0].Points.AddXY(count, reads);
                ioGraph.Series[1].Points.AddXY(count, writes);
                
                if (cpuGraph.Series[0].Points.Count > 30)
                {
                    count = 30;
                    cpuGraph.Series[0].Points.RemoveAt(0);
                    
                    foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in cpuGraph.Series[0].Points)
                    {
                        point.XValue -= 1;
                    }
                }
                if (ioGraph.Series[0].Points.Count > 30)
                {
                    count = 30;
                    ioGraph.Series[0].Points.RemoveAt(0);
                    ioGraph.Series[1].Points.RemoveAt(0);

                    foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in ioGraph.Series[0].Points)
                    {
                        point.XValue -= 1;
                    }
                    foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in ioGraph.Series[1].Points)
                    {
                        point.XValue -= 1;
                    }
                }

            }
            catch { }
            count++;


        }
        
            private void singleProcess_Load(object sender, EventArgs e)
        {
            Thread update = new Thread(()=>Update());
            Thread updateThread = new Thread(() => ThreadUpdate());
            updateThread.Start();
            update.Start();

        }

        public void Update()
        {
            while (true)
            {

                process = new ProcessInfo(-1, null, -1, null, -1, -1, -1,
                    -1, -1, null, null, -1, -1, -1);
                SingleProcess(pid);

                if (process.GetName() == "process terminated")
                {
                    terminate.Text = "Process Ended,\nClosing Window";
                    Thread.Sleep(5000);
                    this.Close();
                }
                else
                {
                    procName.Text = process.GetName();
                    processID.Text = pid.ToString();
                    procPriority.Text = process.GetPriority().ToString();
                    procAffinity.Text = process.GetAffinity();
                    procHandle.Text = process.GetHandleCount().ToString();
                    procPrivate.Text = process.GetPrivateSize().ToString();
                    procVirtual.Text = process.GetVirtualSize().ToString();
                    procWS.Text = process.GetWS().ToString();
                    procPrivateWS.Text = process.GetPrivateWS().ToString();
                    procSharedWS.Text = process.GetShared().ToString();
                    procPeakWS.Text = process.GetPeakWs().ToString();
                    procReads.Text = process.GetReads().ToString();
                    procWrites.Text = process.GetWrites().ToString();
                    
                    try
                    {
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE " +
                            $"ProcessId = {process.GetPID()}");

                        foreach (ManagementObject obj in searcher.Get())
                        {
                            
                            command.Text = obj["CommandLine"].ToString();
                            path.Text = obj["ExecutablePath"].ToString();
                            parent.Text = obj["ParentProcessId"].ToString();

                        }

                    }
                    catch { }

                }
            }
        }
        public void ThreadUpdate()
        {
            while (true)
            {

                try
                {
                    
                    double[] processorTime = new double[process.GetThreads().Count];
                    double[] now = new double[process.GetThreads().Count];
                    ProcessThreadCollection threads = process.GetThreads();
                    for (int i = 0; i < process.GetThreads().Count; i++)
                    {
                        try
                        {
                            processorTime[i] = threads[i].TotalProcessorTime.Ticks;
                            now[i] = DateTime.Now.Ticks;
                        }
                        catch
                        {
                            processorTime[i] = 0;
                            now[i] = 0;

                        }
                        Thread.Sleep(500);
                        int count = 0;
                        procThreads.SuspendLayout();
                        procThreads.BeginUpdate();
                        procThreads.Items.Clear();
                        foreach (ProcessThread thread in process.GetThreads())
                        {
                            count++;
                            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Thread WHERE Handle = {thread.Id}");
                            string state = "";
                            foreach (ManagementObject obj in searcher.Get())
                            {
                                state = obj["ThreadState"].ToString();
                            }
                            double cpuThread = 0;
                            try
                            {
                                cpuThread = ((thread.TotalProcessorTime.Ticks - processorTime[count]) / (DateTime.Now.Ticks - now[count])) / Environment.ProcessorCount;
                            }
                            catch
                            {

                            }
                            string[] data = { "", "", "", "" };
                            string threadState = "";
                            switch (state)
                            {

                                case "0":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Initialized", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "Initialized";
                                    break;
                                case "1":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Ready", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "Ready";
                                    break;
                                case "2":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Running", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "Running";
                                    break;
                                case "3":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Standby", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "StandBy";
                                    break;
                                case "4":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Terminated", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "Terminated";
                                    break;
                                case "5":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Waiting", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "Waiting";
                                    break;
                                case "6":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Transition", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "Transition";
                                    break;
                                case "7":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Unknown", GetIdealProcessor((uint)thread.Id) };
                                    threadState = "Unknown";
                                    break;
                            }


                            var ListViewItemData = new ListViewItem(data);
                            procThreads.Items.Add(ListViewItemData);
                        }
                        procThreads.EndUpdate();
                        procThreads.Update();
                        procThreads.ResumeLayout();


                    }
                }
                catch
                {
                    continue;
                }
                Thread.Sleep(1000);
                }
                
            
        }

        private void procThreads_MouseClick(object sender, MouseEventArgs e)
        {
            int tid;
            int count = 0;
            for (int i = 0; i < procThreads.Columns.Count; i++)
            {
                if (procThreads.Columns[i].Text == "tid")
                    count = i;
            }
            try
            {
                tid = int.Parse(procThreads.SelectedItems[0].SubItems[count].Text);

                foreach (ProcessThread thread in process.GetThreads())
                {
                    if (tid == thread.Id)
                    {
                        threadID.Text = thread.Id.ToString();
                        Bpriority.Text = thread.BasePriority.ToString();
                        Cpriority.Text = thread.CurrentPriority.ToString();
                        startThread.Text = thread.StartTime.ToString();
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Thread WHERE Handle = {thread.Id}");
                        string state = "";
                        state = procThreads.SelectedItems[0].SubItems["state"].Text;
                        if (state == "Waiting")
                        {
                            threadState.Text = state + ": " + thread.WaitReason;
                        }
                        else
                            threadState.Text = state;

                    }
                }

            }
            catch { }
        }
    }
    
}
