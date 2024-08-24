using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using DVLD_DataAccessLayer.Entities;

namespace DVLD_DataAccessLayer
{

    public static class clsDriverData
    {
        public static async Task<IEnumerable<DriverView>> GetAllDriversAsync()
        {

            List<DriverView> DriversList = new List<DriverView>();
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_GetDriversList", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())

                            {
                                DriversList.Add
                                   (
                                        MapReaderToDriverView(reader)
                                   );
                            }
                        }

                            

                    }

                }



                

            
                

                


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }
            

            return DriversList;
        }
    
        public static async Task<Driver> FindByDriverIDAsync(int DriverID)
        {
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_FindDriverByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                MapReaderToDriver(reader);

                            }
                        }

                            

                    }
                   

                    

                }

                   


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return null;
            }
           

            return null;

        }
        
        public static async Task<Driver> FindByPersonIDAsync(int PersonID)
        {

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_FindDriverByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        connection.Open();
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {

                                MapReaderToDriver(reader);

                            }
                        }



                    }




                }




            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return null;
            }


           



          

            return null;

        }

        public static async Task<int?> AddNewDriverAsync(Driver DriverDTO)
        {

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                   
                    using (var command = new SqlCommand("SP_AddNewDriver", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        var outputIdParam = new SqlParameter("@NewDriverID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        command.Parameters.AddWithValue("@PersonID", DriverDTO.PersonID);
                        command.Parameters.AddWithValue("@CreatedByUserID", DriverDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                        return (int)outputIdParam.Value;
                    }
                    
                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

            }

            


            return null;





        }

        public static async Task<bool> UpdateDriverAsync(Driver DriverDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_UpdateDriver", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", DriverDTO.PersonID);
                        command.Parameters.AddWithValue("@CreatedByUserID", DriverDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@DriverID", DriverDTO.DriverID);
                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();

                    }






                }
                    

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
               
            }

            

            return (rowsAffected == 1);





        }

        public static async Task<bool> DeleteDriverAsync(int DriverID)
        {

            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    
                    using (var command = new SqlCommand("SP_DeleteDriver", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }

                    
                }

                

           
               

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                
            }
            

            return (rowsAffected== 1);

        }

        public static async Task<bool> IsDriverExistByPersonIDAsync(int PersonID)
        {
    
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_CheckDriverExistsByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
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
            return false;

        }
        
        public static async Task<bool> IsDriverExistsAsync(int DriverID)
        {

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_CheckDriverExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DriverID", DriverID);
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
            return false;

        }

        private static DriverView MapReaderToDriverView(IDataReader reader)
        {
            return new DriverView
                                 (
                                          reader.GetInt32(reader.GetOrdinal("DriverID")),
                                          reader.GetInt32(reader.GetOrdinal("PersonID")),
                                          reader.GetString(reader.GetOrdinal("NationalNo")),
                                          reader.GetString(reader.GetOrdinal("FullName")),
                                          reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                          Convert.ToByte(reader.GetInt32(reader.GetOrdinal("NumberOfActiveLicenses")))
                                 );
        }

        private static Driver MapReaderToDriver(IDataReader reader)
        {
            return new Driver
                                  (

                                      reader.GetInt32(reader.GetOrdinal("DriverID")),
                                      reader.GetInt32(reader.GetOrdinal("PersonID")),
                                      reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                      reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                                  );
        }

    }
}
