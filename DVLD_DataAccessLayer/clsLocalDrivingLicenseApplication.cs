using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;

namespace DVLD_DataAccessLayer
{
    public  class clsLocalDrivingLicenseApplicationData:IDALLocalDrivingLicenseApplication
    {
        private readonly string _ConnectionString;
        public clsLocalDrivingLicenseApplicationData(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
        }
        
        public async Task<IEnumerable<LDLApplicatoinViewDTO>> GetAllApplications()
        {

            List<LDLApplicatoinViewDTO> LDLApplicatoinsList = new List<LDLApplicatoinViewDTO>();
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "Select * from LocalDrivingLicenseApplications_View order by ApplicationDate Desc";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                LDLApplicatoinsList.Add(_MapReaderToViewDTO(reader));
                            }
                        }

                    }


                        




                }



                


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return LDLApplicatoinsList;

        }

        public async Task< LDLApplicatoinDTO> Find(int LocalDrivingLicenseApplicationID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "Select * from LocalDrivingLicenseApplications Where LocalDrivingLicenseApplicationID= @LocalDrivingLicenseApplicationID ";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                                return _MapReaderToDTO(reader);
                        }

                           

                        
                    }
                        

                }
            }
            
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return null;
        }

        public async Task<LDLApplicatoinDTO> FindByApplicationID(int ApplicationID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // The record was found
                                return _MapReaderToDTO(reader);
                                

                            }
                        }

                        
                    }

                        
                    
                }

                    



            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return null;
        }

        public async Task<int?> AddNewApplication(LDLApplicatoinDTO lDLApplicatoinDTO)
        {
            int ?LocalDrivingLicenseApplicationID = null;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"INSERT INTO LocalDrivingLicenseApplications (ApplicationID,LicenseClassID)
                             VALUES (@ApplicationID,@LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ApplicationID", lDLApplicatoinDTO.ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClassID", lDLApplicatoinDTO. LicenseClassID);

                    connection.Open();

                    object result =await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LocalDrivingLicenseApplicationID = insertedID;
                        }
                    }
               

            }
                



           
                
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            return LocalDrivingLicenseApplicationID;
        }

        public async Task<bool> UpdateApplication(LDLApplicatoinDTO lDLApplicatoinDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Update LocalDrivingLicenseApplications 
                            set ApplicationID=@ApplicationID,
                                LicenseClassID=@LicenseClassID
                             where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", lDLApplicatoinDTO.ApplicationID);
                        command.Parameters.AddWithValue("@LicenseClassID", lDLApplicatoinDTO.LicenseClassID);
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", lDLApplicatoinDTO.LocalDrivingLicenseApplicationID);

                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    
                }
                    

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }

            

            return (rowsAffected==1);
        }

        public async Task<int?> IsAlreadyExist(string NationalNo, string ClassName)
        {
            int? LocalDrivingLicenseApplicationID = null;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"SELECT LocalDrivingLicenseApplicationID FROM  LocalDrivingLicenseApplications_View WHERE( NationalNo =@NationalNo and ClassName=@ClassName);";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);
                        command.Parameters.AddWithValue("@ClassName", ClassName);


                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                            }


                        }


                    }


                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }
            return LocalDrivingLicenseApplicationID;
        }

        public async Task<bool> DeleteLocalLicenseApp(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Delete LocalDrivingLicenseApplications
                                    where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();

                    }





                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                return false;
            }

            return (rowsAffected==1);
        }

        public async Task<int> PassedTests(int LocalDrivingLicenseApplicationID)
        {
            int PassedTest = -1;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select PassedTestCount From LocalDrivingLicenseApplications_View where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                        connection.Open();

                        object result =await command.ExecuteScalarAsync();


                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            PassedTest = insertedID;
                        }
                    }
                        
                }
                    
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            
            return PassedTest;
        }

        public async Task<bool> DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            bool Result = false;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @" SELECT top 1 TestResult
                            FROM LocalDrivingLicenseApplications INNER JOIN TestAppointments
                            ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                            INNER JOIN Tests
                            ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                        connection.Open();

                        object result =await command.ExecuteScalarAsync();

                        if (result != null && bool.TryParse(result.ToString(), out bool returnedResult))
                        {
                            Result = returnedResult;
                        }
                    }

                   
                }

                
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            return Result;

        }

        public async Task<bool> DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {


            bool IsFound = false;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                            ORDER BY TestAppointments.TestAppointmentID desc";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                        connection.Open();

                        object result = await command.ExecuteScalarAsync();

                        if (result != null)
                        {
                            IsFound = true;
                        }

                    }

                    
                }

                    
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            

            return IsFound;

        }

        public async Task<byte> TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte TotalTrialsPerTest = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @" SELECT TotalTrialsPerTest = count(TestID)
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                 Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                            AND(TestAppointments.TestTypeID = @TestTypeID)
                       ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                        connection.Open();

                        object result = await command.ExecuteScalarAsync();

                        if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                        {
                            TotalTrialsPerTest = Trials;
                        }
                    }

                    
                }

                    
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            return TotalTrialsPerTest;

        }

        public async Task<bool> IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool Result = false;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @" SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


                    connection.Open();

                    object result =await command.ExecuteScalarAsync();


                    if (result != null)
                    {
                        Result = true;
                    }
                }

                    

            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

           return Result;

        }
        
        private LDLApplicatoinDTO _MapReaderToDTO(IDataReader reader)
        {
            return new LDLApplicatoinDTO
                (
                 (int)reader["LocalDrivingLicenseApplicationID"],
                 (int)reader["ApplicationID"],
                 (int)reader["LicenseClassID"]
                 );

        }
        private LDLApplicatoinViewDTO _MapReaderToViewDTO(IDataReader reader)
        {
            return new LDLApplicatoinViewDTO
                (
                    (int)reader["LocalDrivingLicenseApplicationID"],
                    (string)reader["ClassName"],
                    (string)reader["NationalNo"],
                    (string)reader["FullName"],
                    Convert.ToDateTime(reader["ApplicationDate"]),
                    Convert.ToByte(reader["PassedTestCount"]),
                    (string)reader["Status"]
                 );

        }


    }


}















