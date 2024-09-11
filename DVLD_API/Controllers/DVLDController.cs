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
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.ComponentModel;

namespace DVLD_API.Controllers
{
    //Done
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

            if (Person.Result == null)
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

            if (Person.Result == null)
            {
                return NotFound($"Person with NationalNo {NationalNo} not found.");
            }

            //here we get only the DTO object to send it back.
            PersonDTO PDTO = Person.Result.PersonInfo;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }


        [HttpPost("AddNewPerson", Name = "AddPerson")]
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


            PersonDTO Person = new PersonDTO(NewPersonDTO.PersonID, NewPersonDTO.NationalNo, NewPersonDTO.FirstName, NewPersonDTO.SecondName, NewPersonDTO.ThirdName, NewPersonDTO.LastName,
                NewPersonDTO.DateOfBirth, NewPersonDTO.Gender, NewPersonDTO.Address, NewPersonDTO.Phone, NewPersonDTO.Email, NewPersonDTO.Nationality, NewPersonDTO.ImagePath);
            NewPersonDTO.PersonID = _Person.CreatePerson(Person).Result;

            //we return the DTO only not the full Person object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.

            return(NewPersonDTO.PersonID!=null)? CreatedAtRoute("GetPersonByID", new { PersonID = NewPersonDTO.PersonID }, NewPersonDTO)
                :BadRequest("Something wrong the data not added");

        }




        [HttpPut("UpdatePerson/{PersonID}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> UpdatePerson(int PersonID, PersonDTO UpdatedPerson)
        {
            var Person = _Person.GetPerson(PersonID);

            if (Person.Result == null)
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
            Person.Result.Nationality = (clsPerson.enNatinoality)UpdatedPerson.Nationality;
            Person.Result.ImagePath = UpdatedPerson.ImagePath;

            var result = _Person.UpdatePerson(Person.Result.PersonInfo);

            return (result.Result)? Ok(Person.Result.PersonInfo):BadRequest("Something is wrong the data not updated");

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
                return NotFound($"Person with ID {PersonID} Have data connected to him can not deleted!");
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
                return NotFound($"Person with NationalNo  {NationalNo} Have data connected to him can not deleted!");
        }




        [HttpGet("IsPersonExistByID/{PersonID}", Name = "IsPersonExistByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsPersonExist(int PersonID)
        {

            if (_Person.CheckExistAsync(PersonID).Result)
            {
                return Ok("The person exists.");

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

    //Done
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

            var User = _User.FindByUserID(UserID);

            if (User.Result == null)
            {
                return NotFound($"User with ID {UserID} not found.");
            }

            return Ok(User.Result.UDTO);

        }





        [HttpGet("FindUserByPersonID/{PersonID}", Name = "GetUserByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByPersonID(int PersonID)
        {

            var User = _User.FindByPersonID(PersonID);

            if (User.Result == null)
            {
                return NotFound($"User with PersonID {PersonID} not found.");
            }

            return Ok(User.Result.UDTO);

        }





        [HttpGet("FindUserByUserName&Password/{UserName}/{Password}", Name = "GetUserByUserName&Password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> GetUserByUserNameAndPassword(string UserName, string Password)
        {

            var User = _User.Find(UserName, Password);

            if (User.Result == null)
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


            NewUserDTO.UserID = (User.Result == null) ? 0 : User.Result.Value;

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

            var User = _User.FindByUserID(UserID);

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


            var Result = _User.UpdateUser(User.Result.UDTO);

            return (Result.Result)? Ok(User.Result.UDTO):BadRequest("Something is wrong the data not Updated");

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
                return Ok("The user exists.");

            }
            else
            {
                return NoContent();

            }
        }


    }

    //Done
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

            if (Driver.Result == null)
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

            if (Driver.Result == null)
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

            var Driver = _Driver.AddNewDriver(new DriverDTO(NewDriverDTO.DriverID, NewDriverDTO.PersonID, NewDriverDTO.CreatedByUserID,
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

            return (Result.Result) ? Ok(Driver.Result.DDTO) : BadRequest("Something is wrong data not updated") ;

        }



        [HttpGet("IsDriverExists/{DriverID}", Name = "IsDriverExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsDriverExists(int DriverID)
        {
            if (_Driver.IsDriverExists(DriverID).Result)
            {
                return Ok("The Driver exists.");

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


    //Done
    [Route("api/DVLD/ApplicationType")]
    [ApiController]
    public class ApplicationTypeController : ControllerBase
    {
        private readonly IBLLApplicationTypes _Applicatointype;
        public ApplicationTypeController(IBLLApplicationTypes applicatointype)
        {
            _Applicatointype = applicatointype;
        }



        [HttpGet("AllApplicationTypes", Name = "GetAllApplicationTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ApplicationTypeDTO>> GetAllAppsTypes()
        {
            var _ApplicatointypeList = _Applicatointype.GetAllApplicatoinTypes();
            if (_ApplicatointypeList.Result == null || !_ApplicatointypeList.Result.Any())
            {
                return NotFound("No Types found!");
            }
            return Ok(_ApplicatointypeList.Result);

        }

        [HttpGet("FindByID/{AppTypeID}", Name = "GetApplicationTypeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationTypeDTO> GetAppTypeByID(int AppTypeID)
        {


            if (AppTypeID < 1)
            {
                return BadRequest($"Not accepted ID {AppTypeID}");
            }

            var Type = _Applicatointype.Find(AppTypeID);

            if (Type.Result == null)
            {
                return NotFound($"Applicatoin type  with ID {AppTypeID} not found.");
            }

            //here we get only the DTO object to send it back.
            ApplicationTypeDTO ATDTO = Type.Result.ATDTO;

            //we return the DTO not the student object.
            return Ok(ATDTO);

        }

        [HttpPut("UpdateApplicationType/{AppTypeID}", Name = "UpdateApplicationType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ApplicationTypeDTO> UpdateAppType(int AppTypeID, ApplicationTypeDTO UpdatedAppType)
        {
            var AppType = _Applicatointype.Find(AppTypeID);

            if (AppType == null)
            {
                return NotFound($" Application type with ID {AppType} not found.");
            }

            switch (AppType.Result.IsValid(UpdatedAppType))
            {
                case clsApplicationType.enApplicatoinTypeValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsApplicationType.enApplicatoinTypeValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsApplicationType.enApplicatoinTypeValidationType.WrongType:
                    return BadRequest($"Wrong application type!! the types is from 1 to 7");


            }

            AppType.Result.ApplicationID = (clsApplicationType.enApplicationTypes)UpdatedAppType.ApplicationID;
            AppType.Result.Title = UpdatedAppType.Title;
            AppType.Result.Fees = UpdatedAppType.Fees;


            var result = _Applicatointype.UpdateApplicationType(AppType.Result.ATDTO);

            return (result.Result)? Ok(AppType.Result.ATDTO) :BadRequest("Something is woring data is not updated");

        }

    }
    
    //Done
    [Route("api/DVLD/LicenseClasses")]
    [ApiController]
    public class LicenseClassesController : ControllerBase
    {
        private readonly IBLLLicenseClass _LicenseClass;
        public LicenseClassesController(IBLLLicenseClass bLLLicenseClass)
        {
            this._LicenseClass = bLLLicenseClass;
        }

        [HttpGet("LicenseClasses", Name = "GetAllLicenseClasses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LicenseClassDTO>> GetAllClasses()
        {
            var _LicenseClassesList = _LicenseClass.GetAllLicenseClasses();
            if (_LicenseClassesList.Result == null || !_LicenseClassesList.Result.Any())
            {
                return NotFound("No classes found!");
            }
            return Ok(_LicenseClassesList.Result);

        }


        [HttpGet("FindByID/{LicenseClassID}", Name = "GetLicenseClassByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseClassDTO> GetLicenseClassByID(int LicenseClassID)
        {


            if (LicenseClassID < 1)
            {
                return BadRequest($"Not accepted ID {LicenseClassID}");
            }

            var Class = _LicenseClass.Find(LicenseClassID);

            if (Class.Result == null)
            {
                return NotFound($"License Class with ID {LicenseClassID} not found.");
            }

            LicenseClassDTO LCDTO = Class.Result.LCDTO;

            return Ok(LCDTO);

        }

        [HttpGet("FindByName/{ClassName}", Name = "GetLicenseClassByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseClassDTO> GetLicenseClassByName(string ClassName)
        {

            var Class = _LicenseClass.Find(ClassName);

            if (Class.Result == null)
            {
                return NotFound($"License Class with name {ClassName} not found.");
            }

            LicenseClassDTO LCDTO = Class.Result.LCDTO;

            return Ok(LCDTO);

        }

        [HttpPut("UpdateLicenseClass/{LicenseClassID}", Name = "UpdateLicenseClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseClassDTO> UpdateLicenseClass(int LicenseClassID, LicenseClassDTO UpdatedLicenseClassDTO)
        {
            var Class = _LicenseClass.Find(LicenseClassID);

            if (Class == null)
            {
                return NotFound($" Class with ID {Class} not found.");
            }

            switch (Class.Result.IsValid(UpdatedLicenseClassDTO))
            {
                case clsLicenseClasses.enLicenseClassessValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsLicenseClasses.enLicenseClassessValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsLicenseClasses.enLicenseClassessValidationType.WrongClass:
                    return BadRequest($"Wrong Class type!! the types is from 1 to 7");


            }

            Class.Result.LicenseClassesID = (clsLicenseClasses.enLicenseClasses)UpdatedLicenseClassDTO.LicenseClassesID;
            Class.Result.ClassName = UpdatedLicenseClassDTO.ClassName;
            Class.Result.ClassDescription = UpdatedLicenseClassDTO.ClassDescription;
            Class.Result.MinimumAllowedAge = UpdatedLicenseClassDTO.MinimumAllowedAge;
            Class.Result.DefalutValidityLength = UpdatedLicenseClassDTO.DefalutValidityLength;
            Class.Result.ClassFees = UpdatedLicenseClassDTO.ClassFees;

            var result = _LicenseClass.UpdateLicenseClass(Class.Result.LCDTO);

            return (result.Result) ? Ok(Class.Result.LCDTO) : BadRequest("Something wrong the data not updated");

        }


    }
    
    //Done
    [Route("api/DVLD/TestTypes")]
    [ApiController]
    public class TestTypesController : ControllerBase
    {
        private readonly IBLLTestTypes _TestTypes;
        public TestTypesController(IBLLTestTypes bLLTestTypes)
        {
            this._TestTypes = bLLTestTypes;
        }

        [HttpGet("TestTypesList", Name = "GetAllTestTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestTypeDTO>> GetAllTestTypes()
        {
            var _TestTypesList = _TestTypes.GetAllTestTypes();
            if (_TestTypesList.Result == null || !_TestTypesList.Result.Any())
            {
                return NotFound("No types found!");
            }
            return Ok(_TestTypesList.Result);

        }
        
        [HttpGet("FindByID/{TestTypeID}", Name = "GetTestTypeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestTypeDTO> GetTestType(int TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }

            // Fetch the test type from the database (assuming asynchronous call)
            var TestType = _TestTypes.Find((clsTestTypes.enTestType) TestTypeID);

            // Check if the TestType is found
            if (TestType.Result == null)
            {
                return NotFound($"TestType with ID {TestTypeID} not found.");
            }

            // Get the DTO object to send it back
            TestTypeDTO TTDTO = TestType.Result.TestTypeDTO;

            // Return the DTO object
            return Ok(TTDTO);
        }

        [HttpPut("UpdateTestType/{TestTypeID}", Name = "UpdateTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestTypeDTO> UpdateTestType(int TestTypeID, TestTypeDTO UpdatedTestType)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }
            
            var TestType = _TestTypes.Find((clsTestTypes.enTestType) TestTypeID);

            if (TestType.Result == null)
            {
                return NotFound($"Type with ID {TestTypeID} not found.");
            }

            switch (_TestTypes.IsValid(UpdatedTestType))
            {
                case clsTestTypes.enTestTypeValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsTestTypes.enTestTypeValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsTestTypes.enTestTypeValidationType.WrongType:
                    return BadRequest($"Invalid Type");

            }

            TestType.Result.TestTypeID = (clsTestTypes.enTestType)UpdatedTestType.TestTypeID;
            TestType.Result.Title = UpdatedTestType.Title;
            TestType.Result.Description = UpdatedTestType.Description;
            TestType.Result.Fees = UpdatedTestType.TestFees;
           

            var result = _TestTypes.UpdateTestType(TestType.Result.TestTypeDTO);

            return(result.Result)? Ok(TestType.Result.TestTypeDTO):BadRequest("Something wrong!! data not updated");

        }

        


    }


    //Done without verfying & Testing
    [Route("api/DVLD/Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IBLLTest _Test;
        public TestController(IBLLTest bLLTest)
        {
            this._Test = bLLTest;
        }

        [HttpGet("Tests", Name = "GetAllTests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestDTO>> GetAllTests()
        {
            var _TestsList = _Test.GetAllTests();
            if (_TestsList.Result == null || !_TestsList.Result.Any())
            {
                return NotFound("No types found!");
            }
            return Ok(_TestsList.Result);


        }

        [HttpGet("FindByID/{TestID}", Name = "GetTestByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestDTO> GetTest(int TestID)
        {

            if (TestID < 1)
            {
                return BadRequest($"Not accepted ID {TestID}");
            }

            var Test = _Test.Find(TestID);

            if (Test.Result == null)
            {
                return NotFound($"Driver with ID {TestID} not found.");
            }


            TestDTO TDTO = Test.Result.TDTO;

            return Ok(TDTO);

        }

        // without verfying
        [HttpPost("AddNewTest", Name = "AddTest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TestDTO> AddTest(TestDTO NewTestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*switch (_Person.IsValid(NewPersonDTO))
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
            }*/


            TestDTO Test = new TestDTO(NewTestDTO.TestID, NewTestDTO.TestAppointmentID, NewTestDTO.TestResult, NewTestDTO.Notes, NewTestDTO.CreatedBy);
            var AddtionResult = _Test.AddNewTest(Test);

                NewTestDTO.TestID = (AddtionResult.Result==null)? 0: AddtionResult.Result.Value;

            
            return (NewTestDTO.TestID != 0) ? CreatedAtRoute("GetTestByID", new { TestID = NewTestDTO.TestID }, NewTestDTO)
                : BadRequest("Something wrong the data not added");

        }
        
        // without verfying
        [HttpPut("UpdateTest/{TestID}", Name = "UpdateTeste")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestDTO> UpdateTest(int TestID, TestDTO UpdatedTest)
        {
            if (TestID < 1)
            {
                return BadRequest("Invalid ID .");
            }

            var Test = _Test.Find(TestID);

            if (Test.Result == null)
            {
                return NotFound($"Test with ID {TestID} not found.");
            }

            /*switch (_Test.IsValid(UpdatedTest))
            {
                case clsTestTypes.enTestTypeValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsTestTypes.enTestTypeValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsTestTypes.enTestTypeValidationType.WrongType:
                    return BadRequest($"Invalid Type");

            }*/
            Test.Result.TestAppointmentID = UpdatedTest.TestAppointmentID;
            Test.Result.TestResult = UpdatedTest.TestResult;
            Test.Result.Notes = UpdatedTest.Notes;
            Test.Result.CreatedBy = UpdatedTest.CreatedBy;



            var result = _Test.UpdateTest(Test.Result.TDTO);

            return (result.Result) ? Ok(Test.Result.TDTO) : BadRequest("Something wrong!! data not updated");

        }


        [HttpGet("GetLastTestPerPersonAndLicenseClass/{PersonID},{LicenseClass},{TestType}", Name = "GetLastTestPerPersonAndLicenseClass")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestDTO> GetLastTestPerPersonAndLicenseClass(int PersonID,int LicenseClass,int TestType)
        {

            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }
            if (LicenseClass < 1 ||LicenseClass>7)
            {
                return BadRequest($"Not accepted License class ID {LicenseClass} it's should between 1 and 7");
            }
            if (TestType < 1 || TestType > 7)
            {
                return BadRequest($"Not accepted test type ID {TestType} it's should between 1 and 3");
            }

            var Test = _Test.FindLastTestPerPersonAndLicenseClass(PersonID,LicenseClass,(clsTestTypes.enTestType)TestType);

            if (Test.Result == null)
            {
                return NotFound($"Driver with ID {PersonID} not found.");
            }


            TestDTO TDTO = Test.Result.TDTO;

            return Ok(TDTO);

        }

        [HttpGet("GetPassedTestCount/{LocalDrivingLicenseID}", Name = "GetPassedTestCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> GetPassedTestCount(int LocalDrivingLicenseID)
        {
            if(LocalDrivingLicenseID < 1)
            {
                return BadRequest($"Not accepted ID {LocalDrivingLicenseID}");

            }
            var PassedTestCount=_Test.GetPassedTestCount(LocalDrivingLicenseID);
            return (!(PassedTestCount.Result>=0)) ? NotFound($"No Application with ID {LocalDrivingLicenseID}"):Ok(PassedTestCount.Result);

        }

        [HttpGet("IsPassedAllTests/{LocalDrivingLicenseID}", Name = "IsPassedAllTests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<byte> IsPassedAllTests(int LocalDrivingLicenseID)
        {
            if (LocalDrivingLicenseID < 1)
            {
                return BadRequest($"Not accepted ID {LocalDrivingLicenseID}");

            }
            var PassedTestCount = _Test.PassedAllTests(LocalDrivingLicenseID);
            return (PassedTestCount.Result ) ? Ok($"All tests passed for ID { LocalDrivingLicenseID}"):NotFound($"Tests are not fully passed for ID {LocalDrivingLicenseID}"); ;

        }


    }


    //Done without verfying & Testing
    [Route("api/DVLD/TestAppointment")]
    [ApiController]
    public class TestAppointmentController : ControllerBase
    {
        private readonly IBLLTestAppointment _TestAppointment;
        public TestAppointmentController(IBLLTestAppointment bLLTestAppointment)
        {
            this._TestAppointment = bLLTestAppointment;
        }

        [HttpGet("AllTestAppointment", Name = "GetAllTestAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestAppointmentDTO>> GetAllTestAppointment()
        {
            var TestAppointmentList = _TestAppointment.GetAllAppointmentAsync();
            if (TestAppointmentList.Result == null || !TestAppointmentList.Result.Any())
            {
                return NotFound("No Test appointments found!");
            }
            return Ok(TestAppointmentList.Result);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("AllTestAppointmentsPerTestType", Name = "GetAllTestAppointmentsPerTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TestAppointmentDTO>> GetApplicationTestAppointmentsPerTestTypeAsync(int LicenseApplicationID, int TestTypeID)
        {

            var ApplicationTestAppointmentsList = _TestAppointment.GetApplicationTestAppointmentsPerTestTypeAsync(LicenseApplicationID, (clsTestTypes.enTestType)TestTypeID);
            if (ApplicationTestAppointmentsList.Result == null || !ApplicationTestAppointmentsList.Result.Any())
            {
                return NotFound("No Test appointments found!");
            }
            return Ok(ApplicationTestAppointmentsList.Result);

        }
        //---------------------------------------------------------------------------------

        [HttpGet("FindByID/{TestAppointmentID}", Name = "GetTestAppointmentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestAppointmentDTO> GetTestAppointmentByID(int TestAppointmentID)
        {


            if (TestAppointmentID < 1)
            {
                return BadRequest($"Not accepted ID {TestAppointmentID}");
            }

            var TestAppointment = _TestAppointment.FindAsync(TestAppointmentID);

            if (TestAppointment.Result == null)
            {
                return NotFound($"Test Appointment with ID {TestAppointmentID} not found.");
            }

            TestAppointmentDTO TADTO = TestAppointment.Result.TADTO;

            return Ok(TADTO);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("IsTestAppointmentExistByIDPerTestType/{TestAppointmentID}/{TestTypeID}", Name = "IsTestAppointmentExistByIDPerTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsTestApointmentExist(int TestAppoitmentID, byte TestTypeID)
        {

            if (_TestAppointment.ExistAppointmentAsync(TestAppoitmentID, TestTypeID).Result)
            {
                return Ok("The Appointment exists.");

            }
            else
            {
                return NoContent();

            }
        }

        //---------------------------------------------------------------------------------

        [HttpGet("GetLastTestAppointment/{TestAppointmentID},{TestTypeID}", Name = "GetLastTestAppointmentByLDLAppAndTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestAppointmentDTO> GetLastTestAppointmentAsync(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest($"Not accepted TypeID {LocalDrivingLicenseApplicationID}  types id between 1 and 3");

            }

            if (LocalDrivingLicenseApplicationID < 1)
            {
                return BadRequest($"Not accepted ID {LocalDrivingLicenseApplicationID}");
            }

            var TestAppointment = _TestAppointment.GetLastTestAppointmentAsync(LocalDrivingLicenseApplicationID, (clsTestTypes.enTestType)TestTypeID);

            if (TestAppointment.Result == null)
            {
                return NotFound($"Test Appointment for this  ID {LocalDrivingLicenseApplicationID} not found.");
            }

            TestAppointmentDTO TADTO = TestAppointment.Result.TADTO;

            return Ok(TADTO);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("GetTestIDForAppointment/{TestAppointmentID},{TestTypeID}", Name = "GetTestIDForAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> GetTestIDOfTestAppointment(int TestAppointmentID)
        {


            if (TestAppointmentID < 1)
            {
                return BadRequest($"Not accepted ID {TestAppointmentID}");
            }

            var TestAppointment = _TestAppointment.GetTestIDAsync(TestAppointmentID);

            if (TestAppointment.Result == -1)
            {
                return NotFound($" No Test Found for this  ID {TestAppointmentID}");
            }

            int TestID = TestAppointment.Result;

            return Ok(TestID);

        }

        //---------------------------------------------------------------------------------

        //without verfying
        [HttpPost("AddNewTestAppointment", Name = "AddNewTestAppointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TestAppointmentDTO> AddAppointment(TestAppointmentDTO NewTestAppointmentDTO)
        {

            /*switch (_localDrivingLicenseApp.IsValid(NewDriverDTO))
            {
                case clsDriver.enDriverValidationTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsDriver.enDriverValidationTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsDriver.enDriverValidationTypes.InvalidPersonID:
                    return BadRequest($"The personID {NewDriverDTO.PersonID} is not exists");

                case clsDriver.enDriverValidationTypes.AlreadyDriver:
                    return BadRequest($"This person is already a Driver");
            }*/

            var TestAppointment = _TestAppointment.AddNewAppointmentAsync(new TestAppointmentDTO(NewTestAppointmentDTO.TestAppointmentID, NewTestAppointmentDTO.TestTypeID, NewTestAppointmentDTO.LocalDrivingLicenseApplicationID, DateTime.Now, NewTestAppointmentDTO.PaidFees, NewTestAppointmentDTO.CreatedByUserID, NewTestAppointmentDTO.IsLocked, NewTestAppointmentDTO.RetakeTestApplicationID));


            NewTestAppointmentDTO.TestAppointmentID = (TestAppointment.Result == null) ? 0 : TestAppointment.Result.Value;

            if (NewTestAppointmentDTO.LocalDrivingLicenseApplicationID != 0)
                return CreatedAtRoute("GetTestAppointmentByID", new { TestAppointmentID = NewTestAppointmentDTO.TestAppointmentID }, NewTestAppointmentDTO);
            else
                return BadRequest("Adding Failed");



        }

        //---------------------------------------------------------------------------------

        //without verfying
        [HttpPut("UpdateTestAppointment/{TestAppointmentID}", Name = "UpdateTestAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestAppointmentDTO> UpdateAppointment(int TestAppointmentID, TestAppointmentDTO UpdatedTestAppointment)
        {


            var TestAppointment = _TestAppointment.FindAsync(TestAppointmentID);

            if (TestAppointment.Result == null)
            {
                return NotFound($"TestAppointment with ID {TestAppointmentID} not found.");
            }

            /*switch (_TestTypes.IsValid(UpdatedTestAppointment))
            {
                case clsTestTypes.enTestTypeValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsTestTypes.enTestTypeValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsTestTypes.enTestTypeValidationType.WrongType:
                    return BadRequest($"Invalid Type");

            }*/

            TestAppointment.Result.TestAppointmentID = UpdatedTestAppointment.TestTypeID;
            TestAppointment.Result.TestTypeID = (clsTestTypes.enTestType)UpdatedTestAppointment.TestTypeID;
            TestAppointment.Result.LocalDrivingApplicationID = UpdatedTestAppointment.LocalDrivingLicenseApplicationID;
            TestAppointment.Result.AppointmentDate = UpdatedTestAppointment.AppointmentDate;
            TestAppointment.Result.PaidFees = UpdatedTestAppointment.PaidFees;
            TestAppointment.Result.CreatedBy = UpdatedTestAppointment.CreatedByUserID;
            TestAppointment.Result.IsLocked = UpdatedTestAppointment.IsLocked;
            TestAppointment.Result.RetakeTestApplicationID = UpdatedTestAppointment.RetakeTestApplicationID;




            var result = _TestAppointment.UpdateAppointmentAsync(TestAppointment.Result.TADTO);

            return (result.Result) ? Ok(TestAppointment.Result.TADTO) : BadRequest("Something wrong!! data not updated");

        }



    }


    //Done without verfying & Testing
    [Route("api/DVLD/LocalDrivingLicenseApplication")]
    [ApiController]
    public class LocalDrivingLicenseApplicationController : ControllerBase
    {
        private readonly IBLLLocalDrivingLicenseApp _localDrivingLicenseApp;
        public LocalDrivingLicenseApplicationController(IBLLLocalDrivingLicenseApp _localDrivingLicenseApp)
        {
            this._localDrivingLicenseApp = _localDrivingLicenseApp;

        }


        [HttpGet("AllLocalDrivingLicenseApplication", Name = "GetAllDrivingLicenseApplication")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<LDLApplicatoinViewDTO>> GetAllLocalDrivingLicenseApplication()
        {
            var DrivingLicenseApplicationList = _localDrivingLicenseApp.GetAllApplicatoins();
            if (DrivingLicenseApplicationList.Result == null || !DrivingLicenseApplicationList.Result.Any())
            {
                return NotFound("No people found!");
            }
            return Ok(DrivingLicenseApplicationList.Result);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("FindByID/{LDLApplicationID}", Name = "GetLDLApplicationByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LDLApplicatoinDTO> GetLocalDrivingLicenseAppByID(int LDLApplicationID)
        {


            if (LDLApplicationID < 1)
            {
                return BadRequest($"Not accepted ID {LDLApplicationID}");
            }

            var LDLApp = _localDrivingLicenseApp.Find(LDLApplicationID);

            if (LDLApp.Result == null)
            {
                return NotFound($"Applicaton with ID {LDLApplicationID} not found.");
            }

            LDLApplicatoinDTO LDLADTO = LDLApp.Result._LDLApplicatoinDTO;

            return Ok(LDLADTO);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("FindByApplicationID/{ApplicationID}", Name = "GetLDLApplicationByApplicationID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LDLApplicatoinDTO> GetLocalDrivingLicenseAppByApplicatoinID(int ApplicationID)
        {


            if (ApplicationID < 1)
            {
                return BadRequest($"Not accepted ID {ApplicationID}");
            }

            var LDLApp = _localDrivingLicenseApp.FindByApplicationID(ApplicationID);

            if (LDLApp.Result == null)
            {
                return NotFound($"Application with ID {ApplicationID} not found.");
            }

            LDLApplicatoinDTO LDLADTO = LDLApp.Result._LDLApplicatoinDTO;

            return Ok(LDLADTO);

        }

        //---------------------------------------------------------------------------------

        // Without verfying
        [HttpPost("AddNewLocalDrivingLicenseApplicatoin", Name = "AddLocalDrivingLicenseApplicatoin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LDLApplicatoinDTO> AddLocalDrivingLicenseApplicatoin(LDLApplicatoinDTO NewLDLApplicationDTO)
        {

            /*switch (_localDrivingLicenseApp.IsValid(NewDriverDTO))
            {
                case clsDriver.enDriverValidationTypes.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsDriver.enDriverValidationTypes.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsDriver.enDriverValidationTypes.InvalidPersonID:
                    return BadRequest($"The personID {NewDriverDTO.PersonID} is not exists");

                case clsDriver.enDriverValidationTypes.AlreadyDriver:
                    return BadRequest($"This person is already a Driver");
            }*/

            var LDLApplication = _localDrivingLicenseApp.AddNewApplication(new LDLApplicatoinDTO(NewLDLApplicationDTO.LocalDrivingLicenseApplicationID, NewLDLApplicationDTO.ApplicationID, NewLDLApplicationDTO.LicenseClassID));


            NewLDLApplicationDTO.LocalDrivingLicenseApplicationID = (LDLApplication.Result == null) ? 0 : LDLApplication.Result.Value;

            if (NewLDLApplicationDTO.LocalDrivingLicenseApplicationID != 0)
                return CreatedAtRoute("GetLDLApplicationByApplicationID", new { LDLApplicationID = NewLDLApplicationDTO.LocalDrivingLicenseApplicationID }, NewLDLApplicationDTO);
            else
                return BadRequest("Adding Failed");



        }

        //---------------------------------------------------------------------------------

        //without verfying, need some modfying
        [HttpPut("UpdateLocalDrivingLicenseApp/{LDLAppID}", Name = "UpdateLocalDrivingLicenseApp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LDLApplicatoinDTO> UpdateAppointment(int LDLAppID, LDLApplicatoinDTO UpdatedLDLApplicationDTO)
        {


            var LDLApp = _localDrivingLicenseApp.Find(LDLAppID);

            if (LDLApp.Result == null)
            {
                return NotFound($"TestAppointment with ID {LDLAppID} not found.");
            }

            /*switch (_TestTypes.IsValid(UpdatedTestAppointment))
            {
                case clsTestTypes.enTestTypeValidationType.NullObject:
                    return BadRequest($"The Object is Null fill it ");

                case clsTestTypes.enTestTypeValidationType.EmptyFileds:
                    return BadRequest($"Some fileds is empty,please fill it");

                case clsTestTypes.enTestTypeValidationType.WrongType:
                    return BadRequest($"Invalid Type");

            }*/

            LDLApp.Result.ApplicationID = UpdatedLDLApplicationDTO.ApplicationID;
            LDLApp.Result.LicenseClassID = UpdatedLDLApplicationDTO.LicenseClassID;
            
            var result = _localDrivingLicenseApp.UpdateApplication(LDLApp.Result._LDLApplicatoinDTO);

            return (result.Result) ? Ok(LDLApp.Result._LDLApplicatoinDTO) : BadRequest("Something wrong!! data not updated");

        }

        //---------------------------------------------------------------------------------

        [HttpGet("DoesAttendTestType/{TestTypeID}", Name = "DoesAttendTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> DoesAttendTestType(byte TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }

            return (_localDrivingLicenseApp.DoesAttendTestType((clsTestTypes.enTestType) TestTypeID).Result) ? Ok("The Test attend.") :NoContent();
            
        }

        //---------------------------------------------------------------------------------

        [HttpGet("DoesPassPreviousTest/{TestTypeID}", Name = "DoesPassPreviousTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> DoesPassPreviousTest(byte TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }

            return (_localDrivingLicenseApp.DoesPassPreviousTest((clsTestTypes.enTestType)TestTypeID).Result) ? Ok("The pervious test passed.") : NoContent();








        }


        //---------------------------------------------------------------------------------

        [HttpGet("AttendedTest/{TestTypeID}", Name = "AttendedTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> AttendedTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }

            return (_localDrivingLicenseApp.AttendedTest(LocalDrivingLicenseApplicationID,(clsTestTypes.enTestType)TestTypeID).Result) ? Ok("This test is attened.") : NoContent();








        }

        //---------------------------------------------------------------------------------

        [HttpGet("DoesPassTestType/{TestTypeID}", Name = "DoesPassTestType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> DoesPassTestType(int LocalDrivingLicenseApp, byte TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }

            return (_localDrivingLicenseApp.DoesPassTestType(LocalDrivingLicenseApp,(clsTestTypes.enTestType)TestTypeID).Result) ? Ok("The test passed.") : NoContent();








        }

        //---------------------------------------------------------------------------------

        [HttpGet("DeleteLocalLicenseApp/{TestTypeID}", Name = "DeleteLocalLicenseApp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> DeleteLocalLicenseApp(int LocalDrivingLicenseApplicationID)
        {
            

            return (_localDrivingLicenseApp.DeleteLocalLicenseApp(LocalDrivingLicenseApplicationID).Result) ? Ok("Deleted successfully.") : NoContent();








        }

        //---------------------------------------------------------------------------------

        [HttpGet("IsThereAnActiveScheduledTest/{TestTypeID}", Name = "IsThereAnActiveScheduledTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<bool> IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }

            return (_localDrivingLicenseApp.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (clsTestTypes.enTestType)TestTypeID).Result) ? Ok("Yes there an active scheduled test.") : NoContent();

        }

        //---------------------------------------------------------------------------------

        [HttpGet("IsAlreadyExist/{NatoinalNO},{ClassName}", Name = "IsAlreadyExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int?> IsAlreadyExist(string NatoinalNO, string ClassName)
        {
            var LDLApp = _localDrivingLicenseApp.IsAlreadyExist(NatoinalNO,ClassName);

            return LDLApp.Result!=null? Ok(LDLApp.Result):NotFound(" Is not exists");

        }

        //---------------------------------------------------------------------------------

        [HttpGet("PassedTest/{LocalDrivingLicenseApplicationID}", Name = "PassedTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int?> PassedTest(int LocalDrivingLicenseApplicationID)
        {
            var LDLApp = _localDrivingLicenseApp.PassedTest(LocalDrivingLicenseApplicationID);

            return LDLApp.Result != -1 ? Ok(LDLApp.Result) : NotFound(" Is not exists");

        }

        //---------------------------------------------------------------------------------
        [HttpGet("TotalTrialsPerTest/{LocalDrivingLicenseApplicationID}", Name = "TotalTrialsPerTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int?> TotalTrialsPerTest(int LocalDrivingLicenseApplicationID,byte TestTypeID)
        {
            if (TestTypeID < 1 || TestTypeID > 3)
            {
                return BadRequest("TestTypeID must be between 1 and 3.");
            }
            var LDLApp = _localDrivingLicenseApp.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (clsTestTypes.enTestType)TestTypeID);

            return Ok(LDLApp.Result);

        }

        //---------------------------------------------------------------------------------
    }

    [Route("api/DVLD/License")]
    [ApiController]

    //Done without verfying & Testing
    public class LicenseController : ControllerBase
    {
        private readonly IBLLLicnese _Licnese;
        public LicenseController(IBLLLicnese bLLLicnese)
        {
            _Licnese = bLLLicnese;
        }

        [HttpGet("FindByID/{LicnenseID}", Name = "GetLicenseByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseDTO> Find(int LicnenseID)
        {

            if (LicnenseID < 1)
            {
                return BadRequest($"Not accepted ID {LicnenseID}");
            }

            var License = _Licnese.Find(LicnenseID);

            if (License.Result == null)
            {
                return NotFound($"License with ID {LicnenseID} not found.");
            }


            LicenseDTO LDTO = License.Result.LDTO;

            return Ok(LDTO);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("FindByID/{LicnenseID},{LicnenseClass}", Name = "GetLicenseByID&Class")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseDTO> Find(int LicnenseID,int LicnenseClass)
        {

            if (LicnenseID < 1)
            {
                return BadRequest($"Not accepted ID {LicnenseID}");
            }

            var License = _Licnese.Find(LicnenseID,LicnenseClass);

            if (License.Result == null)
            {
                return NotFound($"License with ID {LicnenseID} not found.");
            }


            LicenseDTO LDTO = License.Result.LDTO;

            return Ok(LDTO);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("AllDriverLicneses/{DriverID}", Name = "GetAllDriverLicneses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DriverLicensesDTO>> GetAllDriverLicneses(int DriverID)
        {
            var DriverLicensesList = _Licnese.GetAllDriverLicenses(DriverID); 
            if (DriverLicensesList.Result == null || !DriverLicensesList.Result.Any())
            {
                return NotFound("No people found!");
            }
            return Ok(DriverLicensesList.Result);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("FindByDriverID/{DriverID}", Name = "GetLicenseByDriverID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseDTO> FindByDriverID(int DriverID)
        {

            if (DriverID < 1)
            {
                return BadRequest($"Not accepted ID {DriverID}");
            }

            var License = _Licnese.FindByDriverID(DriverID);

            if (License.Result == null)
            {
                return NotFound($"License for driver ID {DriverID} not found.");
            }


            LicenseDTO LDTO = License.Result.LDTO;

            return Ok(LDTO);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("FindByApplicationID/{ApplicationID}", Name = "GetLicenseByApplicationID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseDTO> FindByApplicationID(int ApplicationID)
        {

            if (ApplicationID < 1)
            {
                return BadRequest($"Not accepted ID {ApplicationID}");
            }

            var License = _Licnese.FindByApplicationID(ApplicationID);

            if (License.Result == null)
            {
                return NotFound($"License for ApplicatoinID {ApplicationID} not found.");
            }


            LicenseDTO LDTO = License.Result.LDTO;

            return Ok(LDTO);

        }

        //---------------------------------------------------------------------------------

        [HttpGet("IsLicenseExist/{ApplicationID}", Name = "IsLicenseExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsLicenseExist(int ApplicationID)
        {
            if (ApplicationID < 1)
            {
                return BadRequest($"Not accepted ID {ApplicationID}");
            }
            var LDLApp = _Licnese.IsLicenseExist(ApplicationID);

            return LDLApp.Result? Ok("it's exists") : NotFound(" Is not exists");

        }

        //---------------------------------------------------------------------------------

        [HttpGet("IsLicenseExistByPersonID/{PersonID},{LicenseClass}", Name = "IsLicenseExistByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsLicenseExistByPersonID(int PersonID,int LicenseClass)
        {
            if (PersonID < 1|| LicenseClass < 1)
            {
                return BadRequest($"Not accepted ID");
            }
            var LDLApp = _Licnese.IsLicenseExistByPersonID(PersonID,LicenseClass);

            return LDLApp.Result ? Ok("it's exists") : NotFound(" Is not exists");

        }

        //---------------------------------------------------------------------------------

        [HttpGet("GetActiveLicenseIDByPersonID/{PersonID},{LicenseClass}", Name = "GetActiveLicenseIDByPersonID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int?> GetActiveLicenseIDByPersonID(int PersonID, int LicenseClass)
        {
            if (PersonID < 1 || LicenseClass < 1)
            {
                return BadRequest($"Not accepted ID");
            }
            var LDLApp = _Licnese.GetActiveLicenseIDByPersonID(PersonID, LicenseClass);

            return LDLApp.Result !=null? Ok(LDLApp.Result) : NotFound(" Is not exists");

        }

        //---------------------------------------------------------------------------------

        [HttpPost("AddNewLicense", Name = "AddLicense")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LicenseDTO> AddLicense(LicenseDTO NewLicenseDTO)
        {

            
            LicenseDTO License = new LicenseDTO(NewLicenseDTO.LicenseID,NewLicenseDTO.ApplicationID, NewLicenseDTO.DriverID, NewLicenseDTO.LicenseClass, DateTime.Now, DateTime.Now.AddYears(1), NewLicenseDTO.Notes,
                NewLicenseDTO.PaidFees, 1, Convert.ToByte( clsLicense.enIssueReason.FirstTime), NewLicenseDTO.CreatedByUserID);
            
            var LicneseTask= _Licnese.AddNewLLicense(License);
            NewLicenseDTO.LicenseID = LicneseTask.Result != null ? LicneseTask.Result.Value : 0;

            

            return (NewLicenseDTO.LicenseID != 0) ? CreatedAtRoute("GetLicenseByID", new { LicnenseID = NewLicenseDTO.LicenseID }, NewLicenseDTO)
                : BadRequest("Something wrong the data not added");

        }

        //---------------------------------------------------------------------------------

        [HttpPut("UpdateLicense/{LicenseID}", Name = "UpdateLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseDTO> UpdateLicense(int LicenseID, LicenseDTO UpdatedLicense)
        {
            var License = _Licnese.Find(LicenseID);

            if (License.Result == null)
            {
                return NotFound($"Person with ID {LicenseID} not found.");
            }

           

            License.Result.ApplicationID = UpdatedLicense.ApplicationID;
            License.Result.DriverID = UpdatedLicense.DriverID;
            License.Result.LicenseClass =(clsLicenseClasses.enLicenseClasses) UpdatedLicense.LicenseClass;
            License.Result.IssueDate = UpdatedLicense.IssueDate;
            License.Result.ExpirationDate = UpdatedLicense.ExpirationDate;
            License.Result.Notes = UpdatedLicense.Notes;
            License.Result.PaidFees = UpdatedLicense.PaidFees;
           

            var result = _Licnese.UpdateLLicense(License.Result.LDTO);

            return (result.Result) ? Ok(License.Result.LDTO) : BadRequest("Something is wrong the data not updated");

        }
    }

    [Route("api/DVLD/InternationalLicense")]
    [ApiController]
    public class InternationalLicenseController : ControllerBase
    {
        private readonly IBLLInternationalLicnense _InternationalLicnense;
        public InternationalLicenseController(IBLLInternationalLicnense bLLInternationalLicnense)
        {
            this._InternationalLicnense = bLLInternationalLicnense;
        }

        [HttpGet("FindByID/{LicnenseID}", Name = "GetInternationalLicenseByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseDTO> Find(int LicnenseID)
        {

            if (LicnenseID < 1)
            {
                return BadRequest($"Not accepted ID {LicnenseID}");
            }

            var License = _InternationalLicnense.FindByLicenseID(LicnenseID);

            if (License.Result == null)
            {
                return NotFound($"License with ID {LicnenseID} not found.");
            }


            InternationalLicenseDTO ILDTO = License.Result.ILDTO;

            return Ok(ILDTO);

        }

        //---------------------------------------------------------------------------------

        [HttpPost("AddNewInternationalLicense", Name = "AddInternationalLicense")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<InternationalLicenseDTO> AddLicense(InternationalLicenseDTO NewLicenseDTO)
        {


            InternationalLicenseDTO License = new InternationalLicenseDTO(NewLicenseDTO.InternationalLicenseID, NewLicenseDTO.ApplicationID, NewLicenseDTO.DriverID,
                NewLicenseDTO.IssuedUsingLocalLicenseID, DateTime.Now, DateTime.Now.AddYears(1),
                1, NewLicenseDTO.CreatedByUserID);

            var LicneseTask = _InternationalLicnense.AddNewLLicense(License);
            NewLicenseDTO.InternationalLicenseID = LicneseTask.Result != null ? LicneseTask.Result.Value : 0;



            return (NewLicenseDTO.InternationalLicenseID != 0) ? CreatedAtRoute("GetLicenseByID", new { LicnenseID = NewLicenseDTO.InternationalLicenseID }, NewLicenseDTO)
                : BadRequest("Something wrong the data not added");

        }

        //---------------------------------------------------------------------------------

        [HttpPut("UpdateInternationalLicense/{LicenseID}", Name = "UpdateInternationalLicense")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InternationalLicenseDTO> UpdateLicense(int LicenseID, InternationalLicenseDTO UpdatedLicense)
        {
            var License = _InternationalLicnense.FindByLicenseID(LicenseID);

            if (License.Result == null)
            {
                return NotFound($"Person with ID {LicenseID} not found.");
            }



            License.Result.ApplicationID = UpdatedLicense.ApplicationID;
            License.Result.DriverID = UpdatedLicense.DriverID;
            License.Result.IssuedUsingLocalLicenseID = UpdatedLicense.IssuedUsingLocalLicenseID;
            License.Result.IssueDate = UpdatedLicense.IssueDate;
            License.Result.ExpirationDate = UpdatedLicense.ExpirationDate;


            var result = _InternationalLicnense.UpdateLLicense(License.Result.ILDTO);

            return (result.Result) ? Ok(License.Result.ILDTO) : BadRequest("Something is wrong the data not updated");

        }

        //---------------------------------------------------------------------------------

        [HttpGet("IsInternationalLicenseExist/{LicenseID}", Name = "IsInternationalLicenseExist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> IsLicenseExist(int LicenseID)
        {
            if (LicenseID < 1)
            {
                return BadRequest($"Not accepted ID {LicenseID}");
            }
            var LDLApp = _InternationalLicnense.IsLicenseExist(LicenseID);

            return LDLApp.Result ? Ok("it's exists") : NotFound(" Is not exists");

        }

        //---------------------------------------------------------------------------------
       
        [HttpGet("AllDriverInternationalLicenses", Name = "GetAllDriverInternationalLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<InternationalLicenseDTO>> GetAllDriverInternationalLicenses(int DriverID)
        {
            var DriverInternationalLicensesList = _InternationalLicnense.GetAllDriverInternationalLicenses(DriverID); // Assuming `GetAllPersons` is implemented in `IPersonService`
            if (DriverInternationalLicensesList.Result == null || !DriverInternationalLicensesList.Result.Any())
            {
                return NotFound("No License found!");
            }
            return Ok(DriverInternationalLicensesList.Result);
        }
        
        [HttpGet("AllInternationalLicenses", Name = "GetAllInternationalLicenses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<InternationalLicenseDTO>> GetAllDriverInternationalLicenses()
        {
            var InternationalLicensesList = _InternationalLicnense.GetAllInternationalLicenses(); 
            if (InternationalLicensesList.Result == null || !InternationalLicensesList.Result.Any())
            {
                return NotFound("No License found!");
            }
            return Ok(InternationalLicensesList.Result);
        }
    }






    /* [Route("api/DVLD/People")]
     [ApiController]
     public class ApplicationTypeController : ControllerBase
     {



     }*/



    /* 
     [Route("api/DVLD/People")]
     [ApiController]
     public class ApplicationTypeController : ControllerBase
     {



     }*/

    /*[Route("api/DVLD/People")]
    [ApiController]
    public class ApplicationTypeController : ControllerBase
    {



    }
*/

}

