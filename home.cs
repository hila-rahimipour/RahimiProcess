using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POC_NEW
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Rectangle screen = Screen.FromPoint(Cursor.Position).WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            Location = new Point(screen.Left + (screen.Width - w) / 2, screen.Top + (screen.Height - h) / 2);
            Size = new Size(w, h);
        }

        private void home_Load(object sender, EventArgs e)
        {
            Load_ListView();
        }
        private void Load_ListView()
        {
            foreach(ProcessInfo proc in Program.procs)
            {
                string[] data = {proc.GetName(), proc.GetPID().ToString(), proc.GetCPU().ToString(),
                    proc.GetWS().ToString(), proc.GetReads().ToString(), proc.GetWrites().ToString(),
                    proc.GetThreads().Count.ToString(), proc.GetHandleCount().ToString()};
                var ListViewItemData = new ListViewItem(data);
                listView1.Items.Add(ListViewItemData);
            }
        }

        public void SetProcs()
        {
            listView1.Clear();
            foreach (ProcessInfo proc in Program.procs)
            {
                string[] data = {proc.GetName(), proc.GetPID().ToString(), proc.GetCPU().ToString(),
                    proc.GetWS().ToString(), proc.GetReads().ToString(), proc.GetWrites().ToString(),
                    proc.GetThreads().Count.ToString(), proc.GetHandleCount().ToString()};
                var ListViewItemData = new ListViewItem(data);
                listView1.Items.Add(ListViewItemData);
            }
            listView1.Update();
        }
    }
}
