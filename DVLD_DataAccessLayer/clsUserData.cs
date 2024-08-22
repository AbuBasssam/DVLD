using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Security.Cryptography;

namespace DVLD_DataAccessLayer
{
    
    public class UserDTO
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public UserDTO(int UserID, int PersonID, string UserName, string Password, bool isActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = isActive;
            

        }

    }
    public class ListUsersDTO
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string FullName {  get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }

        public ListUsersDTO(int UserID, int PersonID, string FullName, string UserName, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.FullName = FullName;
            this.UserName = UserName;
            this.IsActive = IsActive;
        }
    }

    public class clsUserData
    {
        public static UserDTO FindByPersonID(int PersonID)
        {
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_FindUserByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("UserID")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("UserName")),
                                    reader.GetString(reader.GetOrdinal("Password")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                         
                                );
                            }
                        }

                            
                    }
                        
                }
                    
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                
            }
           
            return null;
        }
       
        public static UserDTO FindByUserID(int UserID)
        {
            
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    

                    using (var command = new SqlCommand("SP_FindUserByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new UserDTO
                                 (
                                     reader.GetInt32(reader.GetOrdinal("UserID")),
                                     reader.GetInt32(reader.GetOrdinal("PersonID")),
                                     reader.GetString(reader.GetOrdinal("UserName")),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetBoolean(reader.GetOrdinal("IsActive"))

                                 );

                            }
                           
                        }

                            
                    }
                        


                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                
            }
           

            return null;
        }

        public static UserDTO Find(string UserName,string Password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_FindUserByUserNameAndPassword", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                return new UserDTO
                                 (
                                     reader.GetInt32(reader.GetOrdinal("UserID")),
                                     reader.GetInt32(reader.GetOrdinal("PersonID")),
                                     reader.GetString(reader.GetOrdinal("UserName")),
                                     reader.GetString(reader.GetOrdinal("Password")),
                                     reader.GetBoolean(reader.GetOrdinal("IsActive"))

                                 );

                            }
                           


                        }


                    }


                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
           
            return null;
        }

        public static Nullable<int> AddNewUser(UserDTO UserDTO)
        {
           
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        var outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        command.Parameters.AddWithValue("@PersonID", UserDTO.PersonID);
                        command.Parameters.AddWithValue("@UserName", UserDTO.UserName);
                        command.Parameters.AddWithValue("@Password", UserDTO.Password);
                        command.Parameters.AddWithValue("@IsActive", UserDTO.IsActive);
                        connection.Open();

                        int result = command.ExecuteNonQuery();

                        return (int)outputIdParam.Value;
                    }

                }
                    
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }



            return null;
        }

        public static bool UpdateUser(UserDTO UserDTO)
        {
            int rowsAffected = 0;
          
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                   

                    using (var command = new SqlCommand("SP_UpdateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", UserDTO.PersonID);
                        command.Parameters.AddWithValue("@UserName", UserDTO.UserName);
                        command.Parameters.AddWithValue("@Password", UserDTO.Password);
                        command.Parameters.AddWithValue("@IsActive", UserDTO.IsActive);
                        command.Parameters.AddWithValue("@UserID", UserDTO.UserID);
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

          

            return (rowsAffected==1);
        }

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
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

            return (rowsAffected==1);

        }

        public static bool IsUserExist(int UserID)
        {
           bool IsExist=false;



            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_CheckUserExists", connection))
                    {
                        command.CommandType=CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);
                        connection.Open();
                        command.ExecuteNonQuery();
                        IsExist=(int)returnParameter.Value==1;

                    }


                }

                    
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            return IsExist;
        }
        
        public static bool IsUserExist(string UserName)
        {
            bool IsExist = false;



            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_CheckUserExistsByUserName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", UserName);
                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);
                        connection.Open();
                        command.ExecuteNonQuery();
                        return (int)returnParameter.Value == 1;

                    }


                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            return IsExist;
        }

        public static bool IsAlreadyUserExist(int PersonID)
        {
            bool IsExist = false;
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    using (var command = new SqlCommand("SP_CheckAlreadyUserExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        connection.Open();
                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();
                        IsExist = (int)returnParameter.Value == 1;

                    }
                        


                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                
            }
            

            return IsExist;
        }
        
        public static List<ListUsersDTO> GetUsers()
        {

           List<ListUsersDTO> UsersList = new List<ListUsersDTO>();
            
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                   

                    using (var command = new SqlCommand("SP_GetAllUsers", connection))
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsersList.Add(new ListUsersDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("UserID")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("FullName")),
                                    reader.GetString(reader.GetOrdinal("UserName")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                ));
                               
                            }

                        }
                    }


                }



            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }

            return UsersList;

        }


    }
}
