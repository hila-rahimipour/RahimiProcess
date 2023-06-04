
namespace POC_NEW
{
    partial class singleProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(singleProcess));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.general = new System.Windows.Forms.TabPage();
            this.infoHover = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.command = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.Label();
            this.parent = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.priority = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.procName = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.terminate = new System.Windows.Forms.Label();
            this.processID = new System.Windows.Forms.Label();
            this.procPriority = new System.Windows.Forms.Label();
            this.procHandle = new System.Windows.Forms.Label();
            this.handleCount = new System.Windows.Forms.Label();
            this.procAffinity = new System.Windows.Forms.Label();
            this.affinity = new System.Windows.Forms.Label();
            this.memNet = new System.Windows.Forms.TabPage();
            this.memChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.procPeakWS = new System.Windows.Forms.Label();
            this.privateMemory = new System.Windows.Forms.Label();
            this.peakWS = new System.Windows.Forms.Label();
            this.procPrivateWS = new System.Windows.Forms.Label();
            this.procSharedWS = new System.Windows.Forms.Label();
            this.privateWS = new System.Windows.Forms.Label();
            this.SharedWS = new System.Windows.Forms.Label();
            this.procWS = new System.Windows.Forms.Label();
            this.procWrites = new System.Windows.Forms.Label();
            this.workingset = new System.Windows.Forms.Label();
            this.writes = new System.Windows.Forms.Label();
            this.procVirtual = new System.Windows.Forms.Label();
            this.virtualMemory = new System.Windows.Forms.Label();
            this.procReads = new System.Windows.Forms.Label();
            this.reads = new System.Windows.Forms.Label();
            this.procPrivate = new System.Windows.Forms.Label();
            this.threads = new System.Windows.Forms.TabPage();
            this.threadsProc = new POC_NEW.DoubleBufferedListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label14 = new System.Windows.Forms.Label();
            this.threadID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.threadStateText = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.startThread = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Cpriority = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Bpriority = new System.Windows.Forms.Label();
            this.modules = new System.Windows.Forms.TabPage();
            this.dlls = new POC_NEW.DoubleBufferedListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.graphs = new System.Windows.Forms.TabPage();
            this.writeLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.readLabel = new System.Windows.Forms.Label();
            this.cpuLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ioGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cpuGraphics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pipes = new System.Windows.Forms.TabPage();
            this.pipesList = new POC_NEW.DoubleBufferedListView();
            this.Protocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.general.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoHover)).BeginInit();
            this.memNet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memChart)).BeginInit();
            this.threads.SuspendLayout();
            this.modules.SuspendLayout();
            this.graphs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ioGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuGraphics)).BeginInit();
            this.pipes.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.general);
            this.tabControl1.Controls.Add(this.memNet);
            this.tabControl1.Controls.Add(this.threads);
            this.tabControl1.Controls.Add(this.modules);
            this.tabControl1.Controls.Add(this.graphs);
            this.tabControl1.Controls.Add(this.pipes);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 539);
            this.tabControl1.TabIndex = 0;
            // 
            // general
            // 
            this.general.AutoScroll = true;
            this.general.BackColor = System.Drawing.Color.White;
            this.general.Controls.Add(this.infoHover);
            this.general.Controls.Add(this.label1);
            this.general.Controls.Add(this.label2);
            this.general.Controls.Add(this.command);
            this.general.Controls.Add(this.label5);
            this.general.Controls.Add(this.path);
            this.general.Controls.Add(this.parent);
            this.general.Controls.Add(this.start);
            this.general.Controls.Add(this.label11);
            this.general.Controls.Add(this.priority);
            this.general.Controls.Add(this.name);
            this.general.Controls.Add(this.procName);
            this.general.Controls.Add(this.id);
            this.general.Controls.Add(this.terminate);
            this.general.Controls.Add(this.processID);
            this.general.Controls.Add(this.procPriority);
            this.general.Controls.Add(this.procHandle);
            this.general.Controls.Add(this.handleCount);
            this.general.Controls.Add(this.procAffinity);
            this.general.Controls.Add(this.affinity);
            this.general.Location = new System.Drawing.Point(4, 29);
            this.general.Name = "general";
            this.general.Padding = new System.Windows.Forms.Padding(3);
            this.general.Size = new System.Drawing.Size(585, 506);
            this.general.TabIndex = 0;
            this.general.Text = "General";
            // 
            // infoHover
            // 
            this.infoHover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.infoHover.BackgroundImage = global::POC_NEW.Properties.Resources.infoProc;
            this.infoHover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.infoHover.Location = new System.Drawing.Point(8, 356);
            this.infoHover.Name = "infoHover";
            this.infoHover.Size = new System.Drawing.Size(43, 38);
            this.infoHover.TabIndex = 60;
            this.infoHover.TabStop = false;
            this.infoHover.MouseHover += new System.EventHandler(this.infoHover_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 20);
            this.label1.TabIndex = 56;
            this.label1.Text = "Parent Process ID:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 20);
            this.label2.TabIndex = 52;
            this.label2.Text = "Command Line:";
            // 
            // command
            // 
            this.command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.command.AutoSize = true;
            this.command.ForeColor = System.Drawing.Color.Black;
            this.command.Location = new System.Drawing.Point(146, 167);
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(0, 20);
            this.command.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(7, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 54;
            this.label5.Text = "Path:";
            // 
            // path
            // 
            this.path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.path.AutoSize = true;
            this.path.ForeColor = System.Drawing.Color.Black;
            this.path.Location = new System.Drawing.Point(64, 198);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(0, 20);
            this.path.TabIndex = 55;
            // 
            // parent
            // 
            this.parent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.parent.AutoSize = true;
            this.parent.ForeColor = System.Drawing.Color.Black;
            this.parent.Location = new System.Drawing.Point(173, 226);
            this.parent.Name = "parent";
            this.parent.Size = new System.Drawing.Size(0, 20);
            this.parent.TabIndex = 57;
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.start.AutoSize = true;
            this.start.ForeColor = System.Drawing.Color.Black;
            this.start.Location = new System.Drawing.Point(139, 260);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(0, 20);
            this.start.TabIndex = 59;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(7, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 20);
            this.label11.TabIndex = 58;
            this.label11.Text = "Creation Date:";
            // 
            // priority
            // 
            this.priority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.priority.AutoSize = true;
            this.priority.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priority.ForeColor = System.Drawing.Color.Black;
            this.priority.Location = new System.Drawing.Point(7, 75);
            this.priority.Name = "priority";
            this.priority.Size = new System.Drawing.Size(115, 20);
            this.priority.TabIndex = 33;
            this.priority.Text = "Base Priority:";
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.ForeColor = System.Drawing.Color.Black;
            this.name.Location = new System.Drawing.Point(7, 13);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(60, 20);
            this.name.TabIndex = 29;
            this.name.Text = "Name:";
            // 
            // procName
            // 
            this.procName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procName.AutoSize = true;
            this.procName.ForeColor = System.Drawing.Color.Black;
            this.procName.Location = new System.Drawing.Point(73, 13);
            this.procName.Name = "procName";
            this.procName.Size = new System.Drawing.Size(0, 20);
            this.procName.TabIndex = 30;
            // 
            // id
            // 
            this.id.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.id.AutoSize = true;
            this.id.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id.ForeColor = System.Drawing.Color.Black;
            this.id.Location = new System.Drawing.Point(7, 44);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(44, 20);
            this.id.TabIndex = 31;
            this.id.Text = "PID:";
            // 
            // terminate
            // 
            this.terminate.AutoSize = true;
            this.terminate.ForeColor = System.Drawing.Color.Black;
            this.terminate.Location = new System.Drawing.Point(253, 325);
            this.terminate.Name = "terminate";
            this.terminate.Size = new System.Drawing.Size(0, 20);
            this.terminate.TabIndex = 51;
            // 
            // processID
            // 
            this.processID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.processID.AutoSize = true;
            this.processID.ForeColor = System.Drawing.Color.Black;
            this.processID.Location = new System.Drawing.Point(57, 44);
            this.processID.Name = "processID";
            this.processID.Size = new System.Drawing.Size(0, 20);
            this.processID.TabIndex = 32;
            // 
            // procPriority
            // 
            this.procPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procPriority.AutoSize = true;
            this.procPriority.ForeColor = System.Drawing.Color.Black;
            this.procPriority.Location = new System.Drawing.Point(128, 75);
            this.procPriority.Name = "procPriority";
            this.procPriority.Size = new System.Drawing.Size(0, 20);
            this.procPriority.TabIndex = 34;
            // 
            // procHandle
            // 
            this.procHandle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procHandle.AutoSize = true;
            this.procHandle.ForeColor = System.Drawing.Color.Black;
            this.procHandle.Location = new System.Drawing.Point(137, 137);
            this.procHandle.Name = "procHandle";
            this.procHandle.Size = new System.Drawing.Size(0, 20);
            this.procHandle.TabIndex = 46;
            // 
            // handleCount
            // 
            this.handleCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.handleCount.AutoSize = true;
            this.handleCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.handleCount.ForeColor = System.Drawing.Color.Black;
            this.handleCount.Location = new System.Drawing.Point(7, 137);
            this.handleCount.Name = "handleCount";
            this.handleCount.Size = new System.Drawing.Size(124, 20);
            this.handleCount.TabIndex = 45;
            this.handleCount.Text = "Handle Count:";
            // 
            // procAffinity
            // 
            this.procAffinity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procAffinity.AutoSize = true;
            this.procAffinity.ForeColor = System.Drawing.Color.Black;
            this.procAffinity.Location = new System.Drawing.Point(152, 106);
            this.procAffinity.Name = "procAffinity";
            this.procAffinity.Size = new System.Drawing.Size(0, 20);
            this.procAffinity.TabIndex = 44;
            // 
            // affinity
            // 
            this.affinity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.affinity.AutoSize = true;
            this.affinity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.affinity.ForeColor = System.Drawing.Color.Black;
            this.affinity.Location = new System.Drawing.Point(7, 106);
            this.affinity.Name = "affinity";
            this.affinity.Size = new System.Drawing.Size(139, 20);
            this.affinity.TabIndex = 43;
            this.affinity.Text = "Process Affinity:";
            // 
            // memNet
            // 
            this.memNet.BackColor = System.Drawing.Color.White;
            this.memNet.Controls.Add(this.memChart);
            this.memNet.Controls.Add(this.procPeakWS);
            this.memNet.Controls.Add(this.privateMemory);
            this.memNet.Controls.Add(this.peakWS);
            this.memNet.Controls.Add(this.procPrivateWS);
            this.memNet.Controls.Add(this.procSharedWS);
            this.memNet.Controls.Add(this.privateWS);
            this.memNet.Controls.Add(this.SharedWS);
            this.memNet.Controls.Add(this.procWS);
            this.memNet.Controls.Add(this.procWrites);
            this.memNet.Controls.Add(this.workingset);
            this.memNet.Controls.Add(this.writes);
            this.memNet.Controls.Add(this.procVirtual);
            this.memNet.Controls.Add(this.virtualMemory);
            this.memNet.Controls.Add(this.procReads);
            this.memNet.Controls.Add(this.reads);
            this.memNet.Controls.Add(this.procPrivate);
            this.memNet.Location = new System.Drawing.Point(4, 29);
            this.memNet.Name = "memNet";
            this.memNet.Padding = new System.Windows.Forms.Padding(3);
            this.memNet.Size = new System.Drawing.Size(585, 506);
            this.memNet.TabIndex = 1;
            this.memNet.Text = "Memory & Network";
            // 
            // memChart
            // 
            this.memChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.IsMarksNextToAxis = false;
            chartArea1.AxisY.LabelStyle.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.memChart.ChartAreas.Add(chartArea1);
            this.memChart.Location = new System.Drawing.Point(190, 200);
            this.memChart.Name = "memChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series2";
            this.memChart.Series.Add(series1);
            this.memChart.Series.Add(series2);
            this.memChart.Size = new System.Drawing.Size(387, 298);
            this.memChart.TabIndex = 72;
            // 
            // procPeakWS
            // 
            this.procPeakWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procPeakWS.AutoSize = true;
            this.procPeakWS.ForeColor = System.Drawing.Color.Black;
            this.procPeakWS.Location = new System.Drawing.Point(171, 170);
            this.procPeakWS.Name = "procPeakWS";
            this.procPeakWS.Size = new System.Drawing.Size(0, 20);
            this.procPeakWS.TabIndex = 71;
            // 
            // privateMemory
            // 
            this.privateMemory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.privateMemory.AutoSize = true;
            this.privateMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.privateMemory.ForeColor = System.Drawing.Color.Black;
            this.privateMemory.Location = new System.Drawing.Point(8, 15);
            this.privateMemory.Name = "privateMemory";
            this.privateMemory.Size = new System.Drawing.Size(136, 20);
            this.privateMemory.TabIndex = 56;
            this.privateMemory.Text = "Private Memory:";
            // 
            // peakWS
            // 
            this.peakWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.peakWS.AutoSize = true;
            this.peakWS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peakWS.ForeColor = System.Drawing.Color.Black;
            this.peakWS.Location = new System.Drawing.Point(8, 170);
            this.peakWS.Name = "peakWS";
            this.peakWS.Size = new System.Drawing.Size(157, 20);
            this.peakWS.TabIndex = 70;
            this.peakWS.Text = "Peak Working Set:";
            // 
            // procPrivateWS
            // 
            this.procPrivateWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procPrivateWS.AutoSize = true;
            this.procPrivateWS.ForeColor = System.Drawing.Color.Black;
            this.procPrivateWS.Location = new System.Drawing.Point(186, 108);
            this.procPrivateWS.Name = "procPrivateWS";
            this.procPrivateWS.Size = new System.Drawing.Size(0, 20);
            this.procPrivateWS.TabIndex = 63;
            this.procPrivateWS.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // procSharedWS
            // 
            this.procSharedWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procSharedWS.AutoSize = true;
            this.procSharedWS.ForeColor = System.Drawing.Color.Black;
            this.procSharedWS.Location = new System.Drawing.Point(189, 139);
            this.procSharedWS.Name = "procSharedWS";
            this.procSharedWS.Size = new System.Drawing.Size(0, 20);
            this.procSharedWS.TabIndex = 69;
            // 
            // privateWS
            // 
            this.privateWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.privateWS.AutoSize = true;
            this.privateWS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.privateWS.ForeColor = System.Drawing.Color.Black;
            this.privateWS.Location = new System.Drawing.Point(8, 108);
            this.privateWS.Name = "privateWS";
            this.privateWS.Size = new System.Drawing.Size(172, 20);
            this.privateWS.TabIndex = 62;
            this.privateWS.Text = "Private Working Set:";
            // 
            // SharedWS
            // 
            this.SharedWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SharedWS.AutoSize = true;
            this.SharedWS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SharedWS.ForeColor = System.Drawing.Color.Black;
            this.SharedWS.Location = new System.Drawing.Point(8, 139);
            this.SharedWS.Name = "SharedWS";
            this.SharedWS.Size = new System.Drawing.Size(175, 20);
            this.SharedWS.TabIndex = 68;
            this.SharedWS.Text = "Shared Working Set:";
            // 
            // procWS
            // 
            this.procWS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procWS.AutoSize = true;
            this.procWS.ForeColor = System.Drawing.Color.Black;
            this.procWS.Location = new System.Drawing.Point(126, 77);
            this.procWS.Name = "procWS";
            this.procWS.Size = new System.Drawing.Size(0, 20);
            this.procWS.TabIndex = 61;
            // 
            // procWrites
            // 
            this.procWrites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procWrites.AutoSize = true;
            this.procWrites.ForeColor = System.Drawing.Color.Black;
            this.procWrites.Location = new System.Drawing.Point(79, 257);
            this.procWrites.Name = "procWrites";
            this.procWrites.Size = new System.Drawing.Size(0, 20);
            this.procWrites.TabIndex = 67;
            // 
            // workingset
            // 
            this.workingset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.workingset.AutoSize = true;
            this.workingset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.workingset.ForeColor = System.Drawing.Color.Black;
            this.workingset.Location = new System.Drawing.Point(8, 77);
            this.workingset.Name = "workingset";
            this.workingset.Size = new System.Drawing.Size(112, 20);
            this.workingset.TabIndex = 60;
            this.workingset.Text = "Working Set:";
            // 
            // writes
            // 
            this.writes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.writes.AutoSize = true;
            this.writes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writes.ForeColor = System.Drawing.Color.Black;
            this.writes.Location = new System.Drawing.Point(8, 257);
            this.writes.Name = "writes";
            this.writes.Size = new System.Drawing.Size(65, 20);
            this.writes.TabIndex = 66;
            this.writes.Text = "Writes:";
            // 
            // procVirtual
            // 
            this.procVirtual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procVirtual.AutoSize = true;
            this.procVirtual.ForeColor = System.Drawing.Color.Black;
            this.procVirtual.Location = new System.Drawing.Point(147, 46);
            this.procVirtual.Name = "procVirtual";
            this.procVirtual.Size = new System.Drawing.Size(0, 20);
            this.procVirtual.TabIndex = 59;
            // 
            // virtualMemory
            // 
            this.virtualMemory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.virtualMemory.AutoSize = true;
            this.virtualMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.virtualMemory.ForeColor = System.Drawing.Color.Black;
            this.virtualMemory.Location = new System.Drawing.Point(8, 46);
            this.virtualMemory.Name = "virtualMemory";
            this.virtualMemory.Size = new System.Drawing.Size(133, 20);
            this.virtualMemory.TabIndex = 58;
            this.virtualMemory.Text = "Virtual Memory:";
            // 
            // procReads
            // 
            this.procReads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procReads.AutoSize = true;
            this.procReads.ForeColor = System.Drawing.Color.Black;
            this.procReads.Location = new System.Drawing.Point(80, 226);
            this.procReads.Name = "procReads";
            this.procReads.Size = new System.Drawing.Size(0, 20);
            this.procReads.TabIndex = 65;
            // 
            // reads
            // 
            this.reads.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.reads.AutoSize = true;
            this.reads.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reads.ForeColor = System.Drawing.Color.Black;
            this.reads.Location = new System.Drawing.Point(8, 226);
            this.reads.Name = "reads";
            this.reads.Size = new System.Drawing.Size(66, 20);
            this.reads.TabIndex = 64;
            this.reads.Text = "Reads:";
            // 
            // procPrivate
            // 
            this.procPrivate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.procPrivate.AutoSize = true;
            this.procPrivate.ForeColor = System.Drawing.Color.Black;
            this.procPrivate.Location = new System.Drawing.Point(150, 15);
            this.procPrivate.Name = "procPrivate";
            this.procPrivate.Size = new System.Drawing.Size(0, 20);
            this.procPrivate.TabIndex = 57;
            // 
            // threads
            // 
            this.threads.BackColor = System.Drawing.Color.White;
            this.threads.Controls.Add(this.threadsProc);
            this.threads.Controls.Add(this.label14);
            this.threads.Controls.Add(this.threadID);
            this.threads.Controls.Add(this.label12);
            this.threads.Controls.Add(this.threadStateText);
            this.threads.Controls.Add(this.label8);
            this.threads.Controls.Add(this.startThread);
            this.threads.Controls.Add(this.label6);
            this.threads.Controls.Add(this.Cpriority);
            this.threads.Controls.Add(this.label3);
            this.threads.Controls.Add(this.Bpriority);
            this.threads.Location = new System.Drawing.Point(4, 29);
            this.threads.Name = "threads";
            this.threads.Size = new System.Drawing.Size(585, 506);
            this.threads.TabIndex = 2;
            this.threads.Text = "Threads";
            // 
            // threadsProc
            // 
            this.threadsProc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.threadsProc.Dock = System.Windows.Forms.DockStyle.Top;
            this.threadsProc.FullRowSelect = true;
            this.threadsProc.HideSelection = false;
            this.threadsProc.Location = new System.Drawing.Point(0, 0);
            this.threadsProc.MultiSelect = false;
            this.threadsProc.Name = "threadsProc";
            this.threadsProc.Size = new System.Drawing.Size(585, 331);
            this.threadsProc.TabIndex = 68;
            this.threadsProc.UseCompatibleStateImageBehavior = false;
            this.threadsProc.View = System.Windows.Forms.View.Details;
            this.threadsProc.MouseClick += new System.Windows.Forms.MouseEventHandler(this.procThreads_MouseClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ID";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "CPU";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Ideal Processor";
            this.columnHeader6.Width = 100;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(8, 350);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 20);
            this.label14.TabIndex = 66;
            this.label14.Text = "Thread ID:";
            // 
            // threadID
            // 
            this.threadID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.threadID.AutoSize = true;
            this.threadID.ForeColor = System.Drawing.Color.Black;
            this.threadID.Location = new System.Drawing.Point(108, 350);
            this.threadID.Name = "threadID";
            this.threadID.Size = new System.Drawing.Size(0, 20);
            this.threadID.TabIndex = 67;
            this.threadID.Click += new System.EventHandler(this.threadID_Click);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(233, 392);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 20);
            this.label12.TabIndex = 64;
            this.label12.Text = "State:";
            // 
            // threadStateText
            // 
            this.threadStateText.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.threadStateText.AutoSize = true;
            this.threadStateText.ForeColor = System.Drawing.Color.Black;
            this.threadStateText.Location = new System.Drawing.Point(297, 392);
            this.threadStateText.Name = "threadStateText";
            this.threadStateText.Size = new System.Drawing.Size(0, 20);
            this.threadStateText.TabIndex = 65;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(233, 350);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 20);
            this.label8.TabIndex = 62;
            this.label8.Text = "Start Time:";
            // 
            // startThread
            // 
            this.startThread.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.startThread.AutoSize = true;
            this.startThread.ForeColor = System.Drawing.Color.Black;
            this.startThread.Location = new System.Drawing.Point(336, 350);
            this.startThread.Name = "startThread";
            this.startThread.Size = new System.Drawing.Size(0, 20);
            this.startThread.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(8, 434);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 20);
            this.label6.TabIndex = 60;
            this.label6.Text = "Current Priority:";
            // 
            // Cpriority
            // 
            this.Cpriority.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Cpriority.AutoSize = true;
            this.Cpriority.ForeColor = System.Drawing.Color.Black;
            this.Cpriority.Location = new System.Drawing.Point(148, 434);
            this.Cpriority.Name = "Cpriority";
            this.Cpriority.Size = new System.Drawing.Size(0, 20);
            this.Cpriority.TabIndex = 61;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(8, 392);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 58;
            this.label3.Text = "Base Priority:";
            // 
            // Bpriority
            // 
            this.Bpriority.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Bpriority.AutoSize = true;
            this.Bpriority.ForeColor = System.Drawing.Color.Black;
            this.Bpriority.Location = new System.Drawing.Point(129, 392);
            this.Bpriority.Name = "Bpriority";
            this.Bpriority.Size = new System.Drawing.Size(0, 20);
            this.Bpriority.TabIndex = 59;
            // 
            // modules
            // 
            this.modules.BackColor = System.Drawing.Color.White;
            this.modules.Controls.Add(this.dlls);
            this.modules.Location = new System.Drawing.Point(4, 29);
            this.modules.Name = "modules";
            this.modules.Size = new System.Drawing.Size(585, 506);
            this.modules.TabIndex = 3;
            this.modules.Text = "Modules";
            // 
            // dlls
            // 
            this.dlls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dlls.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.dlls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dlls.HideSelection = false;
            this.dlls.Location = new System.Drawing.Point(0, 0);
            this.dlls.Name = "dlls";
            this.dlls.Size = new System.Drawing.Size(585, 506);
            this.dlls.TabIndex = 0;
            this.dlls.UseCompatibleStateImageBehavior = false;
            this.dlls.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 700;
            // 
            // graphs
            // 
            this.graphs.BackColor = System.Drawing.Color.White;
            this.graphs.Controls.Add(this.writeLabel);
            this.graphs.Controls.Add(this.label10);
            this.graphs.Controls.Add(this.readLabel);
            this.graphs.Controls.Add(this.cpuLabel);
            this.graphs.Controls.Add(this.label9);
            this.graphs.Controls.Add(this.label4);
            this.graphs.Controls.Add(this.ioGraph);
            this.graphs.Controls.Add(this.cpuGraphics);
            this.graphs.Location = new System.Drawing.Point(4, 29);
            this.graphs.Name = "graphs";
            this.graphs.Size = new System.Drawing.Size(585, 506);
            this.graphs.TabIndex = 4;
            this.graphs.Text = "Graphs";
            // 
            // writeLabel
            // 
            this.writeLabel.AutoSize = true;
            this.writeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writeLabel.Location = new System.Drawing.Point(315, 248);
            this.writeLabel.Name = "writeLabel";
            this.writeLabel.Size = new System.Drawing.Size(0, 17);
            this.writeLabel.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(245, 248);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 17);
            this.label10.TabIndex = 8;
            this.label10.Text = "Writes:";
            // 
            // readLabel
            // 
            this.readLabel.AutoSize = true;
            this.readLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.readLabel.Location = new System.Drawing.Point(148, 248);
            this.readLabel.Name = "readLabel";
            this.readLabel.Size = new System.Drawing.Size(0, 17);
            this.readLabel.TabIndex = 7;
            // 
            // cpuLabel
            // 
            this.cpuLabel.AutoSize = true;
            this.cpuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuLabel.Location = new System.Drawing.Point(201, 6);
            this.cpuLabel.Name = "cpuLabel";
            this.cpuLabel.Size = new System.Drawing.Size(0, 17);
            this.cpuLabel.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(76, 248);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Reads:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(76, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Current Value:";
            // 
            // ioGraph
            // 
            this.ioGraph.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX.MinorGrid.Interval = double.NaN;
            chartArea2.AxisX.MinorGrid.IntervalOffset = double.NaN;
            chartArea2.AxisX.MinorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.NotSet;
            chartArea2.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.NotSet;
            chartArea2.AxisX.MinorGrid.LineWidth = 0;
            chartArea2.AxisY.IsLabelAutoFit = false;
            chartArea2.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea2.Name = "ChartArea1";
            this.ioGraph.ChartAreas.Add(chartArea2);
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.ioGraph.Legends.Add(legend1);
            this.ioGraph.Location = new System.Drawing.Point(17, 262);
            this.ioGraph.Name = "ioGraph";
            this.ioGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            series3.Legend = "Legend1";
            series3.LegendText = "Reads";
            series3.Name = "Series1";
            series4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            series4.Legend = "Legend1";
            series4.LegendText = "Writes";
            series4.Name = "Series2";
            this.ioGraph.Series.Add(series3);
            this.ioGraph.Series.Add(series4);
            this.ioGraph.Size = new System.Drawing.Size(550, 241);
            this.ioGraph.TabIndex = 3;
            this.ioGraph.Text = "ioGraph";
            // 
            // cpuGraphics
            // 
            this.cpuGraphics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea3.AxisX.LabelStyle.Enabled = false;
            chartArea3.AxisX.LineColor = System.Drawing.Color.DarkGray;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisX.MajorTickMark.Enabled = false;
            chartArea3.AxisX.MinorGrid.Interval = double.NaN;
            chartArea3.AxisX.MinorGrid.IntervalOffset = double.NaN;
            chartArea3.AxisX.MinorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.NotSet;
            chartArea3.AxisX.MinorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.NotSet;
            chartArea3.AxisX.MinorGrid.LineWidth = 0;
            chartArea3.AxisY.IsLabelAutoFit = false;
            chartArea3.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY.LineColor = System.Drawing.Color.DarkGray;
            chartArea3.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea3.Name = "ChartArea1";
            this.cpuGraphics.ChartAreas.Add(chartArea3);
            legend2.Alignment = System.Drawing.StringAlignment.Center;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            this.cpuGraphics.Legends.Add(legend2);
            this.cpuGraphics.Location = new System.Drawing.Point(17, 14);
            this.cpuGraphics.Name = "cpuGraphics";
            this.cpuGraphics.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series5.BorderColor = System.Drawing.Color.Goldenrod;
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series5.Color = System.Drawing.Color.LightYellow;
            series5.Legend = "Legend1";
            series5.LegendText = "CPU";
            series5.Name = "Series1";
            this.cpuGraphics.Series.Add(series5);
            this.cpuGraphics.Size = new System.Drawing.Size(550, 241);
            this.cpuGraphics.TabIndex = 3;
            this.cpuGraphics.Text = "cpuGraphics";
            // 
            // pipes
            // 
            this.pipes.Controls.Add(this.pipesList);
            this.pipes.Location = new System.Drawing.Point(4, 29);
            this.pipes.Name = "pipes";
            this.pipes.Padding = new System.Windows.Forms.Padding(3);
            this.pipes.Size = new System.Drawing.Size(585, 506);
            this.pipes.TabIndex = 5;
            this.pipes.Text = "Connections";
            this.pipes.UseVisualStyleBackColor = true;
            // 
            // pipesList
            // 
            this.pipesList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pipesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Protocol,
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader8});
            this.pipesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pipesList.HideSelection = false;
            this.pipesList.Location = new System.Drawing.Point(3, 3);
            this.pipesList.Name = "pipesList";
            this.pipesList.Size = new System.Drawing.Size(579, 500);
            this.pipesList.TabIndex = 0;
            this.pipesList.UseCompatibleStateImageBehavior = false;
            this.pipesList.View = System.Windows.Forms.View.Details;
            // 
            // Protocol
            // 
            this.Protocol.Text = "Protocol";
            this.Protocol.Width = 50;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Local Address";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Forgin Adress";
            this.columnHeader7.Width = 110;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "State";
            this.columnHeader8.Width = 90;
            // 
            // singleProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 539);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "singleProcess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "singleProcess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.singleProcess_FormClosing);
            this.Load += new System.EventHandler(this.singleProcess_Load);
            this.tabControl1.ResumeLayout(false);
            this.general.ResumeLayout(false);
            this.general.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoHover)).EndInit();
            this.memNet.ResumeLayout(false);
            this.memNet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memChart)).EndInit();
            this.threads.ResumeLayout(false);
            this.threads.PerformLayout();
            this.modules.ResumeLayout(false);
            this.graphs.ResumeLayout(false);
            this.graphs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ioGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpuGraphics)).EndInit();
            this.pipes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage general;
        private System.Windows.Forms.Label priority;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label procName;
        private System.Windows.Forms.Label id;
        private System.Windows.Forms.Label terminate;
        private System.Windows.Forms.Label processID;
        private System.Windows.Forms.Label procPriority;
        private System.Windows.Forms.Label procHandle;
        private System.Windows.Forms.Label handleCount;
        private System.Windows.Forms.Label procAffinity;
        private System.Windows.Forms.Label affinity;
        private System.Windows.Forms.TabPage memNet;
        private System.Windows.Forms.Label procPeakWS;
        private System.Windows.Forms.Label privateMemory;
        private System.Windows.Forms.Label peakWS;
        private System.Windows.Forms.Label procPrivateWS;
        private System.Windows.Forms.Label procSharedWS;
        private System.Windows.Forms.Label privateWS;
        private System.Windows.Forms.Label SharedWS;
        private System.Windows.Forms.Label procWS;
        private System.Windows.Forms.Label procWrites;
        private System.Windows.Forms.Label workingset;
        private System.Windows.Forms.Label writes;
        private System.Windows.Forms.Label procVirtual;
        private System.Windows.Forms.Label virtualMemory;
        private System.Windows.Forms.Label procReads;
        private System.Windows.Forms.Label reads;
        private System.Windows.Forms.Label procPrivate;
        private System.Windows.Forms.TabPage threads;
        private System.Windows.Forms.TabPage modules;
        private System.Windows.Forms.TabPage graphs;
        private System.Windows.Forms.TabPage pipes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label command;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label path;
        private System.Windows.Forms.Label parent;
        private System.Windows.Forms.Label start;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label threadID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label threadStateText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label startThread;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Cpriority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Bpriority;
        private System.Windows.Forms.DataVisualization.Charting.Chart ioGraph;
        private System.Windows.Forms.DataVisualization.Charting.Chart memChart;
        private DoubleBufferedListView dlls;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private DoubleBufferedListView pipesList;
        private System.Windows.Forms.ColumnHeader Protocol;
        private System.Windows.Forms.PictureBox infoHover;
        private DoubleBufferedListView threadsProc;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.DataVisualization.Charting.Chart cpuGraphics;
        private System.Windows.Forms.Label writeLabel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label readLabel;
        private System.Windows.Forms.Label cpuLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
    }
}