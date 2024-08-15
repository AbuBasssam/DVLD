namespace DVLD
{
    partial class frmInternationalDrivingLicense
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cmsInternationalLIcense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmshowPersonDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmshowLicenseDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowPersonHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.cbIsReleased = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.cmsInternationalLIcense.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddNew.Location = new System.Drawing.Point(1023, 181);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(296, 68);
            this.btnAddNew.TabIndex = 19;
            this.btnAddNew.Text = "Add International Driving License Application";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFilterBy.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterBy.Location = new System.Drawing.Point(328, 214);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.Size = new System.Drawing.Size(275, 35);
            this.txtFilterBy.TabIndex = 18;
            this.txtFilterBy.Visible = false;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged);
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 36);
            this.label1.TabIndex = 17;
            this.label1.Text = "Filter By";
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterBy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "None",
            "International License ID",
            "Application ID",
            "Driver ID",
            "Local License ID",
            "Is Active"});
            this.cmbFilterBy.Location = new System.Drawing.Point(130, 217);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(192, 32);
            this.cmbFilterBy.TabIndex = 16;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(360, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(726, 48);
            this.label2.TabIndex = 15;
            this.label2.Text = "International Driving License Application";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.cmsInternationalLIcense;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 263);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1332, 300);
            this.dataGridView1.TabIndex = 14;
            // 
            // cmsInternationalLIcense
            // 
            this.cmsInternationalLIcense.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsInternationalLIcense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmshowPersonDetails,
            this.tsmshowLicenseDetails,
            this.tsmShowPersonHistory});
            this.cmsInternationalLIcense.Name = "cmsInternationalLIcense";
            this.cmsInternationalLIcense.Size = new System.Drawing.Size(216, 76);
            // 
            // tsmshowPersonDetails
            // 
            this.tsmshowPersonDetails.Name = "tsmshowPersonDetails";
            this.tsmshowPersonDetails.Size = new System.Drawing.Size(215, 24);
            this.tsmshowPersonDetails.Text = "Show person details";
            this.tsmshowPersonDetails.Click += new System.EventHandler(this.tsmshowPersonDetails_Click);
            // 
            // tsmshowLicenseDetails
            // 
            this.tsmshowLicenseDetails.Name = "tsmshowLicenseDetails";
            this.tsmshowLicenseDetails.Size = new System.Drawing.Size(215, 24);
            this.tsmshowLicenseDetails.Text = "Show license details ";
            this.tsmshowLicenseDetails.Click += new System.EventHandler(this.tsmshowLicenseDetails_Click);
            // 
            // tsmShowPersonHistory
            // 
            this.tsmShowPersonHistory.Name = "tsmShowPersonHistory";
            this.tsmShowPersonHistory.Size = new System.Drawing.Size(215, 24);
            this.tsmShowPersonHistory.Text = "Show person history";
            this.tsmShowPersonHistory.Click += new System.EventHandler(this.tsmShowPersonHistory_Click);
            // 
            // cbIsReleased
            // 
            this.cbIsReleased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsReleased.FormattingEnabled = true;
            this.cbIsReleased.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsReleased.Location = new System.Drawing.Point(328, 217);
            this.cbIsReleased.Name = "cbIsReleased";
            this.cbIsReleased.Size = new System.Drawing.Size(121, 24);
            this.cbIsReleased.TabIndex = 164;
            this.cbIsReleased.Visible = false;
            // 
            // frmInternationalDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1332, 563);
            this.Controls.Add(this.cbIsReleased);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.txtFilterBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmInternationalDrivingLicense";
            this.Text = "International Driving License";
            this.Load += new System.EventHandler(this.frmInternationalDrivingLicenseApplication_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.cmsInternationalLIcense.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.TextBox txtFilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip cmsInternationalLIcense;
        private System.Windows.Forms.ToolStripMenuItem tsmshowPersonDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmshowLicenseDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmShowPersonHistory;
        private System.Windows.Forms.ComboBox cbIsReleased;
    }
}