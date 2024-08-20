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

    }
}
