using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLInternationalLicnense
    {
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        Task<IEnumerable<InternationalLicenseDTO>> GetAllDriverInternationalLicenses(int DriverID);
        Task<clsInternationalLicense> FindByLicenseID(int InternationalLicenseID);
        Task<clsInternationalLicense> FindByLicenseID(int InternationalLicenseID, int IssuedUsingLocalLicenseID);
        Task<clsInternationalLicense> FindByDriverID(int DriverID);
        Task<clsInternationalLicense> FindByApplicationID(int ApplicationID);
        Task<int?> AddNewLLicense(InternationalLicenseDTO ILDTO);
        Task<bool> UpdateLLicense(InternationalLicenseDTO ILDTO);
        Task<bool> IsLicenseExist(int InternationalLicenseID);
        Task<IEnumerable<InternationalLicenseDTO>> GetAllInternationalLicenses();
    }
}
