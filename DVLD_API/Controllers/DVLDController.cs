using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DVlD_BusinessLayer;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace DVLD_API.Controllers
{

    [Route("api/DVLD/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPerson _Person;
        public PeopleController(IPerson PersonInterface)
        {
            _Person = PersonInterface;
        }

        [HttpGet("AllPeople", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PersonViewDTO>> GetAllPeople()
        {
            var peopleList = _Person.GetAllAsync(); // Assuming `GetAllPersons` is implemented in `IPersonService`
            if (peopleList.Result == null || !peopleList.Result.Any())
            {
                return NotFound("No people found!");
            }
            return Ok(peopleList.Result);

        }

        
        
        [HttpGet("FindByID/{PersonID}", Name = "GetPersonByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> GetPersonByID(int PersonID)
        {

            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }

           var Person = _Person.GetPerson(PersonID);

            if (Person == null)
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }

            //here we get only the DTO object to send it back.
            PersonDTO PDTO = Person.Result.PersonInfo;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }




        [HttpGet("FindByNationalNo/{NationalNo}", Name = "GetPersontByNationalNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> GetPersontByNationalNo(string NationalNo)
        {

            var Person = _Person.GetPerson(NationalNo);

            if (Person == null)
            {
                return NotFound($"Person with NationalNo {NationalNo} not found.");
            }

            //here we get only the DTO object to send it back.
            PersonDTO PDTO = Person.Result.PersonInfo;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }


        [HttpPost("AddNewPerson",Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> AddPerson(PersonDTO NewPersonDTO)
        {
            switch (_Person.IsValid(NewPersonDTO))
            {
                case clsPerson.enPersonValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsPerson.enPersonValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsPerson.enPersonValidationType.UnderAge:
                    return BadRequest($"Invalid Date of birth  the age is under 18");

                case clsPerson.enPersonValidationType.NationalNoDuplicate:
                    return BadRequest($"The National number '{NewPersonDTO.NationalNo}' already exists.");
                
                case clsPerson.enPersonValidationType.WrongNationality:
                    return BadRequest($"The NationalityID '{NewPersonDTO.Nationality}' is out of range the NationalityID show between 1 and 193 .");
            }


            PersonDTO Person = new PersonDTO(NewPersonDTO.PersonID,NewPersonDTO.NationalNo, NewPersonDTO.FirstName, NewPersonDTO.SecondName, NewPersonDTO.ThirdName, NewPersonDTO.LastName,
                NewPersonDTO.DateOfBirth, NewPersonDTO.Gender, NewPersonDTO.Address, NewPersonDTO.Phone, NewPersonDTO.Email, NewPersonDTO.Nationality, NewPersonDTO.ImagePath);
            NewPersonDTO .PersonID= _Person.CreatePerson(Person).Result;

            //we return the DTO only not the full Person object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
           
           return CreatedAtRoute("GetPersonByID", new { PersonID = NewPersonDTO.PersonID }, NewPersonDTO);
            
        }




        [HttpPut("UpdatePerson/{PersonID}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> UpdatePerson(int PersonID, PersonDTO UpdatedPerson)
        {
             var Person = _Person.GetPerson(PersonID);

             if (Person == null)
             {
                 return NotFound($"Person with ID {PersonID} not found.");
             }

             switch (_Person.IsValid(UpdatedPerson))
             {
                 case clsPerson.enPersonValidationType.NullObject:
                     return BadRequest($"The Object is Null fill it ");

                 case clsPerson.enPersonValidationType.EmptyFileds:
                     return BadRequest($"Some fileds is empty,please fill it");

                 case clsPerson.enPersonValidationType.UnderAge:
                     return BadRequest($"Invalid Date of birth  the age is under 18");

                 case clsPerson.enPersonValidationType.NationalNoDuplicate:

                     if (Person.Result.NationalNo != UpdatedPerson.NationalNo)
                         return BadRequest($"The National number '{UpdatedPerson.NationalNo}' already exists.");
                     else
                         break;
                case clsPerson.enPersonValidationType.WrongNationality:
                    return BadRequest($"The NationalityID '{UpdatedPerson.Nationality}' is out of range the NationalityID show between 1 and 193 .");
            }

            Person.Result.NationalNo = UpdatedPerson.NationalNo;
            Person.Result.FirstName = UpdatedPerson.FirstName;
            Person.Result.SecondName = UpdatedPerson.SecondName;
            Person.Result.ThirdName = UpdatedPerson.ThirdName;
            Person.Result.LastName = UpdatedPerson.LastName;
            Person.Result.DateOfBirth = UpdatedPerson.DateOfBirth;
            Person.Result.Gender = UpdatedPerson.Gender;
            Person.Result.Address = UpdatedPerson.Address;
            Person.Result.Phone = UpdatedPerson.Phone;
            Person.Result.Email = UpdatedPerson.Email;
            Person.Result.Nationality = (clsPerson.enNatinoality) UpdatedPerson.Nationality;
            Person.Result.ImagePath = UpdatedPerson.ImagePath;

            var result = _Person.UpdatePerson(Person.Result.PersonInfo);

            return Ok(Person.Result.PersonInfo);

        }


        [HttpDelete("DeletePersonByID/{PersonID}", Name = "DeletePersonByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeletePerson(int PersonID)
        {
            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }
            var Deleting = _Person.DeleteAsync(PersonID);
            if (Deleting.Result)

                return Ok($"Person with ID {PersonID} has been deleted.");
            else
                return NotFound($"Person with ID {PersonID} not found. no rows deleted!");
        }
        
        
        
        
        
        [HttpDelete("DeletePersonBy/NationalNo{NationalNo}", Name = "DeletePersonByNationalNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeletePerson(string NationalNo)
        {
            
            var Deleting = _Person.DeleteAsync(NationalNo);
            if (Deleting.Result)

                return Ok($"Person with No {NationalNo} has been deleted.");
            else
                return NotFound($"Person with No {NationalNo} not found. no rows deleted!");
        }

        
        
        
        [HttpGet("IsPersonExistByID/{PersonID}", Name = "IsPersonExistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsPersonExist(int PersonID)
        {
            
            if (_Person.CheckExistAsync(PersonID).Result)
            {
                return Ok("The person exists." );

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
            if (_Person.CheckExistAsync(NationalNo).Result)
            {
                return Ok("The person exists.");

            }
            else
            {
                return NoContent();

            }
        }

    }

    
    [Route("api/DVLD/Users")]
    [ApiController]
    public class UsersController : ControllerBase

    {
        private IUser _User;
        public UsersController(IUser UserInterface)
        {
            _User = UserInterface;
        }

        [HttpGet("AllUsers", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UsersViewDTO>> GetAllUsers()
        {
           var UsersList = _User.GetAllUsers();
            if (UsersList.Result.Count() == 0)
            {
                return NotFound("No Users Found!");
            }
            return Ok(UsersList.Result); 

        }

       
        
        
        
        [HttpGet("FindByID/{UserID}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByID(int UserID)
        {

            if (UserID < 1)
            {
                return BadRequest($"Not accepted ID {UserID}");
            }

           var User =_User.FindByUserID(UserID);

            if (User == null)
            {
                return NotFound($"User with ID {UserID} not found.");
            }

            //here we get only the DTO object to send it back.

            //we return the DTO not the User object.
            return Ok(User.Result.UDTO);

        }





        [HttpGet("FindUserByPersonID/{PersonID}", Name = "GetUserByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByPersonID(int PersonID){

           var User = _User.FindByPersonID(PersonID);

            if (User == null)
            {
                return NotFound($"User with PersonID {PersonID} not found.");
            }

            //here we get only the DTO object to send it back.
            //we return the DTO not the User object.
            return Ok(User.Result.UDTO); 

        }
        



        
        [HttpGet("FindUserByUserName&Password/{UserName}/{Password}", Name = "GetUserByUserName&Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByUserNameAndPassword(string UserName,string Password)
        {

          var User = _User.Find(UserName,Password);

            if (User == null)
            {
                return NotFound($"User with these information not found.");
            }


            return Ok(User.Result.UDTO);

        }


        [HttpPost("AddNewUser", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> AddUser(UserDTO NewUserDTO)
        {

            switch (_User.IsValid(NewUserDTO))
            {
                case clsUser.enUserValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUser.enUserValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsUser.enUserValidationType.InvalidPersonID:
                    return BadRequest($"The personID {NewUserDTO.PersonID} is not exists");

                case clsUser.enUserValidationType.UserNameDuplicate:
                    return BadRequest($"The UserName '{NewUserDTO.UserName}' already exists.");

                case clsUser.enUserValidationType.AlreadyUser:
                    return BadRequest($"This person is already a user");
            }

            
            
            var User = _User.CreateUserAsync(new UserDTO(NewUserDTO.UserID, NewUserDTO.PersonID,
                    NewUserDTO.UserName, NewUserDTO.Password, NewUserDTO.IsActive));


            NewUserDTO.UserID = (User.Result==null)? 0: User.Result.Value;

            if (NewUserDTO.UserID != 0)
                return CreatedAtRoute("GetUserByID", new { UserID = NewUserDTO.UserID }, NewUserDTO);
            else
                return BadRequest("Adding Failed");

        }

       
        [HttpPut("UpdateUser/{UserID}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdateUser(int UserID, UserDTO UpdatedUser)
        {
           
           var User =_User.FindByUserID(UserID);

                 if (User == null)
                 {
                     return NotFound($"User with ID {UserID} not found.");
                 }


            switch (_User.IsValid(UpdatedUser))
            {
                case clsUser.enUserValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUser.enUserValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsUser.enUserValidationType.InvalidPersonID:
                    return BadRequest($"The personID {UpdatedUser.PersonID} is not exists");

               

                case clsUser.enUserValidationType.UserNameDuplicate:
                    if (User.Result.UserName != UpdatedUser.UserName)
                        return BadRequest($"The UserName '{UpdatedUser.UserName}' already exists.");
                    else
                        break;
            }

            User.Result.PersonID = UpdatedUser.PersonID;
            User.Result.UserName = UpdatedUser.UserName;
            User.Result.Password = UpdatedUser.Password;
            User.Result.IsActive = UpdatedUser.IsActive;


            var Result=_User.UpdateUser (User.Result.UDTO);

            return Ok(User.Result.UDTO);

             }
        
            
        [HttpDelete("DeleteUser/{UserID}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int UserID)
            {
                if (UserID < 1)
                {
                    return BadRequest($"Not accepted ID {UserID}");
                }

            if (_User.DeleteUser(UserID).Result)

                return Ok($"User with ID {UserID} has been deleted.");
            else
                return NotFound($"User with ID {UserID} not found. no rows deleted!");
        }


        [HttpGet("IsUserExist/{UserID}", Name = "IsUserExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsUserExists(int UserID)
            {
            if (_User.IsUserExist(UserID).Result)
            {
                return Ok("The user exists." );

            }
            else
            {
                return NoContent();

            }
        }
        

    }

    
    [Route("api/DVLD/Drivers")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IBLLDriver _Driver;
        public DriversController(IBLLDriver Driver)
        {
            _Driver = Driver;
        }
        [HttpGet("AllDrivers", Name = "GetAllDrivers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DriverViewDTO>> GetDriversList()
        {

            var DriversList = _Driver.GetAllDriver();
            if (DriversList.Result.Count() == 0)
            {
                return NotFound("No Users Found!");
            }
            return Ok(DriversList.Result); 

        }



        

        [HttpGet("FindByID/{DriverID}", Name = "GetDriverByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DriverDTO> GetDriverByID(int DriverID)
        {

            if (DriverID < 1)
            {
                return BadRequest($"Not accepted ID {DriverID}");
            }

            var Driver = _Driver.FindByDriverID(DriverID);

            if (Driver == null)
            {
                return NotFound($"Driver with ID {DriverID} not found.");
            }

            
           DriverDTO DDTO = Driver.Result.DDTO;

            return Ok(DDTO);

        }


       


        [HttpGet("FindDriverByPersonID/{PersonID}", Name = "GetDriverByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DriverDTO> GetDriverByPersonID(int PersonID)
        {

            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }

            var Driver = _Driver.FindByPersonID(PersonID);

            if (Driver == null)
            {
                return NotFound($"Driver with PersonID {PersonID} not found.");
            }


            DriverDTO DDTO = Driver.Result.DDTO;

            return Ok(DDTO);

        }





       
        [HttpPost("AddNewDriver", Name = "AddDriver")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DriverDTO> AddDriver(DriverDTO NewDriverDTO)
        {

            switch (_Driver.IsValid(NewDriverDTO))
            {
                case clsDriver.enDriverValidationTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsDriver.enDriverValidationTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsDriver.enDriverValidationTypes.InvalidPersonID:
                    return BadRequest($"The personID {NewDriverDTO.PersonID} is not exists");

                case clsDriver.enDriverValidationTypes.AlreadyDriver:
                    return BadRequest($"This person is already a Driver");
            }

            var Driver = _Driver.AddNewDriver(new DriverDTO(NewDriverDTO.DriverID, NewDriverDTO.PersonID,NewDriverDTO.CreatedByUserID,
                                                            NewDriverDTO.CreatedDate));


            NewDriverDTO.DriverID = (Driver.Result == null) ? 0 : Driver.Result.Value;

            if (NewDriverDTO.DriverID != 0)
                return CreatedAtRoute("GetDriverByID", new { DriverID = NewDriverDTO.DriverID }, NewDriverDTO);
            else
                return BadRequest("Adding Failed");



        }

        
        [HttpPut("UpdateDriver/{DriverID}", Name = "UpdateDriver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DriverDTO> UpdatDriver(int DriverID, DriverDTO UpdatedDriver)
        {

           var Driver = _Driver.FindByDriverID(DriverID);

            if (Driver == null)
            {
                return NotFound($"Driver with ID {DriverID} not found.");
            }
           
            switch (_Driver.IsValid(UpdatedDriver))
            {
                case clsDriver.enDriverValidationTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsDriver.enDriverValidationTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

            }

            Driver.Result.PersonID = UpdatedDriver.PersonID;
            Driver.Result.CreatedDate = UpdatedDriver.CreatedDate;
            Driver.Result.CreatedByUserID = UpdatedDriver.CreatedByUserID;


            var Result = _Driver.UpdateDriver(Driver.Result.DDTO);

            return Ok(Driver.Result.DDTO);

        }


       
       [HttpGet("IsDriverExists/{DriverID}", Name = "IsDriverExist")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status204NoContent)]
       public ActionResult<bool> IsDriverExists(int DriverID)
       {
           if (_Driver.IsDriverExists(DriverID).Result)
           {
               return Ok("The Driver exists." );

           }
           else
           {
               return NoContent();

           }
       }

        [HttpGet("IsDriverExistsByPersonID/{PersonID}", Name = "IsDriverExistsByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsDriverExistsByPersonID(int PersonID)
        {
            if (_Driver.IsDriverExistByPersonID(PersonID).Result)
            {
                return Ok("The Driver exists.");

            }
            else
            {
                return NoContent();

            }
        }
        

    }
}
