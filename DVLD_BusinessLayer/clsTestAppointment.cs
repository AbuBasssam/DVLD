using DVlD_BusinessLayer.Interfaces;
using DVLD_DataAccessLayer;
using DVLD_DataAccessLayer.Entities;
using DVLD_DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsTestAppointment :IBLLTestAppointment
    {
        private readonly IDALTestAppointment _DALTestAppointment;
        TestAppointmentDTO TADTO
        {
            get
            {
                return new TestAppointmentDTO
                    (
                    this.TestAppointmentID,
                    Convert.ToByte( this.TestTypeID),
                    this.LocalDrivingApplicationID,
                    this.AppointmentDate,
                    this.PaidFees,
                    this.CreatedBy,
                    this.IsLocked,
                    this.RetakeTestApplicationID
                    );
            }
        }
        public int TestAppointmentID { get; set; }
        public clsTestTypes.enTestType TestTypeID {  get; set; }
        public int LocalDrivingApplicationID {  get; set; }
        public DateTime AppointmentDate { get; set; }
        public float PaidFees {  get; set; }
        public int CreatedBy { get; set; }
        public bool IsLocked { get; set; }
        public int? RetakeTestApplicationID { set; get; }
        public int TestID
        {
            get { return _GetTestIDAsync().Result; }

        }
        public clsTestAppointment(IDALTestAppointment DALTestAppointment) 
        {
            this._DALTestAppointment = DALTestAppointment;

        }

        private clsTestAppointment(IDALTestAppointment DALTestAppointment, TestAppointmentDTO TTDTO)
        {
            this._DALTestAppointment = DALTestAppointment;
            this.TestAppointmentID =TTDTO.TestAppointmentID;
            this.TestTypeID =(clsTestTypes.enTestType) TTDTO.TestTypeID;
            this.LocalDrivingApplicationID= TTDTO.LocalDrivingLicenseApplicationID;
            this.AppointmentDate = TTDTO.AppointmentDate;
            this.PaidFees = TTDTO.PaidFees;
            this.CreatedBy = TTDTO.CreatedByUserID;
            this.IsLocked = TTDTO.IsLocked;
            this.RetakeTestApplicationID = TTDTO. RetakeTestApplicationID;

        }

        public async Task<IEnumerable<TestAppointmentDTO>> GetAllAppointmentAsync()
        {
            return await _DALTestAppointment.GetAllAppointmentAsync();
        }

        public async Task<IEnumerable<AppointmentTestTypeDTO>> GetApplicationTestAppointmentsPerTestTypeAsync(int LicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {
            return await _DALTestAppointment.GetApplicationTestAppointmentsPerTestTypeAsync(LicenseApplicationID, (byte)TestTypeID);
        }

        public async Task<IEnumerable<AppointmentTestTypeDTO>> GetApplicationTestAppointmentsPerTestTypeAsync(clsTestTypes.enTestType TestTypeID)//out of the BLL Interface
        {
            return await _DALTestAppointment.GetApplicationTestAppointmentsPerTestTypeAsync(this.LocalDrivingApplicationID, (byte)TestTypeID);

        }

        public async Task<clsTestAppointment> FindAsync(int TestAppointmentID)
        {
            TestAppointmentDTO TADTO = await _DALTestAppointment.FindByIDAsync(TestAppointmentID);

            return (TADTO != null) ? new clsTestAppointment(_DALTestAppointment, TADTO):null;
             
        }

        public async Task<int?> AddNewAppointmentAsync(TestAppointmentDTO TADTO)
        {
            return await _DALTestAppointment.AddNewAppointmentAsync(TADTO);
        }

        public async Task<bool> UpdateAppointmentAsync(TestAppointmentDTO TADTO)
        {
            return await _DALTestAppointment.UpdateAppointmentAsync(TADTO);
        }

        public async Task<bool> ExistAppointmentAsync(int LicenseApplicationID, int TestTypeID)
         {
             return await _DALTestAppointment.ExistAppointmentAsync(LicenseApplicationID, TestTypeID);
         }

        public async Task< clsTestAppointment> GetLastTestAppointmentAsync(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestType TestTypeID)
        {


            TestAppointmentDTO TADTO = await _DALTestAppointment.GetLastTestAppointmentAsync(LocalDrivingLicenseApplicationID, (byte)TestTypeID);

                return(TADTO!=null)? new clsTestAppointment(_DALTestAppointment, TADTO) :null;
            

        }

        private async Task< int> _GetTestIDAsync()
         {
             return await _DALTestAppointment.GetTestIDAsync(TestAppointmentID);
         }


    }
}
