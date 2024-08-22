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

namespace DVLD_DataAccessLayer
{
    public class DriverDTO
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DriverDTO( int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate; 

        }

    }
    public class ListDriverDTO
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FullName { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public  byte NumberOfActiveLicenses {  get; set; }
        public ListDriverDTO(int DriverID, int PersonID,string NationalNo,string FullName, int CreatedByUserID,DateTime CreatedDate, byte NumberOfActiveLicenses)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FullName = FullName;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.NumberOfActiveLicenses = NumberOfActiveLicenses;


        }
    }

    public static class clsDriverData
    {
        public static List<ListDriverDTO> GetAllDrivers()
        {

            List<ListDriverDTO> DriversList = new List<ListDriverDTO>();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

           

            SqlCommand command = new SqlCommand("SP_GetDriversList", connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())

                {
                    DriversList.Add
                       (
                        new ListDriverDTO(
                                     reader.GetInt32(reader.GetOrdinal("DriverID")),
                                     reader.GetInt32(reader.GetOrdinal("PersonID")),
                                     reader.GetString(reader.GetOrdinal("NationalNo")),
                                     reader.GetString(reader.GetOrdinal("FullName")),
                                     reader.GetInt32(reader.GetOrdinal("CreatedByUserID")),
                                     reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                     reader.GetByte(reader.GetOrdinal("NumberOfActiveLicenses"))
                                 ));
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

            return DriversList;
        }
    
        public static DriverDTO FindByDriverID(int DriverID)
        {
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (var command = new SqlCommand("GetDriverByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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
                   

                    

                }

                   


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return null;
            }
           

            return null;

        }
        
        public static DriverDTO FindByPersonID(int PersonID)
        {

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (var command = new SqlCommand("GetDriverByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
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




                }




            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return null;
            }


           



          

            return null;

        }

        public static Nullable<int> AddNewDriver(DriverDTO DriverDTO)
        {

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                   //INSERT INTO Drivers(PersonID,CreatedByUserID,CreatedDate) VALUES(@PersonID,@CreatedByUserID,@CreatedDate);
                             //SELECT SCOPE_IDENTITY();";

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
                        command.ExecuteNonQuery();
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

        public static bool UpdateDriver(DriverDTO DriverDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    //UPDATE Drivers
                    //SET PersonID = @PersonID,
                    //    CreatedByUserID= @CreatedByUserID
                    //    WHERE DriverID=@DriverID


                    using (var command = new SqlCommand("SP_UpdateDriver", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", DriverDTO.PersonID);
                        command.Parameters.AddWithValue("@CreatedByUserID", DriverDTO.CreatedByUserID);
                        command.Parameters.AddWithValue("@DriverID", DriverDTO.DriverID);
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    




                    
                }
                    

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
               
            }

            

            return (rowsAffected == 1);





        }

        public static bool DeleteDriver(int DriverID)
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
                        rowsAffected = command.ExecuteNonQuery();
                    }

                    
                }

                

           
               

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                
            }
            

            return (rowsAffected== 1);

        }

        public static bool DeleteDriverByPersonID(int PersonID)
        {

            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    

                    using (var command = new SqlCommand("SP_DeleteDriverByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }


                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

            }

            return (rowsAffected == 1);
        }
        
        public static bool IsDriverExistByPersonID(int PersonID)
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
                        command.ExecuteNonQuery();
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
        
        public static bool IsDriverExists(int DriverID)
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
                        command.ExecuteNonQuery();
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

    }
}
