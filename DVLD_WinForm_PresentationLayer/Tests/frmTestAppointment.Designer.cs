namespace DVLD
{
    partial class frmTestAppointment
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
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.dgvLicenseTestAppointments = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ctrlAppointmentDetails1 = new DVLD.ctrlAppointmentDetails();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLicenseTestAppointments)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.Transparent;
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbImage.Location = new System.Drawing.Point(314, 12);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(154, 130);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            // 
            // dgvLicenseTestAppointments
            // 
            this.dgvLicenseTestAppointments.AllowUserToAddRows = false;
            this.dgvLicenseTestAppointments.AllowUserToDeleteRows = false;
            this.dgvLicenseTestAppointments.AllowUserToOrderColumns = true;
            this.dgvLicenseTestAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvLicenseTestAppointments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvLicenseTestAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLicenseTestAppointments.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvLicenseTestAppointments.Location = new System.Drawing.Point(0, 835);
            this.dgvLicenseTestAppointments.Name = "dgvLicenseTestAppointments";
            this.dgvLicenseTestAppointments.ReadOnly = true;
            this.dgvLicenseTestAppointments.RowHeadersWidth = 51;
            this.dgvLicenseTestAppointments.RowTemplate.Height = 26;
            this.dgvLicenseTestAppointments.Size = new System.Drawing.Size(760, 150);
            this.dgvLicenseTestAppointments.TabIndex = 3;
            this.dgvLicenseTestAppointments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllAppointment_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 52);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(137, 24);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTesToolStripMenuItem
            // 
            this.takeTesToolStripMenuItem.Name = "takeTesToolStripMenuItem";
            this.takeTesToolStripMenuItem.Size = new System.Drawing.Size(137, 24);
            this.takeTesToolStripMenuItem.Text = "Take Test";
            this.takeTesToolStripMenuItem.Click += new System.EventHandler(this.takeTesToolStripMenuItem_Click);
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.Location = new System.Drawing.Point(519, 763);
            this.btnAddAppointment.Margin = new System.Windows.Forms.Padding(10);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(222, 59);
            this.btnAddAppointment.TabIndex = 4;
            this.btnAddAppointment.Text = "Add New Appointment";
            this.btnAddAppointment.UseVisualStyleBackColor = true;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(189, 178);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(124, 48);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "label1";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctrlAppointmentDetails1
            // 
            this.ctrlAppointmentDetails1.AutoSize = true;
            this.ctrlAppointmentDetails1.Location = new System.Drawing.Point(7, 269);
            this.ctrlAppointmentDetails1.Margin = new System.Windows.Forms.Padding(10);
            this.ctrlAppointmentDetails1.Name = "ctrlAppointmentDetails1";
            this.ctrlAppointmentDetails1.Size = new System.Drawing.Size(739, 474);
            this.ctrlAppointmentDetails1.TabIndex = 2;
            // 
            // frmTestAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(760, 985);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.dgvLicenseTestAppointments);
            this.Controls.Add(this.ctrlAppointmentDetails1);
            this.Controls.Add(this.pbImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTestAppointment";
            this.Text = "Test Appointment";
            this.Load += new System.EventHandler(this.frmTestAppointment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLicenseTestAppointments)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private ctrlAppointmentDetails ctrlAppointmentDetails1;
        private System.Windows.Forms.DataGridView dgvLicenseTestAppointments;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTesToolStripMenuItem;
    }
}