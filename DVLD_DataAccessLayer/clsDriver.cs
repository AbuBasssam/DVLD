using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public static class clsDriverData
    {
        public static DataTable GetAllDrivers()
        {
            
                DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT dbo.Drivers.DriverID, dbo.Drivers.PersonID, dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + ISNULL(dbo.People.ThirdName, '') + ' ' + dbo.People.LastName AS FullName, dbo.Drivers.CreatedDate," +
                "(SELECT COUNT(LicenseID) AS NumberOfActiveLicenses " +
                "FROM dbo.Licenses WHERE(IsActive = 1) AND(DriverID = dbo.Drivers.DriverID)) AS NumberOfActiveLicenses " +
                "FROM dbo.Drivers INNER JOIN  dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    
        public static bool FindByDriverID(int DriverID, ref int  PersonID, ref  int CreatedByUserID, ref DateTime CreatedDate)
        {
           
                 bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " Select* From Drivers where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);




            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];






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
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }
        
        public static bool FindByPersonID(ref int DriverID,  int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " Select* From Drivers where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);




            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    DriverID = (int)reader["DriverID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];






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
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }

        public static int AddNewDriver(int PersonID,  int CreatedByUserID)
        {

            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Drivers(PersonID,CreatedByUserID,CreatedDate) VALUES(@PersonID,@CreatedByUserID,@CreatedDate);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID = insertedID;
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


            return DriverID;





        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {

           
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Drivers
                                  SET PersonID = @PersonID,
                                      CreatedByUserID= @CreatedByUserID
                                      WHERE DriverID=@DriverID";

            int rowsAffected = 0;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@DriverID", DriverID);



            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);





        }

        public static bool DeleteDriver(int DriverID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Drivers 
                                where DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }

        public static bool DeleteDriverByPersonID(int PersonID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Drivers 
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
        
        public static bool IsDriverExistByPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }











    }
}
