using Microsoft.AspNetCore.Authorization;//To use AuthorizationHandler
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AC_EmpManagement.Security
{
    public class CaseSuperAdmin_Handler :
        AuthorizationHandler<ManageAdminRolesandClaimsReq>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext AHContext,
            ManageAdminRolesandClaimsReq MARCRequirement)
        {
            if (AHContext.User.IsInRole("SuperAdmin_Role"))
            {
                AHContext.Succeed(MARCRequirement);
            }
            return Task.CompletedTask;
        }
    }
}
