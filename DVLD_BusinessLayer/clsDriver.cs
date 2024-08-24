using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;
using DVlD_BusinessLayer.DTOs;

namespace DVlD_BusinessLayer
{

    public class clsDriver
    {
        public enum enMode { AddNew, Update };
        
        public enMode Mode = enMode.AddNew; 
        public DriverDTO DDTO
        {
            get
            {
                return (new DriverDTO((int)this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate));
            }
        }
        public Nullable<int> DriverID {  get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate { get; set; }   
        public clsPerson PersonInfo { get; set; }
        
        
       
        public clsDriver(DriverDTO DriverDTO ,enMode cMode=enMode.AddNew)
        {
            this.DriverID = DriverDTO.DriverID;
            this.PersonID = DriverDTO.PersonID;
            this.CreatedByUserID = DriverDTO.CreatedByUserID;
            this.CreatedDate = DriverDTO.CreatedDate;
            this.PersonInfo=clsPerson.Find(PersonID).Result;
            this.Mode = cMode;
        }


        public static clsDriver FindByDriverID(int DriverID)
        {
            

            //DriverDTO Driver= clsDriverData.FindByDriverID(DriverID);
            //if (Driver!=null)
            //{
                
            //    return new clsDriver(Driver,enMode.Update);
            //}
            //else
                return null;



        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            //DriverDTO Driver = clsDriverData.FindByPersonID(PersonID);
            //if (Driver != null)
            //{

            //    return new clsDriver(Driver, enMode.Update);
            //}
            //else
                return null;



        }
        public static List<DriverView> GetAllDriver()
        {
            return clsDriverData.GetAllDrivers();
        }
        
        private bool _AddNewDriver()
        {
            //this.DriverID=clsDriverData.AddNewDriver(DDTO);
            return (this.DriverID != null);
        }
       
        private bool _UpdateDriver()
        {

            return false;// clsDriverData.UpdateDriver(DDTO);
        }

       
        public static bool IsDriverExistByPersonID( int PersonID)
        {
            return clsDriverData.IsDriverExistByPersonID(PersonID);
        }

        public static bool IsDriverExists(int DriverID)
        {
            return clsDriverData.IsDriverExists(DriverID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return _UpdateDriver();


                default:
                    return false;
            }
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetAllDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetAllDriverInternationalLicenses(DriverID);
        }

        public  DataTable GetLicenses()
        {
            return clsLicense.GetAllDriverLicenses((int)DriverID);
        }

        public  DataTable GetInternationalLicenses()
        {
            return clsInternationalLicense.GetAllDriverInternationalLicenses((int)DriverID);
        }

















    }
}
