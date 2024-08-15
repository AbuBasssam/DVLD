using DVLD.Properties;
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
    public partial class ctrlInternationalLicenseCard : UserControl
    {
        private clsInternationalLicense _License;
        private void _FillLicenseInfo()
        {
            clsDriver Driver = clsDriver.FindByDriverID(_License.DriverID);
            lblApplicationID.Text=_License.ApplicationID.ToString();
            lblInternationalLicenseID.Text = _License.InternationalLicenseID.ToString();
            lblName.Text = Driver.FullName();
            lblLicenseID.Text = _License.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = Driver.NationalNo;
            lblGender.Text = (Driver.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIsActive.Text = (_License.IsActive == 1) ? "Yes" : "No";
            lblDateOfBirth.Text = Driver.DateOfBirth.ToShortDateString();
            lblDriverID.Text = Driver.DriverID.ToString();
            lblExpiratoinDate.Text = _License.ExpirationDate.ToShortDateString();
            pbImage.Image = (Driver.ImagePath != "") ? Image.FromFile(Driver.ImagePath) : Resources.user;
        }
        public void LoadLicenseInfoByDriverID(int DriverID)
        {
            _License = clsInternationalLicense.FindByDriverID(DriverID);

            if (_License == null)
            {
                MessageBox.Show($"No License to this DriverID:  {DriverID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RestartLicenseInfo();
                return;

            }

            _FillLicenseInfo();




        }
        public void RestartLicenseInfo()
        {
            lblApplicationID.Text= "[???]";
            lblInternationalLicenseID.Text= "[???]";
            lblName.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGender.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblExpiratoinDate.Text = "[???]";
            pbImage.Image = Resources.user;
        }

        public clsInternationalLicense SelectedLicense
        {
            get { return _License; }
        }
        public ctrlInternationalLicenseCard()
        {
            InitializeComponent();
        }
    }
}
