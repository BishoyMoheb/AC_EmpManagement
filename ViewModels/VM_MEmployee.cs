using AC_EmpManagement.Models;//To use EnumDept
using Microsoft.AspNetCore.Http;//To use IFormFile
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use Required, Display, MaxLength
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    public class VM_MEmployee
    {
        [Required]
        [MaxLength(50, ErrorMessage = "The Name can not exceed 50 characters.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
                ErrorMessage = "Invalid Email format")]
        [Display(Name = "Official Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Department filed is required. Please do selection.")]
        public EnumDept? Department { get; set; }
        // The new property
        [Display(Name = "Picture")]
        public IFormFile PicPath { get; set; }

        //[Required]
        //[MaxLength(50, ErrorMessage = "The Name can not exceed 50 characters.")]
        //public string Name { get; set; }
        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        //        ErrorMessage = "Invalid Email format")]
        //[Display(Name = "Official Email")]
        //public string Email { get; set; }
        //[Required(ErrorMessage = "The Department filed is required. Please do selection.")]
        //public EnumDept? Department { get; set; }
        //// List of photos
        //[Display(Name = "Picture")]
        //public List<IFormFile> Photos_Path { get; set; }
    }
}
