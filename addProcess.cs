using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POC_NEW
{
    public partial class addProcess : Form
    {
        public addProcess()
        {
            InitializeComponent();
        }

        private void addProcess_Load(object sender, EventArgs e)
        {
            Thread start = new Thread(() => Update());
            start.Start();
        }

        private string[] knownProcs = new string[15] {"mspaint.exe", "wmplayer.exe", "snippingtool.exe", "notepad.exe",
        "outlook.exe", "powerpnt.exe", "winword.exe", "excel.exe", "lync.exe", "chrome.exe", "acrobat.exe","msedge.exe",
        "control.exe", "powershell.exe", "spotify.exe"};
        public void Update()
        {
            

            while (!Program.isDone)
            {
            }
            processes.BeginUpdate();

            processes.Items.Clear();
            int count = 0;
            foreach (string name in Program.runProcs)
            {
                processes.Items.Add(name);
                if (knownProcs.Contains(processes.Items[count].SubItems[0].Text))
                    processes.Items[count].BackColor = Color.LightGreen;
                count++;
            }

            processes.EndUpdate();
        }




            private void start_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Process.Start(procName.Text);
            }
            catch { }
            this.Close();
        }

        private void processes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string name = processes.SelectedItems[0].SubItems[0].Text;
                
                procName.Text = name;
            }
            catch { }
        }
    }
}


   


