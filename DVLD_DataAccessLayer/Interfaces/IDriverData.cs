using DVLD_DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IDriverData
    {
        // Method to retrieve all drivers asynchronously
        Task<IEnumerable<DriverViewDTO>> GetAllDriversAsync();

        // Method to find a driver by their unique DriverID asynchronously
        Task<DriverDTO> FindByDriverIDAsync(int DriverID);

        // Method to find a driver by their associated PersonID asynchronously
        Task<DriverDTO> FindByPersonIDAsync(int PersonID);

        // Method to add a new driver asynchronously, returning the newly created DriverID
        Task<int?> AddNewDriverAsync(DriverDTO DriverDTO);

        // Method to update an existing driver asynchronously, returning a boolean indicating success
        Task<bool> UpdateDriverAsync(DriverDTO DriverDTO);

        // Method to delete a driver by their DriverID asynchronously, returning a boolean indicating success
        Task<bool> DeleteDriverAsync(int DriverID);

        // Method to check if a driver exists by their PersonID asynchronously, returning a boolean indicating existence
        Task<bool> IsDriverExistByPersonIDAsync(int PersonID);

        // Method to check if a driver exists by their DriverID asynchronously, returning a boolean indicating existence
        Task<bool> IsDriverExistsAsync(int DriverID);
    }
}
