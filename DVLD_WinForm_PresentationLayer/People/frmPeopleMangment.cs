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
    public partial class frmPeopleMangment : Form
    {
        DataTable dt = clsPerson.GetAllPersons();
        
        
        
        int RowIndex = 0;
        public frmPeopleMangment()
        {
            
            InitializeComponent();
            cmbFilterBy.SelectedIndex = 0;
            
        }

        
       
        private void _Refresh()
        {
            dt = clsPerson.GetAllPersons();

            dataGridView1.DataSource = dt;


           




        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
           
            _Refresh();
            dataGridView1.Rows[RowIndex].Selected = true;
            cmbFilterBy.SelectedIndex = 0;

           // dt.Columns[0].ToString();

        }

       

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbFilterBy.SelectedIndex == 0)
            {
                txtFilterBy.Visible = false;
            }
            else { txtFilterBy.Visible = true; }
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
                    Filter.RowFilter = "PersonID='"+ txtFilterBy.Text.ToString()+"'";
                    dataGridView1.DataSource = Filter;
                    
                    break;
                
                case 2:
                    Filter.RowFilter="FirstName LIKE '"+ txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                case 3:
                    Filter.RowFilter = "SecondName LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                case 4:
                    Filter.RowFilter = "ThirdName LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                case 5:
                    Filter.RowFilter = "LastName LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                    case 6:
                    Filter.RowFilter = "CountryName LIKE '" + txtFilterBy.Text .ToString()+ "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                    case 7:
                    Filter.RowFilter = "Gender LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                    case 8:
                    Filter.RowFilter = "Phone LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;
                    break;
                    case 9:
                    Filter.RowFilter = "Email LIKE '" + txtFilterBy.Text.ToString() + "%'";
                    dataGridView1.DataSource = Filter;

                    break;

            }
        }


        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm=new frmPersonDetails((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm=new frmAddNewPerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _Refresh();
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
               frmDeletePerson frm = new frmDeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
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
            if (cmbFilterBy.SelectedIndex == 1 || cmbFilterBy.SelectedIndex == 8)
            {
                if (!char.IsDigit(e.KeyChar)&&e.KeyChar !=(char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            else { e.Handled = false; }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.ShowDialog();
            _Refresh();
        }

        private void AddPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
           
            frm.ShowDialog();
            _Refresh();

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show ("this Fueature is not implmented yet","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("this Fueature is not implmented yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
