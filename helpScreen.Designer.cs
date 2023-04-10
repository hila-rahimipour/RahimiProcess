namespace POC_NEW
{
    partial class helpScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(helpScreen));
            this.details = new System.Windows.Forms.TabControl();
            this.General = new System.Windows.Forms.TabPage();
            this.generalInfo = new System.Windows.Forms.RichTextBox();
            this.CPU = new System.Windows.Forms.TabPage();
            this.cpuInfo = new System.Windows.Forms.RichTextBox();
            this.Memory = new System.Windows.Forms.TabPage();
            this.memInfo = new System.Windows.Forms.RichTextBox();
            this.Network = new System.Windows.Forms.TabPage();
            this.netInfo = new System.Windows.Forms.RichTextBox();
            this.details.SuspendLayout();
            this.General.SuspendLayout();
            this.CPU.SuspendLayout();
            this.Memory.SuspendLayout();
            this.Network.SuspendLayout();
            this.SuspendLayout();
            // 
            // details
            // 
            this.details.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.details.Controls.Add(this.General);
            this.details.Controls.Add(this.CPU);
            this.details.Controls.Add(this.Memory);
            this.details.Controls.Add(this.Network);
            this.details.Location = new System.Drawing.Point(1, 0);
            this.details.Name = "details";
            this.details.SelectedIndex = 0;
            this.details.Size = new System.Drawing.Size(606, 590);
            this.details.TabIndex = 0;
            // 
            // General
            // 
            this.General.Controls.Add(this.generalInfo);
            this.General.Location = new System.Drawing.Point(4, 29);
            this.General.Name = "General";
            this.General.Size = new System.Drawing.Size(598, 557);
            this.General.TabIndex = 0;
            this.General.Text = "General";
            this.General.UseVisualStyleBackColor = true;
            // 
            // generalInfo
            // 
            this.generalInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.generalInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.generalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generalInfo.EnableAutoDragDrop = true;
            this.generalInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generalInfo.Location = new System.Drawing.Point(0, 0);
            this.generalInfo.Name = "generalInfo";
            this.generalInfo.ReadOnly = true;
            this.generalInfo.Size = new System.Drawing.Size(598, 557);
            this.generalInfo.TabIndex = 0;
            this.generalInfo.Text = resources.GetString("generalInfo.Text");
            // 
            // CPU
            // 
            this.CPU.Controls.Add(this.cpuInfo);
            this.CPU.Location = new System.Drawing.Point(4, 29);
            this.CPU.Name = "CPU";
            this.CPU.Size = new System.Drawing.Size(598, 557);
            this.CPU.TabIndex = 0;
            this.CPU.Text = "CPU";
            this.CPU.UseVisualStyleBackColor = true;
            // 
            // cpuInfo
            // 
            this.cpuInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cpuInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpuInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuInfo.Location = new System.Drawing.Point(0, 0);
            this.cpuInfo.Name = "cpuInfo";
            this.cpuInfo.ReadOnly = true;
            this.cpuInfo.Size = new System.Drawing.Size(598, 557);
            this.cpuInfo.TabIndex = 0;
            this.cpuInfo.Text = resources.GetString("cpuInfo.Text");
            // 
            // Memory
            // 
            this.Memory.Controls.Add(this.memInfo);
            this.Memory.Location = new System.Drawing.Point(4, 29);
            this.Memory.Name = "Memory";
            this.Memory.Size = new System.Drawing.Size(598, 557);
            this.Memory.TabIndex = 1;
            this.Memory.Text = "Memory";
            this.Memory.UseVisualStyleBackColor = true;
            // 
            // memInfo
            // 
            this.memInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.memInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memInfo.Location = new System.Drawing.Point(0, 0);
            this.memInfo.Name = "memInfo";
            this.memInfo.ReadOnly = true;
            this.memInfo.Size = new System.Drawing.Size(598, 557);
            this.memInfo.TabIndex = 1;
            this.memInfo.Text = resources.GetString("memInfo.Text");
            // 
            // Network
            // 
            this.Network.Controls.Add(this.netInfo);
            this.Network.Location = new System.Drawing.Point(4, 29);
            this.Network.Name = "Network";
            this.Network.Size = new System.Drawing.Size(598, 557);
            this.Network.TabIndex = 2;
            this.Network.Text = "Network";
            this.Network.UseVisualStyleBackColor = true;
            // 
            // netInfo
            // 
            this.netInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.netInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.netInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.netInfo.Location = new System.Drawing.Point(0, 0);
            this.netInfo.Name = "netInfo";
            this.netInfo.ReadOnly = true;
            this.netInfo.Size = new System.Drawing.Size(598, 557);
            this.netInfo.TabIndex = 1;
            this.netInfo.Text = resources.GetString("netInfo.Text");
            // 
            // helpScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 592);
            this.Controls.Add(this.details);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "helpScreen";
            this.Text = "helpScreen";
            this.details.ResumeLayout(false);
            this.General.ResumeLayout(false);
            this.CPU.ResumeLayout(false);
            this.Memory.ResumeLayout(false);
            this.Network.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl details;
        private System.Windows.Forms.TabPage General;
        private System.Windows.Forms.RichTextBox generalInfo;
        private System.Windows.Forms.TabPage CPU;
        private System.Windows.Forms.TabPage Memory;
        private System.Windows.Forms.TabPage Network;
        private System.Windows.Forms.RichTextBox cpuInfo;
        private System.Windows.Forms.RichTextBox memInfo;
        private System.Windows.Forms.RichTextBox netInfo;
    }   
}