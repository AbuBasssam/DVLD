using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{

    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1}
        public enMode Mode = enMode.AddNew;
        public UserDTO UDTO
        {
            get
            {
                return (new UserDTO((int)this.UserID,this.PersonID,
                this.UserName, this.Password, this.IsActive));
            }
        }
        public Nullable< int> UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool   IsActive {  get; set; }
        public clsPerson PersonInfo { get; set; }
        
        public clsUser(UserDTO UDTO, enMode cMode = enMode.AddNew)
        {
            this.UserID = UDTO.UserID;
            this.PersonID = UDTO.PersonID;
            this.UserName = UDTO.UserName;
            this.Password = UDTO.Password;
            this.IsActive = UDTO.IsActive;
           // this.PersonInfo = clsPerson.Find(UDTO.PersonID).Result;
            Mode = cMode;

        }

        public static async Task<clsUser>FindByPersonID(int PersonID)
        {

            UserDTO UDTO = MapToDTO(await clsUserData.FindByPersonIDAsync(PersonID));

            return (UDTO!=null)? new clsUser(UDTO, enMode.Update):null;
            
        }
        
        public static async Task<clsUser> FindByUserID(int UserID)
        {
            UserDTO UDTO =MapToDTO( await clsUserData.FindByUserIDAsync(UserID));

            return (UDTO != null) ? new clsUser(UDTO, enMode.Update) : null;
        }
       
        public static async Task<clsUser> Find(string UserName,string Password)
        {
            UserDTO UDTO =MapToDTO(await clsUserData.FindAsync(UserName, Password));

            return (UDTO != null) ? new clsUser(UDTO, enMode.Update) : null;
        }

        public async Task<bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if ( await _AddNewUserAsync())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return await _UpdateUser();


                default:
                    return false;
            }
        }

        private async Task<bool> _AddNewUserAsync()
        {
            this.UserID =await clsUserData.AddNewUserAsync(UDTO);
                return (UserID !=null);
        }

        private async Task< bool> _UpdateUser()
        {
            return await clsUserData.UpdateUserAsync(UDTO);
        }

        public static async Task<bool> DeleteUser(int UserID)
        {
            return await clsUserData.DeleteUserAsync(UserID);
        }
      
        public static async Task<bool> IsUserExist(int UserID)
        {
            return await clsUserData.IsUserExistAsync(UserID);
        }
        
        public static async Task<bool> IsUserExist(string UserName)
        {
            return await clsUserData.IsUserExistAsync(UserName);
        }

        public static async Task<bool> IsAlreadyUserExist(int PersonID)
        {
            return await clsUserData.IsAlreadyUserExistAsync(PersonID);
        }

        public static async Task<IEnumerable<UsersViewDTO>> GetAllUsers()
        {
            var Users = await clsUserData.GetUsersAsync();
            return Users;
        }

        private static UserDTO MapToDTO(UserDTO User)
        {
            return new UserDTO(

               User.UserID,
               User.PersonID,
               User.UserName,
               User.Password,
               User.IsActive
               
            );
        }

        
        
        
        

    }


}

