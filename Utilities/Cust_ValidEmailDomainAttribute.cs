using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//To use ValidationAttribute
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Utilities
{
    public class Cust_ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string _allowedDomain;

        public Cust_ValidEmailDomainAttribute(string AllowedDomain)
        {
            _allowedDomain = AllowedDomain;
        }

        public override bool IsValid(object ObjValue)
        {
            string[] str_Email = ObjValue.ToString().Split('@');
            return str_Email[1].ToUpper() == _allowedDomain.ToUpper();
        }
    }
}
