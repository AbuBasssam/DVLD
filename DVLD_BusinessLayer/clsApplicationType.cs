using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVlD_BusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew, Update}
        
        public enMode Mode=enMode.AddNew;
        public int ApplicationID { set; get;}
        public string Title {  set; get; }  
        public int Fees { set; get; }

        public clsApplicationType()
        {
            this.ApplicationID = -1;
            this.Title ="";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }

        private clsApplicationType( int ApplicationID, string Title,int Fees)
        { 
            this.ApplicationID = ApplicationID;
            this.Title = Title; 
            this.Fees = Fees;
            Mode = enMode.Update;
        }
        
        public static DataTable GetAllApplicatoinTypes()
        {
            return clsApplicationTypesData.GetAllApplication();
        }
        
        public static clsApplicationType Find(int ApplicationID)
        {
            string Title = "";
            int ApplicationFees = 0;
            if (clsApplicationTypesData.FindByID(ApplicationID, ref Title, ref ApplicationFees))
            {
                return new clsApplicationType(ApplicationID, Title, ApplicationFees);
            }
            else
                return null;
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ApplicationID = clsApplicationTypesData.AddNewApplicationType(this.Title, this.Fees);


            return (this.ApplicationID != -1);
        }



        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplication(ApplicationID, Title, Fees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplicationType();

            }

            return false;
        }
    }
}
