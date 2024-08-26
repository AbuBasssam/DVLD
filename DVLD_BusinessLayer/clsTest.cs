using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAccessLayer;

namespace DVlD_BusinessLayer
{
    public  class clsTest
    {
        public enum enMode { AddNew = 0, Update=1};
        public enMode Mode { get; set; }
        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public byte TestResult {  get; set; }
        public string Notes {  get; set; }
        public int CreatedBy { get; set; }
        public clsUser UserInfo { get; set; }
        public clsTestAppointment AppointmentInfo {  get; set; }

        public clsTest()
        {
          this.TestID = -1;
          this.TestAppointmentID = -1;
          this.TestResult=0;
          this.Notes = string.Empty;
          this.CreatedBy = -1;
          this.AppointmentInfo=new clsTestAppointment();
          Mode =enMode.AddNew;
        
        }
        
        private clsTest(int TestID,int TestAppointmentID, byte TestResult,string Notes,int CreatedBy)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedBy = CreatedBy;
           // this.UserInfo =  clsUser.FindByUserID(CreatedBy).Result;
            this.AppointmentInfo =clsTestAppointment.Find(TestAppointmentID);
            Mode = enMode.Update;
        }
    
        public static clsTest Find( int TestID)
        {
            int TestAppointmentID = -1, CreatedBy=-1;
            byte TestResult = 0;
            string Notes = "";
            if (clsTestData.Find(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedBy))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedBy);
            }
            else
                return null;

        }
        
        private bool _AddNewTest()
        {
            this.TestID = clsTestData.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedBy);

            return (this.TestID!=-1);
        }
        
        private bool _UpdateTest()
        {
            return clsTestData.UpdateTest(TestID, TestAppointmentID, TestResult, Notes, CreatedBy);
        }
    
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update:

                    return _UpdateTest();


                default:
                    return false;

            }
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

        public static clsTest FindLastTestPerPersonAndLicenseClass
           (int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, Convert.ToByte(TestResult),
                        Notes, CreatedByUserID);
            else
                return null;

        }


    }
}
