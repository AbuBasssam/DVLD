using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DVlD_BusinessLayer;
using DVLD_DataAccessLayer;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;

namespace DVLD_API.Controllers
{
    //[Route("api/DVLD")]
    //[ApiController]
    //public class DVLDController : ControllerBase
    //{
        


    //}

    [Route("api/DVLD/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet("AllPeople", Name = "GetAllPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ListPersonDTO>> GetAllPeople()
        {
            List<ListPersonDTO> PeopleList = DVlD_BusinessLayer.clsPerson.GetAllPersons();
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
        public ActionResult<PersonDTO> GetPersonByID(int PersonID)
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
            PersonDTO PDTO = Person.PDTO;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }




        [HttpGet("FindByNationalNo/{NationalNo}", Name = "GetPersontByNationalNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> GetPersontByNationalNo(string NationalNo)
        {

            DVlD_BusinessLayer.clsPerson Person = DVlD_BusinessLayer.clsPerson.Find(NationalNo);

            if (Person == null)
            {
                return NotFound($"Person with NationalNo {NationalNo} not found.");
            }

            //here we get only the DTO object to send it back.
            PersonDTO PDTO = Person.PDTO;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }


        [HttpPost("AddNewPerson",Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> AddPerson(PersonDTO NewPersonDTO)
        {
            //this code without verfying opreatoin
            switch (clsUtil.PersonCheckConstraints(NewPersonDTO))
            {
                case clsUtil.enPersonBadRequestTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUtil.enPersonBadRequestTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsUtil.enPersonBadRequestTypes.UnderAge:
                    return BadRequest($"Invalid Date of birth  the age is under 18");

                case clsUtil.enPersonBadRequestTypes.NationalNoDuplicate:
                    return BadRequest($"The National number '{NewPersonDTO.NationalNo}' already exists.");
            }
            DVlD_BusinessLayer.clsPerson Person = new DVlD_BusinessLayer.clsPerson(new PersonDTO(NewPersonDTO.PersonID, NewPersonDTO.NationalNo,
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
        public ActionResult<PersonDTO> UpdatePerson(int PersonID, PersonDTO UpdatedPerson)
        {
            DVlD_BusinessLayer.clsPerson Person = DVlD_BusinessLayer.clsPerson.Find(PersonID);

            if (Person == null)
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }
            switch (clsUtil.PersonCheckConstraints(UpdatedPerson))
            {
                case clsUtil.enPersonBadRequestTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUtil.enPersonBadRequestTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsUtil.enPersonBadRequestTypes.UnderAge:
                    return BadRequest($"Invalid Date of birth  the age is under 18");

                case clsUtil.enPersonBadRequestTypes.NationalNoDuplicate:

                    if (Person.NationalNo != UpdatedPerson.NationalNo)
                        return BadRequest($"The National number '{UpdatedPerson.NationalNo}' already exists.");
                    else
                        break;
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
        public ActionResult DeletePerson(int PersonID)
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


    [Route("api/DVLD/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("AlUsers", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ListUsersDTO>> GetAllUsers()
        {
            List<ListUsersDTO> UsersList = DVlD_BusinessLayer.clsUser.GetAllUsers();
            if (UsersList.Count == 0)
            {
                return NotFound("No Users Found!");
            }
            return Ok(UsersList); // Returns the list of students.

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

            DVlD_BusinessLayer.clsUser User = DVlD_BusinessLayer.clsUser.FindByUserID(UserID);

            if (User == null)
            {
                return NotFound($"User with ID {UserID} not found.");
            }

            //here we get only the DTO object to send it back.
            UserDTO UDTO = User.UDTO;

            //we return the DTO not the student object.
            return Ok(UDTO);

        }





        [HttpGet("FindUserByPersonID/{PersonID}", Name = "GetUserByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByPersonID(int PersonID){

            DVlD_BusinessLayer.clsUser User = DVlD_BusinessLayer.clsUser.FindByPersonID(PersonID);

            if (User == null)
            {
                return NotFound($"User with PersonID {PersonID} not found.");
            }

            //here we get only the DTO object to send it back.
            UserDTO UDTO = User.UDTO;

            //we return the DTO not the student object.
            return Ok(UDTO);

        }
        



        
        [HttpGet("FindUserByUserName&Password/{UserName}/{Password}", Name = "GetUserByUserName&Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByUserNameAndPassword(string UserName,string Password)
        {

            DVlD_BusinessLayer.clsUser User = DVlD_BusinessLayer.clsUser.Find(UserName,Password);

            if (User == null)
            {
                return NotFound($"User with these information not found.");
            }

            UserDTO UDTO = User.UDTO;

            return Ok(UDTO);

        }


        [HttpPost("AddNewUser", Name = "AddUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> AddUser(UserDTO NewUserDTO)
        {
            
            switch (clsUtil.UserCheckConstraints(NewUserDTO))
            {
                case clsUtil.enUserBadRequestTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUtil.enUserBadRequestTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsUtil.enUserBadRequestTypes.InvalidPersonID:
                    return BadRequest($"The personID {NewUserDTO.PersonID} is not exists");

                case clsUtil.enUserBadRequestTypes.UserNameDuplicate:
                    return BadRequest($"The UserName '{NewUserDTO.UserName}' already exists.");
                
                case clsUtil.enUserBadRequestTypes.AlreadyUser:
                    return BadRequest($"This person is already a user");
            }

            DVlD_BusinessLayer.clsUser User = new DVlD_BusinessLayer.clsUser(new UserDTO(NewUserDTO.UserID, NewUserDTO.PersonID,
                     NewUserDTO.UserName, NewUserDTO.Password, NewUserDTO.IsActive));

                     User.Save();

                     NewUserDTO.UserID = Convert.ToInt32(User.UserID);

                
                 return CreatedAtRoute("GetUserByID", new { UserID = NewUserDTO.UserID }, NewUserDTO);

        }

       
        [HttpPut("UpdateUser/{UserID}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdateUser(int UserID, UserDTO UpdatedUser)
        {
           
            DVlD_BusinessLayer.clsUser User = DVlD_BusinessLayer.clsUser.FindByUserID(UserID);

                 if (User == null)
                 {
                     return NotFound($"User with ID {UserID} not found.");
                 }


            switch (clsUtil.UserCheckConstraints(UpdatedUser))
            {
                case clsUtil.enUserBadRequestTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUtil.enUserBadRequestTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsUtil.enUserBadRequestTypes.InvalidPersonID:
                    return BadRequest($"The personID {UpdatedUser.PersonID} is not exists");

                case clsUtil.enUserBadRequestTypes.AlreadyUser:
                    return BadRequest($"This person is already a user");

                case clsUtil.enUserBadRequestTypes.UserNameDuplicate:
                    if (User.UserName != UpdatedUser.UserName)
                        return BadRequest($"The UserName '{UpdatedUser.UserName}' already exists.");
                    else
                        break;
            }

                 User.PersonID = UpdatedUser.PersonID;
                 User.UserName = UpdatedUser.UserName;
                 User.Password = UpdatedUser.Password;
                 User.IsActive = UpdatedUser.IsActive;
                 

                 User.Save();

                 return Ok(User.UDTO);

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

                if (DVlD_BusinessLayer.clsUser.DeleteUser(UserID))

                    return Ok($"User with ID {UserID} has been deleted.");
                else
                    return NotFound($"User with ID {UserID} not found. no rows deleted!");
            }
        
        
        [HttpGet("IsUserExist/{UserID}", Name = "IsUserExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsUserExists(int UserID)
            {
                if (clsPerson.IsPersonExist(UserID))
                {
                    return Ok(new { Exists = true, Message = "The user exists." });

                }
                else
                {
                  return NoContent();

                }
            }
        

    }

}
