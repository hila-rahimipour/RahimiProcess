using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RahimiProcess
{
    public partial class AlertPrompt : Form
    {
        public string info;
        private ListViewColumnSorter lvwColumnSorter;

        public AlertPrompt(string info)
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(260, 160);
            procAlerts.Visible = false;
            procAlerts.BackColor = System.Drawing.Color.White;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.info = info;
            lvwColumnSorter = new ListViewColumnSorter();
            procAlerts.ListViewItemSorter = lvwColumnSorter;
            lvwColumnSorter.Order = SortOrder.Ascending;
            lvwColumnSorter.SortColumn = 0;
            procAlerts.BeginUpdate();
            foreach (string data in info.Split(','))
            {
                ListViewItem item = new ListViewItem(new string[1] { data });
                if (data.Contains("above"))
                {
                    item.Font = new Font("", 8, FontStyle.Bold);
                    procAlerts.Items.Add(item);
                    procAlerts.Items.Add(new ListViewItem(new string[1] { "" }));
                }
                else
                {
                    procAlerts.Items.Add(item);
                }
            }

            procAlerts.EndUpdate();
            procAlerts.Update();
        }
        public AlertPrompt()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(300, 160);
            procAlerts.Visible = false;
            procAlerts.BackColor = System.Drawing.Color.White;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            lvwColumnSorter = new ListViewColumnSorter();
            procAlerts.ListViewItemSorter = lvwColumnSorter;
            lvwColumnSorter.Order = SortOrder.Ascending;
            lvwColumnSorter.SortColumn = 0;
            procAlerts.Columns.Clear();
            procAlerts.Columns.Add("Process", 110, HorizontalAlignment.Left);
            procAlerts.Columns.Add("Reason", 150, HorizontalAlignment.Left);
            System.Threading.Thread update = new System.Threading.Thread(() => SetAlerts());
            update.Start();
        }

        private void AlertPrompt_Load(object sender, EventArgs e)
        {

        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            Program.isChanged = false;
            this.Close();
        }

        bool is_click = false;
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!is_click)
            {
                this.Size = new System.Drawing.Size(300, 250);
                procAlerts.Visible = true;

                is_click = true;
            }
            else
            {
                this.Size = new System.Drawing.Size(300, 160);
                procAlerts.Visible = false;

                is_click = false;
            }
        }
        public void SetAlerts()
        {

            procAlerts.BeginUpdate();
            procAlerts.Items.Clear();
            foreach (string key in Program.existAlert.Keys)
            {
                foreach (string data in Program.existAlert[key])
                {
                    ListViewItem item = new ListViewItem(new string[2] { data, key });
                    procAlerts.Items.Add(item);
                }

            }
            procAlerts.EndUpdate();
            procAlerts.Update();
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
                    procAlerts.Sort();
                    isSorted = true;

                }
                catch { }
            }
        }
        private void procAlerts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            System.Threading.Thread sort = new System.Threading.Thread(() => SortColumn(sender, e));
            sort.Start();
        }
    }
}