using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DVLD_DataAccessLayer.Entities;
using static System.Net.Mime.MediaTypeNames;
using DVLD_DataAccessLayer.Interfaces;

namespace DVLD_DataAccessLayer
{
    public class clsInternationalInternationalLicenseData: IDALInternationalLicense
    {
        private readonly string _ConnectionString;
        public clsInternationalInternationalLicenseData(string connectionString)
        {
            _ConnectionString = connectionString;   
        }
        public async Task<IEnumerable<InternationalLicenseDTO>> GetAllInternationalLicenses()
        {
            List<InternationalLicenseDTO> InternationalLicenseList = new List<InternationalLicenseDTO>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive FROM InternationalLicenses";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                InternationalLicenseList.Add(_MapReaderToDTO(reader));
                            }

                        }

                        
                    }


                        

                }

                    


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return InternationalLicenseList;

        }

        public async Task<IEnumerable<InternationalLicenseDTO>> GetAllDriverInternationalLicenses(int DriverID)
        {
            List<InternationalLicenseDTO> DriverInternationalLicenseList = new List<InternationalLicenseDTO>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "Select InternationalLicenseID,ApplicationID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive From InternationalLicenses where DriverID=@DriverID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);

                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DriverInternationalLicenseList.Add(_MapReaderToDTO( reader));
                            }

                        }


                    }
                        
                }

                    


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            

            return DriverInternationalLicenseList;

        }
       
        public async Task<InternationalLicenseDTO> FindByInternationalLicenseID(int InternationalLicenseID)
        {

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select * From InternationalLicenses where InternationalLicenseID=@InternationalLicenseID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
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
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
           

            return null;





        }

        public async Task<InternationalLicenseDTO> FindByInternationalLicenseIDAndIssuedUsingLocalLicenseID(int InternationalLicenseID, int IssuedUsingLocalLicenseID)
        {
            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select * From InternationalLicenses where InternationalLicenseID=@InternationalLicenseID and IssuedUsingLocalLicenseID=@IssuedUsingLocalLicenseID ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                        command.Parameters.AddWithValue("@InternationalLicenseClass", IssuedUsingLocalLicenseID);




                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return _MapReaderToDTO(reader);



                            }
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

        public async Task<InternationalLicenseDTO> FindByDriverID( int DriverID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select * From InternationalLicenses where DriverID=@DriverID";


                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);


                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
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
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
           
            return null;





        }

        public async Task<InternationalLicenseDTO> FindByApplicationID( int ApplicationID)
        {

            using (var connection = new SqlConnection(_ConnectionString))
            {
                string query = @"Select * From InternationalLicenses where ApplicationID=@ApplicationID";

                try
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // The record was found
                                return _MapReaderToDTO(reader);



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
        }


        public async Task<int?> AddNewInternationalInternationalLicense(InternationalLicenseDTO ILDTO)
        {
            int? InternationalLicenseID = null;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"INSERT INTO InternationalLicenses(ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive,CreatedByUserID)
                             VALUES(@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ILDTO.ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", ILDTO.DriverID);
                        command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", ILDTO.IssuedUsingLocalLicenseID);
                        command.Parameters.AddWithValue("@IssueDate", ILDTO.IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", ILDTO.ExpirationDate);
                        command.Parameters.AddWithValue("@IsActive", ILDTO.IsActive);
                        command.Parameters.AddWithValue("@CreatedByUserID", ILDTO.CreatedByUserID);

                        connection.Open();

                        object result =await command.ExecuteScalarAsync();


                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            InternationalLicenseID = insertedID;
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
        
        public async Task<bool> UpdateInternationalLicense(InternationalLicenseDTO ILDTO)
        {
            int rowsAffected = 0;

            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"UPDATE InternationalLicenses
                                  SET ApplicationID = @ApplicationID,
                                      DriverID=@DriverID,
                                      IssuedUsingLocalLicenseID=@IssuedUsingLocalLicenseID,
                                      IssueDate=@IssueDate,
                                      ExpirationDate=@ExpirationDate,
                                      IsActive=@IsActive,
                                     CreatedByUserID= @CreatedByUserID
                                      WHERE InternationalLicenseID=@InternationalLicenseID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ILDTO.ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", ILDTO.DriverID);
                        command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", ILDTO.IssuedUsingLocalLicenseID);
                        command.Parameters.AddWithValue("@IssueDate", ILDTO.IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", ILDTO.ExpirationDate);
                        command.Parameters.AddWithValue("@IsActive", ILDTO.IsActive);
                        command.Parameters.AddWithValue("@CreatedByUserID", ILDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@InternationalLicenseID", ILDTO.InternationalLicenseID);

                        connection.Open();
                        rowsAffected =await command.ExecuteNonQueryAsync();
                    }
                    
                }
                    

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }

            return (rowsAffected == 1);

        }

        public async Task<bool> DeleteInternationalLicense(int InternationalLicenseID)
        {

            int rowsAffected = 0;
            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Delete InternationalLicenses 
                                where InternationalLicenseID = @InternationalLicenseID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);


                        connection.Open();

                        rowsAffected = await command.ExecuteNonQueryAsync();

                    }

                }

                    

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            
            return (rowsAffected==1);

        }

        public async Task<bool> AlreadyHaveInternationalLicense(int DriverID)
        {
            bool isFound = false;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT Found=1 FROM InternationalLicenses WHERE DriverID = @DriverID ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            isFound = reader.HasRows;

                        }
                    }

                        


                }

                    
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
           

            return isFound;
        }

        public async Task<bool> IsLicneseExist(int InternationalLicenseID)
        {
            bool isFound = false;
            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT Found=1 FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);


                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            isFound = reader.HasRows;

                        }
                    }

                        


                }

                    
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            

            return isFound;
        }

        private InternationalLicenseDTO _MapReaderToDTO(IDataReader reader)
        {
            return new InternationalLicenseDTO
                (
                    (int)reader["InternationalLicenseID"],
                    (int)reader["ApplicationID"],
                    (int)reader["DriverID"],
                    (int)reader["IssuedUsingLocalLicenseID"],
                    Convert.ToDateTime(reader["IssueDate"]),
                    Convert.ToDateTime(reader["ExpirationDate"]),
                    Convert.ToByte(reader["IsActive"]),
                    (int)reader["CreatedByUserID"]

                );
        }


    }
}
