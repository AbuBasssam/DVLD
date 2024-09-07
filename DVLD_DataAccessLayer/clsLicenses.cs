using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public class clsLicensesData:IDALLicense
    {
        private readonly string _ConnectionString;
        public clsLicensesData(string connetionString)
        {
            _ConnectionString = connetionString;
        }

        public async Task<IEnumerable<LicenseDTO> >GetAllLicenses()
        {
            List<LicenseDTO> LicensesList = new List<LicenseDTO>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT * FROM Licenses";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                LicensesList.Add(_MapReaderToDTO(reader));

                            }
                        
                        }

                            
                    }


                        


                }

                    


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return LicensesList;

        }
        
        public async Task<IEnumerable<DriverLicensesDTO>> GetAllDriverLicenses(int DriverID)
        {
            List<DriverLicensesDTO> DriverLicensesList = new List<DriverLicensesDTO>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive\r\nFROM     Licenses INNER JOIN\r\nLicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID where DriverID=@DriverID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);


                        connection.Open();

                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                DriverLicensesList.Add(_MapReaderToDriverLicenseDTO(reader));
                            }
                        }

                            




                    }
                        

                }





            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            
            return DriverLicensesList;

        }

        public async Task<LicenseDTO> FindByLicenseID(int LicenseID)
        {
            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select * From Licenses where LicenseID=@LicenseID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);

                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())

                            if (await reader.ReadAsync())
                                return _MapReaderToDTO(reader);
                    }

                    

                    

                }
                



            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            
            return null;





        }

        public async Task<LicenseDTO> FindByLicenseIDAndLicenseClass(int LicenseID, int LicenseClass)
        {
            try
            {

                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select * From Licenses where LicenseID=@LicenseID and LicenseClass=@LicenseClass ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        command.Parameters.AddWithValue("@LicenseClass", LicenseClass);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (await reader.ReadAsync())
                            return _MapReaderToDTO(reader);
                    }
                        
                }
                

           
                

                


            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return null;





        }

        public async Task<LicenseDTO> FindByDriverID(int DriverID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select * From Licenses where DriverID=@DriverID";


                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);


                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // The record was found
                               return _MapReaderToDTO(reader);

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

        public async Task<LicenseDTO> FindByApplicationID(int ApplicationID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Select * From Licenses where ApplicationID=@ApplicationID";


                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                        connection.Open();
                        var reader =await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            // The record was found
                          return  _MapReaderToDTO(reader);

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

        public async Task<int?> AddNewLicense(LicenseDTO LDTO)
         {
            int? LicenseID = null;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"INSERT INTO Licenses(ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID)
                             VALUES(@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", LDTO.ApplicationID);
                        command.Parameters.AddWithValue("@DriverID", LDTO.DriverID);
                        command.Parameters.AddWithValue("@LicenseClass", LDTO.LicenseClass);
                        command.Parameters.AddWithValue("@IssueDate", LDTO.IssueDate);
                        command.Parameters.AddWithValue("@ExpirationDate", LDTO.ExpirationDate);
                        command.Parameters.AddWithValue("@PaidFees", LDTO.PaidFees);
                        command.Parameters.AddWithValue("@IsActive", LDTO.IsActive);
                        command.Parameters.AddWithValue("@IssueReason", LDTO.IssueReason);
                        command.Parameters.AddWithValue("@CreatedByUserID", LDTO.CreatedByUserID);

                        if (LDTO.Notes != "")
                            command.Parameters.AddWithValue("@Notes", LDTO.Notes);
                        else
                            command.Parameters.AddWithValue("@Notes", System.DBNull.Value);


                        connection.Open();

                        object result = await command.ExecuteScalarAsync();


                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LicenseID = insertedID;
                        }

                    }
                   
                }
                   
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }



            return LicenseID;

        }

        public async Task< bool> UpdateLicense(LicenseDTO LDTO)
        {
            int rowsAffected = 0;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"UPDATE Licenses
                                  SET ApplicationID = @ApplicationID,
                                      DriverID=@DriverID,
                                      LicenseClass=@LicenseClass,
                                      IssueDate=@IssueDate,
                                      ExpirationDate=@ExpirationDate,
                                      Notes=@Notes,
                                      PaidFees=@PaidFees,
                                      IsActive=@IsActive,
                                      IssueReason=@IssueReason,
                                     CreatedByUserID= @CreatedByUserID
                                      Where LicenseID=@LicenseID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ApplicationID",LDTO. ApplicationID);
                    command.Parameters.AddWithValue("@LicenseClass",LDTO. LicenseClass);
                    command.Parameters.AddWithValue("@DriverID", LDTO.DriverID);
                    command.Parameters.AddWithValue("@IssueDate", LDTO.IssueDate);
                    command.Parameters.AddWithValue("@ExpirationDate", LDTO.ExpirationDate);
                    command.Parameters.AddWithValue("@PaidFees", LDTO.PaidFees);
                    command.Parameters.AddWithValue("@IsActive", LDTO.IsActive);
                    command.Parameters.AddWithValue("@IssueReason", LDTO.IssueReason);
                    command.Parameters.AddWithValue("@CreatedByUserID", LDTO.CreatedByUserID);
                    command.Parameters.AddWithValue("@LicenseID",  LDTO.LicenseID);
                    if (LDTO.Notes != "")
                        command.Parameters.AddWithValue("@Notes", LDTO.Notes);
                    else
                        command.Parameters.AddWithValue("@Notes", System.DBNull.Value);





                    connection.Open();
                    rowsAffected =await command.ExecuteNonQueryAsync();
                }
            

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
                return false;
            }

           

            return (rowsAffected ==1);





        }

        public async Task<bool> DeleteLicense(int LicenseID)
        {

            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Delete Licenses 
                                where LicenseID = @LicenseID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);


                        connection.Open();

                        rowsAffected =await command.ExecuteNonQueryAsync();
                    }

                    
                } 

           

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return (rowsAffected==1);

        }

        public async Task<bool> AlreadyHaveLicense(int DriverID, int LicenseClass)
        {
            bool isFound = false;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT Found=1 FROM Licenses WHERE DriverID = @DriverID and LicenseClass=@LicenseClass ";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriverID);
                        command.Parameters.AddWithValue("@LicenseClass", LicenseClass);



                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            isFound = reader.HasRows;
                        }

                        


                    }

                    


                }

            
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return isFound;
        }

        public async Task<bool> IsLicneseExist(int ApplicationID)
        {
            bool isFound = false;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT Found=1 FROM Licenses WHERE ApplicationID = @ApplicationID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            isFound = reader.HasRows;
                        }

                            
                    }

                        

                }

            
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return isFound;
        }

        public async Task<int?> GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int? LicenseID = null;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);


                        connection.Open();

                        object result =await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LicenseID = insertedID;
                        }
                    }

                        
                }

                    
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            


            return LicenseID;
        }

        public async Task<bool> DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseID", LicenseID);
                        connection.Open();
                        rowsAffected =await command.ExecuteNonQueryAsync();

                    }



                   
                }

                    

            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }

           

            return (rowsAffected ==1);
        }
        public async Task<int?>Detain(DetainedLicenseDTO DLDTO)
        {
           return await  new clsDetainedLicenseData(_ConnectionString).AddNewDetainedLicense(DLDTO); 
        }
        
        private LicenseDTO _MapReaderToDTO(IDataReader reader)
        {
            return new LicenseDTO
                (
                    (int)reader["LicenseID"],
                    (int)reader["ApplicationID"],
                    (int)reader["DriverID"],
                    Convert.ToByte((int)reader["LicenseClass"]),
                    Convert.ToDateTime(reader["IssueDate"]),
                    Convert.ToDateTime(reader["ExpirationDate"]),
                    (reader["Notes"] != DBNull.Value)? (string)reader["Notes"]: "",
                    Convert.ToSingle(reader["PaidFees"]),
                    Convert.ToByte(reader["IsActive"]),
                    Convert.ToByte(reader["IssueReason"]),
                    (int)reader["CreatedByUserID"]





                );
        }
        private DriverLicensesDTO _MapReaderToDriverLicenseDTO(IDataReader reader)
        {
            return new DriverLicensesDTO
                (
                    (int)reader["LicenseID"],
                    (int)reader["ApplicationID"],
                   (string)reader["ClassName"],
                    Convert.ToDateTime(reader["IssueDate"]),
                    Convert.ToDateTime(reader["ExpirationDate"]),
                    Convert.ToByte(reader["IsActive"])






                );
        }



    }
}
