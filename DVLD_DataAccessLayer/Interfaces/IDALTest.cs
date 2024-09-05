using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALTest
    {
        Task<TestDTO> FindAsync(int TestID);
        Task<int?> AddNewTestAsync(TestDTO testDTO);
        Task<bool> UpdateTestAsync(TestDTO testDTO);
        Task<byte> GetPassedTestCountAsync(int LocalDrivingLicenseApplicationID);
        Task<TestDTO> GetLastTestByPersonAndTestTypeAndLicenseClassAsync(int PersonID, int LicenseClassID, int TestTypeID);
    }

}
