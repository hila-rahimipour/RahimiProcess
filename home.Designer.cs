﻿namespace RahimiProcess
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
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.killProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspendProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAnAlertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.graph = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.suspend = new System.Windows.Forms.Button();
            this.create = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.resume = new System.Windows.Forms.Button();
            this.selectBy = new System.Windows.Forms.ComboBox();
            this.network = new System.Windows.Forms.Button();
            this.alertsBtn = new System.Windows.Forms.Button();
            this.listView1 = new RahimiProcess.DoubleBufferedListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.killProcessToolStripMenuItem,
            this.suspendProcessToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(88, 30);
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
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createAnAlertToolStripMenuItem});
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(62, 29);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // createAnAlertToolStripMenuItem
            // 
            this.createAnAlertToolStripMenuItem.Name = "createAnAlertToolStripMenuItem";
            this.createAnAlertToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.createAnAlertToolStripMenuItem.Text = "Create an Alert";
            this.createAnAlertToolStripMenuItem.Click += new System.EventHandler(this.createAnAlertToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(65, 30);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
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
            this.menu.Size = new System.Drawing.Size(962, 33);
            this.menu.TabIndex = 4;
            this.menu.Text = "menu";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // graph
            // 
            this.graph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.graph.BackColor = System.Drawing.SystemColors.Menu;
            this.graph.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("graph.BackgroundImage")));
            this.graph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.graph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.graph.ForeColor = System.Drawing.SystemColors.Menu;
            this.graph.Location = new System.Drawing.Point(7, 40);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(40, 31);
            this.graph.TabIndex = 5;
            this.graph.UseVisualStyleBackColor = false;
            this.graph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.graph_MouseClick);
            // 
            // info
            // 
            this.info.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.info.BackColor = System.Drawing.SystemColors.Menu;
            this.info.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("info.BackgroundImage")));
            this.info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.info.ForeColor = System.Drawing.SystemColors.Menu;
            this.info.Location = new System.Drawing.Point(64, 40);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(40, 31);
            this.info.TabIndex = 6;
            this.info.UseVisualStyleBackColor = false;
            this.info.MouseClick += new System.Windows.Forms.MouseEventHandler(this.info_MouseClick);
            this.info.MouseHover += new System.EventHandler(this.info_MouseHover);
            // 
            // cancel
            // 
            this.cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancel.BackColor = System.Drawing.SystemColors.Menu;
            this.cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cancel.BackgroundImage")));
            this.cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.ForeColor = System.Drawing.SystemColors.Menu;
            this.cancel.Location = new System.Drawing.Point(121, 40);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(40, 31);
            this.cancel.TabIndex = 7;
            this.cancel.UseVisualStyleBackColor = false;
            this.cancel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cancel_MouseClick);
            // 
            // suspend
            // 
            this.suspend.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.suspend.BackColor = System.Drawing.SystemColors.Menu;
            this.suspend.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("suspend.BackgroundImage")));
            this.suspend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.suspend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.suspend.ForeColor = System.Drawing.SystemColors.Menu;
            this.suspend.Location = new System.Drawing.Point(235, 40);
            this.suspend.Name = "suspend";
            this.suspend.Size = new System.Drawing.Size(40, 31);
            this.suspend.TabIndex = 8;
            this.suspend.UseVisualStyleBackColor = false;
            this.suspend.MouseClick += new System.Windows.Forms.MouseEventHandler(this.suspend_MouseClick);
            this.suspend.MouseHover += new System.EventHandler(this.suspend_MouseHover);
            // 
            // create
            // 
            this.create.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.create.BackColor = System.Drawing.SystemColors.Menu;
            this.create.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("create.BackgroundImage")));
            this.create.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.create.ForeColor = System.Drawing.SystemColors.Menu;
            this.create.Location = new System.Drawing.Point(178, 40);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(40, 31);
            this.create.TabIndex = 9;
            this.create.UseVisualStyleBackColor = false;
            this.create.MouseClick += new System.Windows.Forms.MouseEventHandler(this.create_MouseClick);
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.searchBox.Location = new System.Drawing.Point(831, 40);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(119, 26);
            this.searchBox.TabIndex = 12;
            this.searchBox.Text = "Search";
            this.searchBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.searchBox_MouseClick);
            this.searchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchBox_KeyDown);
            this.searchBox.Leave += new System.EventHandler(this.searchBox_Leave);
            // 
            // resume
            // 
            this.resume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resume.BackColor = System.Drawing.SystemColors.Menu;
            this.resume.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resume.BackgroundImage")));
            this.resume.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.resume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resume.ForeColor = System.Drawing.SystemColors.Menu;
            this.resume.Location = new System.Drawing.Point(292, 40);
            this.resume.Name = "resume";
            this.resume.Size = new System.Drawing.Size(40, 31);
            this.resume.TabIndex = 13;
            this.resume.UseVisualStyleBackColor = false;
            this.resume.MouseClick += new System.Windows.Forms.MouseEventHandler(this.resume_MouseClick);
            this.resume.MouseHover += new System.EventHandler(this.resume_MouseHover);
            // 
            // selectBy
            // 
            this.selectBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectBy.FormattingEnabled = true;
            this.selectBy.Items.AddRange(new object[] {
            "Name",
            "PID"});
            this.selectBy.Location = new System.Drawing.Point(728, 40);
            this.selectBy.Name = "selectBy";
            this.selectBy.Size = new System.Drawing.Size(97, 28);
            this.selectBy.TabIndex = 14;
            // 
            // network
            // 
            this.network.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.network.BackColor = System.Drawing.SystemColors.Menu;
            this.network.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("network.BackgroundImage")));
            this.network.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.network.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.network.ForeColor = System.Drawing.SystemColors.Menu;
            this.network.Location = new System.Drawing.Point(349, 40);
            this.network.Name = "network";
            this.network.Size = new System.Drawing.Size(40, 31);
            this.network.TabIndex = 15;
            this.network.UseVisualStyleBackColor = false;
            this.network.MouseClick += new System.Windows.Forms.MouseEventHandler(this.network_MouseClick);
            this.network.MouseHover += new System.EventHandler(this.network_MouseHover);
            // 
            // alertsBtn
            // 
            this.alertsBtn.BackColor = System.Drawing.Color.LightCoral;
            this.alertsBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.alertsBtn.FlatAppearance.BorderSize = 0;
            this.alertsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.alertsBtn.Location = new System.Drawing.Point(410, 40);
            this.alertsBtn.Name = "alertsBtn";
            this.alertsBtn.Size = new System.Drawing.Size(75, 31);
            this.alertsBtn.TabIndex = 16;
            this.alertsBtn.Text = "Alerts";
            this.alertsBtn.UseVisualStyleBackColor = false;
            this.alertsBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.alertsBtn_MouseClick);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 80);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(962, 474);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "PID";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "CPU";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Working Set";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Read";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Write";
            this.columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "User Name";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Thread Count";
            this.columnHeader8.Width = 80;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Start Time";
            this.columnHeader9.Width = 150;
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(962, 555);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.alertsBtn);
            this.Controls.Add(this.network);
            this.Controls.Add(this.selectBy);
            this.Controls.Add(this.resume);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.create);
            this.Controls.Add(this.suspend);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.info);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RahimiProcess";
            this.Load += new System.EventHandler(this.home_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem killProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspendProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Button graph;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button suspend;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button resume;
        private System.Windows.Forms.ComboBox selectBy;
        private System.Windows.Forms.Button network;
        private System.Windows.Forms.ToolStripMenuItem createAnAlertToolStripMenuItem;
        private System.Windows.Forms.Button alertsBtn;
        private DoubleBufferedListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
    }
}