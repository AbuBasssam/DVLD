using DVlD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmApplicationDetails : Form
    {
         int LicenseApplicationID;
        string ClassName;
        public frmApplicationDetails(int LicenseApplicationID)
        {
            InitializeComponent();
            
            this.LicenseApplicationID = LicenseApplicationID;
        }
        

        private void frmApplicationDetails_Load(object sender, EventArgs e)
        {
            ctrlAppointmentDetails1.LoadApplicationDetalis(clsLocalDrivingLicenseApplication.Find(LicenseApplicationID).ApplicationID);
            ctrlAppointmentDetails1.LoadLicenseApplicationDetalis(LicenseApplicationID);
           
        }
    }
}
