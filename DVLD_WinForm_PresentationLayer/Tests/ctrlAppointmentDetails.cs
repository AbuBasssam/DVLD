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
    public partial class ctrlAppointmentDetails : UserControl
    {
       private clsLocalDrivingLicenseApplication _LicenseApplication;
      //  private int _LicenseApplicationID = -1;
        private clsApplication _Application;

        public ctrlAppointmentDetails()
        {
            InitializeComponent();
            
        }
        
        public void PassedTest(int PassedTest)
        {
            lblPassedTest.Text = PassedTest.ToString()+"/3";
        }

        public void ClassName(string ClassName)
        {
            lblAppliedForLicense.Text = ClassName;
        }

        private void _RestartApplicationInfo()
        {
            lblID.Text = "[???]";
            lblStatus.Text = "[???]";
            lblFees.Text= "[???]";
            lblType.Text = "[???]";
            lblApplicant.Text = "[???]";
            lblDate.Text= "[???]";
            lblStatusDate.Text= "[???]";
            lblCreatedBy.Text= "[???]";

        }
       
        private void _FillApplicationInfo()
        {
            lblID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;  
            lblFees.Text =_Application.ApplicationType.Fees.ToString();
            lblType.Text = _Application.ApplicationType.Title.ToString(); ;
            lblApplicant.Text = _Application.Person.FullName();
            lblDate.Text = _Application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _Application.LastStauteDate.ToShortDateString();
            lblCreatedBy.Text = clsUser.FindByUserID(_Application.CreatedBy).UserName;

        }

        private void _RestartLicenseApplicationInfo()
        {
            lblDLAppID.Text = "[???]";
            lblAppliedForLicense.Text = "[???]";
            lblPassedTest.Text = "[???]";
           
        }
        
        private void _FillLicenseApplicationInfo()
        {
           lblDLAppID.Text=_LicenseApplication.LocalDrivingLicenseApplicationID.ToString();
           llShowLicenseInfo.Enabled= clsLicense.IsLicenseExist(_LicenseApplication.ApplicationID);
           lblAppliedForLicense.Text = _LicenseApplication.License.ClassName;
           lblPassedTest.Text = _LicenseApplication.GetPassedTestCount().ToString() + "/3";


        }

        public int LicenseApplicationID()
        {
            return _LicenseApplication.LocalDrivingLicenseApplicationID;
        }
        
        public int ApplicationID()
        {
            return (int)_Application.ApplicationID;
        }

        public void LoadApplicationDetalis(int ApplicationID)
        {
            _Application = clsApplication.Find(ApplicationID);
            if (_Application == null)
            {
                _RestartApplicationInfo();
                MessageBox.Show($"No Application with ApplicationID:  {ApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
               _FillApplicationInfo();
        }

        public void LoadLicenseApplicationDetalis(int LicenseApplicationID)
        {
            _LicenseApplication = clsLocalDrivingLicenseApplication.Find(LicenseApplicationID);
            if (_LicenseApplication == null)
            {
                _RestartLicenseApplicationInfo();
                MessageBox.Show($"No LicenseApplication  with LicenseApplicationID:  {LicenseApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _FillLicenseApplicationInfo();
        }

        public clsApplication SelectedApplication
       {
            get { return _Application;}
       }
       
        public clsLocalDrivingLicenseApplication SelectedLicenseApplication
       {
            get { return _LicenseApplication; }
       }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(_Application.ApplicationPersonID);
            frm.ShowDialog();
           
            //Refresh
            LoadApplicationDetalis((int)_Application.ApplicationID);
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo licenseInfo=new frmShowLicenseInfo((decimal)_Application.ApplicationID);
            licenseInfo.ShowDialog();
        }
        public void EnableLink()
        {
            llShowLicenseInfo.Enabled = true;
        }
    }
}
