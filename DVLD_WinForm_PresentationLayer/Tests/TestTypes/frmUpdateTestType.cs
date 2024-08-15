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
    public partial class frmUpdateTestType : Form
    {
        int TestTypeID;
        clsTestTypes _TestType;
        public frmUpdateTestType(int TestTypeID)
        {
            InitializeComponent();
            this.TestTypeID = TestTypeID;
        }
        private void _LoadTestData()
        {
            _TestType = clsTestTypes.Find((clsTestTypes.enTestType)TestTypeID);
            if (_TestType != null)
            {
                lblID.Text = TestTypeID.ToString();
                txtDescription.Text = _TestType.Description;
                txtTitle.Text = _TestType.Title;
                txtFees.Text = _TestType.Fees.ToString();
            }
            else
            {
                MessageBox.Show($"No Test with TestID:  {TestTypeID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

            _TestType.Title = txtTitle.Text.Trim();
            _TestType.Description = txtDescription.Text.Trim();
            _TestType.Fees = Convert.ToInt32(txtFees.Text.Trim());


            if (_TestType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _LoadTestData();
        }
    }
}
