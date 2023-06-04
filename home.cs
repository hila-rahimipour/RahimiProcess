using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ToolTip = System.Windows.Forms.ToolTip;

namespace RahimiProcess
{
    struct IO_COUNTERS
    {
        public ulong ReadOperationCount;
        public ulong WriteOperationCount;
        public ulong OtherOperationCount;
        public ulong ReadTransferCount;
        public ulong WriteTransferCount;
        public ulong OtherTransferCount;
    }
    
    public partial class home : Form
    {
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool GetProcessIoCounters(IntPtr hProcess, out IO_COUNTERS counters);
        private int sortColumn = -1;
        private ListViewColumnSorter lvwColumnSorter;
        
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
            lvwColumnSorter = new ListViewColumnSorter();
            listView1.ListViewItemSorter = lvwColumnSorter;
            lvwColumnSorter.Order = SortOrder.Ascending;
            lvwColumnSorter.SortColumn = 0;
            try
            {
                listView1.SelectedItems[0].Selected = false;
            }
            catch { }
            



        }

        private void home_Load(object sender, EventArgs e)
        {

            Thread btnStat = new Thread(() => CheckButtonColor());
            btnStat.Start();
        }

        public void SetProcs()
        {
            listView1.Items.Clear();
            listView1.BeginUpdate();
            try
            {
                foreach (Process proc in Program.procs)
                {
                    double reads = 0; ;
                    double writes = 0;
                    try
                    {
                        if (GetProcessIoCounters(proc.Handle, out IO_COUNTERS counters))
                        {
                            reads = counters.ReadOperationCount;
                            writes = counters.WriteOperationCount;
                        }
                    }
                    catch
                    {

                    }
                    if (proc.Threads[0].WaitReason == ThreadWaitReason.Suspended)
                    {
                        try
                        {
                            string[] data = {proc.ProcessName, proc.Id.ToString(), "Suspended".ToString(),
                    proc.WorkingSet64.ToString(), reads.ToString(), writes.ToString(),
                    proc.StartTime.ToString()};
                            var ListViewItemData = new ListViewItem(data);
                            listView1.Items.Add(ListViewItemData);
                        }
                        catch
                        {
                            string[] data = {proc.ProcessName, proc.Id.ToString(), "Suspended".ToString(),
                    proc.WorkingSet64.ToString(), reads.ToString(), writes.ToString(),
                    ""};
                            var ListViewItemData = new ListViewItem(data);
                            listView1.Items.Add(ListViewItemData);
                        }
                    }
                    else
                    {
                        try
                        {
                            string[] data = {proc.ProcessName, proc.Id.ToString(), Math.Round(Program.cpus[proc.Id][2],2).ToString(),
                    proc.WorkingSet64.ToString(), reads.ToString(), writes.ToString(),
                    proc.StartTime.ToString()};
                            var ListViewItemData = new ListViewItem(data);
                            listView1.Items.Add(ListViewItemData);
                        }
                        catch
                        {
                            string[] data = {proc.ProcessName, proc.Id.ToString(), Math.Round(Program.cpus[proc.Id][2],2).ToString(),
                    proc.WorkingSet64.ToString(), reads.ToString(), writes.ToString(),
                    ""};
                            var ListViewItemData = new ListViewItem(data);
                            listView1.Items.Add(ListViewItemData);
                        }
                    }

                }
            }
            catch { }
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
            try
            {
                int id = int.Parse(listView1.Items[selectedIndex].SubItems[count].Text);
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                
                singleProcess singleproc = new singleProcess(id);
                
                Thread run = new Thread(()=>singleproc.ShowDialog());
                run.Start();
                
            }
            catch { }
            
            
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
                int id = int.Parse(listView1.Items[selectedIndex].SubItems[count].Text);
                Console.WriteLine(id);
                singleProcess singleproc = new singleProcess(id);
                singleproc.ShowDialog();
            }  
            
            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
            
        }

        public static int selectedIndex = 0;
        //suspend process
        private void suspend_MouseClick(object sender, MouseEventArgs e)
        {
            int count = 0;
            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                if (listView1.Columns[i].Text == "PID")
                    count = i;
            }
            int id = -1;
            try
            {
                
                    id = int.Parse(listView1.Items[selectedIndex].SubItems[count].Text);
                
                  
            }

            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            try
            {

                Thread suspend = new Thread(() => Suspend.SuspendProcess(id));
                suspend.Start();
                Console.WriteLine("SUSPENDED");
                
            }
            catch { }
            try
            {
                listView1.Items[selectedIndex].Focused = true;
                listView1.Items[selectedIndex].Checked = true;
                listView1.Focus();
                
            }
            catch { }
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
            int id = -1;
            
            try
            {
                    id = int.Parse(listView1.Items[selectedIndex].SubItems[count].Text);

            }

            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            try
            {
                Console.WriteLine(id);
                Thread resume = new Thread(() => Suspend.ResumeProcess(id));
                resume.Start();
            }
            catch { }
            try
            {
                listView1.Items[selectedIndex].Focused = true;
                listView1.Items[selectedIndex].Checked = true;
                listView1.Focus();
            }
            catch { }
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
                if (searchBox.Text.Length == 1)
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
                int id = int.Parse(listView1.Items[selectedIndex].SubItems[count].Text);
                Console.WriteLine(id);
                singleProcess singleproc = new singleProcess(id);
                singleproc.ShowDialog();
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
            Thread sort = new Thread(() => SortColumn(sender, e));
            sort.Start();

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
                id = int.Parse(listView1.Items[selectedIndex].SubItems[count].Text);
                Console.WriteLine(id);
            }

            catch
            {
                MessageBox.Show("Please select process from the list", "Select Process",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            if (id != -1)
            {
                try
                {
                    Process.GetProcessById(id).Kill();

                }
                catch
                {
                    MessageBox.Show("Can't kill process", "process can't be killed or already been killed",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                }
            }
            try
            {
                listView1.Items[selectedIndex].Focused = true;
                listView1.Items[selectedIndex].Checked = true;
                listView1.Focus();

            }
            catch { }
        }

        private void create_MouseClick(object sender, MouseEventArgs e)
        {
            addProcess add = new addProcess();
            add.Show();
        }

        private void createAnAlertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addAlert alert = new addAlert();
            alert.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("INDEX " + listView1.SelectedItems[0].Index);
            }
            catch { }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                selectedIndex=listView1.SelectedItems[0].Index;
                
            }
            catch { }
        }
        
        private void SortColumn(object sender, ColumnClickEventArgs e)
        {
            bool isSorted = false;
            while (!isSorted)
            {
                try
                {
                    // Determine if clicked column is already the column that is being sorted.
                    if (e.Column == lvwColumnSorter.SortColumn)
                    {
                        // Reverse the current sort direction for this column.
                        if (lvwColumnSorter.Order == SortOrder.Ascending)
                        {
                            lvwColumnSorter.Order = SortOrder.Descending;
                        }
                        else
                        {
                            lvwColumnSorter.Order = SortOrder.Ascending;
                        }
                    }
                    else
                    {
                        // Set the column number that is to be sorted; default to ascending.
                        lvwColumnSorter.SortColumn = e.Column;
                        lvwColumnSorter.Order = SortOrder.Ascending;
                    }

                    // Perform the sort with these new sort options.
                    listView1.Sort();
                    isSorted = true;

                }
                catch { }
            }
        }
        public void CheckButtonColor()
        {
            while (true)
            {
                if (Program.isChanged)
                    alertsBtn.BackColor = Color.LightCoral;
                else if (!Program.isChanged)
                    alertsBtn.BackColor = Color.PaleGreen;
                else if (Program.existAlert.Count == 0)
                    alertsBtn.BackColor = Color.LightGray;
                
                Thread.Sleep(2000);
            }
        }

        private void alertsBtn_MouseClick(object sender, MouseEventArgs e)
        {
            
            AlertPrompt prompt = new AlertPrompt();
            prompt.ShowDialog();
        }
    }
    

}
