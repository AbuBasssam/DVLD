using DVlD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmAddNewUser : Form
    {

        public enum enMode { Add, Update };
        enMode Mode;
        clsUser _User;
       
        
        private void _Lock()
        {
            btnNext.Enabled = false;
            txtUserName.Enabled = false;
            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
            btnSave.Enabled = false;
        }
        
        private void _Open()
        {
            btnNext.Enabled = true;
            txtUserName.Enabled = true;
            txtPassword.Enabled = true;
            txtConfirmPassword.Enabled = true;
            btnSave.Enabled = true;
           
        }
       
        public frmAddNewUser()
        {
            InitializeComponent();
            Mode = enMode.Add;
        }
        public frmAddNewUser(int UserID)
        {
            InitializeComponent();
            Mode = enMode.Update;
            _User = clsUser.FindByUserID(UserID);
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_User.PersonID == -1)
                return;

            
            tabControl1.SelectedIndex = 1;


                txtUserName.Focus();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (txtUserName.Text == "")
            {

                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "Password should have a value!");

            }
           
            else
            {

                errorProvider1.SetError(txtUserName, "");
                txtPassword.Focus();

            }

          
        
        
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {

                txtUserName.Focus();
                errorProvider1.SetError(txtConfirmPassword, "the passwords is not match!");

            }
            else
            {

                errorProvider1.SetError(txtConfirmPassword, "");

            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtPassword.Text)
            {

                txtPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "the passwords is not match!");

            }
            else
            {

                errorProvider1.SetError(txtConfirmPassword, "");
                txtConfirmPassword.Focus(); 
            }

    
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_User.PersonID == -1)
                return;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = (chkIsActive.Checked) ? true : false ;
            if (_User.UserName!="" && _User.Password != "")
            {
              
                if (_User.Save())
                {
                    lblUserID.Text = _User.UserID.ToString();
                    lblTitle.Text = "Update User";
                    Mode = enMode.Update;
                    MessageBox.Show("Data Saved Successfully ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                
                }
                else
                    MessageBox.Show("Data  not Saved", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
               
            }

        }


       /*Old Way
        private void ctrPersonCardWithFilter1_OnPersonSelected(int obj)
        {
            if(clsUser.IsAlreadyUserExist(obj))
            {
              MessageBox.Show($"this person is already a user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _Lock();

            }
            else
            {
                _Open();
                _User.PersonID = obj;
                _User.PersonInfo = clsPerson.Find(obj);
            }
            if (!clsPerson.IsPersonExist(obj))
            {
                //MessageBox.Show($"this person is not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               _Lock();



            }
            else
            {
                _Open();
                _User.PersonID = obj;
                _User.PersonInfo = clsPerson.Find(obj);
            }
        }
*/
        private void _LoadUserInfo()
        {
            _User = clsUser.FindByUserID((int)_User.UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with ID : " + _User.UserID + " User not found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            lblUserID.Text = _User.UserID.ToString();
            chkIsActive.Checked = _User.IsActive;

            ctrPersonCardWithFilter1.PersonsCard().LoadPersonInfo(_User.PersonID);
             

        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                lblTitle.Text = "Update User Details";
                _LoadUserInfo();
                ctrPersonCardWithFilter1.FilterBy(_User.PersonID);
                txtUserName.Text = _User.UserName; 
                txtPassword.Text = _User.Password;
                txtConfirmPassword.Text = _User.Password;
               

            }
               

            else
            {
                lblTitle.Text = "Add New User";

                _User = new clsUser();

                btnNext.Enabled = false;

            }
            
        }

        

        private void ctrPersonCardWithFilter1_OnPersonSelected_1(object sender, ctrPersonCardWithFilter.PersonSelectionEventArgs e)
        {
            if (clsUser.IsAlreadyUserExist((int)e.PersonInfo.PersonID))
            {
                MessageBox.Show($"this person is already a user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _Lock();
                return;

            }
            else
            {
                _Open();
                _User.PersonID = (int)e.PersonInfo.PersonID;
                _User.PersonInfo = e.PersonInfo;
            }
            if (!clsPerson.IsPersonExist((int)e.PersonInfo.PersonID))
            {
                _Lock();



            }
            else
            {
                _Open();
                _User.PersonID = (int)e.PersonInfo.PersonID;
                _User.PersonInfo = e.PersonInfo;
            }
        }
    }
    
}
