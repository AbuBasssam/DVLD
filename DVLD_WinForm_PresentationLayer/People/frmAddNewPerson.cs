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
using System.IO;
using DVLD.Properties;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace DVLD
{
    
    


    public partial class frmAddNewPerson : Form
    {
        string Image="";
        clsPerson _Person;
        int _PersonID;

        public delegate void DataBackEventHandler(object sender, int PersonID);       
        
        public event DataBackEventHandler DataBack;
        
        public enum enMode { Add,Update};
        enMode Mode;
        private void _FiilCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow dr in dt.Rows)
            {
                cbCountry.Items.Add(dr["CountryName"]);

            }
        }

        private void _RestDefualtValues()
        {
            _FiilCountries();
            if (Mode == enMode.Add)
            {
                lblTitle.Text = "Add New Person";
                _Person=new clsPerson();
            }
            else lblTitle.Text = "Updte Person";

            if (rbMale.Checked)
            {
                pbImage.Image = Resources.user;

            }
            else
            {
                pbImage.Image = Resources.Female_User;

            }
            llRemoveImage.Visible=(pbImage.ImageLocation!=null);

            dtpDateOfBirth.MaxDate= DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            cbCountry.SelectedIndex = cbCountry.FindString("Saudi Arabia");
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            rbMale.Checked = true;
            msktxtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("No Person with ID : " + _Person.PersonID + " Person not found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            lblPersonID.Text = _Person.PersonID.ToString();
            txtNationalNo.Text = _Person.NationalNo.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            
            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;

            }
            else
            {
                rbFemale.Checked = true;
            }
           
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            msktxtPhone.Text = _Person.Phone;
            cbCountry.SelectedIndex = clsCountry.Find(_Person.Nationality).ID - 1;
            if (_Person.ImagePath != "")
            {
                pbImage.ImageLocation=_Person.ImagePath;

            }
            else
            {
                if (rbMale.Checked)
                    pbImage.Image = Resources.user;

                else
                    pbImage.Image = Resources.Female_User;

            }
            llRemoveImage.Visible =(_Person.ImagePath != "");
           

        }

        string ReplaceFileNameWithGUID(string SourceFile)
        {
            Guid guid = Guid.NewGuid();
            string fileName=SourceFile;
            FileInfo fi=new FileInfo(fileName);
            string extn=fi.Extension;
            return guid + extn;
        }

        public static bool CreateFolderIfDoesNotExist(string FolderPath)
        {

            // Check if the folder exists
            if (!Directory.Exists(FolderPath))
            {
                try
                {
                    // If it doesn't exist, create the folder
                    Directory.CreateDirectory(FolderPath);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating folder: " + ex.Message);
                    return false;
                }
            }

            return true;

        }

        bool SetImageInProjectImageFolder(ref string SourceFile)
        {
            string NewDestenatoin = @"C:\Users\Hp\Downloads\DVLD Images2\";

            if (!CreateFolderIfDoesNotExist(NewDestenatoin))
            {
                return false;
            }



            string DestinationFile = NewDestenatoin + ReplaceFileNameWithGUID(SourceFile);
            try
            {
                File.Copy(SourceFile, DestinationFile, true);
            }
            catch(IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            SourceFile = DestinationFile;
            return true;



        }

        private bool _HandlePersonImage()
        {
           
            // check if the image is changed
            if (_Person.ImagePath != pbImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch ( IOException)
                    {

                    }
                
                    
               
            }
                if (pbImage.ImageLocation != null)
                {
                    string SourceImageFile = pbImage.ImageLocation.ToString();
                    if (SetImageInProjectImageFolder(ref SourceImageFile))
                    {
                        pbImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Erorr Copying","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                  
                        return false;
                    }
                }              
            }

            return true ;
        }
       
        public frmAddNewPerson()
        {
            InitializeComponent();
           
            Mode = enMode.Add;
           
           
        }

        public frmAddNewPerson(int PersonID)
        {
            InitializeComponent();
    
             Mode = enMode.Update;
             _PersonID= PersonID;


        }

        private bool EmptyCheck()
        {
            if (txtFirstName.Text == "")
            {

                txtFirstName.Focus();
                errorProvider1.SetError(txtFirstName, "text Box should have a value!");
                return true;
            }
            else
            {

                errorProvider1.SetError(txtFirstName, "");

            }
            if (txtSecondName.Text == "")
            {

                txtSecondName.Focus();
                errorProvider1.SetError(txtSecondName, "text Box should have a value!");
                return true;

            }
            else
            {

                errorProvider1.SetError(txtSecondName, "");

            }
            if (txtLastName.Text == "")
            {

                txtLastName.Focus();
                errorProvider1.SetError(txtLastName, "text Box should have a value!");
                return true;

            }
            else
            {

                errorProvider1.SetError(txtLastName, "");

            }
           
            if (!msktxtPhone.MaskFull)
            {
                msktxtPhone.Focus();
                errorProvider1.SetError(msktxtPhone, "this field is required!");
                return true;

            }
            else
            {

                errorProvider1.SetError(msktxtPhone, "");

            }
            return false;
        }
        
        private bool validateEmail()
        {
            var pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"; ;
            var regex=new Regex(pattern);
            return regex.IsMatch(txtEmail.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _RestDefualtValues();
            if (Mode == enMode.Update)
                _LoadData();
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (EmptyCheck())
            {
                return;
            }
            if (!_HandlePersonImage())
                return;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.NationalNo = txtNationalNo.Text.Trim();

            if (rbMale.Checked)
                _Person.Gender = 0;
            else
                _Person.Gender = 1;

            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();

            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Phone = msktxtPhone.Text.Trim();
            _Person.Nationality = (byte)clsCountry.Find(cbCountry.Text).ID;
            if (pbImage.ImageLocation != null)
            {
                _Person.ImagePath = pbImage.ImageLocation;
            }
            else
                _Person.ImagePath = "";
            
            if (_Person.SaveAsync())
            {
                lblPersonID.Text=_Person.PersonID.ToString();
                Mode = enMode.Update;
                lblTitle.Text = "Update Person";
                MessageBox.Show("Data Saved Successfully ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                DataBack?.Invoke(this, (int)_Person.PersonID);
            }
            else
                MessageBox.Show("Data  not Saved", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == "") 
                return;
            
            if (!validateEmail())
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Wrong format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);

            }

        }
        
        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.user;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Resources.Female_User;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\Users\Hp\Downloads\DVLD Images";
            openFileDialog1.Filter = "Image file|*.jpg;*.jpeg;*,png.*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            { 

                Image = openFileDialog1.FileName;
                pbImage.Load(Image);
                llRemoveImage.Visible = true;
            
            
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            pbImage.ImageLocation = null;
            if(rbMale.Checked) 
                pbImage.Image= Resources.user;
            else 
                pbImage.Image = Resources.Female_User;
            llRemoveImage.Visible = false;
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.IsPersonExistAsync(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }
    }
}
