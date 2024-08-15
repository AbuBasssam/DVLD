namespace DVLD
{
    partial class frmDetainLicense
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
            this.llShowLIcenseHistory = new System.Windows.Forms.LinkLabel();
            this.llShowLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.btnDetain = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gpInternationalApplication = new System.Windows.Forms.GroupBox();
            this.txtFineFees = new System.Windows.Forms.TextBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblILicenseID = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.pbIssueReason = new System.Windows.Forms.PictureBox();
            this.pbExpirationDate = new System.Windows.Forms.PictureBox();
            this.pbIsActive = new System.Windows.Forms.PictureBox();
            this.pbNationalNo = new System.Windows.Forms.PictureBox();
            this.pbGender = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ctrlLicenseWithFilter1 = new DVLD.ctrlLicenseWithFilter();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gpInternationalApplication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpirationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNationalNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // llShowLIcenseHistory
            // 
            this.llShowLIcenseHistory.AutoSize = true;
            this.llShowLIcenseHistory.Location = new System.Drawing.Point(178, 779);
            this.llShowLIcenseHistory.Name = "llShowLIcenseHistory";
            this.llShowLIcenseHistory.Size = new System.Drawing.Size(139, 17);
            this.llShowLIcenseHistory.TabIndex = 144;
            this.llShowLIcenseHistory.TabStop = true;
            this.llShowLIcenseHistory.Text = "Show LIcense History";
            this.llShowLIcenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLIcenseHistory_LinkClicked_1);
            // 
            // llShowLicenseInfo
            // 
            this.llShowLicenseInfo.AutoSize = true;
            this.llShowLicenseInfo.Location = new System.Drawing.Point(8, 779);
            this.llShowLicenseInfo.Name = "llShowLicenseInfo";
            this.llShowLicenseInfo.Size = new System.Drawing.Size(118, 17);
            this.llShowLicenseInfo.TabIndex = 143;
            this.llShowLicenseInfo.TabStop = true;
            this.llShowLicenseInfo.Text = "Show License Info";
            this.llShowLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLicenseInfo_LinkClicked);
            // 
            // btnDetain
            // 
            this.btnDetain.BackColor = System.Drawing.Color.DarkGray;
            this.btnDetain.FlatAppearance.BorderSize = 0;
            this.btnDetain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDetain.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetain.Image = global::DVLD.Properties.Resources.driving_license1;
            this.btnDetain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetain.Location = new System.Drawing.Point(741, 758);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(132, 50);
            this.btnDetain.TabIndex = 142;
            this.btnDetain.Text = "Detain";
            this.btnDetain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetain.UseVisualStyleBackColor = false;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkGray;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD.Properties.Resources.close;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(567, 762);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(132, 50);
            this.btnClose.TabIndex = 141;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // gpInternationalApplication
            // 
            this.gpInternationalApplication.BackColor = System.Drawing.Color.DarkGray;
            this.gpInternationalApplication.Controls.Add(this.txtFineFees);
            this.gpInternationalApplication.Controls.Add(this.lblCreatedBy);
            this.gpInternationalApplication.Controls.Add(this.lblILicenseID);
            this.gpInternationalApplication.Controls.Add(this.lblApplicationDate);
            this.gpInternationalApplication.Controls.Add(this.lblDetainID);
            this.gpInternationalApplication.Controls.Add(this.pbIssueReason);
            this.gpInternationalApplication.Controls.Add(this.pbExpirationDate);
            this.gpInternationalApplication.Controls.Add(this.pbIsActive);
            this.gpInternationalApplication.Controls.Add(this.pbNationalNo);
            this.gpInternationalApplication.Controls.Add(this.pbGender);
            this.gpInternationalApplication.Controls.Add(this.label12);
            this.gpInternationalApplication.Controls.Add(this.label9);
            this.gpInternationalApplication.Controls.Add(this.label7);
            this.gpInternationalApplication.Controls.Add(this.label5);
            this.gpInternationalApplication.Controls.Add(this.label22);
            this.gpInternationalApplication.Location = new System.Drawing.Point(11, 553);
            this.gpInternationalApplication.Name = "gpInternationalApplication";
            this.gpInternationalApplication.Size = new System.Drawing.Size(862, 196);
            this.gpInternationalApplication.TabIndex = 140;
            this.gpInternationalApplication.TabStop = false;
            this.gpInternationalApplication.Text = "Application Info";
            // 
            // txtFineFees
            // 
            this.txtFineFees.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFineFees.Location = new System.Drawing.Point(174, 101);
            this.txtFineFees.Name = "txtFineFees";
            this.txtFineFees.Size = new System.Drawing.Size(97, 35);
            this.txtFineFees.TabIndex = 85;
            this.txtFineFees.Validating += new System.ComponentModel.CancelEventHandler(this.txtFineFees_Validating);
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(619, 63);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(39, 17);
            this.lblCreatedBy.TabIndex = 84;
            this.lblCreatedBy.Text = "[???]";
            // 
            // lblILicenseID
            // 
            this.lblILicenseID.AutoSize = true;
            this.lblILicenseID.Location = new System.Drawing.Point(619, 23);
            this.lblILicenseID.Name = "lblILicenseID";
            this.lblILicenseID.Size = new System.Drawing.Size(39, 17);
            this.lblILicenseID.TabIndex = 81;
            this.lblILicenseID.Text = "[???]";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Location = new System.Drawing.Point(171, 63);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(39, 17);
            this.lblApplicationDate.TabIndex = 78;
            this.lblApplicationDate.Text = "[???]";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Location = new System.Drawing.Point(171, 23);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(39, 17);
            this.lblDetainID.TabIndex = 77;
            this.lblDetainID.Text = "[???]";
            // 
            // pbIssueReason
            // 
            this.pbIssueReason.Image = global::DVLD.Properties.Resources.coin;
            this.pbIssueReason.Location = new System.Drawing.Point(126, 106);
            this.pbIssueReason.Name = "pbIssueReason";
            this.pbIssueReason.Size = new System.Drawing.Size(30, 30);
            this.pbIssueReason.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIssueReason.TabIndex = 76;
            this.pbIssueReason.TabStop = false;
            // 
            // pbExpirationDate
            // 
            this.pbExpirationDate.Image = global::DVLD.Properties.Resources.calendar;
            this.pbExpirationDate.Location = new System.Drawing.Point(570, 63);
            this.pbExpirationDate.Name = "pbExpirationDate";
            this.pbExpirationDate.Size = new System.Drawing.Size(30, 30);
            this.pbExpirationDate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbExpirationDate.TabIndex = 74;
            this.pbExpirationDate.TabStop = false;
            // 
            // pbIsActive
            // 
            this.pbIsActive.Image = global::DVLD.Properties.Resources.driving_license1;
            this.pbIsActive.Location = new System.Drawing.Point(570, 18);
            this.pbIsActive.Name = "pbIsActive";
            this.pbIsActive.Size = new System.Drawing.Size(30, 30);
            this.pbIsActive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIsActive.TabIndex = 73;
            this.pbIsActive.TabStop = false;
            // 
            // pbNationalNo
            // 
            this.pbNationalNo.Image = global::DVLD.Properties.Resources.Name_Card;
            this.pbNationalNo.Location = new System.Drawing.Point(124, 18);
            this.pbNationalNo.Name = "pbNationalNo";
            this.pbNationalNo.Size = new System.Drawing.Size(30, 30);
            this.pbNationalNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbNationalNo.TabIndex = 71;
            this.pbNationalNo.TabStop = false;
            // 
            // pbGender
            // 
            this.pbGender.Image = global::DVLD.Properties.Resources.calendar;
            this.pbGender.Location = new System.Drawing.Point(126, 58);
            this.pbGender.Name = "pbGender";
            this.pbGender.Size = new System.Drawing.Size(30, 30);
            this.pbGender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbGender.TabIndex = 70;
            this.pbGender.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(419, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 16);
            this.label12.TabIndex = 68;
            this.label12.Text = "CreatedBy :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(419, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 16);
            this.label9.TabIndex = 65;
            this.label9.Text = "LicenseID  :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 16);
            this.label7.TabIndex = 64;
            this.label7.Text = "Fine Fees :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 62;
            this.label5.Text = "Application Date :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(4, 23);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(72, 16);
            this.label22.TabIndex = 61;
            this.label22.Text = "DetainID :";
            // 
            // ctrlLicenseWithFilter1
            // 
            this.ctrlLicenseWithFilter1.BackColor = System.Drawing.Color.DarkGray;
            this.ctrlLicenseWithFilter1.FilterEnabled = true;
            this.ctrlLicenseWithFilter1.Location = new System.Drawing.Point(2, 3);
            this.ctrlLicenseWithFilter1.Name = "ctrlLicenseWithFilter1";
            this.ctrlLicenseWithFilter1.Size = new System.Drawing.Size(871, 544);
            this.ctrlLicenseWithFilter1.TabIndex = 139;
            this.ctrlLicenseWithFilter1.OnLicenseSelected += new System.Action<int>(this.ctrlLicenseWithFilter1_OnLicenseSelected);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmDetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(882, 833);
            this.Controls.Add(this.llShowLIcenseHistory);
            this.Controls.Add(this.llShowLicenseInfo);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gpInternationalApplication);
            this.Controls.Add(this.ctrlLicenseWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDetainLicense";
            this.Text = "frmDetainLicense";
            this.Load += new System.EventHandler(this.frmDetainLicense_Load);
            this.gpInternationalApplication.ResumeLayout(false);
            this.gpInternationalApplication.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpirationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNationalNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel llShowLIcenseHistory;
        private System.Windows.Forms.LinkLabel llShowLicenseInfo;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gpInternationalApplication;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblILicenseID;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.PictureBox pbIssueReason;
        private System.Windows.Forms.PictureBox pbExpirationDate;
        private System.Windows.Forms.PictureBox pbIsActive;
        private System.Windows.Forms.PictureBox pbNationalNo;
        private System.Windows.Forms.PictureBox pbGender;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label22;
        private ctrlLicenseWithFilter ctrlLicenseWithFilter1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFineFees;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}