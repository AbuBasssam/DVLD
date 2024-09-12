using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALTestTypes
    {
         Task <IEnumerable<TestTypeDTO>> GetTestTypesAsync();
         Task<TestTypeDTO> FindByIDAsync(int TestTypeID);
         Task<bool> UpdateTestAsync(TestTypeDTO TTDTO);
         Task<int?> AddNewTestTypeAsync(TestTypeDTO TTDTO);
    }

}
