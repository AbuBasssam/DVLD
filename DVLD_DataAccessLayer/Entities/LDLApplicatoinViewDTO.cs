using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class LDLApplicatoinViewDTO
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public string ClassName {  get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
        public byte PassedTestCount {  get; set; }
        public string Status {  get; set; }
        
        public LDLApplicatoinViewDTO(int LocalDrivingLicenseApplicationID,string ClassName, string NationalNo, string FullName, DateTime ApplicationDate, byte PassedTestCount, string Status)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ClassName = ClassName;
            this.NationalNo = NationalNo;
            this.FullName = FullName;
            this.ApplicationDate = ApplicationDate;
            this.PassedTestCount = PassedTestCount;
            this.Status = Status;
           
        }
    }
}
