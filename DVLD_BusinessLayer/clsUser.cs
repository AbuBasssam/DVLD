using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Interfaces;
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

    public class clsUser:IUser
    {
        public enum enMode { AddNew = 0, Update = 1}
        public enMode Mode = enMode.AddNew;
        private IUserDataInterface _UserDataInterface { get; set; }

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

        public clsUser(IUserDataInterface UserDataInterface)
        {
            this._UserDataInterface = UserDataInterface;
            this.Mode = enMode.AddNew;
        }
        private clsUser(IUserDataInterface UserDataInterface, UserDTO UDTO)
        {
            this._UserDataInterface = UserDataInterface;
            this.UserID=UDTO.UserID;
            this.PersonID=UDTO.PersonID;
            this.UserName=UDTO.UserName;
            this.Password=UDTO.Password;
            this.IsActive=UDTO.IsActive;
            this.Mode = enMode.Update;
        }
        
        public  async Task<clsUser> FindByPersonID(int PersonID)
        {

            UserDTO UDTO =await _UserDataInterface.FindByPersonIDAsync(PersonID);

            return (UDTO != null) ? new clsUser(_UserDataInterface, UDTO) : null;

        }

        public async Task<clsUser> FindByUserID(int UserID)
        {
           var UDTO=await _UserDataInterface.FindByUserIDAsync(UserID);

            return (UDTO != null) ? new clsUser(_UserDataInterface, UDTO) : null;
        }
        public  async Task<clsUser> Find(string UserName,string Password)
        {
            UserDTO UDTO =await _UserDataInterface.FindAsync(UserName, Password);

            return (UDTO != null) ? new clsUser(_UserDataInterface,UDTO) : null;
        }

        public async Task<int?> CreateUserAsync(UserDTO UDTO)
        {
            return await _UserDataInterface.AddNewUserAsync(UDTO);
                
        }

        public async Task< bool> UpdateUser(UserDTO UDTO)
        {
            return await _UserDataInterface.UpdateUserAsync(UDTO);
        }

        public  async Task<bool> DeleteUser(int UserID)
        {
            return await _UserDataInterface.DeleteUserAsync(UserID);
        }
      
        public  async Task<bool> IsUserExist(int UserID)
        {
            return await _UserDataInterface.IsUserExistAsync(UserID);
        }
        
        public  async Task<bool> IsUserExist(string UserName)
        {
            return await _UserDataInterface.IsUserExistAsync(UserName);
        }

        public  async Task<bool> IsAlreadyUserExist(int PersonID)
        {
            return await _UserDataInterface.IsAlreadyUserExistAsync(PersonID);
        }

        public  async Task<IEnumerable<UsersViewDTO>> GetAllUsers()
        {
            var Users = await _UserDataInterface.GetUsersAsync();
            return Users;
        }



        /*        public static async Task<clsUser>FindByPersonID(int PersonID)
                {

                    UserDTO UDTO = MapToDTO(await clsUserData.FindByPersonIDAsync(PersonID));

                    return (UDTO!=null)? new clsUser(UDTO, enMode.Update):null;

                }
        */

        /*public clsUser(UserDTO UDTO, enMode cMode = enMode.AddNew)
        {
            this.UserID = UDTO.UserID;
            this.PersonID = UDTO.PersonID;
            this.UserName = UDTO.UserName;
            this.Password = UDTO.Password;
            this.IsActive = UDTO.IsActive;
          //this.PersonInfo = clsPerson.Find(UDTO.PersonID).Result;
            Mode = cMode;

        }*/

        /*public static async Task<clsUser> FindByUserID(int UserID)
        {
            UserDTO UDTO =MapToDTO( await clsUserData.FindByUserIDAsync(UserID));

            return (UDTO != null) ? new clsUser(UDTO, enMode.Update) : null;
        }*/
        /*        public async Task<bool> SaveAsync()
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
*/
    }


}

