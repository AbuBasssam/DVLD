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

        public static async Task< clsPerson> Find(string NationalNo)
        {
            PersonDTO PDTO=MapToDTO(await clsPeopleData.FindByNationalNoAsync(NationalNo));

            return( PDTO!=null)?new clsPerson(PDTO, enMode.Update): null;





        }

        public static async Task<clsPerson> Find(int PersonID)
        {
            PersonDTO PDTO = MapToDTO(await clsPeopleData.FindByIDAsync(PersonID));

            return (PDTO != null) ? new clsPerson(PDTO, enMode.Update) : null;
        }

        public async Task< bool> SaveAsync()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return await _UpdatePerson();


                default:
                    return false;

            }
        }

        private async Task< bool> _AddNewPerson()
        {

            this.PersonID =await clsPeopleData.AddNewPersonAsync(MapToEntity(PDTO));
            return (PersonID != null);
        }

        private async Task<bool> _UpdatePerson()
        {
            return await clsPeopleData.UpdatePersonAsync(MapToEntity(PDTO));
        }

        public static async Task<bool> DeletePersonAsync(int PersonID)
        {
            return await clsPeopleData.DeletePersonAsync(PersonID);
        }

        public static async Task<bool> DeletePersonAsync(string NationalNo)
        {
            return await clsPeopleData.DeletePersonAsync(NationalNo);

        }

        public static async Task<bool> IsPersonExistAsync(int ID)
        {
            return await clsPeopleData.IsPersonExistAsync(ID);
        }

        public static async Task<bool> IsPersonExistAsync(string NationalNo)
        {
            return await clsPeopleData.IsPersonExistAsync(NationalNo);
        }

        public static async Task <IEnumerable<ListPersonDTO>> GetAllPeopleAsync()
        {
            var persons = await clsPeopleData.GetPeopleAsync();
            return MapToLDTOs(persons);
        }
        
        private static PersonDTO MapToDTO(Person Person)
        {
            return new PersonDTO(

               Person.PersonID,
               Person.NationalNo,
               Person.FirstName,
               Person.SecondName,
               Person.ThirdName,
               Person.LastName,
               Person.DateOfBirth,
               Person.Gender,
               Person.Address,
               Person.Phone,
               Person.Email,
               Person.Nationality,
               Person.ImagePath

            );
        }
        
        private static ListPersonDTO MapToLDTO(PersonView Person)
        {
            PersonDTO PDTO = new PersonDTO(Person.PersonID,
               Person.NationalNo,
               Person.FirstName,
               Person.SecondName,
               Person.ThirdName,
               Person.LastName,
               Person.DateOfBirth,
               Person.Gender,
               Person.Address,
               Person.Phone,
               Person.Email,
               Person.Nationality,
               Person.ImagePath);

            return new ListPersonDTO(PDTO,Person.CountryName,Person.Genderstr);
        }
        
        private Person MapToEntity(PersonDTO Person)
        {
            return new Person(

                Person.PersonID,
                Person.NationalNo,
                Person.FirstName,
                Person.SecondName,
                Person.ThirdName,
                Person.LastName,
                Person.DateOfBirth,
                Person.Gender,
                Person.Address,
                Person.Phone,
                Person.Email,
                Person.Nationality,
                Person.ImagePath

            );
        }
        
        private static IEnumerable<ListPersonDTO> MapToLDTOs(IEnumerable<PersonView> persons)
        {
            var personDTOs = new List<ListPersonDTO>();
            foreach (var person in persons)
            {
                personDTOs.Add(MapToLDTO(person));
            }
            return personDTOs;
        }
    }


}