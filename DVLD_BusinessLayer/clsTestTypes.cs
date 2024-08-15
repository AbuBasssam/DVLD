using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsTestTypes
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestTypes.enTestType ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public int Fees { set; get; }

        public clsTestTypes()
        {
            this.ID = clsTestTypes.enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }
        
        private clsTestTypes(clsTestTypes.enTestType ID, string Title, string Description, int Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description; 
            this.Fees = Fees;
            Mode = enMode.Update;

        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTest();
        }

        public static clsTestTypes Find(clsTestTypes.enTestType TestTypeID)
        {
            string Title = "", Description="";
            int ApplicationFees = 0;
            if (clsTestTypesData.FindByID((int)TestTypeID, ref Title, ref Description, ref ApplicationFees))
            {
                return new clsTestTypes(TestTypeID, Title, Description, ApplicationFees);
            }
            else
                return null;
        }
        
        private bool _AddNewTestType()
        {
            //call DataAccess Layer 

            this.ID = (clsTestTypes.enTestType)clsTestTypesData.AddNewTestType(this.Title, this.Description, this.Fees);

            return (this.Title != "");
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTest((int)this.ID, Title, Description, Fees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }

    }
}
