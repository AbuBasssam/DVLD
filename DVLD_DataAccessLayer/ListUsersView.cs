using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public class ListUsersView
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }

        public ListUsersView(int UserID, int PersonID, string FullName, string UserName, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.FullName = FullName;
            this.UserName = UserName;
            this.IsActive = IsActive;
        }
    }

}
