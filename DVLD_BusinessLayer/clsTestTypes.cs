using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsTestTypes:IBLLTestTypes
    {

        private readonly IDALTestTypes _DALTestypes ;
        public enum enTestType {VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public TestTypeDTO TestTypeDTO
        {
            get
            {
                return  new TestTypeDTO(Convert.ToInt32(this.TestTypeDTO), this.Title, this.Description, this.Fees);
            }
        } 
        public enTestType TestTypeID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public float Fees { set; get; }

        public clsTestTypes(IDALTestTypes DALTestTypes)
        {
            this._DALTestypes = DALTestTypes;
        }
        
        private clsTestTypes(IDALTestTypes dALTestTypes,TestTypeDTO TTDOT)
        {
            this.TestTypeID = (enTestType) TTDOT.TestTypeID;
            this.Title = TTDOT.Title;
            this.Description = TTDOT.Description; 
            this.Fees = TTDOT.TestFees;
            

        }

        public async Task<IEnumerable<TestTypeDTO>> GetAllTestTypes()
        {
            return await _DALTestypes.GetTestTypesAsync();
        }

        public async Task<clsTestTypes> Find(enTestType TestTypeID)
        {
            TestTypeDTO TTDTO = await _DALTestypes.FindByIDAsync((int)TestTypeID);

                return (TTDTO!=null)? new clsTestTypes(_DALTestypes,TTDTO):null;
            
            
        }
        
        private async Task<int?>  AddNewTestType()
        {
            //call DataAccess Layer 

            return await _DALTestypes.AddNewTestTypeAsync(TestTypeDTO);

        }

        public async Task< bool> UpdateTestType()
        {
            return await _DALTestypes.UpdateTestAsync(TestTypeDTO);
        }

        

    }
}
