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
    public partial class frmChangePassword : Form
    {
        clsUser _User;
        int _UserID = -1;
        public frmChangePassword(int UserID)
        {

            InitializeComponent();
            _UserID = UserID;

        }
        private void _ResetDefualtValues()
        {
            txtCurrentPassword.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtCurrentPassword.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "this field is required!");
                txtCurrentPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
            if (txtCurrentPassword.Text != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Wrong Password!");
                txtCurrentPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "this field is required!");
                txtPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Passwords not matching!");
                txtConfirmPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txtPassword.Text.Trim();

            if (_User.SaveAsync())
            {
          
                MessageBox.Show("Data Saved Successfully ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          
            }
            else
                MessageBox.Show("Data  not Saved", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
          

            
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("Could not Find User with id = " + _UserID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

                return;
            }
            ctrlUserCard1.LoadUserInfo(_UserID);




        }
    }
}   
