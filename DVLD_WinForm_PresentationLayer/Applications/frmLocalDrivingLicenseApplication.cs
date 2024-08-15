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
    public partial class frmLocalDrivingLicenseApplication : Form
    {

        private DataTable _dtAllLocalDrivingLicenseApplications;
       
        private void _Refresh()
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllApplicatoins();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

        }

        private bool _AlreadyHaveLicense(int PersonID, int LicenseClass)
        {
            return clsLicense.AlreadyHaveLicense(PersonID, LicenseClass);
        }

        private void _cmsOpenningSetting()
        {
            int PassedTest = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
           
            string Staute = dgvLocalDrivingLicenseApplications.CurrentRow.Cells[6].Value.ToString();

            tsmEditApplication.Enabled = (Staute == "New");
            tsmDeleteApplication.Enabled = (Staute == "New");
            tsmCancelApplication.Enabled = (Staute == "New");
            tsmSechduleTest.Enabled = (Staute == "New");
            int PersonID = Convert.ToInt32(clsPerson.Find(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value.ToString()).PersonID);
            tsmissueDrivingLicenseFirstTime.Enabled = PassedTest == 3 && !_AlreadyHaveLicense 
                (
                PersonID,
                clsLicenseClasses.Find(Convert.ToString(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[1].Value)).LicenseClassesID) && Staute == "New";

            tsmShowLicense.Enabled = _AlreadyHaveLicense
                (
                PersonID,
                clsLicenseClasses.Find(Convert.ToString(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[1].Value)).LicenseClassesID
                );


        }

        private void frmLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {

            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllApplicatoins();
            dgvLocalDrivingLicenseApplications.DataSource = _dtAllLocalDrivingLicenseApplications;

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }
            cmbFilterBy.SelectedIndex = 0;

        }

        public frmLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.SelectedIndex == 0)
            {
                txtFilterBy.Visible = false;
            }
            else { txtFilterBy.Visible = true; }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            DataView Filter = new DataView(_dtAllLocalDrivingLicenseApplications);

            if (txtFilterBy.Text == "")
            {
                _Refresh();
                return;
            }


            switch (cmbFilterBy.SelectedIndex)
            {
                case 1:
                    Filter.RowFilter = "LocalDrivingLicenseApplicationID='" + txtFilterBy.Text.ToString() + "'";
                    dgvLocalDrivingLicenseApplications.DataSource = Filter;
                    break;

                case 2:
                    Filter.RowFilter = "NationalNO LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dgvLocalDrivingLicenseApplications.DataSource = Filter;
                    break;
                case 3:
                    Filter.RowFilter = "FullName LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dgvLocalDrivingLicenseApplications.DataSource = Filter;
                    break;
                case 4:
                    Filter.RowFilter = "Stauts LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dgvLocalDrivingLicenseApplications.DataSource = Filter;
                    break;
               

            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.SelectedIndex == 1 )
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else { e.Handled = false; }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
            _Refresh();


        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if the row index is valid
            {
                dgvLocalDrivingLicenseApplications.ClearSelection();

                dgvLocalDrivingLicenseApplications.Rows[e.RowIndex].Selected = true;
            }

        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmApplicationDetails frm = new frmApplicationDetails((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void sechduleTestToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            int value = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;
            switch (value) {
                case 0:
                    tsmVisionTest.Enabled = true;
                    tsmWrittenTest.Enabled = false;
                    tsmStreetTest.Enabled = false;
                    break;
                
                case 1:
                    tsmVisionTest.Enabled = false;
                    tsmWrittenTest.Enabled = true;
                    tsmStreetTest.Enabled = false;
                    break;
                
                case 2:
                    tsmVisionTest.Enabled = false;
                    tsmWrittenTest.Enabled = false;
                    tsmStreetTest.Enabled = true;
                    break;
                
                default:
                    tsmVisionTest.Enabled = false;
                    tsmWrittenTest.Enabled = false;
                    tsmStreetTest.Enabled = false;
                    break;  
            
            }

        }

        private void tsmVisionTest_Click(object sender, EventArgs e)
        {
            int localDrivingAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmTestAppointment frm=new frmTestAppointment(localDrivingAppID,clsTestTypes.enTestType.VisionTest);
            frm.ShowDialog();
            _Refresh();
        }

        private void tsmWrittenTest_Click(object sender, EventArgs e)
        {
            int localDrivingAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmTestAppointment frm = new frmTestAppointment(localDrivingAppID, clsTestTypes.enTestType.WrittenTest);
            frm.ShowDialog();
            _Refresh();

        }

        private void tsmStreetTest_Click(object sender, EventArgs e)
        {
            int localDrivingAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmTestAppointment frm = new frmTestAppointment(localDrivingAppID, clsTestTypes.enTestType.StreetTest);
            frm.ShowDialog();
            _Refresh();

           /* PassedTest = (int)dataGridView1.CurrentRow.Cells[5].Value;
            if (PassedTest == 3)
            {
                clsLocalDrivingLicenseApplication App = clsLocalDrivingLicenseApplication.Find((int)dataGridView1.Rows[RowIndex].Cells[0].Value);
                clsApplication application = clsApplication.Find(App.ApplicationID);
                application.ApplicationStaute = (clsApplication.enStaute)3;
                if (application.Save())
                    _Refresh();
            }*/



        }

        private void tsmEditApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication application= new frmNewLocalDrivingLicenseApplication((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            application.ShowDialog();
            _Refresh();
        }

        private void tsmDeleteApplication_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are Sure You Want Delete this Application","Delete",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalLicenseApp((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                    MessageBox.Show("this is data connected with it", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            _Refresh();

        }

        private void tsmCancelApplication_Click(object sender, EventArgs e)
        {
            clsApplication App=clsApplication.Find((clsLocalDrivingLicenseApplication.Find((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).ApplicationID));
            
            if(App.Cancel())
                MessageBox.Show("Data saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            else
                MessageBox.Show("Data  not saved Successfully", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);


            _Refresh();
        }

        private void tsmissueDrivingLicenseFirstTime_Click(object sender, EventArgs e)
        {
            int localDrivingAppID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmIssueLicense frm = new frmIssueLicense(localDrivingAppID);
            frm.ShowDialog();
            _Refresh();
            
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo licenseInfo = new frmShowLicenseInfo((decimal)(clsLocalDrivingLicenseApplication.Find((int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value).ApplicationID));
            licenseInfo.ShowDialog();


        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(Convert.ToInt16(clsPerson.Find(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[2].Value.ToString()).PersonID));
            frm.ShowDialog();  
        }

        private void cmsLocalDrivingLicense_Opening(object sender, CancelEventArgs e)
        {
            _cmsOpenningSetting();
        }
    }
}
