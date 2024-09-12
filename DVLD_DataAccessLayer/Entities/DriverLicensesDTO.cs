using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class DriverLicensesDTO
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public string ClassName {  get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte IsActive { get; set; }
        public DriverLicensesDTO(int LicenseID, int ApplicationID, string ClassName, DateTime IssueDate, DateTime ExpirationDate, byte IsActive)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.ClassName = ClassName;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;


        }
    }
}
