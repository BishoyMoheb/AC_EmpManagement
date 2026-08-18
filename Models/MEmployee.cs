using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//To use Required  
using System.ComponentModel.DataAnnotations.Schema;//To use NotMapped

namespace AC_EmpManagement.Models
{
    //public class MEmployee
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public string Department { get; set; }
    //}

    //public class MEmployee
    //{
    //    // Using Enum
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public EnumDept Department { get; set; }
    //}

    //public class MEmployee
    //{
    //    // Using Validation attribute
    //    public int Id { get; set; }

    //    [Required]
    //    [MaxLength(50, ErrorMessage = "The Name can not exceed 50 characters.")]
    //    public string Name { get; set; }

    //    [Required]
    //    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
    //            ErrorMessage = "Invalid Email format")]
    //    [Display(Name = "Official Email")]
    //    public string Email { get; set; }

    //    [Required(ErrorMessage = "The Department filed is required. Please do selection.")]
    //    public EnumDept? Department { get; set; }
    //}

    //public class MEmployee
    //{
    //    /* Modifying the domain class by adding new features 
    //     * When the domain class has changed - the corresponding changes have 
    //     * to be made to the underlying database schema as well. Otherwise the 
    //     * database schema goes out of sync and the application does not work 
    //     * as expected.
    //     * */
    //    public int Id { get; set; }

    //    [Required]
    //    [MaxLength(50, ErrorMessage = "The Name can not exceed 50 characters.")]
    //    public string Name { get; set; }

    //    [Required]
    //    [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
    //            ErrorMessage = "Invalid Email format")]
    //    [Display(Name = "Official Email")]
    //    public string Email { get; set; }

    //    [Required(ErrorMessage = "The Department filed is required. Please do selection.")]
    //    public EnumDept? Department { get; set; }

    //    // The new property
    //    [Display(Name = "Picture")]
    //    public string PicPath { get; set; }
    //}

    public class MEmployee
    {
        /* Encrypting the Id value and storing it EncryptedId property
         * Thus we do not want EncryptedId property to be mapped to any 
         * column in the underlying DbS_Emps database table
         * */
        public int Id { get; set; }

        [NotMapped]
        public string EncryptedId { get; set; }

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
        public string PicPath { get; set; }
    }
}
