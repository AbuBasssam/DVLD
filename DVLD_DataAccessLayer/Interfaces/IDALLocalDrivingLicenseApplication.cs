using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALLocalDrivingLicenseApplication
    {
        Task<IEnumerable<LDLApplicatoinViewDTO>> GetAllApplications();
        Task<LDLApplicatoinDTO> Find(int LocalDrivingLicenseApplicationID);
        Task<LDLApplicatoinDTO> FindByApplicationID(int ApplicationID);
        Task<int?> AddNewApplication(LDLApplicatoinDTO lDLApplicatoinDTO);
        Task<bool> UpdateApplication(LDLApplicatoinDTO lDLApplicatoinDTO);
        Task<int?> IsAlreadyExist(string NationalNo, string ClassName);
        Task<bool> DeleteLocalLicenseApp(int LocalDrivingLicenseApplicationID);
        Task<int> PassedTests(int LocalDrivingLicenseApplicationID);
        Task<bool> DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID);
        Task<bool> DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID);
        Task<byte> TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID);
        Task<bool> IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID);
    }
}
