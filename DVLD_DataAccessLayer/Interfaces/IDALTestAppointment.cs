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
        Task<DataTable> GetAllAppointmentAsync();
        Task<DataTable> GetApplicationTestAppointmentsPerTestTypeAsync(int LicenseApplicationID, int TestTypeID);
        Task<TestAppointmentDTO> FindByIDAsync(int TestAppointmentID);
        Task<int?> AddNewAppointmentAsync(TestAppointmentDTO TestAppointmentDTO);
        Task<bool> UpdateAppointmentAsync(TestAppointmentDTO TestAppointmentDTO);
        Task<bool> ExistAppointmentAsync(int LicenseApplicationID, int TestTypeID);
        Task<int> TrailAsync(int TestTypeID, int LicenseApplicationID);
        Task<bool> GetLastTestAppointmentAsync(int LocalDrivingLicenseApplicationID, int TestTypeID, CancellationToken cancellationToken, TestAppointmentDTO appointmentDto);
    }
}
