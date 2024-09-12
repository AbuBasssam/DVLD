using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLTest
    {
         int TestID { get; set; }
         int TestAppointmentID { get; set; }
         byte TestResult { get; set; }
         string Notes { get; set; }
         int CreatedBy { get; set; }
         Task<clsTest> Find(int TestID);
        Task<int?> AddNewTest(TestDTO TDTO);
        Task<bool> UpdateTest(TestDTO TDTO);
        Task<byte> GetPassedTestCount(int LocalDrivingLicenseApplicationID);
        Task<bool> PassedAllTests(int LocalDrivingLicenseApplicationID);
        /// <summary>
        /// enTestType datatype it's one of three { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        /// </summary>
        /// <returns></returns>
        Task<clsTest> FindLastTestPerPersonAndLicenseClass(int PersonID, int LicenseClassID, clsTestTypes.enTestType TestTypeID);
        Task<IEnumerable<TestDTO>> GetAllTests();
    }
}
