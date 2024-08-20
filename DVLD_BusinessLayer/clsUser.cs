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
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public Nullable< int> UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool   IsActive {  get; set; }
        public clsPerson PersonInfo { get; set; }
        
        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = false;
        }
        
        private clsUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            this.PersonInfo = clsPerson.Find(PersonID);
            Mode = enMode.Update;
            
            
        }


        public static clsUser FindByPersonID(int PersonID)
        {
            int    UserID   = -1;
            string UserName = default(string);
            string Password = default(string);
            bool   isActive = false;
           

            if (clsUserData.FindByPersonID(ref UserID, PersonID, ref UserName, ref Password, ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);



            }
            else
            {
                return null;
            }
        }
        
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = default(string);
            string Password = default(string);
            bool isActive = false;


            if (clsUserData.FindByIUserD( UserID, ref PersonID, ref UserName, ref Password, ref isActive))
            {

                return new clsUser(UserID, PersonID, UserName, Password, isActive);



            }
            else
            {
                return null;
            }
        }
        public static clsUser Find(string UserName,string Password)
        {
            int PersonID = -1;
            int UserID = -1;
            bool isActive = false;


            if (clsUserData.Find(ref UserID, ref PersonID,  UserName, Password, ref isActive))
            {

                return new clsUser(UserID, PersonID, UserName, Password, isActive);



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
            this.UserID = clsUserData.AddNewUser(PersonID, UserName, Password, IsActive);
                return (UserID !=null);
        }

        private bool _UpdateUser()
        {
            return (clsUserData.UpdateUser((int)UserID,UserName,Password,IsActive));
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
      
        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsAlreadyUserExist(int PersonID)
        {
            return clsUserData.IsAlreadyUserExist(PersonID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetUsers();
        }
    }


}

