using DVlD_BusinessLayer;
using DVLD_DataAccessLayer;
using System.Reflection.Metadata.Ecma335;

namespace DVLD_API
{
    public class clsUtil
    {
        public enum enPersonBadRequestTypes {EmptyFileds=1,UnderAge=2,NationalNoDuplicate=3,NullObject=4,None=5 };
        
        public static Func<string, bool> IsFieldEmpty = str => string.IsNullOrEmpty(str);
        public static bool IsUnderAge(DateTime DateOfBirth)
        {
            DateTime CompareDate = DateTime.Today.AddYears(-18);
            return (DateOfBirth - CompareDate).TotalDays > 0;
        }
        public static enPersonBadRequestTypes PersonCheckConstraints( PeopleDTO NewPersonDTO)
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

            if (clsPerson.IsPersonExist(NewPersonDTO.NationalNo))
            {
                return enPersonBadRequestTypes.NationalNoDuplicate; 
            }

            return enPersonBadRequestTypes.None;
        }
       
    }
}
