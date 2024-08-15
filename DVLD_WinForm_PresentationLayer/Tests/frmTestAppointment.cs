
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Properties;
using DVlD_BusinessLayer;
using static DVlD_BusinessLayer.clsTestTypes;

namespace DVLD
{
    public partial class frmTestAppointment : Form
    {


        int _LocalDrivingLicenseApplicationID;
        clsTestTypes.enTestType  _TestType = clsTestTypes.enTestType.VisionTest;
        private void _OpenningSetting()
        {
            switch (_TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    pbImage.Image = Resources.vision;
                    lblTitle.Text = "Vision Test Appointment";
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    pbImage.Image = Resources.Written_Test;
                    lblTitle.Text = "Written Test Appointment";

                    break;
                case clsTestTypes.enTestType.StreetTest:
                    pbImage.Image = Resources.car_Test;
                    lblTitle.Text = "Street Test Appointment";

                    break;
            }
        }

        private void _Refresh()
        {
            DataTable  dtAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);

            dgvLicenseTestAppointments.DataSource = dtAppointments;

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType, TestAppointmentID);
            frm.ShowDialog();
            frmTestAppointment_Load(null, null);

        }

        private void dgvAllAppointment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if the row index is valid
            {
                dgvLicenseTestAppointments.ClearSelection();

                dgvLicenseTestAppointments.Rows[e.RowIndex].Selected = true;
                
            }

        }

        private void takeTesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestType);
            frm.ShowDialog();
            frmTestAppointment_Load(null, null);


        }

        public frmTestAppointment( int LicenseApplicationID,clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();

            this._LocalDrivingLicenseApplicationID = LicenseApplicationID;
            this._TestType = TestTypeID;

        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            _OpenningSetting();

            ctrlAppointmentDetails1.LoadApplicationDetalis(clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID). ApplicationID);
            ctrlAppointmentDetails1.LoadLicenseApplicationDetalis(_LocalDrivingLicenseApplicationID);
            
            
            _Refresh();

        }
        
        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);


            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            //---
            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmTestAppointment_Load(null, null);
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == 1)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest
                (LastTest.AppointmentInfo.LocalDrivingApplicationID, _TestType);
            frm2.ShowDialog();
            frmTestAppointment_Load(null, null);
            //---







        }
    }
}
