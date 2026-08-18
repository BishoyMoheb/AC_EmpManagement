using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;//To use Claim
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    public static class ClaimsStore
    {
        public static List<Claim> L_Claims = new List<Claim>()
        {
            new Claim("Ability for Creation", "Create ability"),
            new Claim("Ability for Edition", "Edit ability"),
            new Claim("Ability for Deletion", "Delete ability")
        };
    }
}
