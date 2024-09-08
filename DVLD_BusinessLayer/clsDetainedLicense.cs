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
    public class clsDetainedLicense
    {
        private IDALDetainedLicense _DAL;
        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }

        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsReleased { set; get; }
        public DateTime? ReleaseDate { set; get; }
        public int? ReleasedByUserID { set; get; }
        public int? ReleaseApplicationID { set; get; }

        public clsDetainedLicense(IDALDetainedLicense dALDetainedLicense)
        {
            _DAL = dALDetainedLicense;

        }

        private clsDetainedLicense(IDALDetainedLicense dALDetainedLicense,DetainedLicenseDTO DLDTO)

        {
            this.DetainID = DLDTO.DetainID;
            this.LicenseID = DLDTO.LicenseID;
            this.DetainDate = DLDTO.DetainDate;
            this.FineFees =DLDTO. FineFees;
            this.CreatedByUserID = DLDTO.CreatedByUserID;
            this.IsReleased = DLDTO.IsReleased;
            this.ReleaseDate =DLDTO.ReleaseDate;
            this.ReleasedByUserID =DLDTO.ReleasedByUserID;
            this.ReleaseApplicationID = DLDTO.ReleaseApplicationID;
        }

        public async Task<int?> AddNewDetainedLicense(DetainedLicenseDTO DLDTO)
        {
            //call DataAccess Layer 

            return  await _DAL.AddNewDetainedLicense(DLDTO);
        }

        public async Task<bool> UpdateDetainedLicense(DetainedLicenseDTO DLDTO)
        {
            //call DataAccess Layer 

            return await _DAL.UpdateDetainedLicense(DLDTO);
        }

        public async Task< clsDetainedLicense> Find(int DetainID)
        {
            DetainedLicenseDTO detainedLicenseDTO=await _DAL.GetDetainedLicenseInfoByID(DetainID);
            return (detainedLicenseDTO!=null)? new clsDetainedLicense(_DAL,detainedLicenseDTO):null;

        }

        public async Task<IEnumerable<DetainedLicenseDTO> >GetAllDetainedLicenses()
        {
            return await _DAL.GetAllDetainedLicenses();

        }

        public async Task<clsDetainedLicense> FindByLicenseID(int LicenseID)
        {
            DetainedLicenseDTO detainedLicenseDTO = await _DAL.GetDetainedLicenseInfoByLicenseID(LicenseID);
            return (detainedLicenseDTO != null) ? new clsDetainedLicense(_DAL, detainedLicenseDTO) : null;

        }

        
        public async Task<bool> IsLicenseDetained(int LicenseID)
        {
            return await _DAL.IsLicenseDetained(LicenseID);
        }

        public async Task<bool> ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            ReleaseLicenseDTO RLDTO = new ReleaseLicenseDTO(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
            return await _DAL.ReleaseDetainedLicense(RLDTO);
        }

    }
}
