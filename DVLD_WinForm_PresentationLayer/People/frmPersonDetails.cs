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
    public partial class frmPersonDetails : Form
    {
        int ID;
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            ucPersonDetails1.LoadPersonInfo(PersonID);
            ID = PersonID;
        }
        public frmPersonDetails(string NationalNO)
        {
            InitializeComponent();
            ucPersonDetails1.LoadPersonInfo(NationalNO);
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson(ucPersonDetails1.PersonID()); ;
            frm.ShowDialog();
            ucPersonDetails1.LoadPersonInfo(ID);





        }
        
    }
}
