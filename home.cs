using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
            selectBy.Text = "PID";
            
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
            listView1.Items.Clear();
            listView1.BeginUpdate();
            foreach (ProcessInfo proc in Program.procs)
            {
                string[] data = {proc.GetName(), proc.GetPID().ToString(), proc.GetCPU().ToString(),
                    proc.GetWS().ToString(), proc.GetReads().ToString(), proc.GetWrites().ToString(),
                    proc.GetThreads().Count.ToString(), proc.GetHandleCount().ToString()};
                var ListViewItemData = new ListViewItem(data);
                listView1.Items.Add(ListViewItemData);
            }
            listView1.EndUpdate();
            listView1.Update();
        }


        // open form with proc info!!!!!!!!
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                if (listView1.Columns[i].Text == "PID")
                    count = i;
            }
            int id = int.Parse(listView1.SelectedItems[0].SubItems[count].Text);
            Console.WriteLine(id);
            
        }

        
        private void searchBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (searchBox.Text == "Search")
            {
                searchBox.Text = "";
                searchBox.ForeColor = Color.Black;
            }

        }

        private void searchBox_Leave(object sender, EventArgs e)
        {
            if (searchBox.Text != "Search")
            {
                searchBox.Text = "Search";
                searchBox.ForeColor = Color.FromName("ControlDark");
            }
        }

        // open form with proc info!!!!!!
        private void info_MouseClick(object sender, MouseEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                if (listView1.Columns[i].Text == "PID")
                    count = i;
            }
            try
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[count].Text);
                Console.WriteLine(id);
            }  
            
            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
            
        }


        //suspend process
        private void suspend_MouseClick(object sender, MouseEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                if (listView1.Columns[i].Text == "PID")
                    count = i;
            }
            try
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[count].Text);
                Console.WriteLine(id);
                Suspend.SuspendProcess(id);
            }

            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //resume process
        private void resume_MouseClick(object sender, MouseEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                if (listView1.Columns[i].Text == "PID")
                    count = i;
            }
            try
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[count].Text);
                Console.WriteLine(id);
                Suspend.ResumeProcess(id);
            }

            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        //on hover suspend button
        private void suspend_MouseHover(object sender, EventArgs e)
        {
            ToolTip info = new ToolTip();
            info.Show("Suspend Process", suspend);
        }

        //on hover resume button
        private void resume_MouseHover(object sender, EventArgs e)
        {
            ToolTip info = new ToolTip();
            info.Show("Resume Process", resume);
        }

        //on hover network button
        private void network_MouseHover(object sender, EventArgs e)
        {
            ToolTip info = new ToolTip();
            info.Show("Network Information", network);
        }

        //on hover information button
        private void info_MouseHover(object sender, EventArgs e)
        {
            ToolTip information = new ToolTip();
            information.Show("Process Information", info);
        }

        public void SearchResult1(string pid, int count)
        {
            for (int i=0; i < listView1.Items.Count / 2; i++)
            {
                if (listView1.Items[i].SubItems[count].Text != pid)
                    listView1.Items[i].Remove();
            }
        }
        public void SearchResult2(string pid, int count)
        {
            for (int j = 0; j < listView1.Items.Count; j++)
            {
                if (listView1.Items[j].SubItems[count].Text != pid)
                    listView1.Items[j].Remove();
            }
        }

        //search by name or id
        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                string query = searchBox.Text;
                if (selectBy.Text=="PID" && query!="" && query!="Search")
                {
                    int count = 0;
                    for (int i = 0; i < listView1.Columns.Count; i++)
                    {
                        if (listView1.Columns[i].Text == "PID")
                            count = i;
                    }
                    listView1.BeginUpdate();
                    foreach (ListViewItem item in listView1.Items)
                    {

                        if (!item.SubItems[count].Text.Contains(query))
                            item.Remove();
                    }
                    listView1.EndUpdate();
                    listView1.Update();
                }
                else if(selectBy.Text=="Name" && query != "")
                {
                    int count = 0;
                    for(int i=0; i < listView1.Columns.Count; i++)
                    {
                        if (listView1.Columns[i].Text == "Name")
                            count = i;
                    }
                    
                    listView1.BeginUpdate();
                    foreach (ListViewItem item in listView1.Items)
                    {

                        if (!item.SubItems[count].Text.ToUpper().Contains(query.ToUpper()))
                            item.Remove();
                    }
                    listView1.EndUpdate();
                    listView1.Update();
                }
                else if (query=="" || query=="Search")
                    SetProcs();

            }
            else if (e.KeyCode == Keys.Back)
                if (searchBox.Text.Length==1 || searchBox.Text.Length == 0)
                    SetProcs();
            
        }
    }
}
