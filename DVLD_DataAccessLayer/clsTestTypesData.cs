using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer.Entities;
using System.Runtime.Remoting.Messaging;

namespace DVLD_DataAccessLayer
{
    public  class clsTestTypesData :IDALTestTypes
    {
        private readonly string _ConnectoinString;
       public clsTestTypesData(string ConnectoinString)
        {
            this._ConnectoinString = ConnectoinString;
        }
        public  async Task<IEnumerable<TestTypeDTO>> GetTestTypesAsync()
        {
           List<TestTypeDTO>TestTypeList = new List<TestTypeDTO>();
            try
            {
                using (var connection = new SqlConnection(_ConnectoinString))
                {
                    string query = "SELECT * From TestTypes";

                    using (var command = new SqlCommand(query, connection))
                    {


                        connection.Open();

                        var reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            TestTypeList.Add( _MapReaderToTestType(reader));
                        }


                    }

               
                    



                }

                

            }

            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }
            

            return TestTypeList;
        }

        public async Task<TestTypeDTO> FindByIDAsync(int TestTypeID)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectoinString))
                {
                    string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@TestTypeID", SqlDbType.Int) { Value = TestTypeID });

                        connection.Open();
                        using (var reader =await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return _MapReaderToTestType(reader);


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

        public async Task<bool> UpdateTestAsync(TestTypeDTO TestTypeDTO)
        {
            int rowsAffected = 0;
            try
            {
                using (var connection = new SqlConnection(_ConnectoinString))
                {

                    string query = @"Update TestTypes  
                            set TestTypeFees=@TestFees,
                                TestTypeTitle=@TestTypeTitle,
                                TestTypeDescription=@Description
                                where TestTypeID = @TestTypeID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TestTypeID", TestTypeDTO.TestTypeID);
                        command.Parameters.AddWithValue("@TestTypeTitle",TestTypeDTO.Title);
                        command.Parameters.AddWithValue("@Description", TestTypeDTO.Description);
                        command.Parameters.AddWithValue("@TestFees",TestTypeDTO.TestFees);

                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }

                    


                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);
            }

            

            return (rowsAffected ==1);
        }

        public async Task<int?> AddNewTestTypeAsync(TestTypeDTO TestTypeDTO)
        {
            int? TestTypeID = -1;
            try
            {
                using (var connection = new SqlConnection(_ConnectoinString))
                {

                    string query = @"Insert Into TestTypes (TestTypeTitle,TestTypeTitle,TestTypeFees)
                            Values (@TestTypeTitle,@TestTypeDescription,@ApplicationFees)
                            where TestTypeID = @TestTypeID;
                            SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@TestTypeTitle", TestTypeDTO.Title);
                        command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDTO.Description);
                        command.Parameters.AddWithValue("@ApplicationFees", TestTypeDTO.TestFees);


                        connection.Open();

                        object result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            TestTypeID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message);

            }

            return TestTypeID;

        }

        private TestTypeDTO _MapReaderToTestType(IDataReader reader)
        {
            return new TestTypeDTO
                                (
                                    reader.GetInt32(reader.GetOrdinal("TestTypeID")),
                                    reader.GetString(reader.GetOrdinal("TestTypeTitle")),
                                    reader.GetString(reader.GetOrdinal("TestTypeDescription")),
                                     Convert.ToSingle(reader.GetDecimal(reader.GetOrdinal("TestTypeFees")))

                                );
        }

    }

}

