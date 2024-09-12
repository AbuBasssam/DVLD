using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALLicenseClasses
    {
        Task<IEnumerable<LicenseClassDTO>> GetAllLicenseClasses();
        Task<LicenseClassDTO> Find(int LicenseClassesID);
        Task<LicenseClassDTO> Find(string ClassName);
        Task<int?> AddNewLicenseClass(LicenseClassDTO LCDTO);
        Task<bool> UpdateLicenseClass(LicenseClassDTO LCDTO);

    }
}
