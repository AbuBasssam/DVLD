using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface IUserDataInterface
    {
        Task<UserDTO> FindByPersonIDAsync(int PersonID);
        Task<UserDTO> FindByUserIDAsync(int UserID);
        Task<UserDTO> FindAsync(string UserName, string Password);
        Task<int?> AddNewUserAsync(UserDTO UserDTO);
        Task<bool> UpdateUserAsync(UserDTO UserDTO);
        Task<bool> DeleteUserAsync(int UserID);
        Task<bool> IsUserExistAsync(int UserID);
        Task<bool> IsUserExistAsync(string UserName);
        Task<bool> IsAlreadyUserExistAsync(int PersonID);
        Task<IEnumerable<UsersViewDTO>> GetUsersAsync();
    }
}
