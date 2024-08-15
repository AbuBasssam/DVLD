using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVlD.BusinessLayer;
using DVlD_BusinessLayer;

namespace DVLD
{
      public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        public enum enMode { AddNew, Update };
        enMode _Mode;
        private clsApplication _Application=new clsApplication();
        private clsLocalDrivingLicenseApplication licenseApplication;
        int _LocalDrivingLicenseApplicationID;
        private int _SelectedPersonID = -1;


        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values
            _FiilClassesInComboBox();


            if (_Mode == enMode.AddNew)
            {

                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
                licenseApplication = new clsLocalDrivingLicenseApplication();
               // ctrlPersonCardWithFilter1.FilterFocus();
                tpApplicationInfo.Enabled = false;

                cbClasses.SelectedIndex = 2;
                lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).Fees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
                btnNext.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;


            }

        }
        private void _FiilClassesInComboBox()
        {
            DataTable dt = clsLicenseClasses.GetAllLicenseClasses();
            foreach (DataRow dr in dt.Rows)
            {
                cbClasses.Items.Add(dr["ClassName"]);

            }
        }
        private void _LoadData()
        {
            licenseApplication = clsLocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplicationID == null)
            {
                MessageBox.Show("No Application with ID = " + _LocalDrivingLicenseApplicationID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            ctrPersonCardWithFilter2.PersonsCard().LoadPersonInfo(licenseApplication.ApplicationInfo.ApplicationPersonID);
            cbClasses.SelectedIndex = cbClasses.FindString(licenseApplication.License.ClassName);
            lblCreatedBy.Text = licenseApplication.ApplicationInfo.CreatedBy.ToString();
            lblApplicationID.Text = licenseApplication.ApplicationInfo.ApplicationID.ToString();
            lblApplicationDate.Text = licenseApplication.ApplicationInfo.ApplicationDate.ToShortDateString();
            lblFees.Text = licenseApplication.ApplicationInfo.PaidFees.ToString();
            ctrPersonCardWithFilter2.FilterBy(licenseApplication.ApplicationInfo.ApplicationPersonID);
        }
        private void _FillApplication()
        {
            _Application.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _Application.CreatedBy = (int)clsGlobal.CurrentUser.UserID;
            //_Application.User = clsUser.FindByUserID(clsGlobal.CurrentUser.UserID);
            _Application.ApplicationDate = DateTime.Now;
            _Application.LastStauteDate = DateTime.Now;
            _Application.ApplicationTypeID = 1;
            //_Application.ApplicationType = clsApplicationType.Find(_Application.ApplicationTypeID);
            _Application.PaidFees = Convert.ToInt32(lblFees.Text);
            _Application.ApplicationPersonID = _SelectedPersonID;

        }
        
        private bool _SaveApplication()
        {
            int LicenseClass = clsLicenseClasses.Find(cbClasses.Text).LicenseClassesID;
            int ID = clsApplication.GetActiveApplicationIDForLicenseClass((int)clsPerson.Find(ctrPersonCardWithFilter2.PersonsCard().PersonID()).PersonID, clsApplication.enApplicationType.NewDrivingLicense, clsLicenseClasses.Find( cbClasses.Text).LicenseClassesID);
           

            if (_AlreadyHaveLicense(_Application.ApplicationPersonID, LicenseClass))
             { 
                MessageBox.Show(" this person already have  License of this class", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
             }
             
            
             else if (ID != -1)
             {

                MessageBox.Show($" this person already have  Application with {ID}", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
             }
            else if(_Mode == enMode.Update)
            {
                licenseApplication.LicenseClassID = LicenseClass;
                if(licenseApplication.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
                else
                {
                    MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
                
            }
            else
             {
                _FillApplication();
                if (_Application.Save())
                {
                    return true;

                    
                }
               


             }

            return false;

           



        }

        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmNewLocalDrivingLicenseApplication(int LocalDrivingLicenseApplication)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            this._LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplication;
        }

        private bool _AlreadyHaveLicense(int PersonID, int LicenseClass)
        {
            return clsLicense.AlreadyHaveLicense(PersonID, LicenseClass);
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            // _FillApplication();

            if (_SaveApplication())
            {
                if (_Mode == enMode.Update)
                {

                    return;
                }
                licenseApplication = new clsLocalDrivingLicenseApplication();
                licenseApplication.ApplicationID = (int)_Application.ApplicationID;
                licenseApplication.LicenseClassID = clsLicenseClasses.Find(cbClasses.Text).LicenseClassesID;

                if (licenseApplication.Save())
                {
                    MessageBox.Show("Data Saved Successfully ", "Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    lblApplicationID.Text= licenseApplication.LocalDrivingLicenseApplicationID.ToString();
                }
                else
                    MessageBox.Show("Data  not Saved", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*  int LicenseClassID = clsLicenseClasses.Find(cbClasses.Text).LicenseClassesID;


              int ActiveApplicationID = clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID, clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

              if (ActiveApplicationID != -1)
              {
                  MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  cbClasses.Focus();
                  return;
              }


              //check if user already have issued license of the same driving  class.
              if (clsLicense.IsLicenseExistByPersonID(ctrPersonCardWithFilter2.PersonsCard().PersonID(), LicenseClassID))
              {

                  MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  return;
              }

              licenseApplication._App = ctrPersonCardWithFilter2.PersonsCard().PersonID();
              licenseApplication.ApplicationDate = DateTime.Now;
              licenseApplication.ApplicationTypeID = 1;
              licenseApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
              licenseApplication.LastStatusDate = DateTime.Now;
              licenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);
              licenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
              licenseApplication.LicenseClassID = LicenseClassID;


              if (_LocalDrivingLicenseApplication.Save())
              {
                  lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                  //change form mode to update.
                  _Mode = enMode.Update;
                  lblTitle.Text = "Update Local Driving License Application";

                  MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

              }
              else
                  MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
  */




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)   
        {
            _ResetDefualtValues();
           

            if (_Mode == enMode.Update)
            {
                _LoadData();
                
                /* ctrPersonCardWithFilter2.PersonsCard().LoadPersonInfo(clsApplication.Find(licenseApplication.ApplicationID).ApplicationPersonID);
                 cbClasses.SelectedIndex = cbClasses.FindString(clsLicenseClasses.Find(licenseApplication.LicenseClassID).ClassName);
                 lblCreatedBy.Text = clsApplication.Find(licenseApplication.ApplicationID).CreatedBy.ToString();
                 lblApplicationID.Text = clsApplication.Find(licenseApplication.ApplicationID).ApplicationID.ToString();
                 lblApplicationDate.Text = clsApplication.Find(licenseApplication.ApplicationID).ApplicationDate.ToString();
                 lblFees.Text = 15.ToString();
                 ctrPersonCardWithFilter2.FilterBy(clsApplication.Find(licenseApplication.ApplicationID).ApplicationPersonID);
 */
            }
           
           /* else
            {
                cbClasses.SelectedIndex = 0;
                lblFees.Text = 15.ToString();
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedBy.Text = clsGlobal.CurrentUser.UserName;
                btnNext.Enabled = false;
                btnSave.Enabled = false;

            } */
        }

       /*Old Way for PersonSelected
         private void ctrPersonCardWithFilter2_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;

            if (obj != null)
            {
               // licenseApplication.ApplicationInfo.ApplicationPersonID = obj;

                //_Application.Person = clsPerson.Find(obj);
                btnNext.Enabled = true;
                //btnSave.Enabled = true;

            }

        }*/

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tabControl2.SelectedTab = tabControl2.TabPages["tpApplicationInfo"];
                return;
            }


            //incase of add new mode.
            if (ctrPersonCardWithFilter2.PersonsCard().PersonID() != -1)
            {

                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                tabControl2.SelectedTab = tabControl2.TabPages["tpApplicationInfo"];

            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //_FillApplication();
            //tabControl2.SelectedIndex = 1;

        }

        private void ctrPersonCardWithFilter2_OnPersonSelected(object sender, ctrPersonCardWithFilter.PersonSelectionEventArgs e)
        {
            _SelectedPersonID = (int)e.PersonInfo.PersonID;

            if (e.PersonInfo.PersonID != null)
            {
                // licenseApplication.ApplicationInfo.ApplicationPersonID = obj;

                //_Application.Person = clsPerson.Find(obj);
                btnNext.Enabled = true;
                //btnSave.Enabled = true;

            }
        }
    }
}
