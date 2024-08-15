using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;

namespace DVlD_BusinessLayer
{
    public class clsDriver:clsPerson
    {
        public enum enMode { AddNew, Update };
        
        public enMode Mode = enMode.AddNew; 
        public int DriverID {  get; set; }
        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate { get; set; }   
        public clsUser UserInfo { get; set; }
        
        public clsDriver()
        {
            CreatedByUserID = -1;
            DriverID = -1;
            CreatedDate = DateTime.Now;
            Mode= enMode.AddNew;
        }
       
        private clsDriver(int DriverID,int CreatedByUserID, DateTime CreatedDate, int PersonID, string NationalNo, string FirstName, string SecondName
           , string ThirdName, string LastName, DateTime DateOfBirth, byte Gender,
           string Address, string Phone, string Email, byte Nationality, string ImagePath)
        {
            this.DriverID = DriverID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            this.UserInfo = clsUser.FindByUserID(CreatedByUserID);
            
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.Nationality = Nationality;
            this.ImagePath = ImagePath;
            this.Mode = enMode.Update;
        }


        public static  clsDriver FindByDriverID(int DriverID)
        {
            int CreatedByUserID = -1, PersonID = -1 ;
            DateTime CreatedDate = DateTime.Now;
            
            bool Found=clsDriverData.FindByDriverID(DriverID,ref PersonID,ref CreatedByUserID,ref CreatedDate);
            if (Found)
            {
                clsPerson Person = clsPerson.Find(PersonID);
                return new clsDriver(DriverID, CreatedByUserID, CreatedDate, (int)Person.PersonID, Person.NationalNo, Person.FirstName, Person.SecondName,
                    Person.ThirdName, Person.LastName, Person.DateOfBirth, Person.Gender,
                    Person.Address, Person.Phone, Person.Email, Person.Nationality, Person.ImagePath);
            }
            else
               return null;



        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int CreatedByUserID = -1, DriverID = -1;
            DateTime CreatedDate = DateTime.Now;
            bool Found = false;
            Found = clsDriverData.FindByPersonID(ref DriverID,  PersonID, ref CreatedByUserID, ref CreatedDate);
            if (Found)
            {
                clsPerson Person = clsPerson.Find(PersonID);
                return new clsDriver(DriverID, CreatedByUserID, CreatedDate, (int)Person.PersonID, Person.NationalNo, Person.FirstName, Person.SecondName,
                    Person.ThirdName, Person.LastName, Person.DateOfBirth, Person.Gender,
                    Person.Address, Person.Phone, Person.Email, Person.Nationality, Person.ImagePath);
            }
            else
                return null;



        }
        public static DataTable GetAllDriver()
        {
            return clsDriverData.GetAllDrivers();
        }
        
        private bool _AddNewDriver()
        {
            this.DriverID=clsDriverData.AddNewDriver((int)PersonID,CreatedByUserID);
            return (this.DriverID != -1);
        }
       
        private bool _UpdateDriver()
        {

            return clsDriverData.UpdateDriver(DriverID, (int)PersonID, CreatedByUserID);
        }

        public bool Delete(int DriverID)
        {
           return clsDriverData.DeleteDriver(DriverID);
        }
        
        public bool DeleteByPersonID(int PersonID)
        {
            return clsDriverData.DeleteDriverByPersonID(PersonID);
        }
        public static bool IsDriverExistByPersonID( int PersonID)
        {
            return clsDriverData.IsDriverExistByPersonID(PersonID);
        }

        public bool Save()
        {
            //base.Mode = (clsPerson.enMode)Mode;
            //if(!base.Save())
            //    return false;
            
            
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return _UpdateDriver();


                default:
                    return false;
            }
        }

        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetAllDriverLicenses(DriverID);
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetAllDriverInternationalLicenses(DriverID);
        }

        public  DataTable GetLicenses()
        {
            return clsLicense.GetAllDriverLicenses(DriverID);
        }

        public  DataTable GetInternationalLicenses()
        {
            return clsInternationalLicense.GetAllDriverInternationalLicenses(DriverID);
        }

















    }
}
