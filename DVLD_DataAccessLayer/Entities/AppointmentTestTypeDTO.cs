using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class AppointmentTestTypeDTO
    {
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public bool IsLocked { get; set; }
        public AppointmentTestTypeDTO(int TestAppointmentID, DateTime AppointmentDate, float PaidFees, bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
        }

    }
}
