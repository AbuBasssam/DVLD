using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsLicenseClasses
    {
        public  enum enMode { AddNew, Update };
        public enMode Mode { get; set; }
        public int LicenseClassesID {  get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefalutValidityLength { get; set; }
        public int ClassFees { get; set; }

        public clsLicenseClasses()
        {
            this.LicenseClassesID = 0;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefalutValidityLength = 0;
            this.ClassFees = 0;
            Mode = enMode.AddNew;

        }
        
        public clsLicenseClasses(int LicenseClassesID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefalutValidityLength, int ClassFees)
        {
            this.LicenseClassesID = LicenseClassesID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefalutValidityLength = DefalutValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;

        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }

        public static clsLicenseClasses Find(int LicenseClassesID)
        {
            string ClassName = "";
            string ClassDescription = "";
            int ClassFees = 0;
            byte MinimumAllowedAge = 0;
            byte DefalutValidityLength = 0;
            if (clsLicenseClassesData.Find( LicenseClassesID, ref ClassName, ref ClassDescription,ref MinimumAllowedAge, ref DefalutValidityLength, ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassesID, ClassName, ClassDescription, MinimumAllowedAge, DefalutValidityLength, ClassFees);
            }
            else
                return null;
        }
       
        public static clsLicenseClasses Find(string ClassName)
        {
            int LicenseClassesID = -1;
            string ClassDescription = "";
            int ClassFees = 0;
            byte MinimumAllowedAge = 0;
            byte DefalutValidityLength = 0;
           
            if (clsLicenseClassesData.FindByName(ref LicenseClassesID,  ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefalutValidityLength, ref ClassFees))
            {
                return new clsLicenseClasses(LicenseClassesID, ClassName, ClassDescription, MinimumAllowedAge, DefalutValidityLength, ClassFees);
            }
            else
                return null;
        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassesID = clsLicenseClassesData.AddNewLicenseClass( ClassName, ClassDescription, MinimumAllowedAge, DefalutValidityLength, ClassFees);
            return (LicenseClassesID != -1);
        }

        private bool _UpdateLicenseClass()
        {
            return clsLicenseClassesData.UpdateLicenseClass(LicenseClassesID, ClassName, ClassDescription, MinimumAllowedAge, DefalutValidityLength, ClassFees);
        }
        
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return _UpdateLicenseClass();


                default:
                    return false;

            }
        }



    }
}
