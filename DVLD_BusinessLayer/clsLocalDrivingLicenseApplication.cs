using DVLD_DataAccessLayer;
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
    public class clsLocalDrivingLicenseApplication
    {
        public enum enMode{AddNew , Update };
       
        public enMode Mode = enMode.AddNew;
        
        public int LocalDrivingLicenseApplicationID {  get; set; }
       
        public int ApplicationID { get; set; }
        
        public int LicenseClassID { get; set; }

        public clsLicenseClasses License { get; set; }

        public clsApplication ApplicationInfo { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication( int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID ) 
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.License = clsLicenseClasses.Find(LicenseClassID);
            this.ApplicationInfo = clsApplication.Find(ApplicationID);

            Mode = enMode.Update;
        }

        public static DataTable GetAllApplicatoins()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllApplications();
        }

        public static clsLocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationData.Find(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,  ApplicationID,  LicenseClassID);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);

            }
            else
                return null;

        }

        private bool _UpdateApplication()
        {
            
            return clsLocalDrivingLicenseApplicationData.UpdateApplication(LocalDrivingLicenseApplicationID,ApplicationID ,LicenseClassID);
        }

        private bool _AddNewApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewApplication(ApplicationID, LicenseClassID);
            return (LocalDrivingLicenseApplicationID != -1);
        }

        public static bool DeleteLocalLicenseApp(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.DeleteLocalLicenseApp(LocalDrivingLicenseApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return _UpdateApplication();


                default:
                    return false;

            }
        }

        public static int IsAlreadyExist(string NatoinalNO, string ClassName)
        {
            return clsLocalDrivingLicenseApplicationData.IsAlreadyExist(NatoinalNO, ClassName);
        }
        
        public static int PassedTest(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.PassedTests(LocalDrivingLicenseApplicationID);
        }

        public bool DoesPassTestType(clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestTypes.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestTypes.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestTypes.enTestType.VisionTest);


                case clsTestTypes.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestTypes.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestTypes.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypes.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }


          public clsTest GetLastTestPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicationInfo.ApplicationPersonID,LicenseClassID, TestTypeID);
        }

        
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }
        
        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver =clsDriver.FindByPersonID(this.ApplicationInfo.ApplicationPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();
               
                //Driver.PersonID= this.ApplicationInfo.ApplicationPersonID; must modfy
                Driver.CreatedByUserID= CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID= Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID= Driver.DriverID;
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
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() !=-1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return  clsLicense.GetActiveLicenseIDByPersonID(this.ApplicationInfo.ApplicationPersonID, this.LicenseClassID);
        }
         
         


    }
}
