namespace DVLD
{
    partial class frmLocalDrivingLicenseApplication
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
            this.btnAddNew = new System.Windows.Forms.Button();
            this.txtFilterBy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocalDrivingLicenseApplications = new System.Windows.Forms.DataGridView();
            this.cmsLocalDrivingLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showApplicationDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSechduleTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmissueDrivingLicenseFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).BeginInit();
            this.cmsLocalDrivingLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddNew.Location = new System.Drawing.Point(1038, 186);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(281, 68);
            this.btnAddNew.TabIndex = 13;
            this.btnAddNew.Text = "Add Local Driving License Application";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFilterBy.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterBy.Location = new System.Drawing.Point(328, 219);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(275, 35);
            this.txtFilterBy.TabIndex = 12;
            this.txtFilterBy.Visible = false;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 36);
            this.label1.TabIndex = 11;
            this.label1.Text = "Filter By";
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterBy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "none",
            "L.D.AppID",
            "National NO",
            "Full Name",
            "Stauts"});
            this.cmbFilterBy.Location = new System.Drawing.Point(130, 222);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(192, 32);
            this.cmbFilterBy.TabIndex = 10;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(360, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(592, 48);
            this.label2.TabIndex = 9;
            this.label2.Text = "Local Driving License Application";
            // 
            // dgvLocalDrivingLicenseApplications
            // 
            this.dgvLocalDrivingLicenseApplications.AllowUserToAddRows = false;
            this.dgvLocalDrivingLicenseApplications.AllowUserToDeleteRows = false;
            this.dgvLocalDrivingLicenseApplications.AllowUserToOrderColumns = true;
            this.dgvLocalDrivingLicenseApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLocalDrivingLicenseApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalDrivingLicenseApplications.ContextMenuStrip = this.cmsLocalDrivingLicense;
            this.dgvLocalDrivingLicenseApplications.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvLocalDrivingLicenseApplications.Location = new System.Drawing.Point(0, 260);
            this.dgvLocalDrivingLicenseApplications.Name = "dgvLocalDrivingLicenseApplications";
            this.dgvLocalDrivingLicenseApplications.ReadOnly = true;
            this.dgvLocalDrivingLicenseApplications.RowHeadersWidth = 51;
            this.dgvLocalDrivingLicenseApplications.RowTemplate.Height = 26;
            this.dgvLocalDrivingLicenseApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalDrivingLicenseApplications.Size = new System.Drawing.Size(1331, 300);
            this.dgvLocalDrivingLicenseApplications.TabIndex = 8;
            this.dgvLocalDrivingLicenseApplications.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // cmsLocalDrivingLicense
            // 
            this.cmsLocalDrivingLicense.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsLocalDrivingLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showApplicationDetailsToolStripMenuItem,
            this.tsmEditApplication,
            this.tsmDeleteApplication,
            this.tsmCancelApplication,
            this.tsmSechduleTest,
            this.tsmissueDrivingLicenseFirstTime,
            this.tsmShowLicense,
            this.tsmShowPersonLicenseHistory});
            this.cmsLocalDrivingLicense.Name = "cmsLocalDrivingLicense";
            this.cmsLocalDrivingLicense.Size = new System.Drawing.Size(293, 196);
            this.cmsLocalDrivingLicense.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLocalDrivingLicense_Opening);
            // 
            // showApplicationDetailsToolStripMenuItem
            // 
            this.showApplicationDetailsToolStripMenuItem.Name = "showApplicationDetailsToolStripMenuItem";
            this.showApplicationDetailsToolStripMenuItem.Size = new System.Drawing.Size(292, 24);
            this.showApplicationDetailsToolStripMenuItem.Text = "Show Application Details";
            this.showApplicationDetailsToolStripMenuItem.Click += new System.EventHandler(this.showApplicationDetailsToolStripMenuItem_Click);
            // 
            // tsmEditApplication
            // 
            this.tsmEditApplication.Name = "tsmEditApplication";
            this.tsmEditApplication.Size = new System.Drawing.Size(292, 24);
            this.tsmEditApplication.Text = "Edit Application";
            this.tsmEditApplication.Click += new System.EventHandler(this.tsmEditApplication_Click);
            // 
            // tsmDeleteApplication
            // 
            this.tsmDeleteApplication.Name = "tsmDeleteApplication";
            this.tsmDeleteApplication.Size = new System.Drawing.Size(292, 24);
            this.tsmDeleteApplication.Text = "Delete Application";
            this.tsmDeleteApplication.Click += new System.EventHandler(this.tsmDeleteApplication_Click);
            // 
            // tsmCancelApplication
            // 
            this.tsmCancelApplication.Name = "tsmCancelApplication";
            this.tsmCancelApplication.Size = new System.Drawing.Size(292, 24);
            this.tsmCancelApplication.Text = "Cancel Application";
            this.tsmCancelApplication.Click += new System.EventHandler(this.tsmCancelApplication_Click);
            // 
            // tsmSechduleTest
            // 
            this.tsmSechduleTest.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmVisionTest,
            this.tsmWrittenTest,
            this.tsmStreetTest});
            this.tsmSechduleTest.Name = "tsmSechduleTest";
            this.tsmSechduleTest.Size = new System.Drawing.Size(292, 24);
            this.tsmSechduleTest.Text = "Sechdule Test";
            this.tsmSechduleTest.MouseHover += new System.EventHandler(this.sechduleTestToolStripMenuItem_MouseHover);
            // 
            // tsmVisionTest
            // 
            this.tsmVisionTest.Name = "tsmVisionTest";
            this.tsmVisionTest.Size = new System.Drawing.Size(233, 26);
            this.tsmVisionTest.Text = "Sechdule Vision test";
            this.tsmVisionTest.Click += new System.EventHandler(this.tsmVisionTest_Click);
            // 
            // tsmWrittenTest
            // 
            this.tsmWrittenTest.Name = "tsmWrittenTest";
            this.tsmWrittenTest.Size = new System.Drawing.Size(233, 26);
            this.tsmWrittenTest.Text = "Sechdule Written test";
            this.tsmWrittenTest.Click += new System.EventHandler(this.tsmWrittenTest_Click);
            // 
            // tsmStreetTest
            // 
            this.tsmStreetTest.Name = "tsmStreetTest";
            this.tsmStreetTest.Size = new System.Drawing.Size(233, 26);
            this.tsmStreetTest.Text = "Sechdule Street test";
            this.tsmStreetTest.Click += new System.EventHandler(this.tsmStreetTest_Click);
            // 
            // tsmissueDrivingLicenseFirstTime
            // 
            this.tsmissueDrivingLicenseFirstTime.Name = "tsmissueDrivingLicenseFirstTime";
            this.tsmissueDrivingLicenseFirstTime.Size = new System.Drawing.Size(292, 24);
            this.tsmissueDrivingLicenseFirstTime.Text = "Issue Driving License( First Time)";
            this.tsmissueDrivingLicenseFirstTime.Click += new System.EventHandler(this.tsmissueDrivingLicenseFirstTime_Click);
            // 
            // tsmShowLicense
            // 
            this.tsmShowLicense.Name = "tsmShowLicense";
            this.tsmShowLicense.Size = new System.Drawing.Size(292, 24);
            this.tsmShowLicense.Text = "Show License";
            this.tsmShowLicense.Click += new System.EventHandler(this.tsmShowLicense_Click);
            // 
            // tsmShowPersonLicenseHistory
            // 
            this.tsmShowPersonLicenseHistory.Name = "tsmShowPersonLicenseHistory";
            this.tsmShowPersonLicenseHistory.Size = new System.Drawing.Size(292, 24);
            this.tsmShowPersonLicenseHistory.Text = "show Person License History";
            this.tsmShowPersonLicenseHistory.Click += new System.EventHandler(this.tsmShowPersonLicenseHistory_Click);
            // 
            // frmLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 560);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.txtFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvLocalDrivingLicenseApplications);
            this.Name = "frmLocalDrivingLicenseApplication";
            this.Text = "LocalDrivingLicenseApplication";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalDrivingLicenseApplications)).EndInit();
            this.cmsLocalDrivingLicense.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocalDrivingLicenseApplications;
        private System.Windows.Forms.ContextMenuStrip cmsLocalDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem showApplicationDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmEditApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmDeleteApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmCancelApplication;
        private System.Windows.Forms.ToolStripMenuItem tsmSechduleTest;
        private System.Windows.Forms.ToolStripMenuItem tsmVisionTest;
        private System.Windows.Forms.ToolStripMenuItem tsmWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem tsmStreetTest;
        private System.Windows.Forms.ToolStripMenuItem tsmissueDrivingLicenseFirstTime;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicense;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPersonLicenseHistory;
    }
}