using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsInternationalLicense
    {
        public enum enMode { AddNew, Update };
        public enMode Mode { get; set; }
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte IsActive { get; set; }
        public int CreatedByUserID { get; set; }
        public clsApplication ApplicationInfo { get; set; }
        public clsUser UserInfo { get; set; }
        public clsDriver DriverInfo { get; set; }
        public clsInternationalLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.CreatedByUserID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = 0;
            this.Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, byte IsActive, int CreatedByUserID)
        {

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.CreatedByUserID = CreatedByUserID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.ApplicationInfo = clsApplication.Find(ApplicationID);
            this.UserInfo = clsUser.FindByUserID(CreatedByUserID).Result;
            this.DriverInfo = clsDriver.FindByDriverID(DriverID);
            this.Mode = enMode.AddNew;

        }

        public static DataTable GetAllDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalInternationalLicenseData.GetAllDriverInternationalLicenses(DriverID);

        }


        public static clsInternationalLicense FindByLicenseID(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            byte IsActive = 0, IssueReason = 0;
            if (clsInternationalInternationalLicenseData.FindByInternationalLicenseID( InternationalLicenseID,ref ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate,ref IsActive,ref CreatedByUserID))

                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                    IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            else
                return null;

        }

        public static clsInternationalLicense FindByLicenseID(int InternationalLicenseID, int IssuedUsingLocalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, PaidFees = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            byte IsActive = 0, IssueReason = 0;
            if (clsInternationalInternationalLicenseData.FindByInternationalLicenseIDAndIssuedUsingLocalLicenseID(InternationalLicenseID, ref ApplicationID, ref DriverID, IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))

                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, LicenseClass,
                    IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            else
                return null;

        }

        public static clsInternationalLicense FindByDriverID(int DriverID)
        {
            int ApplicationID = -1, LicenseID = -1, IssuedUsingLocalLicenseID = -1, PaidFees = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            byte IsActive = 0, IssueReason = 0;
            if (clsInternationalInternationalLicenseData.FindByDriverID(ref LicenseID, ref ApplicationID, DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate,  ref IsActive, ref CreatedByUserID))

                return new clsInternationalLicense(LicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                    IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            else
                return null;

        }

        public static clsInternationalLicense FindByApplicationID(int ApplicationID)
        {
            int DriverID = -1, InternationalLicenseID = -1, IssuedUsingLocalLicenseID = -1, PaidFees = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            byte IsActive = 0, IssueReason = 0;
            if (clsInternationalInternationalLicenseData.FindByApplicationID(ref InternationalLicenseID, ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID,
                ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))

                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                   IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            else
                return null;

        }

        private bool _AddNewLLicense()
        {
            this.InternationalLicenseID = clsInternationalInternationalLicenseData.AddNewInternationalInternationalLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            return (this.InternationalLicenseID != -1);

        }

        private bool _UpdateLLicense()
        {
            return clsInternationalInternationalLicenseData.UpdateInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,IsActive,CreatedByUserID);


        }

        public static bool DeleteLicense(int LicenseID)
        {
            return clsInternationalInternationalLicenseData.DeleteInternationalLicense(LicenseID);
        }

        public static bool AlreadyHaveLicense(int DriverID)
        {

         return clsInternationalInternationalLicenseData.AlreadyHaveInternationalLicense(DriverID);
        
        }

        public static bool IsLicenseExist(int InternationalLicenseID)
        {
            return clsInternationalInternationalLicenseData.IsLicneseExist(InternationalLicenseID);
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalInternationalLicenseData.GetAllInternationalLicenses();
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

    }
}
