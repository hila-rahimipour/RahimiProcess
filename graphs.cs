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
using System.Windows.Forms.DataVisualization.Charting;


namespace RahimiProcess
{
    public partial class graphs : Form
    {
        int numGraphs;
        int[] count = new int[Environment.ProcessorCount];
        public graphs()
        {
            InitializeComponent();
            numGraphs = Environment.ProcessorCount;
            // Calculate the number of rows and columns needed
            int numRows = (int)Math.Ceiling(Math.Sqrt(numGraphs));
            int numCols = (int)Math.Ceiling((double)numGraphs / numRows);

            // Update the TableLayoutPanel layout
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.ColumnCount = numGraphs;
            tableLayoutPanel1.ColumnStyles.Clear();
            for (int i = 0; i < numGraphs; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / numGraphs));
            }

            // Add the new graph controls
            for (int i = 0; i < numGraphs; i++)
            {
                count[i] = 1;
                Chart graph = new Chart();
                Label label = new Label();
                label.Text = "Core " + i.ToString();
                
                tableLayoutPanel1.Controls.Add(graph, i, 0);
                tableLayoutPanel1.Controls.Add(label, i, 1);
                label.Dock = DockStyle.Fill;
                label.Font = new Font("Consolas", 9, FontStyle.Bold);
                label.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.RowStyles[1].Height = 3;

                graph.Width = tableLayoutPanel1.GetColumnWidths()[i];
                graph.Height = tableLayoutPanel1.ClientSize.Height;

                Series series = new Series();
                ChartArea chartArea = new ChartArea();
                graph.Series.Add(series);
                graph.ChartAreas.Add(chartArea);
                graph.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                graph.ChartAreas[0].AxisX.LabelStyle.Enabled= false;
                
                graph.ChartAreas[0].AxisY.LabelStyle.Enabled= false;
                graph.Series[0].ChartType = SeriesChartType.Area;
                graph.Series[0].Color = Color.FromArgb(180, 246, 186);
                graph.Series[0].BorderColor = Color.FromArgb(22, 178, 37);
                graph.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                graph.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                graph.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;



                graph.ChartAreas[0].AxisX.LineWidth = 1;
                graph.ChartAreas[0].AxisY.LineWidth = 1;
                graph.ChartAreas[0].AxisY.MajorGrid.LineWidth= 1;
                graph.ChartAreas[0].AxisX.LineColor = Color.DarkGray;
                graph.ChartAreas[0].AxisY.LineColor = Color.DarkGray;
                graph.ChartAreas[0].AxisY.Maximum = 101;
                graph.ChartAreas[0].AxisY.Minimum = 0;

                graph.ChartAreas[0].AxisX.Maximum = 30;
                graph.ChartAreas[0].AxisX.Minimum = 0;

                graph.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                graph.ChartAreas[0].AxisY2.LabelStyle.Enabled=false;
                graph.ChartAreas[0].AxisY2.MajorGrid.Enabled=false;
                graph.ChartAreas[0].AxisY2.MajorTickMark.Enabled=false;

                graph.Series[0].Points.AddXY(0, 0);
                
            }
        }

        private void graphs_Load(object sender, EventArgs e)
        {

            Thread getInfo = new Thread(() => Update());
            getInfo.Start();
        }

        private void graphs_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < numGraphs; i++)
            {
                Chart graph = (Chart)tableLayoutPanel1.GetControlFromPosition(i, 0);
                
                graph.Width = tableLayoutPanel1.GetColumnWidths()[i];
                graph.Height = tableLayoutPanel1.ClientSize.Height;

                // Refresh the graph control
                graph.Refresh();
            }
        }

        void Update()
        {
            while (true)
            {
                try
                {
                    PerformanceCounter[] cpus = new PerformanceCounter[numGraphs];
                    for (int i = 0; i < numGraphs; i++)
                    {
                        cpus[i] = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
                        cpus[i].NextValue();
                    }
                    Thread.Sleep(1000);
                    for (int i = 0; i < numGraphs; i++)
                    {

                        double value = cpus[i].NextValue();
                        Chart graph = (Chart)tableLayoutPanel1.GetControlFromPosition(i, 0);
                        graph.Series[0].Points.AddXY(count[i], value);
                        graph.Series[0].Points[count[i]].ToolTip = $"CPU usage: {Math.Round(value,2)}";
                        if (count[i] > 25)
                        {
                            count[i] = 25;
                            graph.Series[0].Points.RemoveAt(0);
                            foreach (System.Windows.Forms.DataVisualization.Charting.DataPoint point in graph.Series[0].Points)
                            {
                                point.XValue -= 1;
                                
                            }
                        }
                        count[i]++;



                    }
                }
                catch { }

            }
        }



    }
}
