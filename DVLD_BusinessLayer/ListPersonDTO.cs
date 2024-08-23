using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class ListPersonDTO
    {
        public Nullable<int> PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public byte Gender { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int Nationality { set; get; }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public string CountryName { set; get; }
        public string Genderstr { set; get; }
        public ListPersonDTO(PersonDTO PersonInfo, string CountryName, string Genderstr)
        {
            this.PersonID = PersonInfo.PersonID;
            this.NationalNo = PersonInfo.NationalNo;
            this.FirstName = PersonInfo.FirstName;
            this.SecondName = PersonInfo.SecondName;
            this.ThirdName = PersonInfo.ThirdName;
            this.LastName = PersonInfo.LastName;
            this.DateOfBirth = PersonInfo.DateOfBirth;
            this.Gender = PersonInfo.Gender;
            this.Address = PersonInfo.Address;
            this.Phone = PersonInfo.Phone;
            this.Email = PersonInfo.Email;
            this.Nationality = PersonInfo.Nationality;
            this.ImagePath = PersonInfo.ImagePath;
            this.CountryName = CountryName;
            this.Genderstr = Genderstr;
        }
    }

}
