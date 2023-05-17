namespace POC_NEW
{
    partial class addAlert
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addAlert));
            this.alerts = new System.Windows.Forms.ListView();
            this.field = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.delete = new System.Windows.Forms.Button();
            this.edit = new System.Windows.Forms.Button();
            this.fieldValue = new System.Windows.Forms.ComboBox();
            this.fieldLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.valueText = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // alerts
            // 
            this.alerts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.alerts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.field,
            this.value});
            this.alerts.FullRowSelect = true;
            this.alerts.HideSelection = false;
            this.alerts.Location = new System.Drawing.Point(12, 12);
            this.alerts.MultiSelect = false;
            this.alerts.Name = "alerts";
            this.alerts.Size = new System.Drawing.Size(212, 254);
            this.alerts.TabIndex = 0;
            this.alerts.UseCompatibleStateImageBehavior = false;
            this.alerts.View = System.Windows.Forms.View.Details;
            // 
            // field
            // 
            this.field.Text = "Field";
            this.field.Width = 70;
            // 
            // value
            // 
            this.value.Text = "Value";
            this.value.Width = 70;
            // 
            // delete
            // 
            this.delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.delete.Location = new System.Drawing.Point(119, 275);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(83, 37);
            this.delete.TabIndex = 1;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.MouseClick += new System.Windows.Forms.MouseEventHandler(this.delete_MouseClick);
            // 
            // edit
            // 
            this.edit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.edit.Location = new System.Drawing.Point(34, 275);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(79, 37);
            this.edit.TabIndex = 2;
            this.edit.Text = "Edit";
            this.edit.UseVisualStyleBackColor = true;
            this.edit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.edit_MouseClick);
            // 
            // fieldValue
            // 
            this.fieldValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fieldValue.BackColor = System.Drawing.Color.White;
            this.fieldValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldValue.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fieldValue.FormattingEnabled = true;
            this.fieldValue.Items.AddRange(new object[] {
            "CPU",
            "Working Set",
            "Private Bytes",
            "Virtual Memory",
            "Reads",
            "Writes",
            "Handle count",
            "Thread Count"});
            this.fieldValue.Location = new System.Drawing.Point(245, 127);
            this.fieldValue.Name = "fieldValue";
            this.fieldValue.Size = new System.Drawing.Size(154, 28);
            this.fieldValue.TabIndex = 3;
            // 
            // fieldLabel
            // 
            this.fieldLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fieldLabel.AutoSize = true;
            this.fieldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLabel.Location = new System.Drawing.Point(241, 91);
            this.fieldLabel.Name = "fieldLabel";
            this.fieldLabel.Size = new System.Drawing.Size(53, 20);
            this.fieldLabel.TabIndex = 4;
            this.fieldLabel.Text = "Field:";
            // 
            // valueLabel
            // 
            this.valueLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.valueLabel.AutoSize = true;
            this.valueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLabel.Location = new System.Drawing.Point(401, 91);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(60, 20);
            this.valueLabel.TabIndex = 5;
            this.valueLabel.Text = "Value:";
            // 
            // valueText
            // 
            this.valueText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.valueText.Location = new System.Drawing.Point(405, 127);
            this.valueText.Multiline = true;
            this.valueText.Name = "valueText";
            this.valueText.Size = new System.Drawing.Size(121, 28);
            this.valueText.TabIndex = 6;
            // 
            // addButton
            // 
            this.addButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.addButton.Location = new System.Drawing.Point(317, 174);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(133, 37);
            this.addButton.TabIndex = 7;
            this.addButton.Text = "Add/Update";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.addButton_MouseClick);
            // 
            // addAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(538, 450);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.valueText);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.fieldLabel);
            this.Controls.Add(this.fieldValue);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.alerts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "addAlert";
            this.Text = "Edit Alerts";
            this.Load += new System.EventHandler(this.addAlert_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView alerts;
        private System.Windows.Forms.Button delete;
        private System.Windows.Forms.Button edit;
        private System.Windows.Forms.ComboBox fieldValue;
        private System.Windows.Forms.ColumnHeader field;
        private System.Windows.Forms.ColumnHeader value;
        private System.Windows.Forms.Label fieldLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.TextBox valueText;
        private System.Windows.Forms.Button addButton;
    }
}