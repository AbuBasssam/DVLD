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
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLD
{
    public partial class ctrlLicenseCard : UserControl
    {
        private int _LicenseID;

        private clsLicense _License;
        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.Gender == 0)
                pbImage.Image = Resources.user;
            else
                pbImage.Image = Resources.Female_User;

            string ImagePath = _License.DriverInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private string _GetIssueReason(clsLicense.enIssueReason Reason)
        {
            switch (Reason)
            {
                case clsLicense.enIssueReason.FirstTime:
                    return "First time";
                
                case clsLicense.enIssueReason.Renew:
                    return "Renew";
                case clsLicense.enIssueReason.ReplacementForDamaged:
                    return "Replacement For Damaged";

                default:
                    return "Replacement For Lost";
                    

            }
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive==1 ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassesInfo.ClassName;
            lblName.Text = _License.DriverInfo.FullName();
            lblNationalNo.Text = _License.DriverInfo.NationalNo;
            lblGender.Text = _License.DriverInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = _License.DriverInfo.DateOfBirth.ToShortDateString();

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblExpiratoinDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNote.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();



        }

        private void _FillLicenseInfo()
        {
            clsDriver Driver = clsDriver.FindByDriverID(_License.DriverID);
            lblClass.Text = clsLicenseClasses.Find(_License.LicenseClass).ClassName;
            lblName.Text = Driver.FullName();
            lblLicenseID.Text = _License.LicenseID.ToString();
            lblNationalNo.Text = Driver.NationalNo;
            lblGender.Text = (Driver.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _GetIssueReason(_License.IssueReason);
            lblNote.Text = _License.Notes;
            lblIsActive.Text = (_License.IsActive == 1) ? "Yes" : "No";
            lblDateOfBirth.Text = Driver.DateOfBirth.ToShortDateString();
            lblDriverID.Text = Driver.DriverID.ToString();
            lblExpiratoinDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIsDetained.Text =clsDetainedLicense.IsLicenseDetained(_License.LicenseID)  ? "Yes" : "No";
            pbImage.Image = (Driver.ImagePath != "") ? Image.FromFile(Driver.ImagePath) : (Driver.Gender == 0) ? Resources.user : Resources.Female_User;
        }
        
        public void LoadLicenseInfoByDriverID(int DriverID)
        {
            _License = clsLicense.FindByDriverID(DriverID);

            if (_License == null)
            {
                MessageBox.Show($"No License to this DriverID:  {DriverID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RestartLicenseInfo();
                return;
               
            }

            _FillLicenseInfo();




        }
        
        public void LoadLicenseInfoByLicenseID(int LicenseID)
        {

            _License = clsLicense.Find(LicenseID);

            if (_License == null)
            {
                MessageBox.Show($"No License  with LicenseID:  {LicenseID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RestartLicenseInfo();
                return;

            }
            _FillLicenseInfo();
        }
        
        public void LoadLicenseInfoByApplicationID(decimal ApplicationID)
        {

            _License = clsLicense.FindByApplicationID(Convert.ToInt32(ApplicationID));

            if (_License == null)
            {
                MessageBox.Show($"No License  with LicenseID:  {ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RestartLicenseInfo();

                return;

            }
            _FillLicenseInfo();

        }

        public int LicenseID()
        {
            return _License.LicenseID;
        }

        public void RestartLicenseInfo()
        {

            lblClass.Text = "[???]";
            lblName.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblGender.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblIssueReason.Text = "[???]";
            lblNote.Text = "[???]";
            lblIsActive.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblDriverID.Text = "[???]";
            lblExpiratoinDate.Text = "[???]";
            lblIsDetained.Text = "[???]";
            pbImage.Image = Resources.user;
        }
                
        public clsLicense SelectedLicense
        {
            get { return _License; }
        }
       
        public ctrlLicenseCard()
        {
            InitializeComponent();
        }
    }
}
