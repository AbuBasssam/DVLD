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

namespace DVLD_DataAccessLayer
{
    public class clsPeopleData
    {
        public static async Task<Person> FindByNationalNoAsync(string NationalNO)
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

        public static async Task<Person> FindByIDAsync(int PersonID)
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
                                MapReaderToPerson(reader);

                            }



                        }

                    }

                }



            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);

                //Console.WriteLine("Error: " + ex.Message);
                
            }


            return null;
        }

        public static async Task<int?> AddNewPersonAsync(Person PeopleDTO)
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

                        command.Parameters.AddWithValue("@NationalNo", PeopleDTO.NationalNo);
                        command.Parameters.AddWithValue("@FirstName", PeopleDTO.FirstName);
                        command.Parameters.AddWithValue("@SecondName", PeopleDTO.SecondName);
                        command.Parameters.AddWithValue("@LastName", PeopleDTO.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", PeopleDTO.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", PeopleDTO.Gender);
                        command.Parameters.AddWithValue("@Address", PeopleDTO.Address);
                        command.Parameters.AddWithValue("@Phone", PeopleDTO.Phone);
                        command.Parameters.AddWithValue("@NationalityCountryID", PeopleDTO.Nationality);

                        if (PeopleDTO.ThirdName != "")
                            command.Parameters.AddWithValue("@ThirdName", PeopleDTO.ThirdName);
                        else
                            command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


                        if (PeopleDTO.Email != "")
                            command.Parameters.AddWithValue("@Email", PeopleDTO.Email);
                        else
                            command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                        if (PeopleDTO.ImagePath != "")
                            command.Parameters.AddWithValue("@ImagePath", PeopleDTO.ImagePath);
                        else
                            command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                        connection.Open();

                        int result = await command.ExecuteNonQueryAsync();

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

        public static async Task<bool> UpdatePersonAsync(Person PeopleDTO)
        {
            int rowsAffected = 0;

            try
            {

                using (var connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (var command = new SqlCommand("SP_UpdatePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@NationalNo", PeopleDTO.NationalNo);
                        command.Parameters.AddWithValue("@FirstName", PeopleDTO.FirstName);
                        command.Parameters.AddWithValue("@SecondName", PeopleDTO.SecondName);
                        command.Parameters.AddWithValue("@LastName", PeopleDTO.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", PeopleDTO.DateOfBirth);
                        command.Parameters.AddWithValue("@Gender", PeopleDTO.Gender);
                        command.Parameters.AddWithValue("@Address", PeopleDTO.Address);
                        command.Parameters.AddWithValue("@Phone", PeopleDTO.Phone);
                        command.Parameters.AddWithValue("@PersonID", PeopleDTO.PersonID);
                        command.Parameters.AddWithValue("@NationalityCountryID", PeopleDTO.Nationality);

                        if (PeopleDTO.ThirdName != "")
                            command.Parameters.AddWithValue("@ThirdName", PeopleDTO.ThirdName);
                        else
                            command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);


                        if (PeopleDTO.Email != "")
                            command.Parameters.AddWithValue("@Email", PeopleDTO.Email);
                        else
                            command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                        if (PeopleDTO.ImagePath != "")
                            command.Parameters.AddWithValue("@ImagePath", PeopleDTO.ImagePath);
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

        public static async Task<bool> DeletePersonAsync(int PersonID)
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

        public static async Task<bool> DeletePersonAsync(string NationalNo)
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

        public static async Task<bool> IsPersonExistAsync(int PersonID)
        {
            bool isFound = false;


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = (int)await command.ExecuteScalarAsync() > 0;
                        }



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

        public static async Task<bool> IsPersonExistAsync(string NationalNo)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = (int)await command.ExecuteScalarAsync() > 0;
                        }
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

        public static async Task<IEnumerable<PersonView>> GetPeopleAsync()
        {
            var PeoplesList = new List<PersonView>();

            try
            {
                string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;";
                using (var connection = new SqlConnection(ConnectionString))
                {
                   

                    using (var command = new SqlCommand("SP_GetPeopleList", connection))
                    {
                        connection.Open();

                        using (var reader =await command.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                PeoplesList.Add(MapReaderToPersonView(reader));
                            }

                        }
                    }



                }



            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return PeoplesList;

        }

        private static Person MapReaderToPerson(IDataReader reader)
        {
            return new Person(

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
                 reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                 reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? "" : reader.GetString(reader.GetOrdinal("ImagePath"))
            );
        }
        
        private static PersonView MapReaderToPersonView(IDataReader reader)
        {
            return new PersonView
                             (
                                new Person(reader.GetInt32(reader.GetOrdinal("PersonID")),
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
                                 reader.GetInt32(reader.GetOrdinal("NationalityCountryID")),
                                 reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? "" : reader.GetString(reader.GetOrdinal("ImagePath"))
                                 ), reader.GetString(reader.GetOrdinal("CountryName")),
                                   reader.GetString(reader.GetOrdinal("GenderCaption"))
                             );
        }
    }

}














