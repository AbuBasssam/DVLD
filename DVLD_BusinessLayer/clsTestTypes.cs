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
        public enum enTestTypeValidationType { EmptyFileds = 1, NullObject = 2, WrongType = 3, Valid = 4 };

        public enum enTestType {VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public TestTypeDTO TestTypeDTO
        {
            get
            {
                return  new TestTypeDTO((int)this.TestTypeID, this.Title, this.Description, this.Fees);
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
            this._DALTestypes = dALTestTypes;
            this.TestTypeID = (enTestType) TTDOT.TestTypeID;
            this.Title = TTDOT.Title;
            this.Description = TTDOT.Description; 
            this.Fees = TTDOT.TestFees;
            

        }
        private Func<string, bool> IsFieldEmpty = str => string.IsNullOrEmpty(str);
        private bool HasTypeHaveEmptyFileds(TestTypeDTO TTDTO)
        {
           return (IsFieldEmpty(TTDTO.Title) || IsFieldEmpty(TTDTO.Description)|| TTDTO.TestFees == 0);
        }
         public enTestTypeValidationType IsValid(TestTypeDTO TTDTO)
         {
            if (HasTypeHaveEmptyFileds(TTDTO))
                return enTestTypeValidationType.EmptyFileds;
            
            if (TTDTO.TestTypeID < 0 || TTDTO.TestTypeID > 3)
                return enTestTypeValidationType.WrongType;

            if (TTDTO == null)
                return enTestTypeValidationType.NullObject;

            return enTestTypeValidationType.Valid;
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
        
        private async Task<int?>  AddNewTestType(TestTypeDTO TTDOT)
        {
            //call DataAccess Layer 

            return await _DALTestypes.AddNewTestTypeAsync(TestTypeDTO);

        }

        public async Task< bool> UpdateTestType(TestTypeDTO TTDOT)
        {
            return await _DALTestypes.UpdateTestAsync(TTDOT);
        }

        

    }
}
