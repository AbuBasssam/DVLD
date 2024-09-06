using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class LicenseDTO
    {
        public int LicenseID;
        public int ApplicationID;
        public int DriverID;
        public byte LicenseClass;
        public DateTime IssueDate;
        public DateTime ExpirationDate;
        public string Notes;
        public float PaidFees;
        public byte IsActive;
        public byte IssueReason;
        public int CreatedByUserID;
        public LicenseDTO(int LicenseID, int ApplicationID,int DriverID, byte LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, byte IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            
                
                
        }
    }
}
