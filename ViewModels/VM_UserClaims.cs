using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_UserClaims
    {
        public VM_UserClaims()
        {
            L_UClaims = new List<UserClaim>();
        }

        public string UserId { get; set; }
        public List<UserClaim> L_UClaims { get; set; }
    }
}
