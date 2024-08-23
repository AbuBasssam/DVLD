using DVlD_BusinessLayer;
using System.Reflection.Metadata.Ecma335;

namespace DVLD_API
{
    public class clsUtil
    {
        public enum enPersonBadRequestTypes {EmptyFileds=1,UnderAge=2,NationalNoDuplicate=3,NullObject=4,None=5 };
        public enum enUserBadRequestTypes { EmptyFileds = 1, InvalidPersonID = 2,UserNameDuplicate = 3, NullObject = 4,AlreadyUser=5, None = 6};

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
            
            if (
                clsUtil.IsFieldEmpty(NewPersonDTO.NationalNo) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.FirstName) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.SecondName) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.LastName) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.Address) ||
                clsUtil.IsFieldEmpty(NewPersonDTO.Phone)
                )
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
            if (
                NewUserDTO.PersonID==0                    ||
                clsUtil.IsFieldEmpty(NewUserDTO.UserName) ||
                clsUtil.IsFieldEmpty(NewUserDTO.Password)
                )
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

    }
}
