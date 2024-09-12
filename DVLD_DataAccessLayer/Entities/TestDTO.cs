using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class TestDTO
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public byte TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }

        public TestDTO(int testID, int testAppointmentID, byte testResult, string notes, int createdBy )
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Notes = notes;
            CreatedBy = createdBy;
        }
    }

}
