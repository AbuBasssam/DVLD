using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsLicenseClasses: IBLLLicenseClass
    {
       
        public enum enLicenseClasses
        {
            SmallMotorcycle=1,
            HeavyMotorcycleLicense,
            OrdinaryDrivingLicense,
            Commercial,
            Agricultural,
            SmallAndMediumBus,
            TruckAndHeavyVehicle

        }
       
        private readonly IDALLicenseClasses _DALLicenseClasses;
        LicenseClassDTO LCDTO
        {
            get
            {
                return new LicenseClassDTO
                    (
                    (int)this.LicenseClassesID,
                    this.ClassName,
                    this.ClassDescription,
                    this.MinimumAllowedAge,
                    this.DefalutValidityLength,
                    this.ClassFees
                    );

            }
        }
        public enLicenseClasses LicenseClassesID {  get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefalutValidityLength { get; set; }
        public float ClassFees { get; set; }

        public clsLicenseClasses(IDALLicenseClasses dALLicenseClasses)
        {
            this._DALLicenseClasses = dALLicenseClasses;

        }
        
        private clsLicenseClasses(IDALLicenseClasses dALLicenseClasses ,LicenseClassDTO licenseClassDTO)
        {
            this.LicenseClassesID =(enLicenseClasses) licenseClassDTO.LicenseClassesID;
            this.ClassName = licenseClassDTO. ClassName;
            this.ClassDescription = licenseClassDTO.ClassDescription;
            this.MinimumAllowedAge = licenseClassDTO.MinimumAllowedAge;
            this.DefalutValidityLength = licenseClassDTO.DefalutValidityLength;
            this.ClassFees =licenseClassDTO.ClassFees;

        }

        public async Task< IEnumerable<LicenseClassDTO>> GetAllLicenseClasses()
            =>await _DALLicenseClasses.GetAllLicenseClasses();
        
        public async Task< clsLicenseClasses> Find(int LicenseClassesID)
        {

            LicenseClassDTO licenseClassDTO = await _DALLicenseClasses.Find(LicenseClassesID);
           
                return (licenseClassDTO != null)? new clsLicenseClasses(_DALLicenseClasses, licenseClassDTO):null;
        }
       
        public async Task<clsLicenseClasses> Find(string ClassName)
        {
            LicenseClassDTO licenseClassDTO = await _DALLicenseClasses.Find(ClassName);

            return (licenseClassDTO != null) ? new clsLicenseClasses(_DALLicenseClasses, licenseClassDTO) : null;
        }

        public  async Task<int?> AddNewLicenseClass(LicenseClassDTO LCDTO)
            => await _DALLicenseClasses.AddNewLicenseClass(LCDTO);

        public async Task<bool> UpdateLicenseClass(LicenseClassDTO LCDTO)
            => await _DALLicenseClasses.UpdateLicenseClass(LCDTO);
        
    }
}
