using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DVlD_BusinessLayer;
using DVLD_DataAccessLayer;

namespace DVLD_API.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("AllPeople", Name = "GetAllPeople")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PeopleDTO>> GetAllStudents() // Define a method to get all students.
        {
            List<PeopleDTO> PeopleList = DVlD_BusinessLayer.clsPerson.GetAllPersons();
            if (PeopleList.Count == 0)
            {
                return NotFound("No Students Found!");
            }
            return Ok(PeopleList); // Returns the list of students.

        }

        [HttpGet("FindByID/{PersonID}", Name = "GetPersonByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<PeopleDTO> GetPersontByID(int PersonID)
        {

            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }

            DVlD_BusinessLayer.clsPerson Person = DVlD_BusinessLayer.clsPerson.Find(PersonID);

            if (Person == null)
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }

            //here we get only the DTO object to send it back.
            PeopleDTO PDTO = Person.PDTO;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }
       
        [HttpGet("FindByNationalNo/{NationalNo}", Name = "GetPersontByNationalNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PeopleDTO> GetPersontByNationalNo(string NationalNo)
        {

           

            DVlD_BusinessLayer.clsPerson Person = DVlD_BusinessLayer.clsPerson.Find(NationalNo);

            if (Person == null)
            {
                return NotFound($"Student with NationalNo {NationalNo} not found.");
            }

            //here we get only the DTO object to send it back.
            PeopleDTO PDTO = Person.PDTO;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }

        
        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PeopleDTO> AddStudent(PeopleDTO NewPersonDTO)
        {
            //this code without verfying opreatoin
           
            DVlD_BusinessLayer.clsPerson Person = new DVlD_BusinessLayer.clsPerson(new PeopleDTO( NewPersonDTO.PersonID, NewPersonDTO.NationalNo,
                NewPersonDTO.FirstName, NewPersonDTO.SecondName, NewPersonDTO.ThirdName, NewPersonDTO.LastName,
                NewPersonDTO.DateOfBirth, NewPersonDTO.Gender, NewPersonDTO.Address, NewPersonDTO.Phone, NewPersonDTO.Email,
                NewPersonDTO.Nationality, NewPersonDTO.ImagePath));
            
            Person.Save();

            NewPersonDTO.PersonID = Person.PersonID;

            //we return the DTO only not the full Person object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
            return CreatedAtRoute("GetPersonByID", new { PersonID = NewPersonDTO.PersonID }, NewPersonDTO);

        }


    }
}
