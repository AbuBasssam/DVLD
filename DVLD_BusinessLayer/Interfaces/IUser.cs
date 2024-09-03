using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVlD_BusinessLayer.clsUser;

namespace DVlD_BusinessLayer.Interfaces
{
    
    public interface IUser
    {
         Nullable<int> UserID { get; set; }
         int PersonID { get; set; }
         string UserName { get; set; }
         string Password { get; set; }
         bool IsActive { get; set; }
        Task<clsUser> FindByPersonID(int PersonID);
        Task<clsUser> FindByUserID(int UserID);
        Task<clsUser> Find(string UserName, string Password);
        Task<int?> CreateUserAsync(UserDTO UDTO);
        Task<bool> UpdateUser(UserDTO UDTO);
        Task<bool> DeleteUser(int UserID);
        Task<bool> IsUserExist(int UserID);
        Task<bool> IsUserExist(string UserName);
        Task<bool> IsAlreadyUserExist(int PersonID);
        Task<IEnumerable<UsersViewDTO>> GetAllUsers();
        /// <summary>
        /// should check if there Empty Fileds or null
        /// ,Unique UserName,User is not already exists
        /// Valid PersonID
        /// </summary>
        /// <param name="UDTO">
        /// container for all User Info
        /// </param>
        /// <returns>
        /// return Valid if he's valid else return the validation error 
        /// </returns>
        public enUserValidationType IsValid(UserDTO UDTO);
        


    }
}
