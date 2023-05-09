using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POC_NEW
{
    public partial class addAlert : Form
    {
        public addAlert()
        {
            InitializeComponent();
        }

        private void addAlert_Load(object sender, EventArgs e)
        {
            
            string selectQuery = "SELECT * FROM alerts";
            DataTable info = MyAdoHelper.ExecuteDataTable("DB.mdf", selectQuery);
            
            alerts.SuspendLayout();
            for (int i = 0; i < info.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(new string[2] { info.Rows[i]["field"].ToString(),
                    info.Rows[i]["value"].ToString() });
                alerts.Items.Add(item); 
            }
            alerts.ResumeLayout();
        }
        bool isEdit = false;

        private void edit_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string field = alerts.SelectedItems[0].SubItems[0].Text;
                string value = alerts.SelectedItems[0].SubItems[1].Text;
                fieldValue.Text = field;
                valueText.Text = value;
                isEdit = true;

            }
            catch
            {
                MessageBox.Show("Please select an alert from the list", "Select Alert",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void delete_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string field = alerts.SelectedItems[0].SubItems[0].Text;
                string value = alerts.SelectedItems[0].SubItems[1].Text;
                string query = $"DELETE FROM alerts WHERE field ='{field}' AND value='{value}'";
                MyAdoHelper.DoQuery("DB.mdf", query);
                valueText.Text = "deleted";

            }
            catch
            {
                MessageBox.Show("Please select an alert from the list", "Select Alert",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void addButton_MouseClick(object sender, MouseEventArgs e)
        {
            float f;
            if (isEdit)
            {
                string field = alerts.SelectedItems[0].SubItems[0].Text;
                string value = alerts.SelectedItems[0].SubItems[1].Text;

                if (float.TryParse(valueText.Text, out f))
                {
                    string query = $"UPDATE alerts SET field = '{fieldValue.Text}', value ='{valueText.Text}' " +
                    $"WHERE field='{field}' AND value ='{value}'";
                    MyAdoHelper.DoQuery("DB.mdf", query);
                }
                else
                    MessageBox.Show("Value needs to be a number", "Type Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                isEdit= false;

            }
            else
            {
                if (float.TryParse(valueText.Text, out f))
                {
                    string query = $"INSERT INTO alerts (field, value) VALUES ('{fieldValue.Text}','{valueText.Text}')" +
                    $"WHERE field='{field}' AND value ='{value}'";
                    MyAdoHelper.DoQuery("DB.mdf", query);
                }
                else
                    MessageBox.Show("Value needs to be a number", "Type Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
