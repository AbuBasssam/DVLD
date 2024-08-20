using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DVlD_BusinessLayer;
using DVLD_DataAccessLayer;

namespace DVLD_API.Controllers
{
    [Route("api/DVLD")]
    [ApiController]
    public class DVLDController : ControllerBase
    {

       
        
    }


    [Route("api/DVLD/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("AllPeople", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PeopleDTO>> GetAllStudents()
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

            DVlD_BusinessLayer.clsPerson Person = new DVlD_BusinessLayer.clsPerson(new PeopleDTO(NewPersonDTO.PersonID, NewPersonDTO.NationalNo,
                NewPersonDTO.FirstName, NewPersonDTO.SecondName, NewPersonDTO.ThirdName, NewPersonDTO.LastName,
                NewPersonDTO.DateOfBirth, NewPersonDTO.Gender, NewPersonDTO.Address, NewPersonDTO.Phone, NewPersonDTO.Email,
                NewPersonDTO.Nationality, NewPersonDTO.ImagePath));

            Person.Save();

            NewPersonDTO.PersonID = Person.PersonID;

            //we return the DTO only not the full Person object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
            return CreatedAtRoute("GetPersonByID", new { PersonID = NewPersonDTO.PersonID }, NewPersonDTO);

        }


        [HttpPut("UpdatePerson/{PersonID}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PeopleDTO> UpdateStudent(int PersonID, PeopleDTO UpdatedPerson)
        {
            //this code without verfying opreatoin


            DVlD_BusinessLayer.clsPerson Person = DVlD_BusinessLayer.clsPerson.Find(PersonID);


            if (Person == null)
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }

            Person.NationalNo = UpdatedPerson.NationalNo;
            Person.FirstName = UpdatedPerson.FirstName;
            Person.SecondName = UpdatedPerson.SecondName;
            Person.ThirdName = UpdatedPerson.ThirdName;
            Person.LastName = UpdatedPerson.LastName;
            Person.DateOfBirth = UpdatedPerson.DateOfBirth;
            Person.Gender = UpdatedPerson.Gender;
            Person.Address = UpdatedPerson.Address;
            Person.Phone = UpdatedPerson.Phone;
            Person.Email = UpdatedPerson.Email;
            Person.Nationality = UpdatedPerson.Nationality;
            Person.ImagePath = UpdatedPerson.ImagePath;
            Person.Save();

            return Ok(Person.PDTO);

        }

        //here we use HttpDelete method
        [HttpDelete("DeletePerson/{PersonID}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int PersonID)
        {
            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }

            if (DVlD_BusinessLayer.clsPerson.DeletePerson(PersonID))

                return Ok($"Student with ID {PersonID} has been deleted.");
            else
                return NotFound($"Student with ID {PersonID} not found. no rows deleted!");
        }

        [HttpGet("IsPersonExistByID/{PersonID}", Name = "IsPersonExistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsPersonExist(int PersonID)
        {
            if (clsPerson.IsPersonExist(PersonID))
            {
                return Ok(new { Exists = true, Message = "The person exists." });

            }
            else
            {
                return NoContent();

            }
        }
        
        
        [HttpGet("IsPersonExistByNationalNo/{NationalNo}", Name = "IsPersonExistByNationalNo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> IsPersonExist(string NationalNo)
        {
            if (clsPerson.IsPersonExist(NationalNo))
            {
                return Ok(new { Exists = true, Message = "The person exists." });
            }
            else
            {
                return NoContent();

            }
        }

    }

}
