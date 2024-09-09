using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVlD_BusinessLayer;
using DVLD_DataAccessLayer.Entities;
using static DVlD_BusinessLayer.clsTestTypes;

namespace DVlD_BusinessLayer.Interfaces
{
     public interface IBLLTestTypes
    {
        /// <summary>
        /// enTestType datatype should  have three  {VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        /// </summary>
        enTestType TestTypeID { set; get; }
        string Title { set; get; }
        string Description { set; get; }
        float Fees { set; get; }
        Task<IEnumerable<TestTypeDTO>> GetAllTestTypes();
        Task<clsTestTypes> Find(enTestType TestTypeID);
        Task<bool> UpdateTestType(TestTypeDTO TTDOT);
        enTestTypeValidationType IsValid(TestTypeDTO TTDTO);


    }
}
