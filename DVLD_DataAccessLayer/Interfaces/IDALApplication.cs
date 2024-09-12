using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALApplication
    {
        Task<IEnumerable<ApplicationDTO>> GetAllApplications();
        Task<int?> AddNewApplication(ApplicationDTO ADTO);
        Task<bool> UpdateApplication(ApplicationDTO ADTO);
        Task<ApplicationDTO> Find(int ApplicationID);
        Task<bool> DeleteApplication(int ApplicationID);
        Task<bool> IsApplicationExist(int ApplicationID);
        Task<bool> DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID);
        Task<int> GetActiveApplicationID(int PersonID, int ApplicationTypeID);
        Task<int> GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID);
        Task<bool> UpdateStatus(int ApplicationID, short NewStatus);



    }
}
