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


namespace DVLD_DataAccessLayer
{
    public static class clsApplicationData
    {

        public static  DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();
           
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "select * from ApplicationsList_View order by ApplicationDate desc";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)

                            {
                                dt.Load(reader);
                            }
                        }

                            
                    }

                        

                }



            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }


            return dt;

        }

        public static Nullable<int> AddNewApplication( int ApplicationPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte Staute, DateTime LastStauteDate, int PeadFees, int CreatedBy)
        {
           Nullable< int> ApplicationID = null;
            

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO Applications (ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus, LastStatusDate,PaidFees,CreatedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantPersonID", ApplicationPersonID);
                        command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationStatus", Staute);
                        command.Parameters.AddWithValue("@LastStatusDate", LastStauteDate);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedBy);
                        command.Parameters.AddWithValue("@PaidFees", PeadFees);
                        connection.Open();

                        object result = command.ExecuteScalar();

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

        public static bool UpdateApplication(int ApplicationID, int ApplicationPersonID, DateTime ApplicationDate,
            int ApplicationTypeID,byte Staute, DateTime LastStauteDate, int PeadFees,int CreatedBy)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Update Applications set ApplicantPersonID=@ApplicantPersonID,
                                    ApplicationDate=@ApplicationDate,
                                    ApplicationTypeID=@ApplicationTypeID,
                                    ApplicationStatus=@ApplicationStatus,
                                    LastStatusDate=@LastStatusDate,
                                    PaidFees=@PaidFees,
                                    CreatedByUserID=@CreatedByUserID
                                    where ApplicationID=@ApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@ApplicantPersonID", ApplicationPersonID);
                        command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationStatus", Staute);
                        command.Parameters.AddWithValue("@LastStatusDate", LastStauteDate);
                        command.Parameters.AddWithValue("@PaidFees", PeadFees);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedBy);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }

                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }

           
            return (rowsAffected > 0);
        }

        public static bool Find(int ApplicationID, ref int ApplicationPersonID,
          ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte Staute, ref DateTime LastStauteDate
          , ref int PeadFees, ref int CreatedBy)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                ApplicationPersonID = (int)reader["ApplicantPersonID"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                                Staute = Convert.ToByte(reader["ApplicationStatus"]);
                                LastStauteDate = (DateTime)reader["LastStatusDate"];
                                PeadFees = Convert.ToInt32(reader["PaidFees"]);
                                CreatedBy = (int)reader["CreatedByUserID"];

                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
                            }
                        }

                            
                    }
                        

                }



            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
           
            return isFound;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete Applications
                                    where ApplicationID=@ApplicationID";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }

                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }

            
            return (rowsAffected > 0);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;

            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found=1 FROM Applications WHERE ApplicationID = @ApplicationID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;

                        }


                    }

                }

                    
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            
            return isFound;
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {

            //incase the ActiveApplication ID !=-1 return true.
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != -1);
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;

           
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT ActiveApplicationID=ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID=@ApplicationTypeID and ApplicationStatus=1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                        connection.Open();
                        object result = command.ExecuteScalar();


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
                return ActiveApplicationID;
            }
           
            return ActiveApplicationID;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                        connection.Open();
                        object result = command.ExecuteScalar();


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
                return ActiveApplicationID;
            }
            
            return ActiveApplicationID;
        }

        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {

            int rowsAffected = 0;
            

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@NewStatus", NewStatus);
                        command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }



                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }

            
            return (rowsAffected > 0);
        }
    }
}
