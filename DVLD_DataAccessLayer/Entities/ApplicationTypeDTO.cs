using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class ApplicationTypeDTO
    {
        public int ApplicationID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }
    }
}
