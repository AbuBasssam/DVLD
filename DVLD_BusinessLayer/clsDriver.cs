using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;

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
            //this.PersonInfo=clsPerson.Find(PersonID).Result;
            this.Mode = cMode;
        }

        public static async Task<clsDriver> FindByDriverID(int DriverID)
        {
            
            DriverDTO Driver= MapToDTO(await clsDriverData.FindByDriverIDAsync(DriverID));
           
            return (Driver!=null)? new clsDriver(Driver,enMode.Update):null;
           
        }

        public static async Task<clsDriver> FindByPersonID(int PersonID)
        {

            DriverDTO Driver = MapToDTO(await clsDriverData.FindByPersonIDAsync(PersonID));

            return (Driver != null) ? new clsDriver(Driver, enMode.Update) : null;

        }
       
        public static async Task<IEnumerable<DriverViewDTO>> GetAllDriver()
        {
            var Drivers = await clsDriverData.GetAllDriversAsync();
            return Drivers;
        }

        private async Task<bool> _AddNewDriver()
        {
            this.DriverID= await clsDriverData.AddNewDriverAsync(DDTO);
            return (this.DriverID != null);
        }
       
        private async Task<bool> _UpdateDriver()
        {

            return await clsDriverData.UpdateDriverAsync(DDTO);
        }

        public static async Task<bool> IsDriverExistByPersonID( int PersonID)
        {
            return await clsDriverData.IsDriverExistByPersonIDAsync(PersonID);
        }

        public static async Task<bool> IsDriverExists(int DriverID)
        {
            return await clsDriverData.IsDriverExistsAsync(DriverID);
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if ( await _AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return await _UpdateDriver();


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

        private static DriverDTO MapToDTO(DriverDTO Driver)
        {
            return new DriverDTO(

               Driver.DriverID,
               Driver.PersonID,
               Driver.CreatedByUserID,
               Driver.CreatedDate
               

            );
        }

       

        
        


    }
}
