using DVLD_DataAccessLayer.Interfaces;
using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer.Interfaces
{
    public interface IPerson
    {
        Task<clsPerson> GetPerson(string NationalNo);
        Task<clsPerson> GetPerson(int PersonID);
        Task<int?> CreatePerson(PersonDTO PersonDTO);
        Task<bool> UpdatePerson(PersonDTO PersonDTO);
        Task<bool> DeleteAsync(int PersonID);
        Task<bool> DeleteAsync(string NationalNo);
        Task<bool> CheckExistAsync(int ID);
        Task<bool> CheckExistAsync(string NationalNo);
        Task<IEnumerable<PersonViewDTO>> GetAllAsync();
        

    }
}
