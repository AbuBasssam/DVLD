using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Entities;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IApplicationTypesDAL
    {
        Task<IEnumerable<ApplicationTypeDTO>> GetAllApplicationTypesAsync();
        Task<ApplicationTypeDTO> FindByIDAsync(int ApplicationTypeID);
        Task<int?> AddNewApplicationTypeAsync(ApplicationTypeDTO ATDTO);
        Task<bool> UpdateApplicationTypeAsync(ApplicationTypeDTO ATDTO);
    }
}
