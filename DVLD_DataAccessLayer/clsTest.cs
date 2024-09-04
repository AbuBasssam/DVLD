using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsTestData:IDALTest
    {
        public async Task<TestDTO> FindAsync(int TestID)
        {
            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    await connection.OpenAsync();
                    var reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        _MapReaderToTest(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }
            return null;
        }

        public async Task<int?> AddNewTestAsync(TestDTO testDTO)
        {
            int? TestID = null;
            string query = @"INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                         VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                         UPDATE TestAppointments 
                            SET IsLocked = 1 WHERE TestAppointmentID = @TestAppointmentID;
                         SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", testDTO.TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", testDTO.TestResult);
                command.Parameters.AddWithValue("@CreatedByUserID", testDTO.CreatedBy);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(testDTO.Notes) ? DBNull.Value : (object)testDTO.Notes);

                try
                {
                    await connection.OpenAsync();
                    object result = await command.ExecuteScalarAsync();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }
            return TestID;
        }

        public async Task<bool> UpdateTestAsync(TestDTO testDTO)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Tests  
                         SET TestAppointmentID = @TestAppointmentID,
                             TestResult = @TestResult, 
                             Notes = @Notes
                         WHERE TestID = @TestID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TestAppointmentID", testDTO.TestAppointmentID);
                command.Parameters.AddWithValue("@TestResult", testDTO.TestResult);
                command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(testDTO.Notes) ? DBNull.Value : (object)testDTO.Notes);
                command.Parameters.AddWithValue("@TestID", testDTO.TestID);

                try
                {
                    await connection.OpenAsync();
                    rowsAffected = await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }
            return (rowsAffected ==1);
        }

        public async Task<byte> GetPassedTestCountAsync(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            string query = @"SELECT COUNT(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                         WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestResult = 1";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    await connection.OpenAsync();
                    object result = await command.ExecuteScalarAsync();

                    if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                    {
                        PassedTestCount = ptCount;
                    }
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                }
            }
            return PassedTestCount;
        }

        public async Task<bool> GetLastTestByPersonAndTestTypeAndLicenseClassAsync(
            int PersonID, int LicenseClassID, int TestTypeID, TestDTO testDTO)
        {
            bool isFound = false;
            string query = @"SELECT TOP 1 Tests.TestID, 
                         Tests.TestAppointmentID, Tests.TestResult, 
                         Tests.Notes, Tests.CreatedByUserID
                         FROM LocalDrivingLicenseApplications INNER JOIN
                         Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID INNER JOIN
                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                         WHERE Applications.ApplicantPersonID = @PersonID 
                         AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                         AND TestAppointments.TestTypeID = @TestTypeID
                         ORDER BY Tests.TestAppointmentID DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        isFound = true;
                        testDTO.TestID = (int)reader["TestID"];
                        testDTO.TestAppointmentID = (int)reader["TestAppointmentID"];
                        testDTO.TestResult = Convert.ToByte(reader["TestResult"]);
                        testDTO.Notes = reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : "";
                        testDTO.CreatedBy = (int)reader["CreatedByUserID"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    clsEventLog.SetEventLog(ex.Message);
                    isFound = false;
                }
            }
            return isFound;
        }

        private TestDTO _MapReaderToTest(IDataReader reader)
        {
            return new TestDTO
                         (
                            (int)reader["TestID"],
                            (int)reader["TestAppointmentID"],
                            Convert.ToByte(reader["TestResult"]),
                            reader["Notes"] != DBNull.Value ? (string)reader["Notes"] : "",
                            (int)reader["CreatedByUserID"]

                         );
        }
    }
    /*public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
            (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
              ref int TestAppointmentID, ref bool TestResult,
              ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }*/
}
