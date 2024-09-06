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
        private IUserDataInterface _UserDataInterface { get; set; }
        public enum enUserValidationType { EmptyFileds = 1, InvalidPersonID = 2, UserNameDuplicate = 3, NullObject = 4, AlreadyUser = 5, Valid = 6 };
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

        public clsUser(IUserDataInterface UserDataInterface)
        {
            this._UserDataInterface = UserDataInterface;
        }
        private clsUser(IUserDataInterface UserDataInterface, UserDTO UDTO)
        {
            this._UserDataInterface = UserDataInterface;
            this.UserID=UDTO.UserID;
            this.PersonID=UDTO.PersonID;
            this.UserName=UDTO.UserName;
            this.Password=UDTO.Password;
            this.IsActive=UDTO.IsActive;
        }

        private Func<string, bool> IsFieldEmpty = str => string.IsNullOrEmpty(str);
        private bool HasUserHaveEmptyFileds(UserDTO NewUserDTO)
        {
            return (
                NewUserDTO.PersonID == 0 ||
               IsFieldEmpty(NewUserDTO.UserName) ||
               IsFieldEmpty(NewUserDTO.Password)
                );
        }

        public enUserValidationType IsValid(UserDTO NewUserDTO)
        {
            string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
            clsPeopleData Person= new clsPeopleData(ConnectionString);

            if (NewUserDTO == null)
            {
                return enUserValidationType.NullObject;
            }
            if (HasUserHaveEmptyFileds(NewUserDTO))
            {
                return enUserValidationType.EmptyFileds;
            }
            if (!Person.IsPersonExistAsync(NewUserDTO.PersonID).Result)
            {

                return enUserValidationType.InvalidPersonID;
            }

            if (IsAlreadyUserExist(NewUserDTO.PersonID).Result)
            {

                return enUserValidationType.AlreadyUser;
            }

            if (IsUserExist(NewUserDTO.UserName).Result)
            {
                return enUserValidationType.UserNameDuplicate;
            }




            return enUserValidationType.Valid;
        }
        

        public async Task<clsUser> FindByPersonID(int PersonID)
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

    }


}

