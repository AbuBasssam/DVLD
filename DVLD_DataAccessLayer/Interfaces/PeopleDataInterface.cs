using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Interfaces
{
    public interface   IPeopleDataInterface
    {
        Task<PersonDTO> FindByNationalNoAsync(string NationalNO);
        Task<PersonDTO> FindByIDAsync(int PersonID);
        Task<int?> AddNewPersonAsync(PersonDTO PeopleDTO);
        Task<bool> UpdatePersonAsync(PersonDTO PeopleDTO);
        Task<bool> DeletePersonAsync(int PersonID);
        Task<bool> DeletePersonAsync(string NationalNo);
        Task<bool> IsPersonExistAsync(int PersonID);
        Task<bool> IsPersonExistAsync(string NationalNo);
        Task<IEnumerable<PersonViewDTO>> GetPeopleAsync();


    }
}
