using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALInternationalLicense
    {
        Task<IEnumerable<InternationalLicenseDTO>> GetAllInternationalLicenses();
        Task<IEnumerable<InternationalLicenseDTO>> GetAllDriverInternationalLicenses(int DriverID);
        Task<InternationalLicenseDTO> FindByInternationalLicenseID(int InternationalLicenseID);
        Task<InternationalLicenseDTO> FindByInternationalLicenseIDAndIssuedUsingLocalLicenseID(int InternationalLicenseID, int IssuedUsingLocalLicenseID);
        Task<InternationalLicenseDTO> FindByDriverID(int DriverID);
        Task<InternationalLicenseDTO> FindByApplicationID(int ApplicationID);
        Task<int?> AddNewInternationalInternationalLicense(InternationalLicenseDTO ILDTO);
        Task<bool> UpdateInternationalLicense(InternationalLicenseDTO ILDTO);
        Task<bool> DeleteInternationalLicense(int InternationalLicenseID);
        Task<bool> AlreadyHaveInternationalLicense(int DriverID);
        Task<bool> IsLicneseExist(int InternationalLicenseID);

    }
}
