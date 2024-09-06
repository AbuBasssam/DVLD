using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class ReleaseLicenseDTO
    {

        public int DetainID { get; set; }

        public int? ReleasedByUserID { get; set; }

        public int? ReleaseApplicationID { get; set; }

        public ReleaseLicenseDTO(int detainID, int? releasedByUserID, int? releaseApplicationID)
        {
            DetainID = detainID;
            ReleasedByUserID = releasedByUserID;
            ReleaseApplicationID = releaseApplicationID;
        }
    }
}
