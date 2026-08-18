using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Required, Display
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_EditRole
    {
        public VM_EditRole()
        {
            LUsers = new List<string>();
        }

        [Display(Name ="Role Id")]
        public string RoleId { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        public List<string> LUsers { get; set; }
    }
}
