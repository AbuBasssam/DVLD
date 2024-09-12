using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVlD_BusinessLayer.clsPerson;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IPerson
    {
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
        
        /// <summary>
        /// enNatinoality iss  List of all possible nationality
        /// </summary>
        public enNatinoality Nationality {  set; get; }
        public string ImagePath{set; get;}

        Task<clsPerson> GetPerson(string NationalNo);
        Task<clsPerson> GetPerson(int PersonID);
        Task<int?> CreatePerson(PersonDTO PersonDTO);
        Task<bool> UpdatePerson(PersonDTO PersonDTO);
        Task<bool> DeleteAsync(int PersonID);
        Task<bool> DeleteAsync(string NationalNo);
        Task<bool> CheckExistAsync(int ID);
        Task<bool> CheckExistAsync(string NationalNo);
        Task<IEnumerable<PersonViewDTO>> GetAllAsync();
       
        /// <summary>
        /// should check if there Empty Fileds
        /// , the person isn't under age,have unique National No and  the Nationality Range between(1,193)
        /// 
        /// </summary>
        /// <param name="PDTO"></param>
        /// <returns>
        /// return Valid if he's valid else return the validation error 
        /// </returns>
        enPersonValidationType IsValid(PersonDTO PDTO);
    }
}
