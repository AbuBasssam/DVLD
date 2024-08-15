namespace DVLD
{
    partial class frmDamagedOrLostLicense
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
            this.ctrlLicenseWithFilter1 = new DVLD.ctrlLicenseWithFilter();
            this.gpReplacementFor = new System.Windows.Forms.GroupBox();
            this.rbLost = new System.Windows.Forms.RadioButton();
            this.rbDamaged = new System.Windows.Forms.RadioButton();
            this.gpInternationalApplication = new System.Windows.Forms.GroupBox();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.lblOldLicenseID = new System.Windows.Forms.Label();
            this.lblIReplacedLicenseID = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.lblApplicationID = new System.Windows.Forms.Label();
            this.pbIssueReason = new System.Windows.Forms.PictureBox();
            this.pbExpirationDate = new System.Windows.Forms.PictureBox();
            this.pbIsActive = new System.Windows.Forms.PictureBox();
            this.pbDateOfBirth = new System.Windows.Forms.PictureBox();
            this.pbNationalNo = new System.Windows.Forms.PictureBox();
            this.pbGender = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.llShowLIcenseHistory = new System.Windows.Forms.LinkLabel();
            this.llShowNewLicenseInfo = new System.Windows.Forms.LinkLabel();
            this.gpReplacementFor.SuspendLayout();
            this.gpInternationalApplication.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpirationDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDateOfBirth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNationalNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGender)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlLicenseWithFilter1
            // 
            this.ctrlLicenseWithFilter1.BackColor = System.Drawing.Color.DarkGray;
            this.ctrlLicenseWithFilter1.Location = new System.Drawing.Point(1, 3);
            this.ctrlLicenseWithFilter1.Name = "ctrlLicenseWithFilter1";
            this.ctrlLicenseWithFilter1.Size = new System.Drawing.Size(871, 544);
            this.ctrlLicenseWithFilter1.TabIndex = 1;
            this.ctrlLicenseWithFilter1.OnLicenseSelected += new System.Action<int>(this.ctrlLicenseWithFilter1_OnLicenseSelected);
            // 
            // gpReplacementFor
            // 
            this.gpReplacementFor.Controls.Add(this.rbLost);
            this.gpReplacementFor.Controls.Add(this.rbDamaged);
            this.gpReplacementFor.Location = new System.Drawing.Point(878, 12);
            this.gpReplacementFor.Name = "gpReplacementFor";
            this.gpReplacementFor.Size = new System.Drawing.Size(191, 167);
            this.gpReplacementFor.TabIndex = 2;
            this.gpReplacementFor.TabStop = false;
            this.gpReplacementFor.Text = "Replacement for";
            // 
            // rbLost
            // 
            this.rbLost.AutoSize = true;
            this.rbLost.Location = new System.Drawing.Point(10, 77);
            this.rbLost.Name = "rbLost";
            this.rbLost.Size = new System.Drawing.Size(103, 21);
            this.rbLost.TabIndex = 1;
            this.rbLost.TabStop = true;
            this.rbLost.Text = "Lost License";
            this.rbLost.UseVisualStyleBackColor = true;
            this.rbLost.CheckedChanged += new System.EventHandler(this.RadioButtonChecked_CheckedChanged);
            // 
            // rbDamaged
            // 
            this.rbDamaged.AutoSize = true;
            this.rbDamaged.Location = new System.Drawing.Point(10, 37);
            this.rbDamaged.Name = "rbDamaged";
            this.rbDamaged.Size = new System.Drawing.Size(131, 21);
            this.rbDamaged.TabIndex = 0;
            this.rbDamaged.TabStop = true;
            this.rbDamaged.Text = "Damaged license";
            this.rbDamaged.UseVisualStyleBackColor = true;
            this.rbDamaged.CheckedChanged += new System.EventHandler(this.RadioButtonChecked_CheckedChanged);
            // 
            // gpInternationalApplication
            // 
            this.gpInternationalApplication.BackColor = System.Drawing.Color.DarkGray;
            this.gpInternationalApplication.Controls.Add(this.lblCreatedBy);
            this.gpInternationalApplication.Controls.Add(this.lblOldLicenseID);
            this.gpInternationalApplication.Controls.Add(this.lblIReplacedLicenseID);
            this.gpInternationalApplication.Controls.Add(this.lblApplicationFees);
            this.gpInternationalApplication.Controls.Add(this.lblApplicationDate);
            this.gpInternationalApplication.Controls.Add(this.lblApplicationID);
            this.gpInternationalApplication.Controls.Add(this.pbIssueReason);
            this.gpInternationalApplication.Controls.Add(this.pbExpirationDate);
            this.gpInternationalApplication.Controls.Add(this.pbIsActive);
            this.gpInternationalApplication.Controls.Add(this.pbDateOfBirth);
            this.gpInternationalApplication.Controls.Add(this.pbNationalNo);
            this.gpInternationalApplication.Controls.Add(this.pbGender);
            this.gpInternationalApplication.Controls.Add(this.label12);
            this.gpInternationalApplication.Controls.Add(this.label10);
            this.gpInternationalApplication.Controls.Add(this.label9);
            this.gpInternationalApplication.Controls.Add(this.label7);
            this.gpInternationalApplication.Controls.Add(this.label5);
            this.gpInternationalApplication.Controls.Add(this.label22);
            this.gpInternationalApplication.Location = new System.Drawing.Point(10, 553);
            this.gpInternationalApplication.Name = "gpInternationalApplication";
            this.gpInternationalApplication.Size = new System.Drawing.Size(862, 196);
            this.gpInternationalApplication.TabIndex = 5;
            this.gpInternationalApplication.TabStop = false;
            this.gpInternationalApplication.Text = "Application Info";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Location = new System.Drawing.Point(619, 106);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(39, 17);
            this.lblCreatedBy.TabIndex = 84;
            this.lblCreatedBy.Text = "[???]";
            // 
            // lblOldLicenseID
            // 
            this.lblOldLicenseID.AutoSize = true;
            this.lblOldLicenseID.Location = new System.Drawing.Point(619, 63);
            this.lblOldLicenseID.Name = "lblOldLicenseID";
            this.lblOldLicenseID.Size = new System.Drawing.Size(39, 17);
            this.lblOldLicenseID.TabIndex = 82;
            this.lblOldLicenseID.Text = "[???]";
            // 
            // lblIReplacedLicenseID
            // 
            this.lblIReplacedLicenseID.AutoSize = true;
            this.lblIReplacedLicenseID.Location = new System.Drawing.Point(619, 23);
            this.lblIReplacedLicenseID.Name = "lblIReplacedLicenseID";
            this.lblIReplacedLicenseID.Size = new System.Drawing.Size(39, 17);
            this.lblIReplacedLicenseID.TabIndex = 81;
            this.lblIReplacedLicenseID.Text = "[???]";
            // 
            // lblApplicationFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Location = new System.Drawing.Point(171, 111);
            this.lblApplicationFees.Name = "lblApplicationFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(39, 17);
            this.lblApplicationFees.TabIndex = 80;
            this.lblApplicationFees.Text = "[???]";
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
            // lblApplicationID
            // 
            this.lblApplicationID.AutoSize = true;
            this.lblApplicationID.Location = new System.Drawing.Point(171, 23);
            this.lblApplicationID.Name = "lblApplicationID";
            this.lblApplicationID.Size = new System.Drawing.Size(39, 17);
            this.lblApplicationID.TabIndex = 77;
            this.lblApplicationID.Text = "[???]";
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
            this.pbExpirationDate.Location = new System.Drawing.Point(570, 98);
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
            // pbDateOfBirth
            // 
            this.pbDateOfBirth.Image = global::DVLD.Properties.Resources.driving_license;
            this.pbDateOfBirth.Location = new System.Drawing.Point(570, 59);
            this.pbDateOfBirth.Name = "pbDateOfBirth";
            this.pbDateOfBirth.Size = new System.Drawing.Size(30, 30);
            this.pbDateOfBirth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDateOfBirth.TabIndex = 72;
            this.pbDateOfBirth.TabStop = false;
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
            this.label12.Location = new System.Drawing.Point(419, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 16);
            this.label12.TabIndex = 68;
            this.label12.Text = "CreatedBy :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(419, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 16);
            this.label10.TabIndex = 66;
            this.label10.Text = "Old LicenseID :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(419, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 16);
            this.label9.TabIndex = 65;
            this.label9.Text = "Replaced LicenseID  :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 16);
            this.label7.TabIndex = 64;
            this.label7.Text = "Application Fees :";
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
            this.label22.Size = new System.Drawing.Size(102, 16);
            this.label22.TabIndex = 61;
            this.label22.Text = "ApplicationID :";
            // 
            // btnReplace
            // 
            this.btnReplace.BackColor = System.Drawing.Color.DarkGray;
            this.btnReplace.FlatAppearance.BorderSize = 0;
            this.btnReplace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReplace.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplace.Image = global::DVLD.Properties.Resources.driving_license1;
            this.btnReplace.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReplace.Location = new System.Drawing.Point(740, 758);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(132, 50);
            this.btnReplace.TabIndex = 136;
            this.btnReplace.Text = "Replace";
            this.btnReplace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReplace.UseVisualStyleBackColor = false;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
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
            this.btnClose.Location = new System.Drawing.Point(566, 762);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(132, 50);
            this.btnClose.TabIndex = 135;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // llShowLIcenseHistory
            // 
            this.llShowLIcenseHistory.AutoSize = true;
            this.llShowLIcenseHistory.Location = new System.Drawing.Point(177, 779);
            this.llShowLIcenseHistory.Name = "llShowLIcenseHistory";
            this.llShowLIcenseHistory.Size = new System.Drawing.Size(139, 17);
            this.llShowLIcenseHistory.TabIndex = 138;
            this.llShowLIcenseHistory.TabStop = true;
            this.llShowLIcenseHistory.Text = "Show LIcense History";
            this.llShowLIcenseHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowLIcenseHistory_LinkClicked);
            // 
            // llShowNewLicenseInfo
            // 
            this.llShowNewLicenseInfo.AutoSize = true;
            this.llShowNewLicenseInfo.Location = new System.Drawing.Point(7, 779);
            this.llShowNewLicenseInfo.Name = "llShowNewLicenseInfo";
            this.llShowNewLicenseInfo.Size = new System.Drawing.Size(148, 17);
            this.llShowNewLicenseInfo.TabIndex = 137;
            this.llShowNewLicenseInfo.TabStop = true;
            this.llShowNewLicenseInfo.Text = "Show New License Info";
            this.llShowNewLicenseInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowNewLicenseInfo_LinkClicked);
            // 
            // frmDamagedOrLostLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1079, 835);
            this.Controls.Add(this.llShowLIcenseHistory);
            this.Controls.Add(this.llShowNewLicenseInfo);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gpInternationalApplication);
            this.Controls.Add(this.gpReplacementFor);
            this.Controls.Add(this.ctrlLicenseWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDamagedOrLostLicense";
            this.Text = "Replacement License";
            this.Load += new System.EventHandler(this.frmDamagedOrLostLicense_Load);
            this.gpReplacementFor.ResumeLayout(false);
            this.gpReplacementFor.PerformLayout();
            this.gpInternationalApplication.ResumeLayout(false);
            this.gpInternationalApplication.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIssueReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExpirationDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDateOfBirth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNationalNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGender)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlLicenseWithFilter ctrlLicenseWithFilter1;
        private System.Windows.Forms.GroupBox gpReplacementFor;
        private System.Windows.Forms.RadioButton rbLost;
        private System.Windows.Forms.RadioButton rbDamaged;
        private System.Windows.Forms.GroupBox gpInternationalApplication;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblOldLicenseID;
        private System.Windows.Forms.Label lblIReplacedLicenseID;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.Label lblApplicationID;
        private System.Windows.Forms.PictureBox pbIssueReason;
        private System.Windows.Forms.PictureBox pbExpirationDate;
        private System.Windows.Forms.PictureBox pbIsActive;
        private System.Windows.Forms.PictureBox pbDateOfBirth;
        private System.Windows.Forms.PictureBox pbNationalNo;
        private System.Windows.Forms.PictureBox pbGender;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel llShowLIcenseHistory;
        private System.Windows.Forms.LinkLabel llShowNewLicenseInfo;
    }
}