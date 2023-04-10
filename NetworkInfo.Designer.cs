
namespace POC_NEW
{
    partial class NetworkInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkInfo));
            this.tcp = new System.Windows.Forms.TabControl();
            this.tcpPage = new System.Windows.Forms.TabPage();
            this.tcpInfo = new System.Windows.Forms.RichTextBox();
            this.udpPage = new System.Windows.Forms.TabPage();
            this.udpInfo = new System.Windows.Forms.RichTextBox();
            this.pipes = new System.Windows.Forms.TabPage();
            this.pipeInfo = new System.Windows.Forms.RichTextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nicInfo = new System.Windows.Forms.RichTextBox();
            this.tcp.SuspendLayout();
            this.tcpPage.SuspendLayout();
            this.udpPage.SuspendLayout();
            this.pipes.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcp
            // 
            this.tcp.Controls.Add(this.tcpPage);
            this.tcp.Controls.Add(this.udpPage);
            this.tcp.Controls.Add(this.pipes);
            this.tcp.Controls.Add(this.tabPage1);
            this.tcp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcp.Location = new System.Drawing.Point(0, 0);
            this.tcp.Name = "tcp";
            this.tcp.SelectedIndex = 0;
            this.tcp.Size = new System.Drawing.Size(542, 519);
            this.tcp.TabIndex = 0;
            this.tcp.Tag = "";
            // 
            // tcpPage
            // 
            this.tcpPage.Controls.Add(this.tcpInfo);
            this.tcpPage.Location = new System.Drawing.Point(4, 29);
            this.tcpPage.Name = "tcpPage";
            this.tcpPage.Padding = new System.Windows.Forms.Padding(3);
            this.tcpPage.Size = new System.Drawing.Size(534, 486);
            this.tcpPage.TabIndex = 0;
            this.tcpPage.Text = "TCP Sockets";
            this.tcpPage.UseVisualStyleBackColor = true;
            // 
            // tcpInfo
            // 
            this.tcpInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tcpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcpInfo.Location = new System.Drawing.Point(3, 3);
            this.tcpInfo.Name = "tcpInfo";
            this.tcpInfo.ReadOnly = true;
            this.tcpInfo.Size = new System.Drawing.Size(528, 480);
            this.tcpInfo.TabIndex = 0;
            this.tcpInfo.Text = "";
            // 
            // udpPage
            // 
            this.udpPage.Controls.Add(this.udpInfo);
            this.udpPage.Location = new System.Drawing.Point(4, 29);
            this.udpPage.Name = "udpPage";
            this.udpPage.Padding = new System.Windows.Forms.Padding(3);
            this.udpPage.Size = new System.Drawing.Size(534, 486);
            this.udpPage.TabIndex = 1;
            this.udpPage.Text = "UDP Sockets";
            this.udpPage.UseVisualStyleBackColor = true;
            // 
            // udpInfo
            // 
            this.udpInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.udpInfo.Location = new System.Drawing.Point(3, 3);
            this.udpInfo.Name = "udpInfo";
            this.udpInfo.ReadOnly = true;
            this.udpInfo.Size = new System.Drawing.Size(528, 480);
            this.udpInfo.TabIndex = 0;
            this.udpInfo.Text = "";
            // 
            // pipes
            // 
            this.pipes.Controls.Add(this.pipeInfo);
            this.pipes.Location = new System.Drawing.Point(4, 29);
            this.pipes.Name = "pipes";
            this.pipes.Padding = new System.Windows.Forms.Padding(3);
            this.pipes.Size = new System.Drawing.Size(534, 486);
            this.pipes.TabIndex = 2;
            this.pipes.Text = "Pipes";
            this.pipes.UseVisualStyleBackColor = true;
            // 
            // pipeInfo
            // 
            this.pipeInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pipeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pipeInfo.Location = new System.Drawing.Point(3, 3);
            this.pipeInfo.Name = "pipeInfo";
            this.pipeInfo.ReadOnly = true;
            this.pipeInfo.Size = new System.Drawing.Size(528, 480);
            this.pipeInfo.TabIndex = 0;
            this.pipeInfo.Text = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nicInfo);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(534, 486);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Network Adapters";
            // 
            // nicInfo
            // 
            this.nicInfo.BackColor = System.Drawing.SystemColors.Control;
            this.nicInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nicInfo.Location = new System.Drawing.Point(1, 3);
            this.nicInfo.Name = "nicInfo";
            this.nicInfo.ReadOnly = true;
            this.nicInfo.Size = new System.Drawing.Size(528, 480);
            this.nicInfo.TabIndex = 0;
            this.nicInfo.Text = "";
            // 
            // NetworkInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 519);
            this.Controls.Add(this.tcp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NetworkInfo";
            this.Text = "NetworkInfo";
            this.Load += new System.EventHandler(this.NetworkInfo_Load);
            this.tcp.ResumeLayout(false);
            this.tcpPage.ResumeLayout(false);
            this.udpPage.ResumeLayout(false);
            this.pipes.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            

        }

        #endregion

        private System.Windows.Forms.TabControl tcp;
        private System.Windows.Forms.TabPage tcpPage;
        private System.Windows.Forms.TabPage udpPage;
        private System.Windows.Forms.TabPage pipes;
        private System.Windows.Forms.RichTextBox tcpInfo;
        private System.Windows.Forms.RichTextBox udpInfo;
        private System.Windows.Forms.RichTextBox pipeInfo;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox nicInfo;
        
    }

}