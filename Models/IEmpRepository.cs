using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    public interface IEmpRepository
    {
        MEmployee GetEmployee(int Id);
        IEnumerable<MEmployee> Get_ALL();
        // Add Employee 
        MEmployee AddEmp(MEmployee mEmp);
        // Update and Delete Employee
        MEmployee UpdateEmp(MEmployee mEmp_Changes);
        MEmployee DeleteEmp(int Id);
    }
}
