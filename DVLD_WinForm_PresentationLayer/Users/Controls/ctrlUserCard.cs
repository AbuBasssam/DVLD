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
    public partial class ctrlUserCard : UserControl
    {
       private clsUser _User;
       private int _UserID;
        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.FindByUserID(UserID);
           
            if (_User == null)
            {
                _ResetUserInfo();
                MessageBox.Show($"No User with UserID:  {UserID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            _FillUserInfo();
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void _FillUserInfo()
        {

            ucPersonDetails1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";

        }

        private void _ResetUserInfo()
        {

            ucPersonDetails1.RestartPersonInfo();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }

        public int UserID
        {
            get { return _UserID; }
        }

        public clsUser SelectedUser
        {
            get { return _User; }
        }
    }
}
