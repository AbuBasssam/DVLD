using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;

namespace DVLD_DataAccessLayer
{



    static class clsDataAccessSettings
    {
        //public static string ConnectionString = ConfigurationManager.ConnectionStrings["Admin"].ToString();
        
         public static string ConnectionString = "Server=.;Database=DVLD;User Id=sa;Password=sa123456;";

    }
}
