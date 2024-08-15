using System;
using System.Collections.Generic;
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
    public static class clsApplicationTypesData
    {
        public static  DataTable GetAllApplication() 
        {
            DataTable dt = new DataTable();
            
            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * From ApplicationTypes";

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

        public static bool FindByID(int ApplicationTypeID, ref string Title, ref int ApplicationFees)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;
                                ApplicationTypeID = (int)reader["ApplicationTypeID"];
                                Title = (string)reader["ApplicationTypeTitle"];
                                ApplicationFees = Convert.ToInt32(reader["ApplicationFees"]);


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

        public static int AddNewApplicationType(string Title, float Fees)
        {
            int ApplicationTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into ApplicationTypes (ApplicationTypeTitle,ApplicationFees)
                            Values (@Title,@Fees)
                            
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    ApplicationTypeID = insertedID;
                }
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

            }

            finally
            {
                connection.Close();
            }


            return ApplicationTypeID;

        }

        public static bool UpdateApplication(int ApplicationTypeID, string Title, int ApplicationFees)
        {
            int rowsAffected = 0;
           
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Update  ApplicationTypes  
                            set ApplicationFees=@ApplicationFees,
                                ApplicationTypeTitle=@ApplicationTypeTitle
                                where ApplicationTypeID = @ApplicationTypeID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {


                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
                        command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
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
