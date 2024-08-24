using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class DriverView
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte NumberOfActiveLicenses { get; set; }
        public DriverView(int DriverID, int PersonID, string NationalNo, string FullName, DateTime CreatedDate, byte NumberOfActiveLicenses)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FullName = FullName;
            this.CreatedDate = CreatedDate;
            this.NumberOfActiveLicenses = NumberOfActiveLicenses;


        }
    }

}
