namespace POC_NEW
{
    partial class home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(home));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspendProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findDllOrHandleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.graph = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.suspend = new System.Windows.Forms.Button();
            this.create = new System.Windows.Forms.Button();
            this.network = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ProcName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cpu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ws = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.read = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.write = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.thread = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.handle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(807, 39);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(143, 26);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Search";
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killProcessToolStripMenuItem,
            this.suspendProcessToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(88, 32);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // killProcessToolStripMenuItem
            // 
            this.killProcessToolStripMenuItem.Name = "killProcessToolStripMenuItem";
            this.killProcessToolStripMenuItem.Size = new System.Drawing.Size(248, 34);
            this.killProcessToolStripMenuItem.Text = "Kill Process";
            // 
            // suspendProcessToolStripMenuItem
            // 
            this.suspendProcessToolStripMenuItem.Name = "suspendProcessToolStripMenuItem";
            this.suspendProcessToolStripMenuItem.Size = new System.Drawing.Size(248, 34);
            this.suspendProcessToolStripMenuItem.Text = "Suspend Process";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(248, 34);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterToolStripMenuItem,
            this.findDllOrHandleToolStripMenuItem});
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(62, 32);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // findDllOrHandleToolStripMenuItem
            // 
            this.findDllOrHandleToolStripMenuItem.Name = "findDllOrHandleToolStripMenuItem";
            this.findDllOrHandleToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.findDllOrHandleToolStripMenuItem.Text = "Find dll or Handle";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 32);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processToolStripMenuItem,
            this.findToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(962, 36);
            this.menu.TabIndex = 4;
            this.menu.Text = "menu";
            // 
            // graph
            // 
            this.graph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.graph.BackColor = System.Drawing.SystemColors.Menu;
            this.graph.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("graph.BackgroundImage")));
            this.graph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.graph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.graph.ForeColor = System.Drawing.SystemColors.Menu;
            this.graph.Location = new System.Drawing.Point(5, 39);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(37, 29);
            this.graph.TabIndex = 5;
            this.graph.UseVisualStyleBackColor = false;
            // 
            // info
            // 
            this.info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.info.BackColor = System.Drawing.SystemColors.Menu;
            this.info.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("info.BackgroundImage")));
            this.info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.info.ForeColor = System.Drawing.SystemColors.Menu;
            this.info.Location = new System.Drawing.Point(44, 38);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(37, 29);
            this.info.TabIndex = 6;
            this.info.UseVisualStyleBackColor = false;
            // 
            // cancel
            // 
            this.cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancel.BackColor = System.Drawing.SystemColors.Menu;
            this.cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancel.BackgroundImage")));
            this.cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.ForeColor = System.Drawing.SystemColors.Menu;
            this.cancel.Location = new System.Drawing.Point(86, 38);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(37, 29);
            this.cancel.TabIndex = 7;
            this.cancel.UseVisualStyleBackColor = false;
            // 
            // suspend
            // 
            this.suspend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.suspend.BackColor = System.Drawing.SystemColors.Menu;
            this.suspend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("suspend.BackgroundImage")));
            this.suspend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.suspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.suspend.ForeColor = System.Drawing.SystemColors.Menu;
            this.suspend.Location = new System.Drawing.Point(128, 38);
            this.suspend.Name = "suspend";
            this.suspend.Size = new System.Drawing.Size(37, 29);
            this.suspend.TabIndex = 8;
            this.suspend.UseVisualStyleBackColor = false;
            // 
            // create
            // 
            this.create.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.create.BackColor = System.Drawing.SystemColors.Menu;
            this.create.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("create.BackgroundImage")));
            this.create.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.create.ForeColor = System.Drawing.SystemColors.Menu;
            this.create.Location = new System.Drawing.Point(169, 38);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(37, 32);
            this.create.TabIndex = 9;
            this.create.UseVisualStyleBackColor = false;
            // 
            // network
            // 
            this.network.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.network.BackColor = System.Drawing.SystemColors.Menu;
            this.network.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("network.BackgroundImage")));
            this.network.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.network.Enabled = false;
            this.network.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.network.ForeColor = System.Drawing.SystemColors.Menu;
            this.network.Location = new System.Drawing.Point(211, 38);
            this.network.Name = "network";
            this.network.Size = new System.Drawing.Size(37, 29);
            this.network.TabIndex = 10;
            this.network.UseVisualStyleBackColor = false;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProcName,
            this.pid,
            this.cpu,
            this.ws,
            this.read,
            this.write,
            this.thread,
            this.handle});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 71);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(962, 486);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ProcName
            // 
            this.ProcName.Text = "Name";
            this.ProcName.Width = 120;
            // 
            // pid
            // 
            this.pid.Text = "PID";
            this.pid.Width = 80;
            // 
            // cpu
            // 
            this.cpu.Text = "CPU";
            this.cpu.Width = 80;
            // 
            // ws
            // 
            this.ws.Text = "Working Set";
            this.ws.Width = 120;
            // 
            // read
            // 
            this.read.Text = "Read";
            this.read.Width = 120;
            // 
            // write
            // 
            this.write.Text = "write";
            this.write.Width = 120;
            // 
            // thread
            // 
            this.thread.Text = "Thread Count";
            this.thread.Width = 120;
            // 
            // handle
            // 
            this.handle.Text = "Handle Count";
            this.handle.Width = 120;
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(962, 555);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.network);
            this.Controls.Add(this.create);
            this.Controls.Add(this.suspend);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.info);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "home";
            this.Text = "home";
            this.Load += new System.EventHandler(this.home_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspendProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findDllOrHandleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Button graph;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button suspend;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Button network;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader ProcName;
        private System.Windows.Forms.ColumnHeader pid;
        private System.Windows.Forms.ColumnHeader cpu;
        private System.Windows.Forms.ColumnHeader ws;
        private System.Windows.Forms.ColumnHeader read;
        private System.Windows.Forms.ColumnHeader write;
        private System.Windows.Forms.ColumnHeader thread;
        private System.Windows.Forms.ColumnHeader handle;
        
    }
}