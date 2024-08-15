using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DVLD_DataAccessLayer
{
    public class clsUserData
    {
        public static bool FindByPersonID( ref int UserID,  int PersonID, ref string UserName, ref string Password, ref bool isActive)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;
                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                isActive = (bool)reader["IsActive"]; ;

                            }
                        }

                            
                    }
                        
                }
                    
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                isFound = false;
            }
           
            return isFound;
        }
       
        public static bool FindByIUserD(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool isActive)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;
                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                isActive = (bool)reader["IsActive"]; ;

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
                clsEventLog.SetEventLog(ex.Message);
                isFound = false;
            }
           

            return isFound;
        }

        public static bool Find( ref int UserID, ref int PersonID, string UserName,string Password, ref bool isActive)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT * FROM Users WHERE UserName = @UserName and Password=@Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;
                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                isActive = (bool)reader["IsActive"]; ;

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
                clsEventLog.SetEventLog(ex.Message);
                isFound = false;
            }
           
            return isFound;
        }

        public static Nullable<int> AddNewUser(int PersonID, string UserName, string Password,bool IsActive)
        {
            Nullable<int> UserID = -1;
           
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"INSERT INTO Users (PersonID,UserName,Password,IsActive)
                             VALUES (@PersonID,@UserName,@Password,@IsActive);
                             SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        connection.Open();

                        object result = command.ExecuteScalar();


                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            UserID = insertedID;
                        }
                    }





                    
                }
                    
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }



            return UserID;
        }

        public static bool UpdateUser(int UserID,string UserName, string Password ,bool IsActive)
        {
            int rowsAffected = 0;
          
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Update  Users  
                           set UserName=@UserName,
                                Password = @Password,
                                IsActive =@IsActive
                                where UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@UserID", UserID);
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }



                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                return false;
            }

          

            return (rowsAffected > 0);
        }

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"Delete Users where UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);

                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();

                    }


                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }

            return (rowsAffected > 0);

        }

        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

           
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);

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
                clsEventLog.SetEventLog(ex.Message);
                isFound = false;
            }
            
            return isFound;
        }

        public static bool IsAlreadyUserExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

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
                clsEventLog.SetEventLog(ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        
        public static DataTable GetUsers()
        {

            DataTable dt = new DataTable();
            
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Users.UserID, Users.PersonID, People.FirstName +' '+ People.SecondName+' '+People.ThirdName+' '+People.LastName as FullName, Users.UserName, Users.IsActive " +
                        "FROM Users INNER JOIN People" +
                        " ON Users.PersonID = People.PersonID";

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
                clsEventLog.SetEventLog(ex.Message);
            }

            return dt;

        }























    }
}
