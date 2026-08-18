using Microsoft.AspNetCore.Identity;//To use IdentityUser
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    public class IdUserExtension : IdentityUser
    {
        public string City { get; set; }
    }
}
