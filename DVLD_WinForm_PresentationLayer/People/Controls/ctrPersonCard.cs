using DVLD.Properties;
using DVlD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DVLD
{


    //Editing after showing Solution
    //1- add link lable for edit person 
    //2- add method to return _Person
    //3- edit method to load the image when we loaded from file

    public partial class UCPersonDetails : UserControl
    {
        private clsPerson _Person;
        
        public  void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                RestartPersonInfo();
                MessageBox.Show($"No person with PersonID:  {PersonID}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
               _FillPersonInfo();
        
        }
        
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);
            if (_Person == null)
            {
                RestartPersonInfo();
                MessageBox.Show($"No person with NationalNo:  {NationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _FillPersonInfo();
        }
        
        public int PersonID()
        {
            return (int)_Person.PersonID;
        }
        
        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
            {
                pbImage.Image = Resources.user;
            }
            else pbImage.Image = Resources.Female_User;

            string ImagePath=_Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbImage.ImageLocation = ImagePath;

                else MessageBox.Show("Could not find this image : = " + ImagePath, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            
        }
        
        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            lblPersonID.Text=_Person.PersonID.ToString();
            lblNationalNO.Text=_Person.NationalNo.ToString();
            lblName.Text = _Person.FullName();
            lblGender.Text = (_Person.Gender == 0) ? "Male" : "Female";
           
            lblEmail.Text = _Person.Email;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString().Substring(0,10);
            lblPhone.Text = _Person.Phone;
            lblCountry.Text=clsCountry.Find(_Person.Nationality).CountryName;
            _LoadPersonImage();

        }
       
        public void RestartPersonInfo()
        {

            lblPersonID.Text = "[???]";
            lblNationalNO.Text = "[???]";
            lblName.Text = "[???]";
            lblGender.Text = "[???]";
            lblEmail.Text = "[???]";
            lblAddress.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblPhone.Text = "[???]";
            lblCountry.Text = "[???]";
            pbGender.Image= Resources.user;
        }

        public UCPersonDetails()
        {

            InitializeComponent();
        
        
        }

        public clsPerson SelectedPerson
        {
            get { return _Person; }
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddNewPerson Info = new frmAddNewPerson((int)_Person.PersonID);
            Info.ShowDialog();
            LoadPersonInfo((int)_Person.PersonID);
        }
    }


}

