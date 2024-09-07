using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Entities;
using DVlD_BusinessLayer.Interfaces;

namespace DVlD_BusinessLayer
{
    public class clsInternationalLicense: IBLLInternationalLicnense
    {
        private readonly IDALInternationalLicense _DALInternationalLicense;
        
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        
        public clsInternationalLicense(IDALInternationalLicense dALInternationalLicense)
        {
            this._DALInternationalLicense = dALInternationalLicense;
        }

        private clsInternationalLicense(IDALInternationalLicense dALInternationalLicense,InternationalLicenseDTO ILDTO)
        {

            this.InternationalLicenseID = ILDTO.InternationalLicenseID;
            this.ApplicationID = ILDTO.ApplicationID;
            this.DriverID = ILDTO.DriverID;
            this.IssuedUsingLocalLicenseID = ILDTO.IssuedUsingLocalLicenseID;
            this.IssueDate = ILDTO.IssueDate;
            this.ExpirationDate =ILDTO.ExpirationDate;
            this.IsActive = ILDTO.IsActive;
            this.CreatedByUserID = ILDTO.CreatedByUserID;




        }

        public async Task<IEnumerable<InternationalLicenseDTO>>GetAllDriverInternationalLicenses(int DriverID)
        {
            return await _DALInternationalLicense.GetAllDriverInternationalLicenses(DriverID);

        }


        public async Task<clsInternationalLicense> FindByLicenseID(int InternationalLicenseID)
        {
            InternationalLicenseDTO ILDTO=await _DALInternationalLicense.FindByInternationalLicenseID(InternationalLicenseID);

                return ILDTO!=null? null: new clsInternationalLicense(_DALInternationalLicense,ILDTO);

        }

        public async Task<clsInternationalLicense> FindByLicenseID(int InternationalLicenseID, int IssuedUsingLocalLicenseID)
        {
            InternationalLicenseDTO ILDTO =
                await _DALInternationalLicense.FindByInternationalLicenseIDAndIssuedUsingLocalLicenseID(InternationalLicenseID, IssuedUsingLocalLicenseID);

            return ILDTO != null ? null : new clsInternationalLicense(_DALInternationalLicense, ILDTO);

        }

        public async Task<clsInternationalLicense> FindByDriverID(int DriverID)
        {
            InternationalLicenseDTO ILDTO = await _DALInternationalLicense.FindByDriverID(DriverID);

            return ILDTO != null ? null : new clsInternationalLicense(_DALInternationalLicense, ILDTO);

        }

        public async Task<clsInternationalLicense> FindByApplicationID(int ApplicationID)
        {
            InternationalLicenseDTO ILDTO = await _DALInternationalLicense.FindByApplicationID(ApplicationID);

            return ILDTO != null ? null : new clsInternationalLicense(_DALInternationalLicense, ILDTO);

        }

        public async Task<int?> AddNewLLicense(InternationalLicenseDTO ILDTO)
        {
            return await _DALInternationalLicense.AddNewInternationalInternationalLicense(ILDTO);

        }

        public async Task<bool> UpdateLLicense(InternationalLicenseDTO ILDTO)
        {
            return await _DALInternationalLicense.UpdateInternationalLicense(ILDTO);


        }

        public async Task<bool> DeleteLicense(int LicenseID)
        {
            return await _DALInternationalLicense.DeleteInternationalLicense(LicenseID);
        }

        /*public static bool AlreadyHaveLicense(int DriverID)
        {

         return clsInternationalInternationalLicenseData.AlreadyHaveInternationalLicense(DriverID);
        
        }*/

        public async Task<bool> IsLicenseExist(int InternationalLicenseID)
        {
            return await _DALInternationalLicense.IsLicneseExist(InternationalLicenseID);
        }
        public async Task<IEnumerable<InternationalLicenseDTO>> GetAllInternationalLicenses()
        {
            return await _DALInternationalLicense.GetAllInternationalLicenses();
        }
       

    }
}
