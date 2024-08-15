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


    public partial class frmRealseLicense : Form
    {
        private int _SelectedLicenseID = -1;
        clsDetainedLicense _DetainedLicense;
        private clsApplication _LoadNewBasicApplication()
        {
            clsApplication Application = new clsApplication();
            Application.ApplicationPersonID = (int)clsLicense.Find(ctrlLicenseWithFilter1.LicenseID).DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = 5;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            Application.LastStauteDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(5).Fees + (int)_DetainedLicense.FineFees;
            Application.CreatedBy = (int)clsGlobal.CurrentUser.UserID;
            if (Application.Save())
            {
                return Application;
            }
            else
                return null;

        }

        private void _LoadApplicationInfo()
        {
            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
            lblApplicationFees.Text = clsApplicationType.Find(5).Fees.ToString();

            lblTotalFees.Text = (_DetainedLicense.FineFees + clsApplicationType.Find(5).Fees).ToString();
            lblLicenseID.Text = _DetainedLicense.LicenseID.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            lblFineFees.Text = _DetainedLicense.FineFees.ToString();


        }

        private void _DefaultSetting()
        {
            lblDetainID.Text = "[???]";
            lblDetainDate.Text = "[???]";
            lblApplicationFees.Text = "[???]";
            lblTotalFees.Text = "[???]";
            lblLicenseID.Text = "[???]";
            lblCreatedBy.Text = "[???]";
            lblFineFees.Text = "[???]";
            lblApplicationID.Text = "[???]";
        }

        private void ctrlLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            llShowLIcenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)

            {
                return;
            }

            //ToDo: make sure the license is not detained already.
            if (!ctrlLicenseWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                            _DetainedLicense = clsDetainedLicense.FindByLicenseID(obj);


            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;

            lblDetainID.Text = clsDetainedLicense.FindByLicenseID( ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseID).DetainID.ToString();
            lblLicenseID.Text = ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseID.ToString();

            lblCreatedBy.Text =_DetainedLicense.CreatedByUserInfo.UserName;
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;



            /*Another Way
             if (obj == -1)
            {
                _DefaultSetting();
                btnRelease.Enabled = false;

                return;
            }


            if (!clsDetainedLicense.IsLicneseDetained(obj))
            {
                MessageBox.Show("Selected License is not Detained", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;

            }

            else
            {
                _DetainedLicense = clsDetainedLicense.FindDetainedLicense(obj);
                _LoadApplicationInfo();

                btnRelease.Enabled = true;
            }*/

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((short)_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this detained  license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;


            bool IsReleased = ctrlLicenseWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense((int)clsGlobal.CurrentUser.UserID, ref ApplicationID); ;

            lblApplicationID.Text = ApplicationID.ToString();

            if (!IsReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;




            /*Another Way
             clsApplication Application = _LoadNewBasicApplication();
            if (Application == null)
                return;

            _DetainedLicense.IsReleased = 1;
            _DetainedLicense.ReleaseDate = DateTime.Now;
            _DetainedLicense.ReleasedByUserID = clsGlobal.CurrentUser.UserID;
            _DetainedLicense.ReleaseApplicationID =Application.ApplicationID;

            if (_DetainedLicense.Save())
            {
                MessageBox.Show("Data saved Successfully ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ctrlLicenseWithFilter1.FilterBy(_DetainedLicense.LicenseID);
                clsLicense _Oldlicense = clsLicense.Find((short)_DetainedLicense.LicenseID);
                _Oldlicense.IsActive = 1;
                _Oldlicense.Save();
                btnRelease.Enabled = false;
                lblApplicationID.Text = Application.ApplicationID.ToString();
            }
            else
                MessageBox.Show("Data saved Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
*/


        }
        public frmRealseLicense()
        {
            InitializeComponent();
        }
        public frmRealseLicense(int License)
        {
            InitializeComponent();
            _SelectedLicenseID = License;

            ctrlLicenseWithFilter1.LoadLicenseInfo(_SelectedLicenseID);
            ctrlLicenseWithFilter1.FilterEnabled = false;

            /*_DetainedLicense = clsDetainedLicense.FindDetainedLicense(License);
            ctrlLicenseWithFilter1.CardInfo.LoadLicenseInfo(_DetainedLicense.LicenseID);
            _LoadApplicationInfo();
            ctrlLicenseWithFilter1.FilterBy(License);

            btnRelease.Enabled = true;*/
        }

        private void llShowLIcenseHistory_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory History = new frmLicenseHistory(clsLicense.Find(_SelectedLicenseID).DriverID);
            History.ShowDialog();
        }

        private void frmRealseLicense_Load(object sender, EventArgs e)
        {

        }
    }
        
    
}
