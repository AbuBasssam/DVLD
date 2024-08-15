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
    public partial class frmDetainLicense : Form
    {
        private int _DetainID = -1;
        private int _SelectedLicenseID = -1;
        private int LicenseID = -1;

        private void _LoadApplicationInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblILicenseID.Text = _SelectedLicenseID.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;


        }
        private void _DefaultSetting()
        {
            lblApplicationDate.Text = "[???]";
            lblCreatedBy.Text = "[???]";
            lblILicenseID.Text= "[???]";



        }
       
        private void ctrlLicenseWithFilter1_OnLicenseSelected(int obj)
        {

            _SelectedLicenseID = obj;

            lblILicenseID.Text = _SelectedLicenseID.ToString();

            llShowLIcenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)

            {
                return;
            }

            //ToDo: make sure the license is not detained already.
            if (ctrlLicenseWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;


            /* Another Way
             if (obj == -1)
             {
                 _DefaultSetting();
                 btnDetain.Enabled = false;

                 return;
             }

             //  LicenseID = obj;
             _LoadApplicationInfo();

             if (clsDetainedLicense.IsLicneseDetained(obj))
             {
                 MessageBox.Show("Sorry this License is Already Detained", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 btnDetain.Enabled = false;
                 return;

             }

             if (clsLicense.Find((short)obj).IsActive == 0)
             {
                 MessageBox.Show("Sorry is License is not active choose another one", "Expired", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 btnDetain.Enabled = false;
                 return;
             }

             else
                 btnDetain.Enabled = true;*/


        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo((short)_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(" Fine fees is required", "warming", MessageBoxButtons.YesNo, MessageBoxIcon.Error) ;
                return;

            }
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            _DetainID = ctrlLicenseWithFilter1.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text), (int)clsGlobal.CurrentUser.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ctrlLicenseWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            llShowLicenseInfo.Enabled = true;

           /*Another Way
            clsDetainedLicense NewLicense = new clsDetainedLicense();
            NewLicense.LicenseID = LicenseID;
            NewLicense.DetainDate = DateTime.Now;
            NewLicense.FineFees = Convert.ToInt32(txtFineFees.Text);
            NewLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            NewLicense.IsReleased =0;
            
            if (NewLicense.Save())
            {
                MessageBox.Show($"Data saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                clsLicense license = clsLicense.Find((short)LicenseID);
                license.IsActive = 0;

                license.Save();
                btnDetain.Enabled = false;
                lblDetainID.Text = NewLicense.DetainID.ToString();
                ctrlLicenseWithFilter1.FilterBy(LicenseID);
                
            }
            else
                MessageBox.Show("Data saved Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
        }
        
        public frmDetainLicense()
        {
        
            InitializeComponent();
        
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
        }

        private void llShowLIcenseHistory_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseHistory frm =
              new frmLicenseHistory((short)ctrlLicenseWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            };


            if (!clsValidatoin.IsNumber(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }
    }
}
