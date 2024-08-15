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
    public partial class frmUserManagement : Form
    {
        public  void AddUser()
        {
            frmAddNewUser newUser = new frmAddNewUser();
            newUser.ShowDialog();
        }
       
        int RowIndex = 0;
        DataTable dt = clsUser.GetAllUsers();
        void _Refresh()
        {
            dt = clsUser.GetAllUsers();
            dataGridView1.DataSource = dt;
        }
        public frmUserManagement()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddNewUser newUser = new frmAddNewUser();
            newUser.ShowDialog();
            _Refresh();
        }

        private void AddUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUser newUser = new frmAddNewUser();
            newUser.ShowDialog();
            _Refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Check if the row index is valid
            {
                dataGridView1.ClearSelection();

                dataGridView1.Rows[e.RowIndex].Selected = true;
                RowIndex = e.RowIndex;
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.SelectedIndex == 1 || cmbFilterBy.SelectedIndex == 2)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else { e.Handled = false; }
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            _Refresh();
            dataGridView1.Rows[RowIndex].Selected = true;
            cmbFilterBy.SelectedIndex = 0;



        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails((int)dataGridView1.Rows[RowIndex].Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dataGridView1.Rows[RowIndex].Cells[0].Value);
            frm.ShowDialog();


        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewUser frm = new frmAddNewUser((int)dataGridView1.Rows[RowIndex].Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsUser.DeleteUser((int)dataGridView1.Rows[RowIndex].Cells[0].Value);

        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible = false;
                    cmbIsActive.Visible = false;
                    break;
                case 5:
                    txtFilterBy.Visible = false;
                    cmbIsActive.Visible = true;
                    cmbIsActive.SelectedIndex = 2;
                    break;
                default:
                    txtFilterBy.Visible = true;
                    cmbIsActive.Visible = false;
                    break;



            }

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            DataView Filter = new DataView(dt);

            if (txtFilterBy.Text == "")
            {
                _Refresh();
                return;
            }


            switch (cmbFilterBy.SelectedIndex)
            {
                case 1:
                    Filter.RowFilter = "UserID='" + txtFilterBy.Text.ToString() + "'";
                    dataGridView1.DataSource = Filter;

                    break;

                case 2:
                    Filter.RowFilter = "PersonID='" + txtFilterBy.Text.ToString() + "'"; ;
                    dataGridView1.DataSource = Filter;
                    break;
                case 3:
                    Filter.RowFilter = "UserName LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                case 4:
                    Filter.RowFilter = "FullName LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;


            }
        }

        private void cmbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView Filter = new DataView(dt);

            switch (cmbIsActive.SelectedIndex)
            {

                case 0:


                    Filter.RowFilter = $"IsActive = '{false}'";
                    dataGridView1.DataSource = Filter;
                    break;
                case 1:
                    Filter.RowFilter = $"IsActive = '{true}'";
                    dataGridView1.DataSource = Filter;
                    break;
                
                case 2:

                    dataGridView1.DataSource = dt;
                    break;
                   
            }
        }
    
    
    
    
    
    }
}


            
    
