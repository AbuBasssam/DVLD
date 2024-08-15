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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD
{
    public partial class frmRenewLicenseApplicatoin : Form
    {
        private clsLicense _Oldlicense;
        int _NewLicenseID=-1;
        public frmRenewLicenseApplicatoin()
        {
            InitializeComponent();
        }

        private clsApplication _LoadNewBasicApplication()
        {
            clsApplication Application = new clsApplication();
            Application.ApplicationPersonID = (int)clsLicense.Find(ctrlLicenseWithFilter1.LicenseID).DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = 2;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            Application.LastStauteDate = DateTime.Now;
            Application.PaidFees = (clsApplicationType.Find(2).Fees + clsLicenseClasses.Find(_Oldlicense.LicenseClass).ClassFees);
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
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblOldLicenseID.Text = ctrlLicenseWithFilter1.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblLicenseFees.Text=clsLicenseClasses.Find(_Oldlicense.LicenseClass).ClassFees.ToString();
            lblApplicationFees.Text=clsApplicationType.Find(2).Fees.ToString();
            lblTotalFees.Text= (clsApplicationType.Find(2).Fees+ clsLicenseClasses.Find(3).ClassFees).ToString();
            txtNotes.Text = "";
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;


        }
        private void _DefaultSetting()
        {
            lblApplicationDate.Text = "[???]";
            lblIssueDate.Text = "[???]";
            lblApplicationFees.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblExpirationDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";
            lblLicenseFees.Text= "[???]";
            lblTotalFees.Text = "[???]";
            txtNotes.Text = "";
        }
        private void ctrlLicenseWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();

           llShowLIcenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)

            {
                return;
            }

            int DefaultValidityLength = ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassesInfo.DefalutValidityLength;
            lblExpirationDate.Text = DateTime.Now.AddYears(DefaultValidityLength).ToShortDateString();
            lblLicenseFees.Text = ctrlLicenseWithFilter1.SelectedLicenseInfo.LicenseClassesInfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlLicenseWithFilter1.SelectedLicenseInfo.Notes;


            //check the license is not Expired.
            if (!ctrlLicenseWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " + ctrlLicenseWithFilter1.SelectedLicenseInfo.ExpirationDate.ToShortDateString()
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                btnRenew.Enabled = false;
                return;
            }

            //check the license is not Expired.
            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsActive!=1)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenew.Enabled = false;
                return;
            }

            btnRenew.Enabled = true;

            /* Another Way
            if (obj == -1)
            {
                _DefaultSetting();
                btnRenew.Enabled = false;

                return;
            }

            _Oldlicense = clsLicense.Find(obj);
            _LoadApplicationInfo();

            if (_Oldlicense != null)
            {
                if (_Oldlicense.ExpirationDate > DateTime.Today.AddDays(-1))
                {
                    MessageBox.Show("this License is Still active","Active",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRenew.Enabled = false;
                    llShowLIcenseHistory.Enabled = true;
                    return ;
                }
                if (clsLicense.Find(obj).IsActive == 0)
                {
                    MessageBox.Show("Sorry is License is not active choose another one", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnRenew.Enabled = false;
                    llShowLIcenseHistory.Enabled = true;
                    return;

                }
                else
                {
                    llShowLIcenseHistory.Enabled = true;
                    btnRenew.Enabled = true;
                }
            }*/

        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_NewLicenseID == null|| _NewLicenseID ==-1)
                return;
           frmShowLicenseInfo frm=new  frmShowLicenseInfo((short)_NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLIcenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory History = new frmLicenseHistory(_Oldlicense.DriverID);
            History.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense =
                ctrlLicenseWithFilter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(),
                (int)clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            label500.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblIRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenew.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;
            llShowNewLicenseInfo.Enabled = true;


           /* Another Way
             clsApplication Application = _LoadNewBasicApplication();
            if (Application == null)
                return;

            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID =_Oldlicense.DriverID;
            NewLicense.LicenseClass = _Oldlicense.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClasses.Find(_Oldlicense.LicenseClass).DefalutValidityLength);
            NewLicense.Notes = txtNotes.Text.Trim();
            NewLicense.PaidFees =clsLicenseClasses.Find(_Oldlicense.LicenseClass).ClassFees;
            NewLicense.IsActive = 1;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            
            if (NewLicense.Save())
            {
                MessageBox.Show($"Data saved Successfully your licenseID is  :{NewLicense.LicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                _Oldlicense.IsActive = 0;
                _Oldlicense.Save();
                btnRenew.Enabled = false;
               lblRenwApplicationID.Text= Application.ApplicationID.ToString();
                lblIRenewedLicenseID.Text=NewLicense.LicenseID.ToString();
                _NewLicenseID = NewLicense.LicenseID;
            }
            else
                MessageBox.Show("Data saved Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);*/

        }

        private void frmRenewLicenseApplicatoin_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text =DateTime.Now.ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;

            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            llShowLIcenseHistory.Enabled = false;
        }
    }
}
