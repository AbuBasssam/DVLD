using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDALTestAppointment
    {
        Task<IEnumerable<TestAppointmentDTO>> GetAllAppointmentAsync();
        Task<IEnumerable<AppointmentTestTypeDTO>> GetApplicationTestAppointmentsPerTestTypeAsync(int LicenseApplicationID, byte TestTypeID);
        Task<TestAppointmentDTO> FindByIDAsync(int TestAppointmentID);
        Task<int?> AddNewAppointmentAsync(TestAppointmentDTO TestAppointmentDTO);
        Task<bool> UpdateAppointmentAsync(TestAppointmentDTO TestAppointmentDTO);
        Task<bool> ExistAppointmentAsync(int LicenseApplicationID, int TestTypeID);
        Task<TestAppointmentDTO> GetLastTestAppointmentAsync(int LocalDrivingLicenseApplicationID, byte TestTypeID);
        Task<int> GetTestIDAsync(int TestAppointmentID);
    }
}
