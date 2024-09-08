using System;
using DVLD_DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Security.Policy;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Entities;
using DVlD_BusinessLayer.Interfaces;

namespace DVlD_BusinessLayer
{
    public class clsApplication: IBLLApplication
    {
        private readonly IDALApplication _DAL;

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public Nullable<int> ApplicationID { get; set; }

        public int ApplicationPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public clsApplicationType.enApplicationTypes ApplicationTypeID { get; set; }

        public clsApplication.enApplicationStatus ApplicationStatus { get; set; }

        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }

        public DateTime LastStauteDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedBy { get; set; }


        public clsApplication( IDALApplication DAL)=>this._DAL = DAL;

        private clsApplication(IDALApplication DAL,ApplicationDTO ADTO)
        {
            this.ApplicationID = ADTO.ApplicationID;
            this.ApplicationPersonID = ADTO.ApplicationPersonID;
            this.ApplicationDate = ADTO.ApplicationDate;
            this.ApplicationTypeID = (clsApplicationType.enApplicationTypes) ADTO.ApplicationTypeID;
            this.ApplicationStatus = (enApplicationStatus)ADTO.Staute;
            this.LastStauteDate =ADTO. LastStauteDate;
            this.PaidFees =ADTO.PeadFees;
            this.CreatedBy = ADTO.CreatedBy;
           


        }

        
        public async Task<clsApplication> Find(int ApplicationID)
        {   
            ApplicationDTO ADTO= await _DAL.Find(ApplicationID);
            return (ADTO == null ? null :new clsApplication(_DAL,ADTO));

        }
        
        public async Task< int?> AddNewApplication(ApplicationDTO ADTO)=> await _DAL.AddNewApplication(ADTO);
        
        public async Task< bool> UpdateApplication(ApplicationDTO ADTO)=> await _DAL.UpdateApplication(ADTO);
        

        public async Task<bool> DeleteApplication(int ApplicationID)=>await _DAL.DeleteApplication(ApplicationID);
        

        public async Task<bool> Cancel() => await _DAL.UpdateStatus((int)ApplicationID, 2);
        

        public async Task<bool> SetComplete()=> await _DAL.UpdateStatus((int)ApplicationID, 3);
    

       
        public async Task<bool> Delete()=> await _DAL.DeleteApplication((int)this.ApplicationID);
        

        public async Task<bool> IsApplicationExist(int ApplicationID)=> await _DAL.IsApplicationExist(ApplicationID);
        

        public async Task<bool> DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
            => await _DAL.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        

        public async Task<bool> DoesPersonHaveActiveApplication(int ApplicationTypeID)
            => await DoesPersonHaveActiveApplication(this.ApplicationPersonID, ApplicationTypeID);


        public async Task<int> GetActiveApplicationID(int PersonID, clsApplicationType.enApplicationTypes ApplicationTypeID)
            => await _DAL.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        

        public async Task<int> GetActiveApplicationIDForLicenseClass(int PersonID, clsApplicationType.enApplicationTypes ApplicationTypeID, int LicenseClassID)
        
            =>await _DAL.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        

        public async Task<int> GetActiveApplicationID(clsApplicationType.enApplicationTypes ApplicationTypeID)
            => await _DAL.GetActiveApplicationID(this.ApplicationPersonID, (int)ApplicationTypeID);
        


    }   
}
