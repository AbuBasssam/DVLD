using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLLicnese
    {
        public int LicenseID { get; set; }

        public int ApplicationID { get; set; }

        public int DriverID { get; set; }

        public clsLicenseClasses.enLicenseClasses LicenseClass { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Notes { get; set; }

        public float PaidFees { get; set; }

        public byte IsActive { get; set; }

        public clsLicense.enIssueReason IssueReason { get; set; }

        public int CreatedByUserID { get; set; }

        Task<clsLicense> Find(int LicenseID);
        Task<clsLicense> Find(int LicenseID, int LicenseClassID);
        Task<IEnumerable<DriverLicensesDTO>> GetAllDriverLicenses(int DriverID);
        Task<clsLicense> FindByDriverID(int DriverID);
        Task<clsLicense> FindByApplicationID(int ApplicationID);
        Task<int?> AddNewLLicense(LicenseDTO LDTO);
        Task<bool> UpdateLLicense(LicenseDTO LDTO);
        Task<bool> IsLicenseExist(int ApplicationID);
        Task<bool> IsLicenseExistByPersonID(int PersonID, int LicenseClassID);
        Task<int?> GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID);
        //Task<bool> DeactivateCurrentLicense();
        









    }
}
