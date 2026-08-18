using Microsoft.Extensions.Logging;//To use ILogger
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    public class EmpSQL_Repository : IEmpRepository
    {
        private readonly MEmpDBContext _medbContext;
        private readonly ILogger<EmpSQL_Repository> _loggerEmpSQL_R_I;

        //public EmpSQL_Repository(MEmpDBContext MEDBContext)
        //{
        //    this._medbContext = MEDBContext;
        //}

        public EmpSQL_Repository(MEmpDBContext MEDBContext,
                                 ILogger<EmpSQL_Repository> LoggerEmpSQL_R_I)
        {
            this._medbContext = MEDBContext;
            _loggerEmpSQL_R_I = LoggerEmpSQL_R_I;
        }

        //public MEmployee GetEmployee(int Id)
        //{
        //    //// My way
        //    //MEmployee Emp_ToSelect = _medbContext.DbS_Emps.FirstOrDefault(e => e.Id == Id);
        //    //return Emp_ToSelect;

        //    // Kudvenket way
        //    return _medbContext.DbS_Emps.Find(Id);
        //}

        public MEmployee GetEmployee(int Id)
        {
            _loggerEmpSQL_R_I.LogTrace("Trace Log");
            _loggerEmpSQL_R_I.LogDebug("Debug Log");
            _loggerEmpSQL_R_I.LogInformation("Information Log");
            _loggerEmpSQL_R_I.LogWarning("Warning Log");
            _loggerEmpSQL_R_I.LogError("Error Log");
            _loggerEmpSQL_R_I.LogCritical("Critical Log");
            return _medbContext.DbS_Emps.Find(Id);
        }

        public IEnumerable<MEmployee> Get_ALL()
        {
            return _medbContext.DbS_Emps;
        }


        public MEmployee AddEmp(MEmployee mEmp)
        {
            _medbContext.DbS_Emps.Add(mEmp);
            _medbContext.SaveChanges();
            return mEmp;
        }

        //public MEmployee UpdateEmp(MEmployee mEmp_Changes)
        //{
        //    // My way
        //    MEmployee Emp_ToUpdate = _medbContext.DbS_Emps.Find(mEmp_Changes.Id);
        //    if (Emp_ToUpdate != null)
        //    {
        //        Emp_ToUpdate.Name = mEmp_Changes.Name;
        //        Emp_ToUpdate.Email = mEmp_Changes.Email;
        //        Emp_ToUpdate.Department = mEmp_Changes.Department;
        //        _medbContext.DbS_Emps.Update(Emp_ToUpdate);
        //        _medbContext.SaveChanges();
        //    }
        //    return Emp_ToUpdate;
        //}

        //public MEmployee UpdateEmp(MEmployee mEmp_Changes)
        //{
        //    // Another way
        //    _medbContext.DbS_Emps.Attach(mEmp_Changes);
        //    _medbContext.Entry(mEmp_Changes).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    _medbContext.SaveChanges();
        //    return mEmp_Changes;
        //}

        public MEmployee UpdateEmp(MEmployee mEmp_Changes)
        {
            // Kudvenket way
            var Emp = _medbContext.DbS_Emps.Attach(mEmp_Changes);
            Emp.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _medbContext.SaveChanges();
            return mEmp_Changes;
        }

        public MEmployee DeleteEmp(int Id)
        {
            MEmployee Emp_ToDelete = _medbContext.DbS_Emps.Find(Id);
            if (Emp_ToDelete != null)
            {
                _medbContext.Remove(Emp_ToDelete);
                _medbContext.SaveChanges();
            }
            return Emp_ToDelete;
        }
    }
}
