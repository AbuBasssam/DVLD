using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using DVLD_DataAccessLayer.Entities;
using System.Threading;
using DVLD_DataAccessLayer.Interfaces;

namespace DVLD_DataAccessLayer
{
    public  class clsTestAppointmentData:IDALTestAppointment
    {
        private string _ConnectionString {  get; set; }
        public clsTestAppointmentData(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public async Task<DataTable> GetAllAppointmentAsync()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM TestAppointments";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }

            return dt;
        }

        public  async Task<DataTable> GetApplicationTestAppointmentsPerTestTypeAsync(int LicenseApplicationID, int TestTypeID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }

            return dt;
        }

        public  async Task<TestAppointmentDTO> FindByIDAsync(int TestAppointmentID)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                           return _MapReaderToTestAppointmentDTO(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }

            return null;
        }

        public  async Task<int?> AddNewAppointmentAsync(TestAppointmentDTO TestAppointmentDTO)
        {
            int? TestAppointmentID = null;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"INSERT INTO TestAppointments (TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                             VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID);
                             SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestTypeID", TestAppointmentDTO.TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",TestAppointmentDTO.LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate",TestAppointmentDTO.AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", TestAppointmentDTO.PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID",TestAppointmentDTO.CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", TestAppointmentDTO.IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID",TestAppointmentDTO.RetakeTestApplicationID == -1 ? (object)DBNull.Value :TestAppointmentDTO.RetakeTestApplicationID);

                try
                {
                    await connection.OpenAsync();
                    object result = await command.ExecuteScalarAsync();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestAppointmentID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }

            return TestAppointmentID;
        }

        public  async Task<bool> UpdateAppointmentAsync(TestAppointmentDTO TestAppointmentDTO)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"UPDATE TestAppointments
                             SET TestTypeID = @TestTypeID,
                                 LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                 AppointmentDate = @AppointmentDate,
                                 PaidFees = @PaidFees,
                                 CreatedByUserID = @CreatedByUserID,
                                 IsLocked = @IsLocked,
                                 RetakeTestApplicationID = @RetakeTestApplicationID
                             WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestAppointmentID",TestAppointmentDTO.TestAppointmentID);
                command.Parameters.AddWithValue("@TestTypeID", TestAppointmentDTO.TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",TestAppointmentDTO.LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@AppointmentDate",TestAppointmentDTO. AppointmentDate);
                command.Parameters.AddWithValue("@PaidFees", TestAppointmentDTO.PaidFees);
                command.Parameters.AddWithValue("@CreatedByUserID", TestAppointmentDTO.CreatedByUserID);
                command.Parameters.AddWithValue("@IsLocked", TestAppointmentDTO.IsLocked);
                command.Parameters.AddWithValue("@RetakeTestApplicationID",TestAppointmentDTO. RetakeTestApplicationID == -1 ? (object)DBNull.Value : TestAppointmentDTO.RetakeTestApplicationID);

                try
                {
                    await connection.OpenAsync();
                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                    return false;
                }
            }

            return rowsAffected==1;
        }

        public  async Task<bool> ExistAppointmentAsync(int LicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT 1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID AND IsLocked = 0";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        isFound = await reader.ReadAsync();
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }

        public  async Task<int> TrailAsync(int TestTypeID, int LicenseApplicationID)
        {
            int numberOfTrails = -1;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"SELECT COUNT(*) FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID AND IsLocked = 1";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LicenseApplicationID);

                try
                {
                    await connection.OpenAsync();
                    object result = await command.ExecuteScalarAsync();
                    if (result != null && int.TryParse(result.ToString(), out int trails))
                    {
                        numberOfTrails = trails;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }

            return numberOfTrails;
        }

        public  async Task<bool> GetLastTestAppointmentAsync(
            int LocalDrivingLicenseApplicationID, int TestTypeID,
            CancellationToken cancellationToken,
            TestAppointmentDTO appointmentDto)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"SELECT TOP 1 * FROM TestAppointments
                             WHERE TestTypeID = @TestTypeID 
                             AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                             ORDER BY TestAppointmentID DESC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    await connection.OpenAsync(cancellationToken);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken))
                    {
                        if (await reader.ReadAsync(cancellationToken))
                        {
                            isFound = true;
                            _MapReaderToTestAppointmentDTO(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }

        private TestAppointmentDTO _MapReaderToTestAppointmentDTO(IDataReader reader)
        {
            return new TestAppointmentDTO
            (
             reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
             reader.GetInt32(reader.GetOrdinal("TestTypeID")),
             reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
             reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
             reader.GetFloat(reader.GetOrdinal("PaidFees")),
             reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
             reader.GetBoolean(reader.GetOrdinal("IsLocked")),
             reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID"))
            );
        }
    }


}

