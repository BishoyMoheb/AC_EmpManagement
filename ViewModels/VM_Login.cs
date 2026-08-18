using Microsoft.AspNetCore.Authentication;//To use AuthenticationScheme
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Required, EmailAddress, DataType, Display 
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    //public class VM_Login
    //{
    //    [Required]
    //    [EmailAddress]
    //    public string Email { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [Display(Name = "Remember me")]
    //    public bool RememberMe { get; set; }
    //}

    public class VM_Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        // Adding properties for the External Authentication
        public string ReturnURL { get; set; }

        public IList<AuthenticationScheme> L_AS_ExternalLoginI { get; set; }
    }
}
