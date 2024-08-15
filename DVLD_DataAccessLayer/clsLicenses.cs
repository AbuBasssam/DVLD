using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsLicensesData
    {
        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Licenses";

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
                clsEventLog.SetEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        public static DataTable GetAllDriverLicenses(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive\r\nFROM     Licenses INNER JOIN\r\n                  LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
                clsEventLog.SetEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static bool FindByLicenseID(int LicenseID,ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref
           DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref int PaidFees, ref byte IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * From Licenses where LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            
            bool isFound = false;
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate =Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    PaidFees = Convert.ToInt32(reader["PaidFees"]);
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    //Notes: allows null in database so we should handle null
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }

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

        public static bool FindByLicenseIDAndLicenseClass(int LicenseID, ref int ApplicationID, ref int DriverID,  int LicenseClass, ref
          DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref int PaidFees, ref byte IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * From Licenses where LicenseID=@LicenseID and LicenseClass=@LicenseClass ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);


            bool isFound = false;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    PaidFees = Convert.ToInt32(reader["PaidFees"]);
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    //Notes: allows null in database so we should handle null
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }

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

        public static bool FindByDriverID(ref int LicenseID, ref int ApplicationID,  int DriverID, ref int LicenseClass, ref
           DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref int PaidFees, ref byte IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * From Licenses where DriverID=@DriverID";

            
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    PaidFees = Convert.ToInt32(reader["PaidFees"]);
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    //Notes: allows null in database so we should handle null
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }

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

        public static bool FindByApplicationID(ref int LicenseID,  int ApplicationID, ref int DriverID, ref int LicenseClass, ref
           DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref int PaidFees, ref byte IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * From Licenses where ApplicationID=@ApplicationID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    PaidFees = Convert.ToInt32(reader["PaidFees"]);
                    IsActive = Convert.ToByte(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    //Notes: allows null in database so we should handle null
                    if (reader["Notes"] != DBNull.Value)
                    {
                        Notes = (string)reader["Notes"];
                    }
                    else
                    {
                        Notes = "";
                    }

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

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, int PaidFees, byte IsActive, byte IssueReason, int CreatedByUserID)
         {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO Licenses(ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID)
                             VALUES(@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return LicenseID;








         }
        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes, int PaidFees, byte IsActive, byte IssueReason, int CreatedByUserID)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE Licenses
                                  SET ApplicationID = @ApplicationID,
                                      DriverID=@DriverID,
                                      LicenseClass=@LicenseClass,
                                      IssueDate=@IssueDate,
                                      ExpirationDate=@ExpirationDate,
                                      Notes=@Notes,
                                      PaidFees=@PaidFees,
                                      IsActive=@IsActive,
                                      IssueReason=@IssueReason,
                                     CreatedByUserID= @CreatedByUserID
                                      Where LicenseID=@LicenseID";

            int rowsAffected = 0;
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);




            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);





        }

        public static bool DeleteLicense(int LicenseID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Licenses 
                                where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }

        public static bool AlreadyHaveLicense(int DriverID, int LicenseClass)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Licenses WHERE DriverID = @DriverID and LicenseClass=@LicenseClass ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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

        public static bool  IsLicneseExist(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Licenses WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return LicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


    }
}
