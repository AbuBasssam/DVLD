using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using static DVlD_BusinessLayer.clsUser;

namespace DVlD_BusinessLayer
{

    public class clsDriver:IBLLDriver
    {
        public enum enDriverValidationTypes { EmptyFileds = 1, InvalidPersonID = 2, NullObject = 3, AlreadyDriver = 4, Valid = 5 };

        private IDriverData _DriverDAL {  get; set; }
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

        public clsDriver(IDriverData DriverDAL)
        {
            this._DriverDAL = DriverDAL;
        }
        
        private clsDriver(IDriverData DriverDAL,DriverDTO DriverDTO )
        {
            _DriverDAL = DriverDAL;
            this.DriverID = DriverDTO.DriverID;
            this.PersonID = DriverDTO.PersonID;
            this.CreatedByUserID = DriverDTO.CreatedByUserID;
            this.CreatedDate = DriverDTO.CreatedDate;
        }
        private  bool HasDriverHaveEmptyFileds(DriverDTO NewDriverDTO)
        {
            return (NewDriverDTO.PersonID == 0 || NewDriverDTO.CreatedByUserID == 0);
        }

        public enDriverValidationTypes IsValid(DriverDTO NewDriverDTO)
        {
            string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
            clsPeopleData Person = new clsPeopleData(ConnectionString);
            if (NewDriverDTO == null)
            {
                return enDriverValidationTypes.NullObject;
            }
            if (HasDriverHaveEmptyFileds(NewDriverDTO))
            {
                return enDriverValidationTypes.EmptyFileds;
            }

            if (!Person.IsPersonExistAsync(NewDriverDTO.PersonID).Result)
            {

                return enDriverValidationTypes.InvalidPersonID;
            }


            if (_DriverDAL.FindByPersonIDAsync(NewDriverDTO.PersonID) != null)
            {

                return enDriverValidationTypes.AlreadyDriver;
            }

            return enDriverValidationTypes.Valid;
        }
        public  async Task<clsDriver> FindByDriverID(int DriverID)
        {
            
            DriverDTO Driver= await _DriverDAL.FindByDriverIDAsync(DriverID);
           
            return (Driver!=null)? new clsDriver(_DriverDAL,Driver):null;
           
        }

        public  async Task<clsDriver> FindByPersonID(int PersonID)
        {

            DriverDTO Driver = await _DriverDAL.FindByPersonIDAsync(PersonID);

            return (Driver != null) ? new clsDriver(_DriverDAL, Driver) : null;

        }
       
        public  async Task<IEnumerable<DriverViewDTO>> GetAllDriver()
        {
            var Drivers = await _DriverDAL.GetAllDriversAsync();
            return Drivers;
        }

        public async Task<int?> AddNewDriver(DriverDTO DDTO)
        {
          return await _DriverDAL.AddNewDriverAsync(DDTO);
            
        }
       
        public async Task<bool>UpdateDriver(DriverDTO DDTO)
        {

            return await _DriverDAL.UpdateDriverAsync(DDTO);
        }

        public  async Task<bool> IsDriverExistByPersonID( int PersonID)
        {
            return await _DriverDAL.IsDriverExistByPersonIDAsync(PersonID);
        }

        public  async Task<bool> IsDriverExists(int DriverID)
        {
            return await _DriverDAL.IsDriverExistsAsync(DriverID);
        }

        public async Task<IEnumerable<DriverLicensesDTO>> GetDriverLicenses(int DriverID)
        {
            return await _DriverDAL.AllDriverLicenses(DriverID);
        }

        /*public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetAllDriverInternationalLicenses(DriverID);
        }*/

        public async Task<IEnumerable<DriverLicensesDTO>> GetLicenses()
        {
            return await GetDriverLicenses((int)DriverID);
        }

        /*public  DataTable GetInternationalLicenses()
        {
            return clsInternationalLicense.GetAllDriverInternationalLicenses((int)DriverID);
        }*/

        

       

        
        


    }
}
