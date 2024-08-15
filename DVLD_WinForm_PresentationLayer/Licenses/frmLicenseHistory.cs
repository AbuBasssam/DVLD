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
    public partial class frmLicenseHistory : Form
    {
        int DriverID;
        short PersonID=0;
        
        clsDriver Driver;
        public frmLicenseHistory(int DriverID)
        {
            InitializeComponent();
            this.DriverID = DriverID;
        }
        public frmLicenseHistory(short PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            if (PersonID == 0)
            {
                Driver = clsDriver.FindByDriverID(DriverID);
                ctrPersonCardWithFilter1.PersonsCard().LoadPersonInfo((int)Driver.PersonID);
                ctrPersonCardWithFilter1.FilterBy((int)Driver.PersonID);
                dataGridView1.DataSource = clsLicense.GetAllDriverLicenses(DriverID);
                dataGridView2.DataSource = clsInternationalLicense.GetAllDriverInternationalLicenses(DriverID);

            }
            else
            {
                Driver = clsDriver.FindByPersonID(PersonID);
                ctrPersonCardWithFilter1.FilterBy(PersonID);
                ctrPersonCardWithFilter1.PersonsCard().LoadPersonInfo(PersonID);
                if (Driver != null)
                {
                    dataGridView1.DataSource = clsLicense.GetAllDriverLicenses(Driver.DriverID);
                    dataGridView2.DataSource = clsInternationalLicense.GetAllDriverInternationalLicenses(DriverID);
                }

            }
        }
        private void showLocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            short LicenseID = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            frmShowLicenseInfo frm=new frmShowLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseInfo frm =new frmInternationalLicenseInfo(DriverID);
            frm.ShowDialog();
        }
    }
}
