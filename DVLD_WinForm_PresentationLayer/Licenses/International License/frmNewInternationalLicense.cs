using DVlD.BusinessLayer;
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
    public partial class frmNewInternationalLicense : Form
    {
        private clsInternationalLicense _License;
        
        public frmNewInternationalLicense()
        {
            InitializeComponent();
        }
        
        private clsApplication _LoadNewBasicApplication()
        {
            clsApplication Application = new clsApplication();
            Application.ApplicationPersonID= (int)clsLicense.Find(ctrlLicenseWithFilter1.LicenseID).DriverInfo.PersonID;
            Application.ApplicationDate= DateTime.Now;
            Application.ApplicationTypeID = 6;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            Application.LastStauteDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(6).Fees;
            Application.CreatedBy = (int)clsGlobal.CurrentUser.UserID;
            if (Application.Save())
            {
                return Application;
            }
            else
                return null;

        }
        
        private void _DefaultSetting()
        {
            lblApplicationDate.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblFees.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";
            
        }
        
        private void _LoadApplicationInfo()
        {
            _License = new clsInternationalLicense();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = clsApplicationType.Find(6).Fees.ToString();
            lblLocalLicenseID.Text=ctrlLicenseWithFilter1.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            if (obj == -1)
            {
                _DefaultSetting();
                btnSave.Enabled = false;

                return;
            }
               _LoadApplicationInfo();
            if (clsLicense.Find(obj,3).IsActive==0) 
            {
                MessageBox.Show("Sorry is License is not active choose another one","Expired",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                btnSave.Enabled = false;
               
            }
            else
                btnSave.Enabled = true;

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsApplication Application = _LoadNewBasicApplication();
            if (Application == null)
                return;
            _License.ApplicationID = (int)Application.ApplicationID;
            _License.DriverID = clsDriver.FindByPersonID(Application.ApplicationPersonID).DriverID;
            _License.IssuedUsingLocalLicenseID = ctrlLicenseWithFilter1.LicenseID;
            _License.IssueDate = DateTime.Now;
            _License.ExpirationDate = DateTime.Now.AddYears(1);
            _License.CreatedByUserID = Application.CreatedBy;
            _License.IsActive = 1;
            if (_License.Save())
            {
                MessageBox.Show($"Data saved Successfully your licenseID is  :{_License.InternationalLicenseID}","Success",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                btnSave.Enabled=false;
            }
            else
                MessageBox.Show("Data saved Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);





        }
    }
}
