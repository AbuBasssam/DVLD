using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVlD.BusinessLayer;
using DVlD_BusinessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class frmIssueLicense : Form
    {
        int _LocalDrivingLicenseApplicationID;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmIssueLicense(int LicenseApplicationID)
        {
            InitializeComponent();
            this._LocalDrivingLicenseApplicationID = LicenseApplicationID;
           
        }

       

        private void frmIssueLicense_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrlAppointmentDetails1.LoadApplicationDetalis(_LocalDrivingLicenseApplication.ApplicationID);
            ctrlAppointmentDetails1.LoadLicenseApplicationDetalis(_LocalDrivingLicenseApplicationID);
           

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), (int)clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





            /* Anothter way
             clsDriver Driver;

            if (clsDriver.IsDriverExistByPersonID(_LocalDrivingLicenseApplication.ApplicationInfo.ApplicationPersonID))
            {
                Driver = clsDriver.FindByPersonID(_LocalDrivingLicenseApplication.ApplicationInfo.ApplicationPersonID);

            }
            else
            {


                Driver = new clsDriver();
                Driver.PersonID = _LocalDrivingLicenseApplication.ApplicationInfo.ApplicationPersonID;
                Driver.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                Driver.CreatedDate = DateTime.Now;
                Driver.Save();
            }

            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = _LocalDrivingLicenseApplication.ApplicationID;
            NewLicense.DriverID = Driver.DriverID;
            NewLicense.LicenseClass = _LocalDrivingLicenseApplication.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate =DateTime.Now.AddYears(_LocalDrivingLicenseApplication.License.DefalutValidityLength);
            NewLicense.Notes = txtNotes.Text.Trim();
            NewLicense.PaidFees = clsLicenseClasses.Find(clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID).LicenseClassID).ClassFees;
            NewLicense.IsActive = 1;
            NewLicense.IssueReason = clsLicense.enIssueReason.FirstTime;
            NewLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (NewLicense.Save())
            {
                MessageBox.Show("Data saved Successfully","Saved",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                clsLocalDrivingLicenseApplication App = clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);
                clsApplication application = clsApplication.Find(App.ApplicationID);
                application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                application.LastStauteDate = DateTime.Now;
                application.Save();
                btnSave.Enabled = false;
                ctrlAppointmentDetails1.EnableLink();
            }*/


        }

    }
}
