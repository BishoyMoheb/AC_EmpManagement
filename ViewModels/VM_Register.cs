using AC_EmpManagement.Utilities;
using Microsoft.AspNetCore.Mvc;//To use Remote
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Required, Compare, DataType, Display, EmailAddress 
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    //public class VM_Register
    //{
    //    [Required]
    //    [EmailAddress]
    //    public string Email { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm Password")]
    //    [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
    //    public string ConfirmPassword { get; set; }
    //}

    //public class VM_Register
    //{
    //    // Using Remote attribute
    //    [Required]
    //    [EmailAddress]
    //    [Remote(action: "IsEmail_Exists", controller: "Account")]
    //    public string Email { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm Password")]
    //    [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
    //    public string ConfirmPassword { get; set; }
    //}

    //public class VM_Register
    //{
    //    // Using Custom validation attribute
    //    [Required]
    //    [EmailAddress]
    //    [Remote(action: "IsEmail_Exists", controller: "Account")]
    //    [Cust_ValidEmailDomain(AllowedDomain: "parthy.com", 
    //                           ErrorMessage = "Email domain must be parthy.com")]
    //    public string Email { get; set; }

    //    [Required]
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm Password")]
    //    [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
    //    public string ConfirmPassword { get; set; }
    //}

    public class VM_Register
    {
        // Adding the new column after Update-Database
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmail_Exists", controller: "Account")]
        [Cust_ValidEmailDomain(AllowedDomain: "parthy.com",
                               ErrorMessage = "Email domain must be parthy.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string City { get; set; }
    }
}
