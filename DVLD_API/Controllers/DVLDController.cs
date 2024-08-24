﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DVlD_BusinessLayer;
using DVlD_BusinessLayer.DTOs;
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
            var PeopleList = clsPerson.GetAllPeopleAsync();
            if (PeopleList==null)
            {
                return NotFound("No Students Found!");
            }
            return Ok(PeopleList); // Returns the list of People.

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

           var Person = clsPerson.Find(PersonID);

            if (Person == null)
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }

            //here we get only the DTO object to send it back.
            PersonDTO PDTO = Person.Result.PDTO;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }




        [HttpGet("FindByNationalNo/{NationalNo}", Name = "GetPersontByNationalNo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> GetPersontByNationalNo(string NationalNo)
        {

            var Person = clsPerson.Find(NationalNo);

            if (Person == null)
            {
                return NotFound($"Person with NationalNo {NationalNo} not found.");
            }

            //here we get only the DTO object to send it back.
            PersonDTO PDTO = Person.Result.PDTO;

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
           clsPerson Person = new clsPerson(new PersonDTO(NewPersonDTO.PersonID, NewPersonDTO.NationalNo,
                NewPersonDTO.FirstName, NewPersonDTO.SecondName, NewPersonDTO.ThirdName, NewPersonDTO.LastName,
                NewPersonDTO.DateOfBirth, NewPersonDTO.Gender, NewPersonDTO.Address, NewPersonDTO.Phone, NewPersonDTO.Email,
                NewPersonDTO.Nationality, NewPersonDTO.ImagePath));

            var result = Person.SaveAsync();

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
            var Person = clsPerson.Find(PersonID);

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

                    if (Person.Result.NationalNo != UpdatedPerson.NationalNo)
                        return BadRequest($"The National number '{UpdatedPerson.NationalNo}' already exists.");
                    else
                        break;
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
            Person.Result.Nationality = UpdatedPerson.Nationality;
            Person.Result.ImagePath = UpdatedPerson.ImagePath;

            var result = Person.Result.SaveAsync();

            return Ok(Person.Result.PDTO);

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
            var Deleting = clsPerson.DeletePersonAsync(PersonID);
            if (Deleting.Result)

                return Ok($"Student with ID {PersonID} has been deleted.");
            else
                return NotFound($"Student with ID {PersonID} not found. no rows deleted!");
        }

        [HttpGet("IsPersonExistByID/{PersonID}", Name = "IsPersonExistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsPersonExist(int PersonID)
        {
            var Exist=clsPerson.IsPersonExistAsync(PersonID);
            if (Exist.Result)
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
            var Exist= clsPerson.IsPersonExistAsync(NationalNo);
            if (Exist.Result)
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
        [HttpGet("AllUsers", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UsersViewDTO>> GetAllUsers()
        {
           var UsersList = clsUser.GetAllUsers().Result;
            if (UsersList.Count() == 0)
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

           var User =clsUser.FindByUserID(UserID);

            if (User == null)
            {
                return NotFound($"User with ID {UserID} not found.");
            }

            //here we get only the DTO object to send it back.
            UserDTO UDTO = User.Result.UDTO;

            //we return the DTO not the student object.
            return Ok(UDTO);

        }





        [HttpGet("FindUserByPersonID/{PersonID}", Name = "GetUserByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByPersonID(int PersonID){

           var User = clsUser.FindByPersonID(PersonID);

            if (User == null)
            {
                return NotFound($"User with PersonID {PersonID} not found.");
            }

            //here we get only the DTO object to send it back.
            UserDTO UDTO = User.Result.UDTO;

            //we return the DTO not the student object.
            return Ok(UDTO);

        }
        



        
        [HttpGet("FindUserByUserName&Password/{UserName}/{Password}", Name = "GetUserByUserName&Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByUserNameAndPassword(string UserName,string Password)
        {

           var User = clsUser.Find(UserName,Password);

            if (User == null)
            {
                return NotFound($"User with these information not found.");
            }

            UserDTO UDTO = User.Result.UDTO;

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

           var User = new clsUser(new UserDTO(NewUserDTO.UserID, NewUserDTO.PersonID,
                     NewUserDTO.UserName, NewUserDTO.Password, NewUserDTO.IsActive));

                    var Result= User.Save();

                     NewUserDTO.UserID = Convert.ToInt32(User.UserID);

                
                 return CreatedAtRoute("GetUserByID", new { UserID = NewUserDTO.UserID }, NewUserDTO);

        }

       
        [HttpPut("UpdateUser/{UserID}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdateUser(int UserID, UserDTO UpdatedUser)
        {
           
           var User =clsUser.FindByUserID(UserID);

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
                    if (User.Result.UserName != UpdatedUser.UserName)
                        return BadRequest($"The UserName '{UpdatedUser.UserName}' already exists.");
                    else
                        break;
            }

                 User.Result.PersonID = UpdatedUser.PersonID;
                 User.Result.UserName = UpdatedUser.UserName;
                 User.Result.Password = UpdatedUser.Password;
                 User.Result.IsActive = UpdatedUser.IsActive;
                 

                var Result= User.Result.Save();

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
                
                if (clsUser.DeleteUser(UserID).Result)

                    return Ok($"User with ID {UserID} has been deleted.");
                else
                    return NotFound($"User with ID {UserID} not found. no rows deleted!");
            }
        
        
        [HttpGet("IsUserExist/{UserID}", Name = "IsUserExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsUserExists(int UserID)
            {
                if (clsUser.IsUserExist(UserID).Result)
                {
                    return Ok(new { Exists = true, Message = "The user exists." });

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
        [HttpGet("AllDrivers", Name = "GetAllDrivers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ListDriverDTO>> GetAllDrivers()
        {
            var DriversList = clsDriver.GetAllDriver();
            if (DriversList.Count == 0)
            {
                return NotFound("No Users Found!");
            }
            return Ok(DriversList); 

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

            clsDriver Driver = clsDriver.FindByDriverID(DriverID);

            if (Driver == null)
            {
                return NotFound($"Driver with ID {DriverID} not found.");
            }

            
           DriverDTO DDTO = Driver.DDTO;

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

            clsDriver Driver = clsDriver.FindByPersonID(PersonID);

            if (Driver == null)
            {
                return NotFound($"Driver with PersonID {PersonID} not found.");
            }


            DriverDTO DDTO = Driver.DDTO;

            return Ok(DDTO);

        }





        
        [HttpPost("AddNewDriver", Name = "AddDriver")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DriverDTO> AddDriver(DriverDTO NewDriverDTO)
        {

            switch (clsUtil.DriverCheckConstraints(NewDriverDTO))
            {
                case clsUtil.enDriverBadRequestTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUtil.enDriverBadRequestTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsUtil.enDriverBadRequestTypes.InvalidPersonID:
                    return BadRequest($"The personID {NewDriverDTO.PersonID} is not exists");

                case clsUtil.enDriverBadRequestTypes.AlreadyDriver:
                    return BadRequest($"This person is already a Driver");
            }

            clsDriver Driver = new clsDriver(new DriverDTO(NewDriverDTO.DriverID, NewDriverDTO.PersonID,NewDriverDTO.CreatedByUserID, NewDriverDTO.CreatedDate));

            Driver.Save();

            NewDriverDTO.DriverID = Convert.ToInt32(Driver.DriverID);


            return CreatedAtRoute("GetDriverByID", new { DriverID = NewDriverDTO.DriverID }, NewDriverDTO);

        }


        [HttpPut("UpdateDriver/{DriverID}", Name = "UpdateDriver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DriverDTO> UpdatDriver(int DriverID, DriverDTO UpdatedDriver)
        {

           clsDriver Driver = DVlD_BusinessLayer.clsDriver.FindByDriverID(DriverID);

            if (Driver == null)
            {
                return NotFound($"Driver with ID {DriverID} not found.");
            }
            switch (clsUtil.DriverCheckConstraints(UpdatedDriver))
            {
                case clsUtil.enDriverBadRequestTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsUtil.enDriverBadRequestTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                
            }

            Driver.PersonID = UpdatedDriver.PersonID;
            Driver.CreatedDate = UpdatedDriver.CreatedDate;
            Driver.CreatedByUserID = UpdatedDriver.CreatedByUserID;


            Driver.Save();

            return Ok(Driver.DDTO);

        }


       
       [HttpGet("IsDriverExists/{DriverID}", Name = "IsDriverExist")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status204NoContent)]
       public ActionResult<bool> IsDriverExists(int DriverID)
       {
           if (clsDriver.IsDriverExists(DriverID))
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
            if (clsDriver.IsDriverExistByPersonID(PersonID))
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
