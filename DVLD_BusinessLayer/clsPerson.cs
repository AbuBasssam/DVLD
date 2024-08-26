using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Interfaces;
using DVlD_BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Entities;

namespace DVlD_BusinessLayer
{
   
    public class clsPerson:IPerson
    {
        private  IPeopleDataInterface _PeopleDataInterface { get; set; }

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        private string GetFullName()
        {
            return  this.FirstName + " " + this.SecondName + " " + this.ThirdName + " " + this.LastName;
        }
        
        public PersonDTO PersonInfo
        { 
            get
            {
                return new PersonDTO(this.PersonID, NationalNo,
                FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gender, Address, Phone,
                Email, Nationality, _ImagePath);
            }
        }
        public int? PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
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
        public string FullName()
        {
           return GetFullName();
        }
       
        private clsPerson(IPeopleDataInterface PeopleDataInterface,PersonDTO PDTO)
        {
            this._PeopleDataInterface = PeopleDataInterface;
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
            this._ImagePath = PDTO.ImagePath;
            
            this.Mode = enMode.Update;
        }
        
        public clsPerson(IPeopleDataInterface peopleDataInterface)
        {
            this._PeopleDataInterface= peopleDataInterface;
            this.Mode=enMode.AddNew;
        }
      
        public async Task<PersonDTO> GetPerson(string NationalNo)
        {
            PersonDTO PDTO =await _PeopleDataInterface.FindByNationalNoAsync(NationalNo);

            return (PDTO != null) ? new clsPerson(_PeopleDataInterface, PDTO).PersonInfo : null;

        }
        
        public async Task<PersonDTO> GetPerson(int PersonID)
        {
            PersonDTO PDTO = await _PeopleDataInterface.FindByIDAsync(PersonID);

            return (PDTO != null) ? new clsPerson(_PeopleDataInterface, PDTO).PersonInfo : null;

        }

        public async Task<bool> CheckExistAsync(string NationalNo)
        {
            return await _PeopleDataInterface.IsPersonExistAsync(NationalNo);

        }

        public async Task<bool> CheckExistAsync(int PersonID)
        {

            return await _PeopleDataInterface.IsPersonExistAsync(PersonID);

        }

        public async Task<int?> CreatePerson(PersonDTO PersonDTO)
        {

             return await _PeopleDataInterface.AddNewPersonAsync(PersonDTO);
            
        }

        public async Task<bool> UpdatePerson(PersonDTO PersonDTO)
        {
            return await _PeopleDataInterface.UpdatePersonAsync(PersonDTO);
        }

        public async Task<bool> DeleteAsync(int PersonID)
        {
            
            return await _PeopleDataInterface.DeletePersonAsync(PersonID);
        }

        public  async Task<bool> DeleteAsync(string NationalNo)
        {
            
            return await _PeopleDataInterface.DeletePersonAsync(NationalNo);

        }

        
        public  async Task<IEnumerable<PersonViewDTO>> GetAllAsync()
        {
            return await _PeopleDataInterface.GetPeopleAsync();



        }


        /*public async Task<bool> SaveAsync(PersonDTO Person = null, enMode Mode = enMode.AddNew)
        {
            Person = Person ?? this.PersonInfo;
            Mode = Mode == enMode.AddNew ? this.Mode : Mode;

            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _CreatePerson(Person))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return await _UpdatePerson(Person);


                default:
                    return false;

            }
        }*/

        /*        public static async Task<bool> DeletePersonAsync(int PersonID)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            return await DataLayer.DeletePersonAsync(PersonID);
        }*/


        /* public static async Task<bool> DeletePersonAsync(string NationalNo)
         {
             clsPeopleData DataLayer = new clsPeopleData();
             return await DataLayer.DeletePersonAsync(NationalNo);

         }
 */


        /*public static async Task<bool> IsPersonExistAsync(int ID)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            return await DataLayer.IsPersonExistAsync(ID);
        }*/

        /*public static async Task<bool> IsPersonExistAsync(string NationalNo)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            return await DataLayer.IsPersonExistAsync(NationalNo);
        }*/

        /* public static async Task<IEnumerable<PersonViewDTO>> GetAllPeopleAsync()
         {
             clsPeopleData DataLayer = new clsPeopleData();
             return await DataLayer.GetPeopleAsync();

         }*/

        /* public static async Task<clsPerson> Find(string NationalNo)
       {
           clsPeopleData DataLayer = new clsPeopleData();
           PersonDTO PDTO =await DataLayer.FindByNationalNoAsync(NationalNo);

           return (PDTO != null) ? new clsPerson(DataLayer, PDTO) : null;





       }*/

        /*public static async Task<clsPerson> Find(int PersonID)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            PersonDTO PDTO = await DataLayer.FindByIDAsync(PersonID);

            return (PDTO != null) ? new clsPerson(DataLayer, PDTO) : null;
        }*/

    }


}