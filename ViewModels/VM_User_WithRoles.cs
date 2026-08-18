using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_User_WithRoles
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
