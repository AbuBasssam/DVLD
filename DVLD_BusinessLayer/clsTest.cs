using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;

namespace DVlD_BusinessLayer
{
    public  class clsTest
    {
        private IDALTest _DALTest {  get; set; }
        public TestDTO TDTO
        {
           get
           {
                return new TestDTO(this.TestID, this.TestAppointmentID, TestResult, Notes, CreatedBy);
           }
        }
        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public byte TestResult {  get; set; }
        public string Notes {  get; set; }
        public int CreatedBy { get; set; }

        public clsTest(IDALTest DALTest)
        {
          this._DALTest = DALTest;
          
        
        }
        
        private clsTest(IDALTest DALTest,TestDTO TDTO)
        {
            this.TestID = TDTO.TestID;
            this.TestAppointmentID = TDTO.TestAppointmentID;
            this.TestResult = TDTO.TestResult;
            this.Notes = TDTO.Notes;
            this.CreatedBy = TDTO. CreatedBy;
           
        }
    
        public async Task<clsTest> Find(int TestID)
        {
            TestDTO TDTO=await _DALTest.FindAsync(TestID);
            
            return(TDTO!= null)?new clsTest(_DALTest,TDTO):null;

        }
        
        public async Task<int?>AddNewTest(TestDTO TDTO)
        {
            return await _DALTest.AddNewTestAsync(TDTO);
        }
        
        public async Task<bool> UpdateTest(TestDTO TDTO)
        {
            return await _DALTest.UpdateTestAsync(TDTO);
        }
    
        public async Task<byte> GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return await _DALTest.GetPassedTestCountAsync(LocalDrivingLicenseApplicationID);
        }
        public async Task<bool> PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return await _DALTest.GetPassedTestCountAsync(LocalDrivingLicenseApplicationID) == 3;
        }

        public async Task<clsTest> FindLastTestPerPersonAndLicenseClass(int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID)
        {
            TestDTO TDTO =await _DALTest.GetLastTestByPersonAndTestTypeAndLicenseClassAsync(PersonID, LicenseClassID, (int)TestTypeID);

            return (TDTO != null) ? new clsTest(_DALTest, TDTO) : null;

        }


    }
}
