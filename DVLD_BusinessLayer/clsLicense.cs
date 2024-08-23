using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVlD_BusinessLayer.clsLicense;

namespace DVlD_BusinessLayer
{
    public class clsLicense
    {
        public enum enIssueReason { FirstTime = 1, Renew = 2, ReplacementForDamaged = 3, ReplacementForLost = 4 }
        
        public enum enMode { AddNew,Update};
        
        public enMode Mode { get; set; }
        
        public int LicenseID { get; set; }
        
        public int ApplicationID { get; set; }
        
        public int DriverID { get; set; }
        
        public int LicenseClass { get; set; }
        
        public DateTime IssueDate { get; set; }
        
        public DateTime ExpirationDate { get; set; }
        
        public string Notes { get; set; }
        
        public int PaidFees { get; set; }
        
        public byte IsActive { get; set; }
        
        public clsLicense.enIssueReason IssueReason { get; set; }
        
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
       
        public int CreatedByUserID { get; set; }
        
        public clsApplication ApplicationInfo { get; set; }
        
        public clsUser UserInfo { get; set; }
        
        public clsLicenseClasses LicenseClassesInfo { get; set; }
        
        public clsDriver DriverInfo { get; set; }

        public clsDetainedLicense DetainedInfo { set; get; }
        public bool IsDetained
        {
            get { return clsDetainedLicense.IsLicenseDetained(this.LicenseID);}
        }
        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.CreatedByUserID = -1;
            this.IssueDate=DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = 0;
            this.IssueReason = enIssueReason.FirstTime;
            this.Mode =enMode.AddNew;
        }

        private clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, 
            DateTime IssueDate, DateTime ExpirationDate, string Notes, int PaidFees, byte IsActive, byte IssueReason, int CreatedByUserID)
        {

            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.CreatedByUserID = CreatedByUserID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = (clsLicense.enIssueReason)IssueReason;
            this.ApplicationInfo = clsApplication.Find(ApplicationID);
            this.UserInfo = clsUser.FindByUserID(CreatedByUserID).Result;
            this.DriverInfo=clsDriver.FindByDriverID(DriverID);
            this.LicenseClassesInfo=clsLicenseClasses.Find(LicenseClass);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);

            this.Mode = enMode.Update;
        
        }

        public static  clsLicense Find(int LicenseID)
        {
            int ApplicationID=-1, DriverID = -1, LicenseClass = -1, PaidFees = -1, CreatedByUserID = -1;
            DateTime IssueDate=DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "" ;
            byte IsActive=0, IssueReason=0;
            if(clsLicensesData.FindByLicenseID(LicenseID,ref ApplicationID, ref DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID,ApplicationID,DriverID,LicenseClass,
                    IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID);
            
            else
                return null;
            
        }

        public static clsLicense Find(int LicenseID,int LicenseClassID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, PaidFees = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            byte IsActive = 0, IssueReason = 0;
            if (clsLicensesData.FindByLicenseIDAndLicenseClass(LicenseID, ref ApplicationID, ref DriverID,  LicenseClassID,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);

            else
                return null;

        }
        public static DataTable GetAllDriverLicenses(int DriverID)
        {
            return clsLicensesData.GetAllDriverLicenses(DriverID);

        }

        public static clsLicense FindByDriverID(int DriverID)
        {
            int ApplicationID = -1, LicenseID = -1, LicenseClass = -1, PaidFees = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            byte IsActive = 0, IssueReason = 0;
            if (clsLicensesData.FindByDriverID(ref LicenseID, ref ApplicationID, DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);

            else
                return null;

        }

        public static clsLicense FindByApplicationID(int ApplicationID)
        {
            int DriverID = -1, LicenseID = -1, LicenseClass = -1, PaidFees = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            byte IsActive = 0, IssueReason = 0;
            if (clsLicensesData.FindByApplicationID(ref LicenseID,  ApplicationID, ref DriverID, ref LicenseClass,
                ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);

            else
                return null;

        }

        private bool _AddNewLLicense()
        {
            this.LicenseID=clsLicensesData.AddNewLicense(ApplicationID, DriverID, LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,(byte)IssueReason, CreatedByUserID);
            return (this.LicenseID != -1);

        }
        
        private bool _UpdateLLicense()
        {
            return clsLicensesData.UpdateLicense(LicenseID,ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);
            

        }

        public static bool DeleteLicense(int LicenseID)
        {
            return clsLicensesData.DeleteLicense(LicenseID);
        }
        
        public static bool AlreadyHaveLicense(int PersonID,int LicenseClass)
        {
           clsDriver Driver= clsDriver.FindByPersonID(PersonID);
            int DriverID = (Driver!=null)? (int)Driver.DriverID:-1;
            if (DriverID == -1)
                return false;
            else
                return clsLicensesData.AlreadyHaveLicense(DriverID,LicenseClass);
        }
        
        public static bool IsLicenseExist(int ApplicationID)
        {
            return clsLicensesData.IsLicneseExist(ApplicationID);
        }
       
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewLLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;


                case enMode.Update:

                    return _UpdateLLicense();


                default:
                    return false;
            }
        }

        public static string GetIssueReasonText(enIssueReason IssueReason)
        {

            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.ReplacementForDamaged:
                    return "Replacement for Damaged";
                case enIssueReason.ReplacementForLost:
                    return "Replacement for Lost";
                default:
                    return "First Time";
            }
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicensesData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public Boolean IsLicenseExpired()
        {

            return (this.ExpirationDate < DateTime.Now);

        }

        public bool DeactivateCurrentLicense()
        {
            return (clsLicensesData.DeactivateLicense(this.LicenseID));
        }

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.DetainDate = DateTime.Now;
            detainedLicense.FineFees = (int)(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;

            if (!detainedLicense.Save())
            {

                return -1;
            }

            return detainedLicense.DetainID;

        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {

            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

           // Application.ApplicationPersonID = (int)this.DriverInfo.PersonID; must modfy
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStauteDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees;
            Application.CreatedBy = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }

            ApplicationID = (int)Application.ApplicationID;


            return this.DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, (int)Application.ApplicationID);
        }

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            //Application.ApplicationPersonID = (int)this.DriverInfo.PersonID;must modfy
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStauteDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            Application.CreatedBy = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = (int)Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.LicenseClassesInfo.DefalutValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.LicenseClassesInfo.ClassFees;
            NewLicense.IsActive = 1;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {


            //First Create Applicaiton 
            clsApplication Application = new clsApplication();

            //Application.ApplicationPersonID = (int)this.DriverInfo.PersonID; must modfy
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.ReplacementForDamaged) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStauteDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).Fees;
            Application.CreatedBy = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = (int)Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.
            NewLicense.IsActive = 1;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;



            if (!NewLicense.Save())
            {
                return null;
            }

            //we need to deactivate the old License.
            DeactivateCurrentLicense();

            return NewLicense;
        }

    }
}
