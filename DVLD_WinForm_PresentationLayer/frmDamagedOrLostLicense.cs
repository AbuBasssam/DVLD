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
using static DVlD_BusinessLayer.clsLicense;

namespace DVLD
{
    public partial class frmDamagedOrLostLicense : Form
    {
        private clsLicense _Oldlicense;
        int _NewLicenseID=-1;

        private clsApplication _LoadNewBasicApplication()
        {
            clsApplication Application = new clsApplication();
            Application.ApplicationPersonID = (int)clsLicense.Find(ctrlLicenseWithFilter1.LicenseID).DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (rbDamaged.Checked) ? 4:3;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            Application.LastStauteDate = DateTime.Now;
            Application.PaidFees = (clsApplicationType.Find(Application.ApplicationTypeID).Fees + clsLicenseClasses.Find(3).ClassFees);
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
            lblApplicationFees.Text =(rbDamaged.Checked)? clsApplicationType.Find(4).Fees.ToString(): clsApplicationType.Find(3).Fees.ToString();
            lblOldLicenseID.Text = ctrlLicenseWithFilter1.LicenseID.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }
        private void _DefaultSetting()
        {
            lblApplicationDate.Text = "[???]";
            lblApplicationFees.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblCreatedBy.Text = "[???]";
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

            //dont allow a replacement if is Active .
            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsActive!=1)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnReplace.Enabled = false;
                return;
            }

            btnReplace.Enabled = true;



           /* Another Way
            if (obj == -1)
            {
                _DefaultSetting();
                btnReplace.Enabled = false;

                return;
            }

            _Oldlicense = clsLicense.Find(obj);
            _LoadApplicationInfo();

            if (_Oldlicense != null)
            {

                if (clsLicense.Find(obj).IsActive == 0)
                {
                    MessageBox.Show("Sorry is License is not active choose another one", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnReplace.Enabled = false;
                    llShowLIcenseHistory.Enabled = true;

                    return;

                }
                else
                {
                    llShowLIcenseHistory.Enabled = true;
                    btnReplace.Enabled = true;
                }
            }*/
        }

        private enIssueReason _GetIssueReason()
        {
            //this will decide which reason to issue a replacement for

            if (rbDamaged.Checked)

                return enIssueReason.ReplacementForDamaged;
            else
                return enIssueReason.ReplacementForLost;
        }

        public frmDamagedOrLostLicense()
        {
            InitializeComponent();
           
        }
        private int _GetApplicationTypeID()
        {
            //this will decide which application type to use accirding 
            // to user selection.

            if (rbDamaged.Checked)

                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }
        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(_NewLicenseID==null||_NewLicenseID==-1) { return; }  

            frmShowLicenseInfo frm = new frmShowLicenseInfo((short)_NewLicenseID);
            frm.ShowDialog();
        }
        
        private void llShowLIcenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory History = new frmLicenseHistory(_Oldlicense.DriverID);
            History.ShowDialog();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense =
               ctrlLicenseWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(),
               (int)clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;

            lblIReplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnReplace.Enabled = false;
            gpReplacementFor.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;
            llShowNewLicenseInfo.Enabled = true;


            /* Another Way
            clsApplication Application = _LoadNewBasicApplication();
            if (Application == null)
                return;

            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = _Oldlicense.DriverID;
            NewLicense.LicenseClass = _Oldlicense.LicenseClass;
            NewLicense.IssueDate = _Oldlicense.IssueDate;
            NewLicense.ExpirationDate = _Oldlicense.ExpirationDate;
            NewLicense.PaidFees = clsLicenseClasses.Find(3).ClassFees;
            NewLicense.IsActive = 1;
            NewLicense.IssueReason = (rbDamaged.Checked) ? clsLicense.enIssueReason.ReplacementForDamaged: clsLicense.enIssueReason.ReplacementForLost;
            NewLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            
            if (NewLicense.Save())
            {
                MessageBox.Show($"Data saved Successfully your licenseID is  :{NewLicense.LicenseID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                _Oldlicense.IsActive = 0;
                _Oldlicense.Save();
                btnReplace.Enabled = false;
                lblApplicationID.Text = Application.ApplicationID.ToString();
                lblIReplacedLicenseID.Text = NewLicense.LicenseID.ToString();
                ctrlLicenseWithFilter1.FilterBy(_Oldlicense.LicenseID);
                NewLicenseID = NewLicense.LicenseID;
                llShowNewLicenseInfo.Enabled = true;

            }
            else
                MessageBox.Show("Data saved Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);*/

        }

        private void RadioButtonChecked_CheckedChanged(object sender, EventArgs e)
        {
            if ((RadioButton)sender == rbDamaged)
            {
                lblApplicationFees.Text = (rbDamaged.Checked) ? clsApplicationType.Find(4).Fees.ToString() : clsApplicationType.Find(3).Fees.ToString();

                ;
                this.Text = "Replacement for Damaged License";
                lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
            }
            else
            {
                this.Text = "Replacement for Lost License";

                lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).Fees.ToString();
            }
        }

        private void frmDamagedOrLostLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
            rbLost.Checked = true;
            llShowLIcenseHistory.Enabled = false;
            llShowNewLicenseInfo.Enabled = false;

        }
    }
}
