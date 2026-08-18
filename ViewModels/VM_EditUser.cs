using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Required, EmailAddress
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_EditUser
    {
        public VM_EditUser()
        {
            L_UClaims = new List<string>();
            L_URolesI = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string City { get; set; }

        public List<string> L_UClaims { get; set; }

        public IList<string> L_URolesI { get; set; }
    }
}
