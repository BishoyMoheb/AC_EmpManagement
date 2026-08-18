using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.ViewModels
{
    // In order not to duplicate code, will make this class inherits 
    // from VM_MEmployee class
    public class VM_MEmpEDIT : VM_MEmployee
    {
        public int Id { get; set; }
        public string Existing_PhotoPath { get; set; }
    }
}
