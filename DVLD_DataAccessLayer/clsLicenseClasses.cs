using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public   class clsLicenseClassesData: IDALLicenseClasses
    {
        private readonly string _ConnectionString;
        public clsLicenseClassesData(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }
        
        public  async Task<IEnumerable<LicenseClassDTO>> GetAllLicenseClasses()
        {

            List<LicenseClassDTO> LicenseClassesList= new List<LicenseClassDTO>();
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "Select * from LicenseClasses";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using(var reader =await command.ExecuteReaderAsync())
                        {
                            while(await reader.ReadAsync())

                            {
                                LicenseClassesList.Add(_MapReaderToDTO((reader)));
                            }

                        }

                            

                    }


                       
                }

                    


            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return LicenseClassesList;

        }
        
        public async Task<LicenseClassDTO> Find( int LicenseClassesID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "Select * from LicenseClasses Where LicenseClassID= @LicenseClassesID ";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseClassesID", LicenseClassesID);

                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
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

        public async Task<LicenseClassDTO> Find(string ClassName)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "Select * from LicenseClasses Where ClassName= @ClassName ";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", ClassName);

                        connection.Open();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
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

        public async Task< int?> AddNewLicenseClass(LicenseClassDTO LCDTO)
        {
            int? LicenseClassesID = null;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                string query = @"INSERT INTO LicenseClasses (ClassName,ClassDescription,MinimumAllowedAge, DefalutValidityLength,ClassFees)
                             VALUES (@ClassName,@ClassDescription,@MinimumAllowedAge,@DefalutValidityLength, @ClassFees);
                             SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", LCDTO.ClassName);
                        command.Parameters.AddWithValue("@ClassDescription", LCDTO.ClassDescription);
                        command.Parameters.AddWithValue("@MinimumAllowedAge", LCDTO.MinimumAllowedAge);
                        command.Parameters.AddWithValue("@DefalutValidityLength", LCDTO.DefalutValidityLength);
                        command.Parameters.AddWithValue("@ClassFees", LCDTO.ClassFees);
                        connection.Open();

                        object result =await command.ExecuteScalarAsync();


                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            LicenseClassesID = insertedID;
                        }
                    }
                        
                }
                
           

            
                
            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

           


            return LicenseClassesID;
        }

        public async Task<bool> UpdateLicenseClass(LicenseClassDTO LCDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"Update LicenseClasses set ClassName=@ClassName,
                                    ClassDescription=@ClassDescription,
                                    MinimumAllowedAge=@MinimumAllowedAge,
                                    DefaultValidityLength=@DefaultValidityLength,
                                    ClassFees=@ClassFees
                                    where LicenseClassID=@LicenseClassID";



                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LicenseClassID", LCDTO.LicenseClassesID);
                        command.Parameters.AddWithValue("@ClassName", LCDTO.ClassName);
                        command.Parameters.AddWithValue("@ClassDescription", LCDTO.ClassDescription);
                        command.Parameters.AddWithValue("@MinimumAllowedAge", LCDTO.MinimumAllowedAge);
                        command.Parameters.AddWithValue("@DefaultValidityLength", LCDTO.DefalutValidityLength);
                        command.Parameters.AddWithValue("@ClassFees", LCDTO.ClassFees);
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

            

            return (rowsAffected ==1);
        }
        private LicenseClassDTO _MapReaderToDTO(IDataReader reader)
        {
            return new LicenseClassDTO
                (
                    (int)reader["LicenseClassID"],
                    (string)reader["ClassName"],
                    (string)reader["ClassDescription"],
                    Convert.ToByte(reader["MinimumAllowedAge"]),
                    Convert.ToByte(reader["DefaultValidityLength"]),
                    Convert.ToSingle(reader["ClassFees"])



                );
        }


    }

}
