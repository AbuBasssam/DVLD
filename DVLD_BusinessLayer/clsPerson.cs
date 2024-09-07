
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Interfaces;
using DVlD_BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer.Entities;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Threading.Channels;

namespace DVlD_BusinessLayer
{
   
    public class clsPerson:IPerson
    {
        public enum enNatinoality
        {
            Afghanistan=1,
            Albania,
            Algeria,
            Andorra,
            Angola,
            AntiguaAndBarbuda,
            Argentina,
            Armenia,
            Austria,
            Azerbaijan,
            Bahrain,
            Bangladesh,
            Barbados,
            Belarus,
            Belgium,
            Belize,
            Benin,
            Bhutan,
            Bolivia,
            BosniaAndHerzegovina,
            Botswana,
            Brazil,
            Brunei,
            Bulgaria,
            BurkinaFaso,
            Burundi,
            CaboVerde,
            Cambodia,
            Cameroon,
            Canada,
            CentralAfricanRepublic,
            Chad,
            ChannelIslands,
            Chile,
            China,
            Colombia,
            Comoros,
            Congo,
            CostaRica,
            CôteDIvoire,
            Croatia,
            Cuba,
            Cyprus,
            CzechRepublic,
            Denmark,
            Djibouti,
            Dominica,
            DominicanRepublic,
            DRCongo,
            Ecuador,
            Egypt,
            ElSalvador,
            EquatorialGuinea,
            Eritrea,
            Estonia,
            Eswatini,
            Ethiopia,
            FaeroeIslands,
            Finland,
            France,
            FrenchGuiana,
            Gabon,
            Gambia,
            Georgia,
            Germany,
            Ghana,
            Gibraltar,
            Greece,
            Grenada,
            Guatemala,
            Guinea,
            GuineaBissau,
            Guyana,
            Haiti,
            HolySee,
            Honduras,
            HongKong,
            Hungary,
            Iceland,
            India,
            Indonesia,
            Iran,
            Iraq,
            Ireland,
            IsleofMan,
            Israel,
            Italy,
            Jamaica,
            Japan,
            Jordan,
            Kazakhstan,
            Kenya,
            Kuwait,
            Kyrgyzstan,
            Laos,
            Latvia,
            Lebanon,
            Lesotho,
            Liberia,
            Libya,
            Liechtenstein,
            Lithuania,
            Luxembourg,
            Macao,
            Madagascar,
            Malawi,
            Malaysia,
            Maldives,
            Mali,
            Malta,
            Mauritania,
            Mauritius,
            Mayotte,
            Mexico,
            Moldova,
            Monaco,
            Mongolia,
            Montenegro,
            Morocco,
            Mozambique,
            Myanmar,
            Namibia,
            Nepal,
            Netherlands,
            Nicaragua,
            Niger,
            Nigeria,
            NorthKorea,
            NorthMacedonia,
            Norway,
            Oman,
            Pakistan,
            Panama,
            Paraguay,
            Peru,
            Philippines,
            Poland,
            Portugal,
            Qatar,
            Réunion,
            Romania,
            Russia,
            Rwanda,
            SaintHelena,
            SaintKittsAndNevis,
            SaintLucia,
            SaintVincentAndTheGrenadines,
            SanMarino,
            SaoTomeAndPrincipe,
            SaudiArabia,
            Senegal,
            Serbia,
            Seychelles,
            SierraLeone,
            Singapore,
            Slovakia,
            Slovenia,
            Somalia,
            SouthAfrica,
            SouthKorea,
            SouthSudan,
            Spain,
            SriLanka,
            StateOfPalestine,
            Sudan,
            Suriname,
            Sweden,
            Switzerland,
            Syria,
            Taiwan,
            Tajikistan,
            Tanzania,
            Thailand,
            TheBahamas,
            TimorLeste,
            Togo,
            TrinidadAndTobago,
            Tunisia,
            Turkey,
            Turkmenistan,
            Uganda,
            Ukraine,
            UnitedArabEmirates,
            UnitedKingdom,
            UnitedStates,
            Uruguay,
            Uzbekistan,
            Venezuela,
            Vietnam,
            WesternSahara,
            Yemen,
            Zambia,
            Zimbabwe
        };
        public enum enPersonValidationType { EmptyFileds = 1, UnderAge = 2, NationalNoDuplicate = 3, NullObject = 4, WrongNationality = 5, Valid = 6 };

        private readonly IPeopleDataInterface _PeopleDataInterface;
        private string GetFullName()
        {
            return  this.FirstName + " " + this.SecondName + " " + this.ThirdName + " " + this.LastName;
        }
        
        public PersonDTO PersonInfo
        { 
            get
            {
                return new PersonDTO(this.PersonID, NationalNo,
                FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gender, Address, Phone,
                Email, (byte)Nationality, _ImagePath);
            }
        }
        public int? PersonID { set; get; }
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
        public enNatinoality Nationality { set; get; }

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }
        public string FullName()
        {
           return GetFullName();
        }
        
        private  Func<string, bool> IsFieldEmpty = str => string.IsNullOrEmpty(str);
        private Func<byte, bool> IsNationalityIDValid = NationalityID => (NationalityID<1|| (NationalityID >193));
        
        private bool HasPersonHaveEmptyFileds(PersonDTO NewPersonDTO)
        {
            return (
               IsFieldEmpty(NewPersonDTO.NationalNo) ||
               IsFieldEmpty(NewPersonDTO.FirstName) ||
               IsFieldEmpty(NewPersonDTO.SecondName) ||
               IsFieldEmpty(NewPersonDTO.LastName) ||
               IsFieldEmpty(NewPersonDTO.Address) ||
               IsFieldEmpty(NewPersonDTO.Phone)||
               (NewPersonDTO.Nationality==0)
                );
        }
        private bool IsUnderAge(DateTime DateOfBirth)
        {
            DateTime CompareDate = DateTime.Today.AddYears(-18);
            return (DateOfBirth - CompareDate).TotalDays > 0;
        }

        private clsPerson(IPeopleDataInterface PeopleDataInterface,PersonDTO PDTO)
        {
            this._PeopleDataInterface = PeopleDataInterface;
            this.PersonID = PDTO.PersonID;
            this.NationalNo = PDTO.NationalNo;
            this.FirstName = PDTO.FirstName;
            this.SecondName = PDTO.SecondName;
            this.ThirdName = PDTO.ThirdName;
            this.LastName = PDTO.LastName;
            this.DateOfBirth = PDTO.DateOfBirth;
            this.Gender = PDTO.Gender;
            this.Address = PDTO.Address;
            this.Phone = PDTO.Phone;
            this.Email = PDTO.Email;
            this.Nationality = (enNatinoality) PDTO.Nationality;
            this._ImagePath = PDTO.ImagePath;
            
        }
        
        public clsPerson(IPeopleDataInterface peopleDataInterface)
        {
            this._PeopleDataInterface= peopleDataInterface;
        }
      
        public async Task<clsPerson> GetPerson(string NationalNo)
        {
            PersonDTO PDTO =await _PeopleDataInterface.FindByNationalNoAsync(NationalNo);

            return (PDTO != null) ? new clsPerson(_PeopleDataInterface, PDTO) : null;

        }
        
        public async Task<clsPerson> GetPerson(int PersonID)
        {
            PersonDTO PDTO = await _PeopleDataInterface.FindByIDAsync(PersonID);

            return (PDTO != null) ? new clsPerson(_PeopleDataInterface, PDTO) : null;

        }

        public async Task<bool> CheckExistAsync(string NationalNo)
        {
            return await _PeopleDataInterface.IsPersonExistAsync(NationalNo);

        }

        public async Task<bool> CheckExistAsync(int PersonID)
        {

            return await _PeopleDataInterface.IsPersonExistAsync(PersonID);

        }

        public async Task<int?> CreatePerson(PersonDTO PersonDTO)
        {

             return await _PeopleDataInterface.AddNewPersonAsync(PersonDTO);
            
        }

        public async Task<bool> UpdatePerson(PersonDTO PersonDTO)
        {
            return await _PeopleDataInterface.UpdatePersonAsync(PersonDTO);
        }

        public async Task<bool> DeleteAsync(int PersonID)
        {
            
            return await _PeopleDataInterface.DeletePersonAsync(PersonID);
        }

        public  async Task<bool> DeleteAsync(string NationalNo)
        {
            
            return await _PeopleDataInterface.DeletePersonAsync(NationalNo);

        }

        public  async Task<IEnumerable<PersonViewDTO>> GetAllAsync()
        {
            return await _PeopleDataInterface.GetPeopleAsync();



        }
        public enPersonValidationType IsValid(PersonDTO PDTO)
        {
            if (PDTO == null)
            {
                return enPersonValidationType.NullObject;
            }

            if (HasPersonHaveEmptyFileds(PDTO))

            {
                return enPersonValidationType.EmptyFileds;
            }

            if (IsUnderAge(PDTO.DateOfBirth))
                return enPersonValidationType.UnderAge;


            if (CheckExistAsync(PDTO.NationalNo).Result)
            {
                return enPersonValidationType.NationalNoDuplicate;
            }
            if (PDTO.Nationality < 1 && PDTO.Nationality > 193)
                return enPersonValidationType.WrongNationality;

            return enPersonValidationType.Valid;
        }

        /*public async Task<bool> SaveAsync(PersonDTO Person = null, enMode Mode = enMode.AddNew)
        {
            Person = Person ?? this.PersonInfo;
            Mode = Mode == enMode.AddNew ? this.Mode : Mode;

            switch (Mode)
            {
                case enMode.AddNew:

                    if (await _CreatePerson(Person))
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return await _UpdatePerson(Person);


                default:
                    return false;

            }
        }*/

        /*        public static async Task<bool> DeletePersonAsync(int PersonID)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            return await DataLayer.DeletePersonAsync(PersonID);
        }*/

        /* public static async Task<bool> DeletePersonAsync(string NationalNo)
         {
             clsPeopleData DataLayer = new clsPeopleData();
             return await DataLayer.DeletePersonAsync(NationalNo);

         }
 */

        /*public static async Task<bool> IsPersonExistAsync(int ID)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            return await DataLayer.IsPersonExistAsync(ID);
        }*/

        /*public static async Task<bool> IsPersonExistAsync(string NationalNo)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            return await DataLayer.IsPersonExistAsync(NationalNo);
        }*/

        /* public static async Task<IEnumerable<PersonViewDTO>> GetAllPeopleAsync()
         {
             clsPeopleData DataLayer = new clsPeopleData();
             return await DataLayer.GetPeopleAsync();

         }*/

        /* public static async Task<clsPerson> Find(string NationalNo)
       {
           clsPeopleData DataLayer = new clsPeopleData();
           PersonDTO PDTO =await DataLayer.FindByNationalNoAsync(NationalNo);

           return (PDTO != null) ? new clsPerson(DataLayer, PDTO) : null;





       }*/

        /*public static async Task<clsPerson> Find(int PersonID)
        {
            clsPeopleData DataLayer = new clsPeopleData();
            PersonDTO PDTO = await DataLayer.FindByIDAsync(PersonID);

            return (PDTO != null) ? new clsPerson(DataLayer, PDTO) : null;
        }*/

    }


}