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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeopleMangment PeopleMangment= new frmPeopleMangment();
            PeopleMangment.ShowDialog();
        }

        private void crruentUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails CurrentUser= new frmUserDetails((int)clsGlobal.CurrentUser.UserID);
            CurrentUser.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           clsGlobal.CurrentUser = null;
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword CurrentUser = new frmChangePassword((int)clsGlobal.CurrentUser.UserID);
            CurrentUser.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserManagement Users= new frmUserManagement();  
            Users.ShowDialog();
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationtypes frm= new frmManageApplicationtypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm= new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplication frm = new frmLocalDrivingLicenseApplication();
            frm.ShowDialog();
        }

        private void localDrivingLicnseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();
            
        }

        private void internationalDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmInternationalDrivingLicense app = new frmInternationalDrivingLicense();
            app.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicenseApplicatoin applicatoin = new frmRenewLicenseApplicatoin();
            applicatoin.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDamagedOrLostLicense app=new frmDamagedOrLostLicense();
            app.ShowDialog();
        }

        private void RealseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRealseLicense app=new frmRealseLicense();
            app.ShowDialog();
        }

        private void driverToolStripMenuItem_Click(object sender, EventArgs e)
        {
          frmLIstDriver app=new frmLIstDriver();
            app.ShowDialog();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense app = new frmNewInternationalLicense();
            app.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetainLicense app=new frmDetainLicense();
            app.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRealseLicense app = new frmRealseLicense();
            app.ShowDialog();

        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListDetainLicense FRM =new frmListDetainLicense();
            FRM.ShowDialog();   
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.ShowDialog();

        }
    }
}
