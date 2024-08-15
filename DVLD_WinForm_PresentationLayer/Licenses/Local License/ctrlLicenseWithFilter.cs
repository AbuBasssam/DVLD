using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVlD_BusinessLayer;

namespace DVLD
{
    public partial class ctrlLicenseWithFilter : UserControl
    {
        clsLicense _License;
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }
        public ctrlLicenseWithFilter()
        {
            InitializeComponent();
        }
        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }
        
        private int _LicenseID = -1;
        
        private void LicenseSelected(object sender, int LicenseID)
        {
            ctrlLicenseCard1.LoadLicenseInfoByLicenseID(LicenseID);
            _License = clsLicense.Find(LicenseID);
        }

        public void LoadLicenseInfo(int LicenseID)
        {


            txtLicenseID.Text = LicenseID.ToString();
            ctrlLicenseCard1.LoadInfo(LicenseID);
            _LicenseID = ctrlLicenseCard1.LicenseID();
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);


        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
          
            //  _License = clsLicense.Find(Convert.ToInt32(txtLicenseID.Text), 3);
           
            /*Another Way
             if (_License != null)
            {

                ctrlLicenseCard1.LoadLicenseInfoByLicenseID(_License.LicenseID);


            }
            else
            {
                MessageBox.Show($"No Ordinary driving license with this LicenseID : {txtFindBy.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _License = new clsLicense();
                ctrlLicenseCard1.RestartLicenseInfo();
                //ctrlLicenseCard1.LoadLicenseInfoByLicenseID(Convert.ToInt32(txtFindBy.Text));


            }

            if (OnLicenseSelected != null && txtFindBy.Enabled )
            {

                OnLicenseSelected(_License.LicenseID);
            }*/
        }

        public void FilterBy(int LicenseID)
        {
            txtLicenseID.Text = LicenseID.ToString();
            gbFilters.Enabled = false;
        }

        public int LicenseID
        {
            get { return ctrlLicenseCard1.LicenseID(); }
        }
       
        public clsLicense SelectedLicenseInfo
        { get { return ctrlLicenseCard1.SelectedLicense; } }
        public ctrlLicenseWithFilter CardInfo
        { get { return this; } }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnSearch.PerformClick();
            }

        }

    }
}
