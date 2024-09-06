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
using DVLD_DataAccessLayer.Interfaces;

namespace DVLD_DataAccessLayer
{

    public  class clsDriverData : IDriverData
    {
        private readonly string _ConnectionString;
        public clsDriverData(string ConnectionString)
        {
            this._ConnectionString = ConnectionString;
        }

        public async Task<IEnumerable<DriverViewDTO>> GetAllDriversAsync()
        {

            List<DriverViewDTO> DriversList = new List<DriverViewDTO>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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
    
        public  async Task<DriverDTO> FindByDriverIDAsync(int DriverID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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
                                return MapReaderToDriver(reader);

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
        
        public  async Task<DriverDTO> FindByPersonIDAsync(int PersonID)
        {

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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

                                return MapReaderToDriver(reader);

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

        public  async Task<int?> AddNewDriverAsync(DriverDTO DriverDTO)
        {

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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

        public  async Task<bool> UpdateDriverAsync(DriverDTO DriverDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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

        public  async Task<bool> DeleteDriverAsync(int DriverID)
        {

            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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

        public  async Task<bool> IsDriverExistByPersonIDAsync(int PersonID)
        {
    
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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
        
        public  async Task<bool> IsDriverExistsAsync(int DriverID)
        {

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
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

        private static DriverViewDTO MapReaderToDriverView(IDataReader reader)
        {
            return new DriverViewDTO
                                 (
                                          reader.GetInt32(reader.GetOrdinal("DriverID")),
                                          reader.GetInt32(reader.GetOrdinal("PersonID")),
                                          reader.GetString(reader.GetOrdinal("NationalNo")),
                                          reader.GetString(reader.GetOrdinal("FullName")),
                                          reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                          Convert.ToByte(reader.GetInt32(reader.GetOrdinal("NumberOfActiveLicenses")))
                                 );
        }

        private static DriverDTO MapReaderToDriver(IDataReader reader)
        {
            return new DriverDTO
                                  (

                                      reader.GetInt32(reader.GetOrdinal("DriverID")),
                                      reader.GetInt32(reader.GetOrdinal("PersonID")),
                                      reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                      reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
                                  );
        }

    }
}
