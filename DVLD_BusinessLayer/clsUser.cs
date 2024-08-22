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
            this.PersonInfo = clsPerson.Find(UDTO.PersonID);
            Mode = cMode;

        }

        public static clsUser FindByPersonID(int PersonID)
        {

            UserDTO UDTO =clsUserData.FindByPersonID(PersonID);

            if (UDTO!=null)
            {
                return new clsUser(UDTO,enMode.Update);

            }
            else
            {
                return null;
            }
        }
        
        public static clsUser FindByUserID(int UserID)
        {
            UserDTO UDTO = clsUserData.FindByUserID(UserID);

            if (UDTO != null)
            {
                return new clsUser(UDTO, enMode.Update);

            }
            else
            {
                return null;
            }
        }
       
        public static clsUser Find(string UserName,string Password)
        {
            UserDTO UDTO = clsUserData.Find(UserName,Password);

            if (UDTO != null)
            {
                return new clsUser(UDTO, enMode.Update);

            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return _UpdateUser();


                default:
                    return false;
            }
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(UDTO);
                return (UserID !=null);
        }

        private bool _UpdateUser()
        {
            return (clsUserData.UpdateUser(UDTO));
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
      
        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }
        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsAlreadyUserExist(int PersonID)
        {
            return clsUserData.IsAlreadyUserExist(PersonID);
        }

        public static List<ListUsersDTO> GetAllUsers()
        {
            return clsUserData.GetUsers();
        }
    }


}

