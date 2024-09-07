using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface BLLTestAppointment
    {
        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID { get; set; }
        public int LocalDrivingApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedBy { get; set; }
        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationID { set; get; }
        Task<IEnumerable<TestAppointmentDTO>> GetAllAppointmentAsync();
        Task<IEnumerable<AppointmentTestTypeDTO>> GetApplicationTestAppointmentsPerTestTypeAsync(int LicenseApplicationID, clsTestTypes.enTestType TestTypeID);
        Task<clsTestAppointment> FindAsync(int TestAppointmentID);
        Task<int?> AddNewAppointmentAsync(TestAppointmentDTO TADTO);
        Task<bool> UpdateAppointmentAsync(TestAppointmentDTO TADTO);
        Task<bool> ExistAppointmentAsync(int LicenseApplicationID, int TestTypeID);
        Task<clsTestAppointment> GetLastTestAppointmentAsync(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID);



    }
}
