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
    public partial class frmDeletePerson : Form
    {
        int id;
        public frmDeletePerson(int PersonID)
        {
            InitializeComponent();
            ucPersonDetails1.LoadPersonInfo(PersonID);
            id = PersonID;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are your Sure You Want to Delete", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.DeletePerson(id))
                {
                    MessageBox.Show("Delete successfully","Done", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    this.Close();
                }
                else MessageBox.Show("Delete Falied", "Falied", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close ();
        }
    }
}
