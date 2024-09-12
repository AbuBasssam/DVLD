using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLDetainLIcnese
    {
        public int DetainID { set; get; }
        public int LicenseID { set; get; }
        public DateTime DetainDate { set; get; }
        public float FineFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsReleased { set; get; }
        public DateTime? ReleaseDate { set; get; }
        public int? ReleasedByUserID { set; get; }
        public int? ReleaseApplicationID { set; get; }
        Task<int?> AddNewDetainedLicense(DetainedLicenseDTO DLDTO);
        Task<bool> UpdateDetainedLicense(DetainedLicenseDTO DLDTO);
        Task<clsDetainedLicense> Find(int DetainID);
        Task<IEnumerable<DetainedLicenseDTO>> GetAllDetainedLicenses();
        Task<clsDetainedLicense> FindByLicenseID(int LicenseID);
        Task<bool> IsLicenseDetained(int LicenseID);
        Task<bool> ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID);

    }
}
