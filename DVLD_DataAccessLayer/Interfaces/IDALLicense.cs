using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALLicense
    {
        Task<IEnumerable<LicenseDTO>> GetAllLicenses();
        Task<IEnumerable<DriverLicensesDTO>> GetAllDriverLicenses(int DriverID);
        Task<LicenseDTO> FindByLicenseID(int LicenseID);
        Task<LicenseDTO> FindByLicenseIDAndLicenseClass(int LicenseID,int LicenseClass);
        Task<LicenseDTO> FindByDriverID(int DriverID);
        Task<LicenseDTO> FindByApplicationID(int ApplicationID);
        Task<int?> AddNewLicense(LicenseDTO LDTO);
        Task<bool> UpdateLicense(LicenseDTO LDTO);
        Task<bool> DeleteLicense(int LicenseID);
        Task<bool> AlreadyHaveLicense(int DriverID, int LicenseClass);
        Task<bool> IsLicneseExist(int ApplicationID);
        Task<int?> GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID);
        Task<bool> DeactivateLicense(int LicenseID);
        Task<int?> Detain(DetainedLicenseDTO DLDTO);

    }
}
