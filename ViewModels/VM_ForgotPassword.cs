using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Required, EmailAddress
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_ForgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
