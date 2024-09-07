using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVlD_BusinessLayer.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace DVlD_BusinessLayer
{
    public class clsLocalDrivingLicenseApplication:IBLLLocalDrivingLicenseApp
    {

        private readonly IDALLocalDrivingLicenseApplication _dALLocalDrivingLicenseApplication;
        public int LocalDrivingLicenseApplicationID {  get; set; }
       
        public int ApplicationID { get; set; }
        
        public int LicenseClassID { get; set; }

       

        public clsLocalDrivingLicenseApplication(IDALLocalDrivingLicenseApplication dALLocalDrivingLicenseApplication)
        {
            this._dALLocalDrivingLicenseApplication = dALLocalDrivingLicenseApplication;
        }

        private clsLocalDrivingLicenseApplication(IDALLocalDrivingLicenseApplication dALLocalDrivingLicenseApplication, LDLApplicatoinDTO LDLADTO ) 
        {
            this._dALLocalDrivingLicenseApplication= dALLocalDrivingLicenseApplication;
            this.LocalDrivingLicenseApplicationID = LDLADTO.LocalDrivingLicenseApplicationID;
            this.ApplicationID = LDLADTO.ApplicationID;
            this.LicenseClassID = LDLADTO.LicenseClassID;

        }

        public async Task<IEnumerable< LDLApplicatoinViewDTO>> GetAllApplicatoins()
        {
            return await _dALLocalDrivingLicenseApplication.GetAllApplications();
        }

        public async Task<clsLocalDrivingLicenseApplication> Find(int LocalDrivingLicenseApplicationID)
        {

            LDLApplicatoinDTO LDLA = await _dALLocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);
            
                return (LDLA!=null)? new clsLocalDrivingLicenseApplication(_dALLocalDrivingLicenseApplication, LDLA):null;
            
            
        }

        public async Task<clsLocalDrivingLicenseApplication> FindByApplicationID(int ApplicationID)
        {

            LDLApplicatoinDTO LDLA = await _dALLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            return (LDLA != null) ? new clsLocalDrivingLicenseApplication(_dALLocalDrivingLicenseApplication, LDLA) : null;

        }

        public async Task<bool> UpdateApplication(LDLApplicatoinDTO lDLApplicatoinDTO)
        {
            
            return await  _dALLocalDrivingLicenseApplication.UpdateApplication(lDLApplicatoinDTO);
        }

        public async Task< int?>AddNewApplication(LDLApplicatoinDTO lDLApplicatoinDTO)
        {
           return await _dALLocalDrivingLicenseApplication.AddNewApplication(lDLApplicatoinDTO);
        }

        public Task<bool> DeleteLocalLicenseApp(int LocalDrivingLicenseApplicationID)
        {
            return _dALLocalDrivingLicenseApplication.DeleteLocalLicenseApp(LocalDrivingLicenseApplicationID);
        }

        public async Task<int?> IsAlreadyExist(string NatoinalNO, string ClassName)
        {
            return await _dALLocalDrivingLicenseApplication.IsAlreadyExist(NatoinalNO, ClassName);
        }
        
        public async Task<int> PassedTest(int LocalDrivingLicenseApplicationID)
        {
            return await _dALLocalDrivingLicenseApplication.PassedTests(LocalDrivingLicenseApplicationID);
        }

        public async Task<bool> DoesPassTestType(clsTestTypes.enTestType TestTypeID)

        {
            return await _dALLocalDrivingLicenseApplication.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public async Task<bool> DoesPassPreviousTest(clsTestTypes.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return await this.DoesPassTestType(clsTestTypes.enTestType.VisionTest);


                case clsTestTypes.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return await this.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public async Task<bool> DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return await _dALLocalDrivingLicenseApplication.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public async Task<bool> DoesAttendTestType(clsTestTypes.enTestType TestTypeID)
            =>await _dALLocalDrivingLicenseApplication.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        

        public async Task<byte> TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID)
            =>await _dALLocalDrivingLicenseApplication.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        

        public async Task<byte> TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
            => await _dALLocalDrivingLicenseApplication.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        

        public async Task<bool> AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
            =>await _dALLocalDrivingLicenseApplication.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        

        public async Task<bool> AttendedTest(clsTestTypes.enTestType TestTypeID)
            => await _dALLocalDrivingLicenseApplication.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        

        public async Task<bool> IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
            => await _dALLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);


        public async Task<bool> IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)
            =>await _dALLocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        

        /*
         * public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
*/

         /* public clsTest GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicationInfo.ApplicationPersonID,LicenseClassID, TestTypeID);
        }*/

        
       /* public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }*/

        /*public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }*/

        /*public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }*/
        
        /*public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver =clsDriver.FindByPersonID(this.ApplicationInfo.ApplicationPersonID).Result;

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                //Driver = new clsDriver();
               
                //Driver.PersonID= this.ApplicationInfo.ApplicationPersonID; must modfy
                Driver.CreatedByUserID= CreatedByUserID;
                if (Driver.SaveAsync().Result)
                {
                    //DriverID= Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID= (int)Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse
            
            clsLicense License= new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID= DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate=DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.License.DefalutValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.License.ClassFees;
            License.IsActive= 1;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID= CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.ApplicationInfo.SetComplete();

                return License.LicenseID;
            }
               
            else
                return -1;
        }*/

        /*public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() !=-1);
        }*/

        /*public int GetActiveLicenseID()
        {
            //this will get the license id that belongs to this application
            return  clsLicense.GetActiveLicenseIDByPersonID(this.ApplicationInfo.ApplicationPersonID, this.LicenseClassID);
        }*/
         
         


    }
}
