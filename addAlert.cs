using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web.UI.WebControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SQLite;

namespace RahimiProcess
{
    public partial class addAlert : Form
    {
        string path = "alerts.db";
        string cs = @"URI=file:" + Application.StartupPath + "\\alerts.db";
        SQLiteConnection con;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        public addAlert()
        {
            InitializeComponent();
            
        }

        private void addAlert_Load(object sender, EventArgs e)
        {
            create_db();
            LoadList();
        }
        bool isEdit = false;

        private void LoadList()
        {
            var con = new SQLiteConnection(cs);
            con.Open();
            string stm = "SELECT * FROM info";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();
            alerts.BeginUpdate();
            alerts.Items.Clear();
            while(dr.Read())
            {
                ListViewItem item = new ListViewItem(new string[2] { dr.GetString(0), dr.GetString(1) });
                alerts.Items.Add(item);

            }
            alerts.EndUpdate();
            alerts.Update();

        }
        private void create_db()
        {
            if (!System.IO.File.Exists(path))
            {

                SQLiteConnection.CreateFile(path);
                using(var sqlite = new SQLiteConnection(@"Data Source="+path))
                {
                    sqlite.Open();
                    string sql = "create table info(field varchar(20), value varchar(20), date varchar(20))";
                    SQLiteCommand command= new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("DATA BASE CANNOT CREATE");
                return;

            }
        }
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
            string field = "";
            string value = "";
            try
            {
                field = alerts.SelectedItems[0].SubItems[0].Text;
                value = alerts.SelectedItems[0].SubItems[1].Text;
                Console.WriteLine($"FIELD: {field}, VALUE: {value}");
            }
            catch
            {
                MessageBox.Show("Please select an alert from the list", "Select Alert",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
           
            if (value != "") 
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);

                cmd.CommandText="DELETE FROM info WHERE field=@field AND value=@value";

                cmd.Prepare();
                cmd.Parameters.AddWithValue("@field", alerts.SelectedItems[0].SubItems[0].Text);
                cmd.Parameters.AddWithValue("@value", alerts.SelectedItems[0].SubItems[1].Text);

                cmd.ExecuteNonQuery();

                
                alerts.Items.Clear();
                LoadList();

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
                    var con = new SQLiteConnection(cs);
                    con.Open();
                    var cmd = new SQLiteCommand(con);
                    var command = new SQLiteCommand("SELECT * FROM info WHERE field=@field AND value=@value", con);


                    cmd.CommandText = $"UPDATE info SET field=@field, value=@value, date=@date WHERE field=@fieldold AND value=@valueold";
                    string FIELD = fieldValue.Text;
                    string VALUE = valueText.Text;

                    command.Parameters.AddWithValue("@field", FIELD);
                    command.Parameters.AddWithValue("value", VALUE);
                    SQLiteDataReader reader = command.ExecuteReader();
                    bool is_exist = false;
                    while (reader.Read())
                    {
                        is_exist = true;
                    }
                    if (!is_exist)
                    {
                        cmd.Parameters.AddWithValue("@field", FIELD);
                        cmd.Parameters.AddWithValue("@value", VALUE);
                        cmd.Parameters.AddWithValue("@fieldold", field);
                        cmd.Parameters.AddWithValue("@valueold", value);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.AddHours(-2).ToString());
                        alerts.SelectedItems[0].SubItems[0].Text = FIELD;
                        alerts.SelectedItems[0].SubItems[1].Text = VALUE;
                        cmd.ExecuteNonQuery();
                        isEdit = false;
                    }
                    else
                    {
                        MessageBox.Show("Alert Already Exists", "Alert Exists", MessageBoxButtons.OK,MessageBoxIcon.Warning);

                    }
                }
                else
                    MessageBox.Show("Value needs to be a number", "Type Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                

            }
            else
            {
                if (float.TryParse(valueText.Text, out f))
                {
                    var con = new SQLiteConnection(cs);
                    con.Open();
                    var cmd = new SQLiteCommand(con);


                    var command = new SQLiteCommand("SELECT * FROM info WHERE field=@field AND value=@value", con);
                    cmd.CommandText = $"INSERT INTO info(field, value, date) VALUES(@field, @value, @date)";
                    string FIELD = fieldValue.Text;
                    string VALUE = valueText.Text;
                    

                    command.Parameters.AddWithValue("@field", FIELD);
                    command.Parameters.AddWithValue("@value", VALUE);
                    SQLiteDataReader reader = command.ExecuteReader();
                    bool is_exist=false;
                    while (reader.Read())
                    {
                        is_exist = true;
                    }

                    if (!is_exist)
                    {
                        cmd.Parameters.AddWithValue("@field", FIELD);
                        cmd.Parameters.AddWithValue("@value", VALUE);
                        cmd.Parameters.AddWithValue("@date", DateTime.Now.AddHours(-2).ToString());
                        alerts.Items.Add(new ListViewItem(new string[2] { FIELD, VALUE }));
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Alert Already Exists", "Alert Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else
                    MessageBox.Show("Value needs to be a number", "Type Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
