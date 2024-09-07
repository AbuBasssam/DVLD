using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public  interface IBLLLocalDrivingLicenseApp
    {
        public int LocalDrivingLicenseApplicationID { get; set; }

        public int ApplicationID { get; set; }

        public int LicenseClassID { get; set; }
        Task<IEnumerable<LDLApplicatoinViewDTO>> GetAllApplicatoins();
        Task<clsLocalDrivingLicenseApplication> Find(int LocalDrivingLicenseApplicationID);
        Task<clsLocalDrivingLicenseApplication> FindByApplicationID(int ApplicationID);
        Task<bool> UpdateApplication(LDLApplicatoinDTO lDLApplicatoinDTO);
        Task<int?> AddNewApplication(LDLApplicatoinDTO lDLApplicatoinDTO);
        Task<bool> DeleteLocalLicenseApp(int LocalDrivingLicenseApplicationID);
        Task<int?> IsAlreadyExist(string NatoinalNO, string ClassName);
        Task<int> PassedTest(int LocalDrivingLicenseApplicationID);
        Task<bool> DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID);
        Task<bool> DoesPassPreviousTest(clsTestTypes.enTestType CurrentTestType);
        Task<bool> DoesAttendTestType(clsTestTypes.enTestType TestTypeID);
        Task<byte> TotalTrialsPerTest(clsTestTypes.enTestType TestTypeID);
        Task<byte> TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID);
        Task<bool> AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID);
        Task<bool> IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID);















    }
}
