using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVlD_BusinessLayer.clsApplicationType;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLApplicationTypes
    {
        /// <summary>
        /// enApplicationTypes is data type contain all Application types(
        ///NewLocalDrivingLicense,
        ///RenewDrivingLicense,
        /// ReplacementForALostDrivingLicense,
        ///ReplacementForADamagedDrivingLicense,
        ///ReleaseDetainedDrivingLicsense,
        ///NewInternationalLicense,
        ///RetakeTest)
        /// </summary>
        enApplicationTypes ApplicationID { set; get; }
         string Title { set; get; }
         float Fees { set; get; }
         Task<IEnumerable<ApplicationTypeDTO>> GetAllApplicatoinTypes();
         Task<clsApplicationType> Find(int ApplicationID);
        Task<bool> UpdateApplicationType(ApplicationTypeDTO ATDTO);
    }
}
