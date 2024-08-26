using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IUser
    {
        Task<clsUser> FindByPersonID(int PersonID);
        Task<clsUser> FindByUserID(int UserID);
        Task<clsUser> Find(string UserName, string Password);
        Task<int?> CreateUserAsync(UserDTO UDTO);
        //Task<int?> CreateUserAsync(UserDTO UDTO,PersonDTO PDTO);
        Task<bool> UpdateUser(UserDTO UDTO);
        Task<bool> DeleteUser(int UserID);
        Task<bool> IsUserExist(int UserID);
        Task<bool> IsUserExist(string UserName);
        Task<bool> IsAlreadyUserExist(int PersonID);
        Task<IEnumerable<UsersViewDTO>> GetAllUsers();
        

    }
}
