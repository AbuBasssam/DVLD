namespace DVLD
{
    partial class frmLicenseHistory
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
            this.gpDriverLicense = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpLocalLicense = new System.Windows.Forms.TabPage();
            this.tpInternationalLicense = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.cmsLocalLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsInternationalLicense = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLocalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInternationalLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctrPersonCardWithFilter1 = new DVLD.ctrPersonCardWithFilter();
            this.gpDriverLicense.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpLocalLicense.SuspendLayout();
            this.tpInternationalLicense.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.cmsLocalLicense.SuspendLayout();
            this.cmsInternationalLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpDriverLicense
            // 
            this.gpDriverLicense.Controls.Add(this.tabControl1);
            this.gpDriverLicense.Location = new System.Drawing.Point(11, 581);
            this.gpDriverLicense.Name = "gpDriverLicense";
            this.gpDriverLicense.Size = new System.Drawing.Size(999, 229);
            this.gpDriverLicense.TabIndex = 1;
            this.gpDriverLicense.TabStop = false;
            this.gpDriverLicense.Text = "Driver License";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpLocalLicense);
            this.tabControl1.Controls.Add(this.tpInternationalLicense);
            this.tabControl1.Location = new System.Drawing.Point(6, 23);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(973, 200);
            this.tabControl1.TabIndex = 0;
            // 
            // tpLocalLicense
            // 
            this.tpLocalLicense.Controls.Add(this.dataGridView1);
            this.tpLocalLicense.Location = new System.Drawing.Point(4, 25);
            this.tpLocalLicense.Name = "tpLocalLicense";
            this.tpLocalLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tpLocalLicense.Size = new System.Drawing.Size(965, 171);
            this.tpLocalLicense.TabIndex = 0;
            this.tpLocalLicense.Text = "Local";
            this.tpLocalLicense.UseVisualStyleBackColor = true;
            // 
            // tpInternationalLicense
            // 
            this.tpInternationalLicense.Controls.Add(this.dataGridView2);
            this.tpInternationalLicense.Location = new System.Drawing.Point(4, 25);
            this.tpInternationalLicense.Name = "tpInternationalLicense";
            this.tpInternationalLicense.Padding = new System.Windows.Forms.Padding(3);
            this.tpInternationalLicense.Size = new System.Drawing.Size(965, 171);
            this.tpInternationalLicense.TabIndex = 1;
            this.tpInternationalLicense.Text = "International";
            this.tpInternationalLicense.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.cmsLocalLicense;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(3, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 26;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(959, 162);
            this.dataGridView1.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ContextMenuStrip = this.cmsInternationalLicense;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView2.Location = new System.Drawing.Point(3, 6);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 26;
            this.dataGridView2.Size = new System.Drawing.Size(959, 162);
            this.dataGridView2.TabIndex = 1;
            // 
            // cmsLocalLicense
            // 
            this.cmsLocalLicense.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsLocalLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLocalLicenseToolStripMenuItem});
            this.cmsLocalLicense.Name = "cmsLocalLicense";
            this.cmsLocalLicense.Size = new System.Drawing.Size(210, 30);
            // 
            // cmsInternationalLicense
            // 
            this.cmsInternationalLicense.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsInternationalLicense.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInternationalLicenseToolStripMenuItem});
            this.cmsInternationalLicense.Name = "cmsInternationalLicense";
            this.cmsInternationalLicense.Size = new System.Drawing.Size(259, 58);
            // 
            // showLocalLicenseToolStripMenuItem
            // 
            this.showLocalLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.driving_license1;
            this.showLocalLicenseToolStripMenuItem.Name = "showLocalLicenseToolStripMenuItem";
            this.showLocalLicenseToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.showLocalLicenseToolStripMenuItem.Text = "Show Local License";
            this.showLocalLicenseToolStripMenuItem.Click += new System.EventHandler(this.showLocalLicenseToolStripMenuItem_Click);
            // 
            // showInternationalLicenseToolStripMenuItem
            // 
            this.showInternationalLicenseToolStripMenuItem.Image = global::DVLD.Properties.Resources.globe_earth__1_;
            this.showInternationalLicenseToolStripMenuItem.Name = "showInternationalLicenseToolStripMenuItem";
            this.showInternationalLicenseToolStripMenuItem.Size = new System.Drawing.Size(258, 26);
            this.showInternationalLicenseToolStripMenuItem.Text = "Show International License";
            this.showInternationalLicenseToolStripMenuItem.Click += new System.EventHandler(this.showInternationalLicenseToolStripMenuItem_Click);
            // 
            // ctrPersonCardWithFilter1
            // 
            this.ctrPersonCardWithFilter1.BackColor = System.Drawing.Color.DarkGray;
            this.ctrPersonCardWithFilter1.Location = new System.Drawing.Point(3, 3);
            this.ctrPersonCardWithFilter1.Name = "ctrPersonCardWithFilter1";
            this.ctrPersonCardWithFilter1.Size = new System.Drawing.Size(992, 570);
            this.ctrPersonCardWithFilter1.TabIndex = 0;
            // 
            // frmLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1020, 853);
            this.Controls.Add(this.gpDriverLicense);
            this.Controls.Add(this.ctrPersonCardWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLicenseHistory";
            this.Text = "frmLicenseHistory";
            this.Load += new System.EventHandler(this.frmLicenseHistory_Load);
            this.gpDriverLicense.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpLocalLicense.ResumeLayout(false);
            this.tpInternationalLicense.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.cmsLocalLicense.ResumeLayout(false);
            this.cmsInternationalLicense.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrPersonCardWithFilter ctrPersonCardWithFilter1;
        private System.Windows.Forms.GroupBox gpDriverLicense;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpLocalLicense;
        private System.Windows.Forms.TabPage tpInternationalLicense;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ContextMenuStrip cmsLocalLicense;
        private System.Windows.Forms.ContextMenuStrip cmsInternationalLicense;
        private System.Windows.Forms.ToolStripMenuItem showLocalLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showInternationalLicenseToolStripMenuItem;
    }
}