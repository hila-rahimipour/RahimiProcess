namespace POC_NEW
{
    partial class addProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addProcess));
            this.start = new System.Windows.Forms.Button();
            this.procName = new System.Windows.Forms.TextBox();
            this.search = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.processes = new POC_NEW.DoubleBufferedListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.LightGray;
            this.start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start.Location = new System.Drawing.Point(419, 406);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 32);
            this.start.TabIndex = 1;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = false;
            this.start.MouseClick += new System.Windows.Forms.MouseEventHandler(this.start_MouseClick);
            // 
            // procName
            // 
            this.procName.Location = new System.Drawing.Point(12, 406);
            this.procName.Multiline = true;
            this.procName.Name = "procName";
            this.procName.Size = new System.Drawing.Size(401, 32);
            this.procName.TabIndex = 2;
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.search.Location = new System.Drawing.Point(12, 12);
            this.search.Multiline = true;
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(390, 32);
            this.search.TabIndex = 4;
            this.search.Text = "Search";
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.Color.LightGray;
            this.searchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchButton.Location = new System.Drawing.Point(408, 12);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(86, 32);
            this.searchButton.TabIndex = 5;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = false;
            // 
            // processes
            // 
            this.processes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name});
            this.processes.FullRowSelect = true;
            this.processes.HideSelection = false;
            this.processes.Location = new System.Drawing.Point(12, 50);
            this.processes.MultiSelect = false;
            this.processes.Name = "processes";
            this.processes.Size = new System.Drawing.Size(482, 344);
            this.processes.TabIndex = 3;
            this.processes.UseCompatibleStateImageBehavior = false;
            this.processes.View = System.Windows.Forms.View.Details;
            this.processes.SelectedIndexChanged += new System.EventHandler(this.processes_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "Name";
            this.name.Width = 380;
            // 
            // addProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(506, 450);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.search);
            this.Controls.Add(this.processes);
            this.Controls.Add(this.procName);
            this.Controls.Add(this.start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "addProcess";
            this.Text = "Create New Process";
            this.Load += new System.EventHandler(this.addProcess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.TextBox procName;
        private DoubleBufferedListView processes;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Button searchButton;
    }
}