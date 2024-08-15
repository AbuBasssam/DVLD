using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public static class clsTestTypesData
    {

        public static DataTable GetAllTest()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * From TestTypes";

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

        public static bool FindByID(int TestTypeID, ref string Title, ref string Description, ref int TestFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    Title = (string)reader["TestTypeTitle"];
                    Description= (string)reader["TestTypeDescription"];
                    TestFees = Convert.ToInt32(reader["TestTypeFees"]);


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

        public static bool UpdateTest(int TestTypeID, string Title, string Description, int TestFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update TestTypes  
                            set TestTypeFees=@TestFees,
                                TestTypeTitle=@TestTypeTitle,
                                TestTypeDescription=@Description
                                where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@TestFees", TestFees);

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

        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int TestTypeID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeTitle,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@ApplicationFees)
                            where TestTypeID = @TestTypeID;
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
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


            return TestTypeID;

        }
    }
}
