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
    public partial class AlertPrompt : Form
    {
        public string info;
        
        public AlertPrompt(string info)
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(260, 160);
            richTextBox1.Visible = false;
            richTextBox1.BackColor= System.Drawing.Color.White;
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.info = info;
        }

        private void AlertPrompt_Load(object sender, EventArgs e)
        {

        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        bool is_click=false;
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!is_click) 
            {
                this.Size = new System.Drawing.Size(260, 250);
                richTextBox1.Visible = true;
                richTextBox1.Text = info;
                is_click = true;
            }
            else
            {
                this.Size = new System.Drawing.Size(260, 160);
                richTextBox1.Visible = false;
                richTextBox1.Text = "";
                is_click = false;
            }
        }
    }
}
