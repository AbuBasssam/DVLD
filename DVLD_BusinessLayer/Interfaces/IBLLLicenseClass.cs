using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Entities;
using static DVlD_BusinessLayer.clsLicenseClasses;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLLicenseClass
    {
        /// <summary>
        /// enLicenseClasses Contain all License classes
        /// {
        /// SmallMotorcycle=1,
        /// HeavyMotorcycleLicense=2,
        /// OrdinaryDrivingLicense=3,
        /// Commercial=4,
        /// Agricultural=5,
        /// SmallAndMediumBus=6
        /// TruckAndHeavyVehicle=7
        /// } 
        ///</summary>
        public enLicenseClasses LicenseClassesID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefalutValidityLength { get; set; }
        public float ClassFees { get; set; }
        Task<IEnumerable<LicenseClassDTO>> GetAllLicenseClasses();
        Task<clsLicenseClasses> Find(int LicenseClassesID);
        Task<clsLicenseClasses> Find(string ClassName);
        Task<int?> AddNewLicenseClass(LicenseClassDTO LCDTO);
        Task<bool> UpdateLicenseClass(LicenseClassDTO LCDTO);

    }
}
