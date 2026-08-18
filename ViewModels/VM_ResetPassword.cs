using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Required, EmailAddress, DataType, Display, Compare
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_ResetPassword
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }

        public string PRToken { get; set; }
    }
}
