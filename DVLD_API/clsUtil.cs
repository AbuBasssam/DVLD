﻿using DVlD_BusinessLayer;
using DVlD_BusinessLayer.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace DVLD_API
{
    public class clsUtil
    {
        public enum enPersonBadRequestTypes {EmptyFileds=1,UnderAge=2,NationalNoDuplicate=3,NullObject=4,None=5 };
        public enum enUserBadRequestTypes { EmptyFileds = 1, InvalidPersonID = 2,UserNameDuplicate = 3, NullObject = 4,AlreadyUser=5, None = 6};
        public enum enDriverBadRequestTypes { EmptyFileds = 1, InvalidPersonID = 2, NullObject = 3, AlreadyDriver = 4, None = 5 };

        private static bool HasPersonHaveEmptyFileds(PersonDTO NewPersonDTO)
        {
            return (
                clsUtil.IsFieldEmpty(NewPersonDTO.NationalNo) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.FirstName) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.SecondName) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.LastName) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.Address) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.Phone)
                );
        }
        private static bool HasUserHaveEmptyFileds(UserDTO NewUserDTO)
        {
            return (
                NewUserDTO.PersonID == 0 ||
                clsUtil.IsFieldEmpty(NewUserDTO.UserName) ||
                clsUtil.IsFieldEmpty(NewUserDTO.Password)
                );
        }
        private static bool HasDriverHaveEmptyFileds(DriverDTO NewDriverDTO)
        {
            return (NewDriverDTO.PersonID == 0||NewDriverDTO.CreatedByUserID==0);
        }

        public static Func<string, bool> IsFieldEmpty = str => string.IsNullOrEmpty(str);
        
        public static bool IsUnderAge(DateTime DateOfBirth)
        {
            DateTime CompareDate = DateTime.Today.AddYears(-18);
            return (DateOfBirth - CompareDate).TotalDays > 0;
        }
        
        public static enPersonBadRequestTypes PersonCheckConstraints( PersonDTO NewPersonDTO)
        {
            if (NewPersonDTO == null)
            {
                return enPersonBadRequestTypes.NullObject; 
            }

            if(HasPersonHaveEmptyFileds(NewPersonDTO))
            
            {
                return enPersonBadRequestTypes.EmptyFileds; 
            }

            if (clsUtil.IsUnderAge(NewPersonDTO.DateOfBirth))
                return enPersonBadRequestTypes.UnderAge;

            var Exist = clsPerson.IsPersonExistAsync(NewPersonDTO.NationalNo);
            if (Exist.Result)
            {
                return enPersonBadRequestTypes.NationalNoDuplicate; 
            }

            return enPersonBadRequestTypes.None;
        }

        public static enUserBadRequestTypes UserCheckConstraints(UserDTO NewUserDTO)
        {
            if (NewUserDTO == null)
            {
                return enUserBadRequestTypes.NullObject;
            }
            if (HasUserHaveEmptyFileds(NewUserDTO))
            {
                return enUserBadRequestTypes.EmptyFileds;
            }

            if (clsPerson.Find(NewUserDTO.PersonID) == null)
            {

                return enUserBadRequestTypes.InvalidPersonID;
            }

            if (clsUser.IsAlreadyUserExist(NewUserDTO.PersonID).Result)
            {

                return enUserBadRequestTypes.AlreadyUser;
            }

            if (clsUser.IsUserExist(NewUserDTO.UserName).Result)
            {
                return enUserBadRequestTypes.UserNameDuplicate;
            }
            

            

            return enUserBadRequestTypes.None;
        }
       
        public static enDriverBadRequestTypes DriverCheckConstraints(DriverDTO NewDriverDTO)
        {
            if (NewDriverDTO == null)
            {
                return enDriverBadRequestTypes.NullObject;
            }
            if (HasDriverHaveEmptyFileds(NewDriverDTO))
            {
                return enDriverBadRequestTypes.EmptyFileds;
            }
            
            if (clsPerson.Find(NewDriverDTO.PersonID) == null)
            {

                return enDriverBadRequestTypes.InvalidPersonID;
            }

            if (clsDriver.FindByPersonID(NewDriverDTO.PersonID)!=null)
            {

                return enDriverBadRequestTypes.AlreadyDriver;
            }

            return enDriverBadRequestTypes.None;
        }

    }
}
