using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD.ScreenLoader;

namespace DVLD
{
    public partial class ctrlLIsts : UserControl
    {
        
        public enum enScreens {enListPopel=1,enListUsers,enListLocalDrivingApplication,enListDrivers}
        enScreens Screen;
        ScreenLoader _ScreenLoader;
        public ctrlLIsts()
        {
            InitializeComponent();
        }
        public void LoadSecreen(ButtonAction btnAddAction,LoadDataSet DataSet, FilteringAction Filtering, ButtonAction btnRelaseAction)
        {
            _ScreenLoader = new ScreenLoader(btnAddAction, DataSet, Filtering, btnRelaseAction);        




        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            _ScreenLoader.btnAddClick();
        }                                                                           

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
