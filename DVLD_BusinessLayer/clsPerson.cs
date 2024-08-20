using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public PeopleDTO PDTO
        {
            get
            {
                return (new PeopleDTO(this.PersonID, this.NationalNo,
                this.FirstName, this.SecondName, this.ThirdName, this.LastName,
                this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email,
                this.Nationality, this._ImagePath));
            }
        }
        public Nullable<int> PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string FullName()
        {
            return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
        }
        public DateTime DateOfBirth { set; get; }
        public byte Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public byte Nationality { set; get; }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsPerson(PeopleDTO PDTO, enMode cMode = enMode.AddNew)
        {
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
            this.Mode = cMode;
        }

        public static clsPerson Find(string NationalNo)
        {
            PeopleDTO PDTO = clsPeopleData.FindByNationalNo(NationalNo);


            if (PDTO != null)
            {
                return new clsPerson(PDTO, enMode.Update);



            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(int PersonID)
        {
            PeopleDTO PDTO = clsPeopleData.FindByID(PersonID);


            if (PDTO != null)
            {
                return new clsPerson(PDTO, enMode.Update);



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

                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return _UpdatePerson();


                default:
                    return false;

            }
        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 
            this.PersonID = clsPeopleData.AddNewPerson(PDTO);
            return (PersonID != null);
        }

        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePerson(PDTO);
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleData.DeletePerson(PersonID);
        }

        public static bool DeletePerson(string NationalNo)
        {
            return clsPeopleData.DeletePerson(NationalNo);

        }

        public static bool IsPersonExist(int ID)
        {
            return clsPeopleData.IsPersonExist(ID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPeopleData.IsPersonExist(NationalNo);
        }

        public static List<PeopleDTO> GetAllPersons()
        {
            return clsPeopleData.GetPeople();
        }
    }


}