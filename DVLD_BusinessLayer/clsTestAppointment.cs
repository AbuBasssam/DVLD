using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew=0, Update=1};
        public enMode Mode;
        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID {  get; set; }
        public int LocalDrivingApplicationID {  get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees {  get; set; }
        public int CreatedBy { get; set; }
        public bool IsLocked { get; set; }
        public clsTestTypes TestTypes { get; set; }
        public  clsUser UserInfo { get; set; }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo { get; set;}
        public int RetakeTestApplicationID { set; get; }
        public clsApplication RetakeTestAppInfo { set; get; }
        public int TestID
        {
            get { return _GetTestID(); }

        }
        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = clsTestTypes.enTestType.VisionTest;
            LocalDrivingApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedBy = -1;
            RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private clsTestAppointment(int TestAppointmentID, clsTestTypes.enTestType TestTypeID, int LocalDrivingApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedBy, bool IsLocked, int RetakeTestApplicationID
)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingApplicationID= LocalDrivingApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedBy = CreatedBy;
            this.IsLocked = IsLocked;
            this.UserInfo = clsUser.FindByUserID(this.CreatedBy);
            this.TestTypes = clsTestTypes.Find((clsTestTypes.enTestType) TestTypeID);
            this.LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.Find(LocalDrivingApplicationID);
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo=clsApplication.Find(RetakeTestApplicationID);

            Mode = enMode.Update;
        }
        
        
        public static DataTable GetAllAppointment()
        {
            return clsTestAppointmentData.GetAllAppointment();
        }


        public static DataTable GetApplicationTestAppointmentsPerTestType(int LicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LicenseApplicationID, (int)TestTypeID);
        }
        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestTypes.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingApplicationID, (int)TestTypeID);

        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.FindByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, (clsTestTypes.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;
        }

        private bool _AddNewAppointment()
        {
            this. TestAppointmentID= clsTestAppointmentData.AddNewAppointment((int)TestTypeID, LocalDrivingApplicationID, AppointmentDate, PaidFees, CreatedBy, IsLocked, RetakeTestApplicationID);
            return (TestAppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            return clsTestAppointmentData.UpdateAppointment(TestAppointmentID, (int)TestTypeID, LocalDrivingApplicationID, AppointmentDate, PaidFees, CreatedBy, IsLocked, RetakeTestApplicationID);
        }
        
        public static bool ExistAppointment(int LicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.ExistAppointment(LicenseApplicationID, TestTypeID);
        }

       /* public static bool AlreadyPassed(int LicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentData.AlreadyPassed(LicenseApplicationID, TestTypeID);
        }*/


        public static int Trail(int TestTypeID,int LocalDrivingApplicationID)
        {
            return clsTestAppointmentData.Trail(TestTypeID, LocalDrivingApplicationID);   
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;


                case enMode.Update:

                    return _UpdateAppointment();


                default:
                    return false;

            }
        }
        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

        
        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }


    }
}
