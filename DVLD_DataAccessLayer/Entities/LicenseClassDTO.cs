using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class LicenseClassDTO
    {
       public int LicenseClassesID { get; set; }
        
       public string ClassName {  get; set; }
        
       public string ClassDescription {  get; set; }
       public byte MinimumAllowedAge {  get; set; } 

       public byte DefalutValidityLength { get; set; }
       public float ClassFees { get; set; }
        public LicenseClassDTO(int LicenseClassesID, string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefalutValidityLength, float ClassFees)
        {
            this.LicenseClassesID = LicenseClassesID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefalutValidityLength = DefalutValidityLength;
            this.ClassFees = ClassFees;
        }   

    }
}
