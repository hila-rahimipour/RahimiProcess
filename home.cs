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
using System.Threading;
using System.Diagnostics;

namespace POC_NEW
{
    public partial class home : Form
    {
        private int sortColumn = -1;
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
            
            
        }

        

        public void SetProcs()
        {
            listView1.Items.Clear();
            listView1.BeginUpdate();
            foreach (ProcessInfo proc in Program.procs)
            {
                if (proc.GetThreads()[0].ThreadState.ToString()== "Suspended") 
                {
                    string[] data = {proc.GetName(), proc.GetPID().ToString(), "Suspended".ToString(),
                    proc.GetWS().ToString(), proc.GetReads().ToString(), proc.GetWrites().ToString(),
                    proc.GetThreads().Count.ToString(), proc.GetHandleCount().ToString()};
                    var ListViewItemData = new ListViewItem(data);
                    listView1.Items.Add(ListViewItemData);
                }
                else
                {
                    string[] data = {proc.GetName(), proc.GetPID().ToString(), proc.GetCPU().ToString(),
                    proc.GetWS().ToString(), proc.GetReads().ToString(), proc.GetWrites().ToString(),
                    proc.GetThreads().Count.ToString(), proc.GetHandleCount().ToString()};
                    var ListViewItemData = new ListViewItem(data);
                    listView1.Items.Add(ListViewItemData);
                }
                
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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            singleProcess singleproc = new singleProcess(id);
            singleproc.Show();
            
            
            //Application.Run(singleproc);
            
            
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
            if (searchBox.Text == "Search" || searchBox.Text=="")
            {
                searchBox.Text = "Search";
                searchBox.ForeColor = Color.FromName("ControlDark");
            }
        }

        
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
                singleProcess singleproc = new singleProcess(id);
                singleproc.Show();
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



        //search by name or id
        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                string query = searchBox.Text;
                if (selectBy.Text == "PID" && query != "" && query != "Search")
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
                else if (selectBy.Text == "Name" && query != "")
                {
                    int count = 0;
                    for (int i = 0; i < listView1.Columns.Count; i++)
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
                else if (query == "" || query == "Search")
                    SetProcs();

            }
            else if (e.KeyCode == Keys.Back)
                if (searchBox.Text.Length == 1 || searchBox.Text.Length == 0)
                    SetProcs();




        }

        //open socket info
        private void network_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine("here");
            NetworkInfo network = new NetworkInfo();
            network.Show();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
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
                singleProcess singleproc = new singleProcess(id);
                singleproc.Show();
            }

            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            helpScreen help = new helpScreen();
            help.Show();    
        }

        private void graph_MouseClick(object sender, MouseEventArgs e)
        {
            graphs graph = new graphs();
            graph.Show();
        }

        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listView1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1.Sorting);
        }

        private void cancel_MouseClick(object sender, MouseEventArgs e)
        {
            int count = 0;
            int id = -1;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                if (listView1.Columns[i].Text == "PID")
                    count = i;
            }
            try
            {
                id = int.Parse(listView1.SelectedItems[0].SubItems[count].Text);
                Console.WriteLine(id);
            }

            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            try
            {
                Process proc = Process.GetProcessById(id);
                proc.Kill();
            }
            catch
            {
                MessageBox.Show("Can't kill process", "process can't be killed or already been killed",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
            }
        }

        private void create_MouseClick(object sender, MouseEventArgs e)
        {
            addProcess add = new addProcess();
            add.Show();
        }
    }
    

}
