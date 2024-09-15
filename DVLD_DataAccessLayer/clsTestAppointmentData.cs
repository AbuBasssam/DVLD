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
        private readonly string _ConnectionString;
        public clsTestAppointmentData(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public async Task<IEnumerable<TestAppointmentDTO>> GetAllAppointmentAsync()
        {
            List<TestAppointmentDTO> TestAppointmentsList = new List<TestAppointmentDTO>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM TestAppointments";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                       while (await reader.ReadAsync())
                       {
                            TestAppointmentsList.Add(_MapReaderToTestAppointmentDTO((reader)));
                       }
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }

            return TestAppointmentsList;
        }

        public  async Task<IEnumerable<AppointmentTestTypeDTO>> GetApplicationTestAppointmentsPerTestTypeAsync(int LicenseApplicationID, byte TestTypeID)
        {
            List<AppointmentTestTypeDTO> ATTList = new List<AppointmentTestTypeDTO>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT TestAppointmentID, AppointmentDate, PaidFees, IsLocked FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                ATTList.Add(_MapReaderToAppointmentTestTypeDTO(reader));
                            }
                        }
                    }

                }
               
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
                return ATTList;
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


        public  async Task<TestAppointmentDTO> GetLastTestAppointmentAsync(int LocalDrivingLicenseApplicationID,byte TestTypeID)
        {
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
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
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

        public async Task<int> GetTestIDAsync(int TestAppointmentID)
        {
            int TestID = -1;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


                        connection.Open();

                        object result =await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestID = insertedID;
                        }
                    }


                        

                }

           
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

           


            return TestID;

        }

        private TestAppointmentDTO _MapReaderToTestAppointmentDTO(IDataReader reader)
        {
            return new TestAppointmentDTO
            (
             reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
             Convert.ToByte(reader.GetInt32(reader.GetOrdinal("TestTypeID"))),
             reader.GetInt32(reader.GetOrdinal("LocalDrivingLicenseApplicationID")),
             reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
            Convert.ToSingle( reader.GetDecimal(reader.GetOrdinal("PaidFees"))),
             reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
             reader.GetBoolean(reader.GetOrdinal("IsLocked")),
             reader.IsDBNull(reader.GetOrdinal("RetakeTestApplicationID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("RetakeTestApplicationID"))
            );
        }
        private AppointmentTestTypeDTO _MapReaderToAppointmentTestTypeDTO(IDataReader reader)
        {
            return new AppointmentTestTypeDTO
                (
                    reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                    reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                    Convert.ToSingle(reader.GetDecimal(reader.GetOrdinal("PaidFees"))),
                    reader.GetBoolean(reader.GetOrdinal("IsLocked"))

                );
        }
    }


}

