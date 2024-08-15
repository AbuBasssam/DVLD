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
    public partial class frmInternationalDrivingLicense : Form
    {
        DataTable dt;

        int RowIndex = 0;
        private void frmInternationalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {

             dt = clsInternationalLicense.GetAllInternationalLicenses();
            dataGridView1.DataSource = dt;
            dataGridView1.Rows[RowIndex].Selected = true;
            cmbFilterBy.SelectedIndex = 0;
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Int.License ID";
                dataGridView1.Columns[0].Width = 160;

                dataGridView1.Columns[1].HeaderText = "Application ID";
                dataGridView1.Columns[1].Width = 150;

                dataGridView1.Columns[2].HeaderText = "Driver ID";
                dataGridView1.Columns[2].Width = 130;

                dataGridView1.Columns[3].HeaderText = "L.License ID";
                dataGridView1.Columns[3].Width = 130;

                dataGridView1.Columns[4].HeaderText = "Issue Date";
                dataGridView1.Columns[4].Width = 180;

                dataGridView1.Columns[5].HeaderText = "Expiration Date";
                dataGridView1.Columns[5].Width = 180;

                dataGridView1.Columns[6].HeaderText = "Is Active";
                dataGridView1.Columns[6].Width = 120;

            }

        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "Is Active")
            {
                txtFilterBy.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else

            {

                txtFilterBy.Visible = (cmbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cmbFilterBy.Text == "None")
                {
                    txtFilterBy.Enabled = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtFilterBy.Enabled = true;

                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }
       
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cmbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    };

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                dt.DefaultView.RowFilter = "";
                return;
            }



            dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
            frmInternationalDrivingLicenseApplication_Load(null, null);

        }

        public frmInternationalDrivingLicense()
        {
            InitializeComponent();
        }

        private void tsmshowLicenseDetails_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo License=new frmInternationalLicenseInfo((int)dataGridView1.CurrentRow.Cells[2].Value);
            License.ShowDialog();
        }

        private void tsmShowPersonHistory_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory((int)dataGridView1.CurrentRow.Cells[2].Value);
            frm.ShowDialog();
        }

        private void tsmshowPersonDetails_Click(object sender, EventArgs e)
        {
            int PersonID =(int) clsDriver.FindByDriverID((int)dataGridView1.CurrentRow.Cells[2].Value).PersonID;
            frmPersonDetails Person =  new frmPersonDetails(PersonID);
            Person.ShowDialog();
        }
    }
}
