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
    public partial class frmUserDetails : Form
    {
        
        int UserID=-1;
        public frmUserDetails(int UserID)
        {
            InitializeComponent();
            this.UserID= UserID;
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(UserID);

        }
    }
}
