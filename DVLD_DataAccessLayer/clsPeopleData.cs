using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System.Configuration;

namespace DVLD_DataAccessLayer
{
    public class clsPeopleData:IPeopleDataInterface
    {
        public  async Task<PersonDTO> FindByNationalNoAsync(string NationalNO)
        {

            try
            {

                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_FindPersonByNationalNO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNO", NationalNO);
                        connection.Open();

                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            if ( await reader.ReadAsync())
                            {
                                // The record was found

                                return MapReaderToPerson(reader);
                            }


                        }

                    }

                }



            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return null;

        }

        public  async Task<PersonDTO> FindByIDAsync(int PersonID)
        {
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_FindPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        connection.Open();

                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return MapReaderToPerson(reader);

                            }



                        }

                    }

                }



            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

                
            }


            return null;
        }

        public  async Task<int?> AddNewPersonAsync(PersonDTO PersonDTO)
        {


            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    using (var command = new SqlCommand("SP_AddNewPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        command.Parameters.AddWithValue("@NationalNo", PersonDTO.NationalNo);
                        command.Parameters.AddWithValue("@FirstName", PersonDTO.FirstName);
                        command.Parameters.AddWithValue("@SecondName", PersonDTO.SecondName);
                        command.Parameters.AddWithValue("@LastName", PersonDTO.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", PersonDTO.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", PersonDTO.Gender);
                        command.Parameters.AddWithValue("@Address", PersonDTO.Address);
                        command.Parameters.AddWithValue("@Phone", PersonDTO.Phone);
                        command.Parameters.AddWithValue("@NationalityCountryID", PersonDTO.Nationality);

                        if (PersonDTO.ThirdName != "")
                            command.Parameters.AddWithValue("@ThirdName", PersonDTO.ThirdName);
                        else
                            command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


                        if (PersonDTO.Email != "")
                            command.Parameters.AddWithValue("@Email", PersonDTO.Email);
                        else
                            command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                        if (PersonDTO.ImagePath != "")
                            command.Parameters.AddWithValue("@ImagePath", PersonDTO.ImagePath);
                        else
                            command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                        connection.Open();

                        int result = await command.ExecuteNonQueryAsync();

                        return (int?)outputIdParam.Value;
                    }

                }


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);


            }



            return null;
        }

        public  async Task<bool> UpdatePersonAsync(PersonDTO PersonDTO)
        {
            int rowsAffected = 0;

            try
            {

                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_UpdatePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@NationalNo",PersonDTO.NationalNo);
                        command.Parameters.AddWithValue("@FirstName", PersonDTO.FirstName);
                        command.Parameters.AddWithValue("@SecondName",PersonDTO.SecondName);
                        command.Parameters.AddWithValue("@LastName",  PersonDTO.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth",PersonDTO.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", PersonDTO.Gender);
                        command.Parameters.AddWithValue("@Address", PersonDTO.Address);
                        command.Parameters.AddWithValue("@Phone", PersonDTO.Phone);
                        command.Parameters.AddWithValue("@PersonID", PersonDTO.PersonID);
                        command.Parameters.AddWithValue("@NationalityCountryID", PersonDTO.Nationality);

                        if (PersonDTO.ThirdName != "")
                            command.Parameters.AddWithValue("@ThirdName", PersonDTO.ThirdName);
                        else
                            command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


                        if (PersonDTO.Email != "")
                            command.Parameters.AddWithValue("@Email", PersonDTO.Email);
                        else
                            command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                        if (PersonDTO.ImagePath != "")
                            command.Parameters.AddWithValue("@ImagePath", PersonDTO.ImagePath);
                        else
                            command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();                        

                    }
                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                return false;
            }


            return (rowsAffected ==1);
        }

        public  async Task<bool> DeletePersonAsync(int PersonID)
        {
            int rowsAffected = 0;
            
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    using (var command = new SqlCommand("SP_DeletePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public  async Task<bool> DeletePersonAsync(string NationalNo)
        {

            int rowsAffected = 0;


            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_DeletePersonByNationalNO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        connection.Open();

                        rowsAffected =await command.ExecuteNonQueryAsync();

                    }

                }

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return (rowsAffected == 1);

        }

        public  async Task<bool> IsPersonExistAsync(int PersonID)
        {
            bool isFound = false;


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_CheckPersonExists", connection))
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
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
                isFound = false;
            }

            return isFound;
        }

        public  async Task<bool> IsPersonExistAsync(string NationalNo)
        {
           

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_CheckPersonExistsByNationalNo", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

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
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
               
            }

            return false;
        }

        public  async Task<IEnumerable<PersonViewDTO>> GetPeopleAsync()
        {
            List<PersonViewDTO> PeopleList = new List<PersonViewDTO>();

            try
            {
                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {


                    using (var command = new SqlCommand("SP_GetPeopleList", connection))
                    {
                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                PeopleList.Add(MapReaderToPersonView(reader));

                            }

                        }
                    }


                }



            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }

            return PeopleList;

        }

        private static PersonDTO MapReaderToPerson(IDataReader reader)
        {
            return new PersonDTO(

                 reader.GetInt32(reader.GetOrdinal("PersonID")),
                 reader.GetString(reader.GetOrdinal("NationalNO")),
                 reader.GetString(reader.GetOrdinal("FirstName")),
                 reader.GetString(reader.GetOrdinal("SecondName")),
                 reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? "" : reader.GetString(reader.GetOrdinal("ThirdName")),
                 reader.GetString(reader.GetOrdinal("LastName")),
                 reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                 reader.GetByte(reader.GetOrdinal("Gender")),
                 reader.GetString(reader.GetOrdinal("Address")),
                 reader.GetString(reader.GetOrdinal("Phone")),
                 reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                 Convert.ToByte(reader.GetInt32(reader.GetOrdinal("NationalityCountryID"))),
                 reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? "" : reader.GetString(reader.GetOrdinal("ImagePath"))

            );
        }
        
        private static PersonViewDTO MapReaderToPersonView(IDataReader reader)
        {
            PersonDTO PersonInfo=new PersonDTO (
                                 reader.GetInt32(reader.GetOrdinal("PersonID")),
                                 reader.GetString(reader.GetOrdinal("NationalNO")),
                                 reader.GetString(reader.GetOrdinal("FirstName")),
                                 reader.GetString(reader.GetOrdinal("SecondName")),
                                 reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? "" : reader.GetString(reader.GetOrdinal("ThirdName")),
                                 reader.GetString(reader.GetOrdinal("LastName")),
                                 reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                 reader.GetByte(reader.GetOrdinal("Gender")),
                                 reader.GetString(reader.GetOrdinal("Address")),
                                 reader.GetString(reader.GetOrdinal("Phone")),
                                 reader.IsDBNull(reader.GetOrdinal("Email")) ? "" : reader.GetString(reader.GetOrdinal("Email")),
                                 Convert.ToByte(reader.GetInt32(reader.GetOrdinal("NationalityCountryID"))),
                                 reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? "" : reader.GetString(reader.GetOrdinal("ImagePath"))

                
                );

            string CountryName = reader.GetString(reader.GetOrdinal("CountryName"));
            string Gendarstr = reader.GetString(reader.GetOrdinal("GenderCaption"));
            
            return new PersonViewDTO(PersonInfo, CountryName, Gendarstr);
        }
    }

}














