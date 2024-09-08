using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Security.Policy;
using System.Diagnostics;
using DVLD_DataAccessLayer.Entities;
using System.Runtime.Remoting.Messaging;
using DVLD_DataAccessLayer.Interfaces;


namespace DVLD_DataAccessLayer
{
    public class clsApplicationData:IDALApplication
    {
        private readonly string _ConnectionString;
        public clsApplicationData(string connectionString)
        {
            _ConnectionString = connectionString;
        }
        public async Task<IEnumerable<ApplicationDTO>> GetAllApplications()
        {
            List<ApplicationDTO> ApplicationList = new List<ApplicationDTO>();

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "select * from Applications order by ApplicationDate desc";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                           while(await reader.ReadAsync())
                           {
                                ApplicationList.Add(_MapReaderToDTO(reader));
                           }
                        }


                    }



                }



            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }


            return ApplicationList;

        }

        public async Task<int?> AddNewApplication(ApplicationDTO ADTO)
        {
            Nullable<int> ApplicationID = null;


            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"INSERT INTO Applications (ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus, LastStatusDate,PaidFees,CreatedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantPersonID", ADTO.ApplicationPersonID);
                        command.Parameters.AddWithValue("@ApplicationDate", ADTO.ApplicationDate);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ADTO.ApplicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationStatus", ADTO.Staute);
                        command.Parameters.AddWithValue("@LastStatusDate", ADTO.LastStauteDate);
                        command.Parameters.AddWithValue("@CreatedByUserID",ADTO.CreatedBy);
                        command.Parameters.AddWithValue("@PaidFees", ADTO.PeadFees);
                        connection.Open();

                        object result =await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ApplicationID = insertedID;
                        }


                    }

                }

            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

            }

            return ApplicationID;

        }

        public async Task< bool> UpdateApplication(ApplicationDTO ADTO)
        {
            int rowsAffected = 0;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Update Applications set ApplicantPersonID=@ApplicantPersonID,
                                    ApplicationDate=@ApplicationDate,
                                    ApplicationTypeID=@ApplicationTypeID,
                                    ApplicationStatus=@ApplicationStatus,
                                    LastStatusDate=@LastStatusDate,
                                    PaidFees=@PaidFees,
                                    CreatedByUserID=@CreatedByUserID
                                    where ApplicationID=@ApplicationID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID",ADTO.ApplicationID);
                        command.Parameters.AddWithValue("@ApplicantPersonID", ADTO.ApplicationPersonID);
                        command.Parameters.AddWithValue("@ApplicationDate", ADTO .ApplicationDate);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ADTO .ApplicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationStatus", ADTO .Staute);
                        command.Parameters.AddWithValue("@LastStatusDate", ADTO .LastStauteDate);
                        command.Parameters.AddWithValue("@PaidFees", ADTO.PeadFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", ADTO.CreatedBy);

                        connection.Open();
                        rowsAffected =await command.ExecuteNonQueryAsync();

                    }

                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }


            return (rowsAffected ==1);
        }

        public  async Task<ApplicationDTO> Find(int ApplicationID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
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

        public async Task< bool> DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete Applications
                                    where ApplicationID=@ApplicationID";


                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        connection.Open();
                        rowsAffected =await command.ExecuteNonQueryAsync();

                    }

                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }


            return (rowsAffected ==1);
        }

        public async Task<bool> IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;


            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
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

        public async Task<bool> DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (await GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public async Task<int> GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;


            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                        connection.Open();
                        object result =await command.ExecuteScalarAsync();


                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }
                    }


                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return ActiveApplicationID;
        }

        public async Task<int> GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {

                    string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        connection.Open();
                        object result =await command.ExecuteScalarAsync();


                        if (result != null && int.TryParse(result.ToString(), out int AppID))
                        {
                            ActiveApplicationID = AppID;
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return ActiveApplicationID;
        }

        public async Task<bool> UpdateStatus(int ApplicationID, short NewStatus)
        {

            int rowsAffected = 0;


            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@NewStatus", NewStatus);
                        command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);

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


            return (rowsAffected ==1);
        }
        
        private ApplicationDTO _MapReaderToDTO(IDataReader reader)
        {
            return new ApplicationDTO
            (
                (int)reader["ApplicationID"],
                (int)reader["ApplicantPersonID"],
                (DateTime)reader["ApplicationDate"],
                (int)reader["ApplicationTypeID"],
                Convert.ToByte(reader["ApplicationStatus"]),
                (DateTime)reader["LastStatusDate"],
                Convert.ToSingle(reader["PaidFees"]),
                (int)reader["CreatedByUserID"]
            );



        }
       

    
                                     
                                     
                                      
        
    
    
    
    }
}
    