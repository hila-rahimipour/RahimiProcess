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
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;

namespace RahimiProcess
{

    public partial class NetworkInfo : Form
    {

        public NetworkInfo()
        {

            InitializeComponent();

        }

        private void NetworkInfo_Load(object sender, EventArgs e)
        {
            Thread update = new Thread(() => Update());
            update.Start();
        }
        public void Update()
        {
            while (true)
            {
                try
                {
                    List<string> tcp = Program.tcpSockets;
                    List<string> udp = Program.udpSockets;
                    List<string> pipes = Program.openPipes;

                    tcpInfo.BackColor = Color.White;
                    udpInfo.BackColor = Color.White;
                    pipeInfo.BackColor = Color.White;
                    nicInfo.BackColor = Color.White;

                    // Create a new RichTextBox control in memory
                    RichTextBox tempRichTextBox = new RichTextBox();
                    tempRichTextBox.Font = new Font("Consolas", 10);
                    tempRichTextBox.BackColor = Color.White;
                    tempRichTextBox.BorderStyle = BorderStyle.None;
                    tempRichTextBox.WordWrap = false;

                    // Update the contents of the RichTextBox in memory
                    string tcpText = "";
                    string udpText = "";
                    string pipeText = "";
                    foreach (string tcpinfo in tcp)
                    {
                        tcpText += tcpinfo;
                        tcpText += "\n\n";
                    }
                    foreach (string udpinfo in udp)
                    {
                        udpText += udpinfo;
                        udpText += "\n\n";
                    }
                    foreach (string pipe in pipes)
                    {
                        pipeText += pipe;
                        pipeText += "\n\n";
                    }
                    tempRichTextBox.AppendText(tcpText);
                    tempRichTextBox.AppendText(udpText);
                    tempRichTextBox.AppendText(pipeText);

                    Process process = new Process();
                    process.StartInfo.FileName = "ipconfig";
                    process.StartInfo.Arguments = "/all";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();

                    process.WaitForExit();

                    tempRichTextBox.AppendText(output);

                    // Replace the contents of the visible RichTextBox with the contents of the RichTextBox in memory
                    BeginInvoke(new Action(() =>
                    {
                        tcpInfo.SuspendLayout();
                        udpInfo.SuspendLayout();
                        pipeInfo.SuspendLayout();
                        nicInfo.SuspendLayout();

                        tcpInfo.Text = tcpText;
                        udpInfo.Text = udpText;
                        pipeInfo.Text = pipeText;
                        nicInfo.Text = output;

                        tcpInfo.ResumeLayout();
                        udpInfo.ResumeLayout();
                        pipeInfo.ResumeLayout();
                        nicInfo.ResumeLayout();
                    }));

                    Thread.Sleep(5000);
                }
                catch
                {
                    continue;
                }
                
            }
        }

    }
}
