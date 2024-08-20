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
    public class PeopleDTO
    {

        public Nullable<int> PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public byte Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int Nationality { set; get; }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public PeopleDTO(Nullable<int> PersonID, string NationalNo, string FirstName, string SecondName
            , string ThirdName, string LastName, DateTime DateOfBirth, byte Gender,
            string Address, string Phone, string Email, int Nationality, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.Nationality = Nationality;
            this.ImagePath = ImagePath;

        }


    }
    public class clsPeopleData
    {
        public static PeopleDTO FindByNationalNo(string NationalNO)
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

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found

                                return new PeopleDTO
                                    (
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

        public static PeopleDTO FindByID(int PersonID)
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

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new PeopleDTO
                                    (
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

        public static Nullable<int> AddNewPerson(PeopleDTO PeopleDTO)
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

                        int result = command.ExecuteNonQuery();

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

        public static bool UpdatePerson(PeopleDTO PeopleDTO)
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
                        rowsAffected = command.ExecuteNonQuery();                        

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

        public static bool DeletePerson(int PersonID)
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

        public static bool DeletePerson(string NationalNo)
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

        public static bool IsPersonExist(int PersonID)
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
                            isFound = reader.HasRows;
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

        public static bool IsPersonExist(string NationalNo)
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
                            isFound = reader.HasRows;
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

        public static List<PeopleDTO> GetPeople()
        {
            var PeoplesList = new List<PeopleDTO>();

            try
            {
                string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;";
                using (var connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT * From People";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                PeoplesList.Add(new PeopleDTO
                                    (
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

                                    ));
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



    }


}











