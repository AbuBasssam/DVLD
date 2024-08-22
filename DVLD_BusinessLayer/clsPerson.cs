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
        public PersonDTO PDTO
        {
            get
            {
                return (new PersonDTO(this.PersonID, this.NationalNo,
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
        public int Nationality { set; get; }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsPerson(PersonDTO PDTO, enMode cMode = enMode.AddNew)
        {
            this.PersonID = PDTO.PersonID;
            this.NationalNo = PDTO.NationalNo;
            this.FirstName = PDTO.FirstName;
            this.SecondName = PDTO.SecondName;
            this.ThirdName = PDTO.ThirdName;
            this.LastName = PDTO.LastName;
            this.DateOfBirth = PDTO.DateOfBirth;
            this.Gender = PDTO.Gender;
            this.Address = PDTO.Address;
            this.Phone = PDTO.Phone;
            this.Email = PDTO.Email;
            this.Nationality = PDTO.Nationality;
            this.ImagePath = PDTO.ImagePath;
            this.Mode = cMode;
        }

        public static clsPerson Find(string NationalNo)
        {
            PersonDTO PDTO = clsPeopleData.FindByNationalNo(NationalNo);


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
            PersonDTO PDTO = clsPeopleData.FindByID(PersonID);


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

        public static List<ListPersonDTO> GetAllPersons()
        {
            return clsPeopleData.GetPeople();
        }
    }


}