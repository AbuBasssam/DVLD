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


    public class clsUserData
    {
        public static async Task<UserDTO> FindByPersonIDAsync(int PersonID)
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
                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                MapReaderToUser(reader);
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
       
        public static async Task<UserDTO> FindByUserIDAsync(int UserID)
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
                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                MapReaderToUser(reader);

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

        public static async Task<UserDTO> FindAsync(string UserName,string Password)
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
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {

                                MapReaderToUser(reader);

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

        public static async Task<int?> AddNewUserAsync(UserDTO UserDTO)
        {
           
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_AddNewUser", connection))
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

                        int result =await command.ExecuteNonQueryAsync();

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

        public static async Task<bool> UpdateUserAsync(UserDTO UserDTO)
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
                        rowsAffected = await command.ExecuteNonQueryAsync();

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

        public static async Task<bool> DeleteUserAsync(int UserID)
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

                        rowsAffected = await command.ExecuteNonQueryAsync();

                    }


                }


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }

            return (rowsAffected==1);

        }

        public static async Task<bool> IsUserExistAsync(int UserID)
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
                        await command.ExecuteNonQueryAsync();
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
        
        public static async Task<bool> IsUserExistAsync(string UserName)
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
                        await command.ExecuteNonQueryAsync();
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

        public static async Task<bool> IsAlreadyUserExistAsync(int PersonID)
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
                        await command.ExecuteNonQueryAsync();
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
        
        public static async Task<IEnumerable<UsersViewDTO>> GetUsersAsync()
        {

           List<UsersViewDTO> UsersList = new List<UsersViewDTO>();
            
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                   

                    using (var command = new SqlCommand("SP_GetAllUsers", connection))
                    {
                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                UsersList.Add(MapReaderToUserView(reader));
                               
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

        private static UserDTO MapReaderToUser(IDataReader reader)
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

        private static UsersViewDTO MapReaderToUserView(IDataReader reader)
        {
            return new UsersViewDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("UserID")),
                                    reader.GetInt32(reader.GetOrdinal("PersonID")),
                                    reader.GetString(reader.GetOrdinal("FullName")),
                                    reader.GetString(reader.GetOrdinal("UserName")),
                                    reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                );
        }
    }
}
