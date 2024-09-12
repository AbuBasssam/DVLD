using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class ApplicationDTO
    {
        public int? ApplicationID { get; set; }
        public int ApplicationPersonID {  get; set; }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }
        
        public byte Staute { get; set; }
        
        public DateTime LastStauteDate { get; set; }
        
        public float PeadFees { get; set; }
        
        public  int CreatedBy {  get; set; }

        public ApplicationDTO(int? ApplicationID, int applicationPersonID, DateTime applicationDate, int applicationTypeID, byte staute, DateTime lastStauteDate, float peadFees, int createdBy)
        {
            this.ApplicationID = ApplicationID;
            ApplicationPersonID = applicationPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            Staute = staute;
            LastStauteDate = lastStauteDate;
            PeadFees = peadFees;
            CreatedBy = createdBy;
        }
    }
}
