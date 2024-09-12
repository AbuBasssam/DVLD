using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsApplicationType:IBLLApplicationTypes
    {
        public enum enApplicationTypes
        {
            NewLocalDrivingLicense =1,
            RenewDrivingLicense=2,
            ReplacementForALostDrivingLicense =3,
            ReplacementForADamagedDrivingLicense=4,
            ReleaseDetainedDrivingLicsense=5,
            NewInternationalLicense=6,
            RetakeTest= 7
        }
        public enum enApplicatoinTypeValidationType { EmptyFileds = 1, NullObject = 2,WrongType=3,Valid = 4 };


        private readonly IApplicationTypesDAL _DALApplicationType;
        public ApplicationTypeDTO ATDTO { get { return new ApplicationTypeDTO((int)this.ApplicationID, this.Title, this.Fees); } }
        public enApplicationTypes ApplicationID { set; get;}
        public string Title {  set; get; }  
        public float Fees { set; get; }

        private Func<string, bool> IsFieldEmpty = str => string.IsNullOrEmpty(str);
        private bool HasTypeHaveEmptyFileds(ApplicationTypeDTO ATDTO) =>(IsFieldEmpty(ATDTO.Title) || ATDTO.Fees==0 );
        
        public enApplicatoinTypeValidationType IsValid(ApplicationTypeDTO ATDTO)
        {
           
             if(HasTypeHaveEmptyFileds(ATDTO))
                    return enApplicatoinTypeValidationType.EmptyFileds;
            if (ATDTO == null)
            {
                return enApplicatoinTypeValidationType.NullObject;
            }
            if ((ATDTO.ApplicationID < 0 || ATDTO.ApplicationID > 7))
                return enApplicatoinTypeValidationType.WrongType;



            return enApplicatoinTypeValidationType.Valid;
        }
        public clsApplicationType(IApplicationTypesDAL applicationTypesDAL)
        {
            this._DALApplicationType = applicationTypesDAL;
        }

        private clsApplicationType(IApplicationTypesDAL applicationTypesDAL,ApplicationTypeDTO ATDTO)
        { 
            this.ApplicationID = (enApplicationTypes) ATDTO.ApplicationID;
            this.Title = ATDTO.Title; 
            this.Fees = ATDTO.Fees;
        }
        
        public async Task<IEnumerable<ApplicationTypeDTO>> GetAllApplicatoinTypes()
        {
            return await _DALApplicationType.GetAllApplicationTypesAsync();
        }
        
        public async Task< clsApplicationType> Find(int ApplicationID)
        {
            ApplicationTypeDTO ATDTO=await _DALApplicationType.FindByIDAsync(ApplicationID);
           
            return (ATDTO!=null)? new clsApplicationType(_DALApplicationType, ATDTO) : null;
        }

        private async Task<int?> AddNewApplicationType(ApplicationTypeDTO ATDTO)
        {
            return await _DALApplicationType.AddNewApplicationTypeAsync(ATDTO);

        }

        public async Task<bool> UpdateApplicationType(ApplicationTypeDTO ATDTO)
        {
            return await _DALApplicationType.UpdateApplicationTypeAsync(ATDTO);
        }

        
    }
}
