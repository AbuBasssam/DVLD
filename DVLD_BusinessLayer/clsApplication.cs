using System;
using DVLD_DataAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Security.Policy;

namespace DVlD_BusinessLayer
{
    public class clsApplication
    {
        public enum enMode { AddNew, Update };

        public enMode Mode;
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public Nullable<int> ApplicationID { get; set; }

        public int ApplicationPersonID { get; set; }

        public DateTime ApplicationDate { get; set; }

        public int ApplicationTypeID { get; set; }

        public clsApplication.enApplicationStatus ApplicationStatus { get; set; }

        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }

        public DateTime LastStauteDate { get; set; }

        public int PaidFees { get; set; }

        public int CreatedBy { get; set; }

        public clsUser User { get; set; }
        public clsApplicationType ApplicationType { get; set; }

        public clsPerson Person { get; set; }
        public clsApplication()
        {
            this.ApplicationID = null;
            this.ApplicationPersonID = -1;
            this.ApplicationDate = DateTime.Today;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStauteDate = DateTime.Today;
            this.PaidFees = PaidFees;
            this.CreatedBy = -1;
            this.User = new clsUser();
            this.ApplicationType = new clsApplicationType();
            Mode = enMode.AddNew;


        }

        private clsApplication(int applicationID, int applicationPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStaute, DateTime LastStauteDate, int PaidFees, int CreatedBy)
        {
            this.ApplicationID = applicationID;
            this.ApplicationPersonID = applicationPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = (enApplicationStatus)ApplicationStaute;
            this.LastStauteDate = LastStauteDate;
            this.PaidFees = PaidFees;
            this.CreatedBy = CreatedBy;
            this.User = clsUser.FindByUserID(CreatedBy);
            this.Person = clsPerson.Find(applicationPersonID);
            this.ApplicationType = clsApplicationType.Find(ApplicationTypeID);
            Mode = enMode.Update;


        }

        public static  clsApplication Find(int ApplicationID)
        {
            int applicationPersonID = -1, ApplicationTypeID = -1, PaidFees=0, CreatedBy=-1;
            DateTime ApplicationDate = DateTime.Now, LastStauteDate=DateTime.Now;
            byte ApplicationStaute = 0;
            if (clsApplicationData.Find(ApplicationID, ref applicationPersonID, ref ApplicationDate, ref ApplicationTypeID
              , ref ApplicationStaute, ref LastStauteDate, ref PaidFees, ref CreatedBy))
            {
                return new clsApplication(ApplicationID,  applicationPersonID,  ApplicationDate,  ApplicationTypeID
              ,  ApplicationStaute,  LastStauteDate,  PaidFees,  CreatedBy);



            }
            else
            {
                return null;
            }

        }
        
        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(ApplicationPersonID, ApplicationDate, ApplicationTypeID, Convert.ToByte(ApplicationStatus), LastStauteDate, PaidFees, CreatedBy);
            return (ApplicationID != null);
            
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication((int)ApplicationID, ApplicationPersonID, ApplicationDate, ApplicationTypeID, Convert.ToByte(ApplicationStatus), LastStauteDate, PaidFees, CreatedBy);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
           return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public bool Cancel()

        {
            return clsApplicationData.UpdateStatus((int)ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationData.UpdateStatus((int)ApplicationID, 3);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update:

                    return _UpdateApplication();


                default:
                    return false;

            }

        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication((int)this.ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicationPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplication.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicationPersonID, ApplicationTypeID);
        }


    }   
}
