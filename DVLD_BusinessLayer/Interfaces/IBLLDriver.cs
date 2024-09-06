using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVlD_BusinessLayer.clsDriver;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IBLLDriver
    {
        public Nullable<int> DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        Task<clsDriver> FindByDriverID(int DriverID);
        Task<clsDriver> FindByPersonID(int PersonID);
        Task<IEnumerable<DriverViewDTO>> GetAllDriver();
        Task<int?> AddNewDriver(DriverDTO DDTO);
        Task<bool> UpdateDriver(DriverDTO DDTO);
        Task<bool> IsDriverExistByPersonID(int PersonID);
        Task<bool> IsDriverExists(int DriverID);
        /// <summary>
        ///  check the empty and null fileds ,valid PersonID and not allow to add the same driver twice
        /// </summary>
        /// <param name="NewDriverDTO">
        /// Variable contian all Driver data
        /// </param>
        /// <returns></returns>
        enDriverValidationTypes IsValid(DriverDTO NewDriverDTO);


    }
}
