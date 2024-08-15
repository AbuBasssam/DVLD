using DVLD.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmNewLocalDrivingLicenseApplication());
           // Application.Run(new frmLocalDrivingLicenseApplication());
            //Application.Run(new frmTest());
            //Application.Run(new frmDamagedOrLostLicense());
           // Application.Run(new frmRealseLicense());
           // Application.Run(new frmLicenseHistory(9));
            Application.Run(new frmLogin());









        }
    }
}
