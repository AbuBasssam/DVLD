using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALDetainedLicense
    {
        Task<DetainedLicenseDTO> GetDetainedLicenseInfoByID(int DetainID);
        Task<DetainedLicenseDTO> GetDetainedLicenseInfoByLicenseID(int LicenseID);
        Task<IEnumerable<DetainedLicenseDTO>> GetAllDetainedLicenses();
        Task<int?> AddNewDetainedLicense(DetainedLicenseDTO DLDTO);
        Task<bool> UpdateDetainedLicense(DetainedLicenseDTO DLDTO);
        Task<bool> ReleaseDetainedLicense(ReleaseLicenseDTO RLDTO);
        Task<bool> IsLicenseDetained(int LicenseID);


















    }
}
