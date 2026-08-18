using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    public class EmpRepository : IEmpRepository
    {
        private List<MEmployee> _empList;

        public EmpRepository()
        {
            //_empList = new List<MEmployee>()
            //{
            //    new MEmployee(){ Id=1, Name="Bishoy", Department="IT", Email="bishoy@parthy.com" },
            //    new MEmployee(){ Id=2, Name="Nosa", Department="HR", Email="nosa@parthy.com" },
            //    new MEmployee(){ Id=3, Name="Parthy", Department="Payroll", Email="parthy@parthy.com"}
            //};

            // Using Enum
            _empList = new List<MEmployee>()
            {
                new MEmployee(){ Id=1, Name="Bishoy", Department=EnumDept.IT, Email="bishoy@parthy.com" },
                new MEmployee(){ Id=2, Name="Nosa", Department=EnumDept.HR, Email="nosa@parthy.com" },
                new MEmployee(){ Id=3, Name="Parthy", Department=EnumDept.PayRoll, Email="parthy@parthy.com"}
            };
        }

        public MEmployee GetEmployee(int Given_ID)
        {
            MEmployee Emp = _empList.FirstOrDefault(e => e.Id == Given_ID);
            return Emp;
        }

        public IEnumerable<MEmployee> Get_ALL()
        {
            return _empList;
        }
        
        public MEmployee AddEmp(MEmployee mEmp)
        {
            mEmp.Id = _empList.Max(e => e.Id) + 1;
            _empList.Add(mEmp);
            return mEmp;
        }

        public MEmployee UpdateEmp(MEmployee mEmp_Changes)
        {
            MEmployee Emp_ToUpdate = _empList.FirstOrDefault(e => e.Id == mEmp_Changes.Id);
            if (Emp_ToUpdate != null)
            {
                Emp_ToUpdate.Name = mEmp_Changes.Name;
                Emp_ToUpdate.Email = mEmp_Changes.Email;
                Emp_ToUpdate.Department = mEmp_Changes.Department;
            }
            return Emp_ToUpdate;
        }

        public MEmployee DeleteEmp(int Id)
        {
            MEmployee Emp_ToDelete = _empList.FirstOrDefault(e => e.Id == Id);
            if (Emp_ToDelete != null)
                _empList.Remove(Emp_ToDelete);
            return Emp_ToDelete;
        }
    }
}
