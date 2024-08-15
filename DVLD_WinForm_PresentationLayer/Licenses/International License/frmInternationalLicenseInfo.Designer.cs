namespace DVLD
{
    partial class frmInternationalLicenseInfo
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
            this.ctrlInternationalLicenseCard1 = new DVLD.ctrlInternationalLicenseCard();
            this.SuspendLayout();
            // 
            // ctrlInternationalLicenseCard1
            // 
            this.ctrlInternationalLicenseCard1.BackColor = System.Drawing.Color.DarkGray;
            this.ctrlInternationalLicenseCard1.Location = new System.Drawing.Point(12, 0);
            this.ctrlInternationalLicenseCard1.Name = "ctrlInternationalLicenseCard1";
            this.ctrlInternationalLicenseCard1.Size = new System.Drawing.Size(858, 353);
            this.ctrlInternationalLicenseCard1.TabIndex = 0;
            // 
            // frmInternationalLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(878, 375);
            this.Controls.Add(this.ctrlInternationalLicenseCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmInternationalLicenseInfo";
            this.Text = "International License Info";
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlInternationalLicenseCard ctrlInternationalLicenseCard1;
    }
}