using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public static  class clsLicenseClassesData
    {
        public static DataTable GetAllLicenseClasses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * from LicenseClasses";

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

        public static bool Find( int LicenseClassesID, ref string ClassName, ref string ClassDescription,
            ref byte MinimumAllowedAge, ref byte DefalutValidityLength, ref int ClassFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * from LicenseClasses Where LicenseClassID= @LicenseClassesID ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassesID", LicenseClassesID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefalutValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToInt32(reader["ClassFees"]);





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


        public static bool FindByName(ref int LicenseClassesID,  string ClassName, ref string ClassDescription,
            ref byte MinimumAllowedAge, ref byte DefalutValidityLength, ref int ClassFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * from LicenseClasses Where ClassName= @ClassName ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    LicenseClassesID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefalutValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToInt32(reader["ClassFees"]);





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



        public static int AddNewLicenseClass(string ClassName,  string ClassDescription,
             byte MinimumAllowedAge,  byte DefalutValidityLength,  int ClassFees)
        {
            int LicenseClassesID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO LicenseClasses (ClassName,ClassDescription,MinimumAllowedAge, DefalutValidityLength,ClassFees)
                             VALUES (@ClassName,@ClassDescription,@MinimumAllowedAge,@DefalutValidityLength, @ClassFees);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefalutValidityLength", DefalutValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
           

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseClassesID = insertedID;
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


            return LicenseClassesID;
        }


        public static bool UpdateLicenseClass(int LicenseClassesID,string ClassName, string ClassDescription,
            byte MinimumAllowedAge, byte DefalutValidityLength, int ClassFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update LicenseClasses set ClassName=@ClassName,
                                    ClassDescription=@ClassDescription,
                                    MinimumAllowedAge=@MinimumAllowedAge,
                                    DefalutValidityLength=@DefalutValidityLength,
                                    ClassFees=@ClassFees
                                    where LicenseClassesID=@LicenseClassesID";



            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefalutValidityLength", DefalutValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);


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
