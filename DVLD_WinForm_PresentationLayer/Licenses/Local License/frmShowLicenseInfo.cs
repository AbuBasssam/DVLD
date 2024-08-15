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
    public partial class frmShowLicenseInfo : Form
    {
        private int _LicenseID;
        public frmShowLicenseInfo(short LicenseID)
        {
            InitializeComponent();
            ctrlLicenseCard1.LoadInfo(LicenseID);
            this._LicenseID = LicenseID;

        }
        public frmShowLicenseInfo(int DriverID)
        {
            InitializeComponent();
            ctrlLicenseCard1.LoadLicenseInfoByDriverID(DriverID);
        }
        public frmShowLicenseInfo(decimal ApplicationID)
        {
            InitializeComponent();
            ctrlLicenseCard1.LoadLicenseInfoByApplicationID(ApplicationID);

        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            //ctrlLicenseCard1.LoadInfo(_LicenseID);

        }
    }
}
