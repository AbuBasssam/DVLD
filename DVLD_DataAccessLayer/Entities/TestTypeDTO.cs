using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer.Entities
{
    public class TestTypeDTO
    {
        public int TestTypeID {get; set;}
        public string Title {get;set;}
        public string Description { get; set; }
        public float TestFees {get;set; }
       public TestTypeDTO(int TestTypeID,string Title, string Description, float TestFees)
       {
            this.TestTypeID = TestTypeID;
            this.Title = Title;
            this.Description = Description;
            this.TestFees = TestFees;

       }
    }
}
