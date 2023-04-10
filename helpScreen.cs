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

    public partial class helpScreen : Form
    {
        public helpScreen()
        {
            InitializeComponent();
            generalInfo.BackColor = Color.White;
            cpuInfo.BackColor = Color.White;
            memInfo.BackColor = Color.White;
            netInfo.BackColor = Color.White;

            Font font= new Font("Microsoft Sans Serif", 10, FontStyle.Bold); 

            cpuInfo.SelectionStart = cpuInfo.Find("CPU usage");
            cpuInfo.SelectionFont = font;
            cpuInfo.SelectionStart = cpuInfo.Find("CPU usage", 9, RichTextBoxFinds.None);
            cpuInfo.SelectionFont = font;
            cpuInfo.SelectionStart = cpuInfo.Find("CPU usage = (Process's processor time 2 - Process's processor time 1)/(Time 2 - Time 2)/Amount of cores");
            cpuInfo.SelectionFont = font;
            cpuInfo.SelectionStart = cpuInfo.Find("Processor Time ", 100, RichTextBoxFinds.MatchCase);
            cpuInfo.SelectionFont = font;
            cpuInfo.SelectionStart = cpuInfo.Find("High CPU usage", 100, RichTextBoxFinds.None);
            cpuInfo.SelectionFont = font;


            memInfo.SelectionStart = memInfo.Find("Private Bytes");
            memInfo.SelectionFont = font;
            memInfo.SelectionStart = memInfo.Find("Working Set");
            memInfo.SelectionFont = font;
            memInfo.SelectionStart = memInfo.Find("Private Working Set");
            memInfo.SelectionFont = font;
            memInfo.SelectionStart = memInfo.Find("Virtual Memory");
            memInfo.SelectionFont = font;


            netInfo.SelectionStart = netInfo.Find("‘Reads’", 0,RichTextBoxFinds.None);
            netInfo.SelectionFont = font;
            netInfo.SelectionStart = netInfo.Find("‘Writes’");
            netInfo.SelectionFont = font;
            netInfo.SelectionStart = netInfo.Find("Network Adapters");
            netInfo.SelectionFont = font;

        }
    }
}
