using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{

    public class TestAppointmentDTO
    {
        public int TestAppointmentID { get; set; }
        public byte TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationID { get; set; }
        public TestAppointmentDTO(int testAppointmentID, byte testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, float paidFees, int createdByUserID, bool isLocked, int? retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID==0? -1:retakeTestApplicationID;
        }
    }
}

