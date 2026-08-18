using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Compare, DataType, Display, Required
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_ChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "The New Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
