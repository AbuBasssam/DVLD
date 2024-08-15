namespace DVLD
{
    partial class frmApplicationDetails
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
            this.ctrlAppointmentDetails1 = new DVLD.ctrlAppointmentDetails();
            this.SuspendLayout();
            // 
            // ctrlAppointmentDetails1
            // 
            this.ctrlAppointmentDetails1.AutoSize = true;
            this.ctrlAppointmentDetails1.Location = new System.Drawing.Point(2, -2);
            this.ctrlAppointmentDetails1.Name = "ctrlAppointmentDetails1";
            this.ctrlAppointmentDetails1.Size = new System.Drawing.Size(734, 698);
            this.ctrlAppointmentDetails1.TabIndex = 0;
            // 
            // frmApplicationDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(745, 470);
            this.Controls.Add(this.ctrlAppointmentDetails1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmApplicationDetails";
            this.Text = "frmApplicationDetails";
            this.Load += new System.EventHandler(this.frmApplicationDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlAppointmentDetails ctrlAppointmentDetails1;
    }
}