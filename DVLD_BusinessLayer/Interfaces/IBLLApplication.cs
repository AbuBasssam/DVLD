using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVlD_BusinessLayer.clsApplication;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLApplication
    {
        public Nullable<int> ApplicationID { get; set; }

        public int ApplicationPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public clsApplicationType.enApplicationTypes ApplicationTypeID { get; set; }

        public clsApplication.enApplicationStatus ApplicationStatus { get; set; }
        public DateTime LastStauteDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedBy { get; set; }
        Task<clsApplication> Find(int ApplicationID);
        Task<int?> AddNewApplication(ApplicationDTO ADTO);
        Task<bool> UpdateApplication(ApplicationDTO ADTO);
        Task<bool> DeleteApplication(int ApplicationID);
        Task<bool> IsApplicationExist(int ApplicationID);
        Task<bool> DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID);
        Task<int> GetActiveApplicationID(int PersonID, clsApplicationType.enApplicationTypes ApplicationTypeID);
        Task<int> GetActiveApplicationIDForLicenseClass(int PersonID, clsApplicationType.enApplicationTypes ApplicationTypeID, int LicenseClassID);
        Task<int> GetActiveApplicationID(clsApplicationType.enApplicationTypes ApplicationTypeID);
          
          //Task<bool> Cancel();
         //Task<bool> SetComplete();
        //Task<bool> Delete();
       //Task<bool> DoesPersonHaveActiveApplication(int ApplicationTypeID);

    }
}
