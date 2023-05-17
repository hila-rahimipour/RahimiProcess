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
using System.Xml;

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
        int count = 0;
        string tooltipInfo = "";
        public singleProcess(int pid)
        {
            this.pid = pid;
            this.Text = $"Process {pid}";
            
            InitializeComponent();


            cpuGraphics.ChartAreas[0].AxisY.Maximum=100;
            cpuGraphics.ChartAreas[0].AxisY.Minimum = 0;
            cpuGraphics.ChartAreas[0].AxisX.Maximum = 30;
            cpuGraphics.ChartAreas[0].AxisX.Minimum = 0;
            //cpuGraphics.ChartAreas[0].RecalculateAxesScale();

            


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
        bool isFirst=true;
        public void SingleProcess()
        {
            try
            {
                Process proc = Process.GetProcessById(pid);
                double wsValue = 0;
                this.Text = $"{proc.ProcessName}: {proc.Id}";
                Thread thread = new Thread(() => GetProcessInstanceName(pid));
                thread.Start();

                //Process[] sameName = Process.GetProcessesByName(proc.ProcessName);
                //int instance = 0;
                //for (int i = 0; i < sameName.Length; i++)
                //{
                //    if (sameName[i].Id == proc.Id)
                //        instance = i;
                //}

                int pipeTop = 0;
                int dllTop = 0;
                if (isFirst)
                {
                    try
                    {
                        pipesList.BeginUpdate();
                        dlls.BeginUpdate();

                        try { pipeTop = pipesList.TopItem.Index; }
                        catch { }
                        try { dllTop = dlls.TopItem.Index; }
                        catch { }

                        pipesList.Items.Clear();
                        dlls.Items.Clear();
                        foreach (ProcessModule module in proc.Modules)
                        {
                            //Check if the module represents a pipe
                            if (module.ModuleName.Contains("pipe"))
                            {
                                ListViewItem item = new ListViewItem(new string[1] { module.ModuleName });
                                pipesList.Items.Add(item);
                            }
                            ListViewItem dllItem = new ListViewItem(new string[1] { module.ModuleName });
                            dlls.Items.Add(dllItem);
                        }
                        pipesList.EndUpdate();
                        dlls.EndUpdate();

                        pipesList.Update();

                        dlls.Update();

                    }
                    catch
                    {
                        pipesList.BeginUpdate();
                        pipesList.Items.Clear();
                        dlls.BeginUpdate();
                        dlls.Items.Clear();
                        pipesList.Items.Add(new ListViewItem(new string[1] { "Can't Access" }));
                        dlls.Items.Add(new ListViewItem(new string[1] { "Can't Access" }));
                        pipesList.EndUpdate();
                        pipesList.Update();
                        dlls.EndUpdate();
                        dlls.Update();
                    }
                    isFirst = false;
                }

                string affinity;
                try
                {
                    affinity = Convert.ToString((int)proc.ProcessorAffinity, 2);
                }
                catch
                {
                    affinity = "Can't Access";
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
                procName.Text = proc.ProcessName;
                processID.Text = pid.ToString();
                procPriority.Text = proc.BasePriority.ToString();
                procAffinity.Text = affinity;
                procHandle.Text = proc.HandleCount.ToString();
                procPrivate.Text = proc.PrivateMemorySize64.ToString();
                procWS.Text = proc.WorkingSet64.ToString();
                procReads.Text = reads.ToString();
                procWrites.Text = writes.ToString();
                try
                {
                    start.Text = proc.StartTime.ToString();
                }
                catch { }


                ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Process WHERE ProcessId = {proc.Id}");
                long virtualMemorySize = Convert.ToInt64(-1);
                foreach (ManagementObject obj in searcher.Get())
                {
                    procVirtual.Text = (Convert.ToInt64(obj["VirtualSize"]).ToString());
                    command.Text = obj["CommandLine"].ToString();
                    path.Text = obj["ExecutablePath"].ToString();
                    parent.Text = obj["ParentProcessId"].ToString();

                }



                double[] cpu = new double[2] { 0, 0 };
                PerformanceCounter workingSet;
                PerformanceCounter readBytes;
                PerformanceCounter writeBytes;
                double read = 0;
                double write = 0;
                while (instanceName == "") { }
                workingSet = new PerformanceCounter("Process", "Working Set - Private",instanceName);
                readBytes = new PerformanceCounter("Process", "IO Read Bytes/sec", instanceName);
                writeBytes = new PerformanceCounter("Process", "IO Write Bytes/sec", instanceName);
                Console.WriteLine("in cpu");
                
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
                read = readBytes.NextValue();
                write = writeBytes.NextValue();
                double cpuVal = 0;
                try
                {
                    cpuVal = (100*(proc.TotalProcessorTime.Ticks - cpu[0])/(DateTime.Now.Ticks - cpu[1])) / Environment.ProcessorCount;
                    if ((int)cpuVal > 10)
                        if (!tooltipInfo.Contains("High CPU usage!"))
                            tooltipInfo += "High CPU usage!\n";
                    Console.WriteLine("CPU VALUE!!!!!"+cpuVal);
                }
                catch
                {
                    cpuVal = 0;
                }
                if (proc.Threads.Count > 10)
                    if (!tooltipInfo.Contains("Pay attention to the number"))
                        tooltipInfo += "Pay attention to the number of threads\n";
                wsValue = workingSet.NextValue();

                if (proc.WorkingSet64 - wsValue > wsValue)
                    if (!tooltipInfo.Contains("The process uses DLL"))
                        tooltipInfo += "The process uses DLL's a lot\n";

               
                procPrivateWS.Text = wsValue.ToString();
                procSharedWS.Text = (proc.WorkingSet64 - wsValue).ToString();
                procPeakWS.Text = proc.PeakWorkingSet64.ToString();
               
                


                Console.WriteLine("done cpu single");
                try
                {
                    if (cpuVal<1)
                        cpuGraphics.ChartAreas[0].AxisY.Maximum = 1;
                    else if (cpuVal < 5)
                        cpuGraphics.ChartAreas[0].AxisY.Maximum = 5;
                    else if (cpuVal < 10)
                        cpuGraphics.ChartAreas[0].AxisY.Maximum = 10;
                    else if (cpuVal < 20)
                        cpuGraphics.ChartAreas[0].AxisY.Maximum = 20;



                }
                catch { }

                if (read > ioGraph.ChartAreas[0].AxisY.Maximum)
                {
                    ioGraph.ChartAreas[0].AxisY.Maximum = read+50;
                    ioGraph.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                }
                
                Console.WriteLine($"Bounds {cpuGraphics.ChartAreas[0].AxisY.Maximum} {cpuGraphics.ChartAreas[0].AxisY.Minimum}");
                try
                {
                    Console.WriteLine("CPU VALUE:"+cpuVal);
                    cpuGraphics.Series[0].Points.AddXY(count, Math.Round(cpuVal));
                    
                    
                    ioGraph.Series[0].Points.AddXY(count, read);
                    ioGraph.Series[1].Points.AddXY(count, write);

                    if (cpuGraphics.Series[0].Points.Count > 30)
                    {
                        count = 30;
                        cpuGraphics.Series[0].Points.RemoveAt(0);

                        foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in cpuGraphics.Series[0].Points)
                        {
                            point.XValue -= 1;
                            point.ToolTip = "cpu value:" + point.YValues[0];
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
                            point.ToolTip = "read bytes/sec:" + point.YValues[0];
                        }
                        foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in ioGraph.Series[1].Points)
                        {
                            point.XValue -= 1;
                            point.ToolTip = "write bytes/sec:" + point.YValues[0];
                        }
                    }
                    count++;
                }
                catch { }

                try
                {
                    memChart.Series[0].Points.Clear();
                    memChart.Series[1].Points.Clear();

                    //memChart.Series[0].Points.AddXY("private Mem", proc.PrivateMemorySize64);
                    //memChart.Series[0].Points.AddXY("WS", proc.WorkingSet64 - wsValue);

                    //memChart.Series[1].Points.AddXY("private Mem", proc.PrivateMemorySize64-wsValue);

                    //memChart.Series[1].Points.AddXY("WS", wsValue);

                    memChart.Series[0].Points.AddXY("private Mem", proc.PrivateMemorySize64);
                    memChart.Series[0].Points.AddXY("WS", proc.WorkingSet64);

                    memChart.Series[1].Points.AddXY("private Mem", wsValue);

                    memChart.Series[1].Points.AddXY("WS", wsValue);

                    
                    memChart.Series[0].Points[0].ToolTip = $"Private Size:\n{memChart.Series[0].Points[0].YValues[0]-wsValue}";
                    memChart.Series[0].Points[1].ToolTip = $"Shared Working Set:\n{memChart.Series[0].Points[1].YValues[0]-wsValue}";
                    memChart.Series[1].Points[0].ToolTip = $"Private Working Set:\n{memChart.Series[1].Points[0].YValues[0]}";
                    memChart.Series[1].Points[1].ToolTip = $"Private Working Set:\n{memChart.Series[1].Points[1].YValues[0]}";
                }
                catch { }
                
            }
            catch
            {
                terminate.Text = "Process Terminated, Closing window";
                this.Close();
            }
        }
        public static string instanceName = "";
        private void singleProcess_Load(object sender, EventArgs e)
        {
            Thread update = new Thread(() => Update());
            Thread updateThread = new Thread(() => ThreadUpdate());
            updateThread.Start();
            update.Start();

        }

        public void Update()
        {
            
            while (true)
            {
                SingleProcess();

            }
        }
        private int selectedThreadIndex = -1;
        private int topIndex=0;
        public void ThreadUpdate()
        {
            while (true)
            {
                
                try
                {
                    
                    var threadItems = new List<ListViewItem>();
                    ProcessThreadCollection threads = Process.GetProcessById(pid).Threads;
                    for (int i = 0; i < threads.Count; i++)
                    {
                        try
                        {
                            ProcessThread thread = threads[i];
                            double processorTime = thread.TotalProcessorTime.Ticks;
                            double now = DateTime.Now.Ticks;

                            ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Thread WHERE Handle = {thread.Id}");
                            string state = "";
                            foreach (ManagementObject obj in searcher.Get())
                            {
                                state = obj["ThreadState"].ToString();
                            }

                            double cpuThread = ((thread.TotalProcessorTime.Ticks - processorTime) / (DateTime.Now.Ticks - now)) / Environment.ProcessorCount;

                            string[] data = { "", "", "", "" };
                            
                            switch (state)
                            {
                                case "0":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Initialized", GetIdealProcessor((uint)thread.Id) };
                                    
                                    break;
                                case "1":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Ready", GetIdealProcessor((uint)thread.Id) };
                                   
                                    break;
                                case "2":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Running", GetIdealProcessor((uint)thread.Id) };
                                    break;
                                case "3":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Standby", GetIdealProcessor((uint)thread.Id) };
                                    break;
                                case "4":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Terminated", GetIdealProcessor((uint)thread.Id) };
                                    break;
                                case "5":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Waiting", GetIdealProcessor((uint)thread.Id) };
                                    break;
                                case "6":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Transition", GetIdealProcessor((uint)thread.Id) };
                                    break;
                                case "7":
                                    data = new string[] { thread.Id.ToString(), cpuThread.ToString(), "Unknown", GetIdealProcessor((uint)thread.Id) };
                                    break;
                            }

                            var ListViewItemData = new ListViewItem(data);
                            threadItems.Add(ListViewItemData);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    procThreads.BeginInvoke(new Action(() =>
                    {
                        // Save the selected index if there is a selection
                        if (procThreads.SelectedIndices.Count > 0)
                        {
                            selectedThreadIndex = procThreads.SelectedIndices[0];
                        }

                        procThreads.Items.Clear();
                        procThreads.Items.AddRange(threadItems.ToArray());

                        // Restore the selected index if there was a selection
                        if (selectedThreadIndex >= 0 && selectedThreadIndex < procThreads.Items.Count)
                        {
                            procThreads.Items[selectedThreadIndex].Selected = true;
                            procThreads.TopItem = procThreads.Items[topIndex];
                            
                        }
                        else
                        {
                            procThreads.Items[0].Selected = true;
                            procThreads.Items[0].Focused = true;
                            procThreads.TopItem = procThreads.Items[topIndex];

                        }
                    }));



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
            topIndex = procThreads.TopItem.Index;
            for (int i = 0; i < procThreads.Columns.Count; i++)
            {
                if (procThreads.Columns[i].Text == "tid")
                    count = i;
            }
            try
            {
                tid = int.Parse(procThreads.SelectedItems[0].SubItems[count].Text);

                foreach (ProcessThread thread in Process.GetProcessById(pid).Threads)
                {
                    if (tid == thread.Id)
                    {
                        threadID.Text = thread.Id.ToString();
                        Bpriority.Text = thread.BasePriority.ToString();
                        Cpriority.Text = thread.CurrentPriority.ToString();
                        startThread.Text = thread.StartTime.ToString();
                        
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_Thread WHERE Handle = {thread.Id}");
                        string state = "";
                        state = procThreads.SelectedItems[0].SubItems[2].Text;

                        if (state == "Waiting")
                        {
                            threadStateText.Text = state + ": " + thread.WaitReason;
                           
                        }
                        else if (state == "")
                        {
                            state = thread.ThreadState.ToString();
                            if (state == "Waiting")
                                threadStateText.Text = state + ": " + thread.WaitReason;
                            else
                                threadStateText.Text = state;
                           
                        }
                        else
                        {
                            threadStateText.Text = state;
                           
                        }



                    }
                }

            }
            catch { }
        }

        private void threadID_Click(object sender, EventArgs e)
        {

        }


        private void infoHover_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip info = new System.Windows.Forms.ToolTip();
            info.Show(tooltipInfo, infoHover,10000);
            

        }
        public static void GetProcessInstanceName(int pid)
        {
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

            string[] instances = cat.GetInstanceNames();
            foreach (string instance in instances)
            {
                try
                {

                    using (PerformanceCounter cnt = new PerformanceCounter("Process",
                         "ID Process", instance, true))
                    {
                        int val = (int)cnt.RawValue;
                        if (val == pid)
                        {
                            instanceName = instance;
                        }
                    }
                }
                catch { }
            }
            if (instanceName == "")
                instanceName = Process.GetProcessById(pid).ProcessName;
        }
    }

}
