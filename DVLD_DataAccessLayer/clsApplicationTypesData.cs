using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class clsApplicationTypesData : IApplicationTypesDAL
    {
        private readonly string _ConnectionString;
        public clsApplicationTypesData(string ConnectionString)
        {
            _ConnectionString = ConnectionString;

        }
        public async Task<IEnumerable<ApplicationTypeDTO>> GetAllApplicationTypesAsync()
        {
            List<ApplicationTypeDTO> applicationTypesList = new List<ApplicationTypeDTO>();

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT * FROM ApplicationTypes";

                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                applicationTypesList.Add(MapReaderToApplicationType(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return applicationTypesList;
        }

        public async Task<ApplicationTypeDTO> FindByIDAsync(int ApplicationTypeID)
        {
            ApplicationTypeDTO applicationType = null;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                        connection.Open();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                applicationType = MapReaderToApplicationType(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return applicationType;
        }

        public async Task<int?> AddNewApplicationTypeAsync(ApplicationTypeDTO ATDTO)
        {
            int? ApplicationTypeID = null;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"
                    INSERT INTO ApplicationTypes (ApplicationTypeTitle, ApplicationFees)
                    VALUES (@Title, @Fees);
                    SELECT SCOPE_IDENTITY();";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", ATDTO.Title);
                        command.Parameters.AddWithValue("@Fees", ATDTO.Fees);

                        connection.Open();

                        var result = await command.ExecuteScalarAsync();
                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            ApplicationTypeID = insertedID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return ApplicationTypeID;
        }

        public async Task<bool> UpdateApplicationTypeAsync(ApplicationTypeDTO ATDTO)
        {
            int rowsAffected = 0;

            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    string query = @"
                    UPDATE ApplicationTypes
                    SET ApplicationFees = @ApplicationFees,
                        ApplicationTypeTitle = @Title
                    WHERE ApplicationTypeID = @ApplicationTypeID";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApplicationTypeID", ATDTO.ApplicationID);
                        command.Parameters.AddWithValue("@Title", ATDTO.Title);
                        command.Parameters.AddWithValue("@ApplicationFees", ATDTO.Fees);

                        connection.Open();
                        rowsAffected = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLog.SetEventLog(ex.Message, EventLogEntryType.Error);
            }

            return rowsAffected > 0;
        }

        private  ApplicationTypeDTO MapReaderToApplicationType(IDataReader reader)
        {
            return new ApplicationTypeDTO
                (
                    reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                    reader.GetString(reader.GetOrdinal("ApplicationTypeTitle")),
                    reader.GetFloat(reader.GetOrdinal("ApplicationFees"))
                );
        }
    }

}
