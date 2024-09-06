using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsDetainedLicenseData : IDALDetainedLicense
    {
        private readonly string _ConnectionString;
        public clsDetainedLicenseData( string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public async Task<DetainedLicenseDTO>GetDetainedLicenseInfoByID(int DetainID)
        {
            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DetainID", DetainID);
                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                                _MapReaderToDTO(reader);

                        }

                            
                    }

                       
                   

                }

                    


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            

            return null;
        }


        public async Task<DetainedLicenseDTO> GetDetainedLicenseInfoByLicenseID(int LicenseID)
        {

            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                                _MapReaderToDTO(reader);

                        }
                    }

                        
                }

            


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            

            return null;
        }

        public async Task<IEnumerable<DetainedLicenseDTO>> GetAllDetainedLicenses()
        {

            List <DetainedLicenseDTO> DLList = new List<DetainedLicenseDTO>();
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "select * from detainedLicenses_View order by IsReleased ,DetainID;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (await reader.ReadAsync())

                            {
                                DLList.Add(_MapReaderToDTO(reader));
                            }
                        }
                    }


                        

                       


                }

                   


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            

            return DLList;

        }

        public async Task<int?> AddNewDetainedLicense(DetainedLicenseDTO DLDTO)
        {
            int? DetainID = -1;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"INSERT INTO dbo.DetainedLicenses
                               (LicenseID,
                               DetainDate,
                               FineFees,
                               CreatedByUserID,
                               IsReleased
                               )
                            VALUES
                               (@LicenseID,
                               @DetainDate, 
                               @FineFees, 
                               @CreatedByUserID,
                               0
                             );
                            
                            SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", DLDTO.LicenseID);
                        command.Parameters.AddWithValue("@DetainDate", DLDTO.DetainDate);
                        command.Parameters.AddWithValue("@FineFees", DLDTO.FineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", DLDTO.CreatedByUserID);

                        connection.Open();

                        object result =await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            DetainID = insertedID;
                        }



                    }

                    
                }

                

           
                
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

            }

            


            return DetainID;

        }

        public async Task< bool> UpdateDetainedLicense(DetainedLicenseDTO DLDTO)
        {

            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainID=@DetainID;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DetainID", DLDTO.DetainID);
                        command.Parameters.AddWithValue("@LicenseID", DLDTO.LicenseID);
                        command.Parameters.AddWithValue("@DetainDate", DLDTO.DetainDate);
                        command.Parameters.AddWithValue("@FineFees", DLDTO.FineFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", DLDTO.CreatedByUserID);

                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();





                    }

                }

                    




            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }

            

            return (rowsAffected ==1);
        }


        public async Task<bool> ReleaseDetainedLicense(ReleaseLicenseDTO RLDTO)
        {

            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"UPDATE dbo.DetainedLicenses
                              SET IsReleased = 1, 
                              ReleaseDate = @ReleaseDate, 
                              ReleaseApplicationID = @ReleaseApplicationID   
                              WHERE DetainID=@DetainID;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DetainID", RLDTO.DetainID);
                        command.Parameters.AddWithValue("@ReleasedByUserID", RLDTO.ReleasedByUserID);
                        command.Parameters.AddWithValue("@ReleaseApplicationID", RLDTO.ReleaseApplicationID);
                        command.Parameters.AddWithValue("@ReleaseDate", DateTime.Now);

                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }

                        
                }

            

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }

            

            return (rowsAffected==1);
        }

        public async Task<bool> IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;
            try
            {
                using(var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"select IsDetained=1 
                            from detainedLicenses 
                            where 
                            LicenseID=@LicenseID 
                            and IsReleased=0;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);


                        connection.Open();

                        object result =await command.ExecuteScalarAsync();

                        if (result != null)
                        {
                            IsDetained = Convert.ToBoolean(result);
                        }
                    }

                        

                }

            
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

            }

            return IsDetained;
            

        }
        private DetainedLicenseDTO _MapReaderToDTO(IDataReader reader)
        {
            return new DetainedLicenseDTO
                (
                    (int)reader["DetainID"],
                    (int)reader["LicenseID"],
                    (DateTime)reader["DetainDate"],
                    Convert.ToSingle(reader["FineFees"]),
                    (int)reader["CreatedByUserID"],
                    (bool)reader["IsReleased"],
                    (reader["ReleaseDate"] == DBNull.Value)?null : (DateTime?)reader["ReleaseDate"],
                    (reader["ReleasedByUserID"] == DBNull.Value) ? null : (int?)reader["ReleasedByUserID"],
                    (reader["ReleaseApplicationID"] == DBNull.Value) ? null : (int?)reader["ReleaseApplicationID"]
                );








                
        }
    }
}
