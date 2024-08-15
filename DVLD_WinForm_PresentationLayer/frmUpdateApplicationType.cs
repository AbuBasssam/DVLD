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
    public partial class frmUpdateApplicationType : Form
    {

        private int _ApplicationTypeID;
        private clsApplicationType _ApplicatoinType;
        public frmUpdateApplicationType(int ApplicationID)
        {
            InitializeComponent();
            this._ApplicationTypeID = ApplicationID;
        }
        private void _LoadApplicationData()
        {
            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();

            _ApplicatoinType = clsApplicationType.Find(_ApplicationTypeID);
           if(_ApplicatoinType != null )
            {
                txtTitle.Text = _ApplicatoinType.Title;
                txtFees.Text= _ApplicatoinType.Fees.ToString();
           }
           else
           {
                MessageBox.Show($"No applicatoin with applicatoinID:  {_ApplicationTypeID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
            
        }
        
        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadApplicationData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _ApplicatoinType.Title = txtTitle.Text.Trim();
            _ApplicatoinType.Fees = Convert.ToInt32(txtFees.Text.Trim());


            if (_ApplicatoinType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            };
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);

            };


            if (!clsValidatoin.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            };

        }
    }
}
