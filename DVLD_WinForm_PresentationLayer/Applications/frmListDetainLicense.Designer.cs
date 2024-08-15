namespace DVLD
{
    partial class frmListDetainLicense
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
            this.components = new System.ComponentModel.Container();
            this.btnDetain = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmsDetainLIcense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnRelease = new System.Windows.Forms.Button();
            this.cbIsReleased = new System.Windows.Forms.ComboBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.showPersonDetalisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.releaseLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cmsDetainLIcense.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDetain
            // 
            this.btnDetain.AutoSize = true;
            this.btnDetain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDetain.Location = new System.Drawing.Point(1412, 179);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(91, 60);
            this.btnDetain.TabIndex = 25;
            this.btnDetain.Text = "Detain";
            this.btnDetain.UseVisualStyleBackColor = true;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 36);
            this.label1.TabIndex = 23;
            this.label1.Text = "Filter By";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(433, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(374, 48);
            this.label2.TabIndex = 21;
            this.label2.Text = "List Detain  License ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.cmsDetainLIcense;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 263);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1522, 300);
            this.dataGridView1.TabIndex = 20;
            // 
            // cmsDetainLIcense
            // 
            this.cmsDetainLIcense.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsDetainLIcense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonDetalisToolStripMenuItem,
            this.showLicenseInfoToolStripMenuItem,
            this.showLicenseHistoryToolStripMenuItem,
            this.releaseLicenseToolStripMenuItem});
            this.cmsDetainLIcense.Name = "cmsDetainLIcense";
            this.cmsDetainLIcense.Size = new System.Drawing.Size(212, 128);
            this.cmsDetainLIcense.Opening += new System.ComponentModel.CancelEventHandler(this.cmsDetainLIcense_Opening);
            // 
            // btnRelease
            // 
            this.btnRelease.AutoSize = true;
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRelease.Location = new System.Drawing.Point(1302, 179);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(91, 60);
            this.btnRelease.TabIndex = 161;
            this.btnRelease.Text = "Release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // cbIsReleased
            // 
            this.cbIsReleased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsReleased.FormattingEnabled = true;
            this.cbIsReleased.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsReleased.Location = new System.Drawing.Point(346, 224);
            this.cbIsReleased.Name = "cbIsReleased";
            this.cbIsReleased.Size = new System.Drawing.Size(121, 24);
            this.cbIsReleased.TabIndex = 164;
            this.cbIsReleased.Visible = false;
            this.cbIsReleased.SelectedIndexChanged += new System.EventHandler(this.cbIsReleased_SelectedIndexChanged);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Detain ID",
            "Is Released",
            "National No.",
            "Full Name",
            "Release Application ID"});
            this.cbFilterBy.Location = new System.Drawing.Point(130, 224);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(210, 24);
            this.cbFilterBy.TabIndex = 163;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilterBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Location = new System.Drawing.Point(347, 224);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(256, 24);
            this.txtFilterValue.TabIndex = 162;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress);
            // 
            // showPersonDetalisToolStripMenuItem
            // 
            this.showPersonDetalisToolStripMenuItem.Name = "showPersonDetalisToolStripMenuItem";
            this.showPersonDetalisToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
            this.showPersonDetalisToolStripMenuItem.Text = "Show person detalis";
            this.showPersonDetalisToolStripMenuItem.Click += new System.EventHandler(this.showPersonDetalisToolStripMenuItem_Click);
            // 
            // showLicenseInfoToolStripMenuItem
            // 
            this.showLicenseInfoToolStripMenuItem.Name = "showLicenseInfoToolStripMenuItem";
            this.showLicenseInfoToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
            this.showLicenseInfoToolStripMenuItem.Text = "Show license info";
            this.showLicenseInfoToolStripMenuItem.Click += new System.EventHandler(this.showLicenseInfoToolStripMenuItem_Click);
            // 
            // showLicenseHistoryToolStripMenuItem
            // 
            this.showLicenseHistoryToolStripMenuItem.Name = "showLicenseHistoryToolStripMenuItem";
            this.showLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
            this.showLicenseHistoryToolStripMenuItem.Text = "show license history";
            this.showLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showLicenseHistoryToolStripMenuItem_Click);
            // 
            // releaseLicenseToolStripMenuItem
            // 
            this.releaseLicenseToolStripMenuItem.Name = "releaseLicenseToolStripMenuItem";
            this.releaseLicenseToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
            this.releaseLicenseToolStripMenuItem.Text = "Release License";
            this.releaseLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseLicenseToolStripMenuItem_Click);
            // 
            // frmListDetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1522, 563);
            this.Controls.Add(this.cbIsReleased);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmListDetainLicense";
            this.Text = "frmListDetainLicense";
            this.Load += new System.EventHandler(this.frmListDetainLicense_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cmsDetainLIcense.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip cmsDetainLIcense;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.ComboBox cbIsReleased;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ToolStripMenuItem showPersonDetalisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem releaseLicenseToolStripMenuItem;
    }
}