using Microsoft.AspNetCore.Authorization;//To use AuthorizationHandler
using Microsoft.AspNetCore.Mvc.Filters;//To use AuthorizationFilterContext
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AC_EmpManagement.Security
{
    public class EditOnlyOtherAdminRolesandClaimsHandler :
        AuthorizationHandler<ManageAdminRolesandClaimsReq>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext AHContext,
            ManageAdminRolesandClaimsReq MARCRequirement)
        {
            var AFilterContext = AHContext.Resource as AuthorizationFilterContext;
            if (AFilterContext == null)
            {
                return Task.CompletedTask;
            }
            string LoggedIn_AdminID = AHContext.User
                                               .Claims
                                               .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string AdminID_Edited = AFilterContext.HttpContext.Request.Query["UserId"];
            if (AHContext.User.IsInRole("Admin_Role")
               && AHContext.User.HasClaim(c => c.Type == "Edit ability" && c.Value == "true")
               && AdminID_Edited.ToLower() != LoggedIn_AdminID.ToLower())
            {
                AHContext.Succeed(MARCRequirement);
            }
            return Task.CompletedTask;
        }
    }

    //public class EditOnlyOtherAdminRolesandClaimsHandler :
    //    AuthorizationHandler<ManageAdminRolesandClaimsReq>
    //{
    //    protected override Task HandleRequirementAsync(AuthorizationHandlerContext AHContext,
    //        ManageAdminRolesandClaimsReq MARCRequirement)
    //    {
    //        var AFilterContext = AHContext.Resource as AuthorizationFilterContext;
    //        if (AFilterContext == null)
    //        {
    //            return Task.CompletedTask;
    //        }
    //        string LoggedIn_AdminID = AHContext.User
    //                                           .Claims
    //                                           .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
    //        string AdminID_Edited = AFilterContext.HttpContext.Request.Query["UserId"];
    //        if (AHContext.User.IsInRole("Admin_Role")
    //           && AHContext.User.HasClaim(c => c.Type == "Edit ability" && c.Value == "true")
    //           && AdminID_Edited.ToLower() != LoggedIn_AdminID.ToLower())
    //        {
    //            AHContext.Succeed(MARCRequirement);
    //        }
    //        else
    //        {
    //            // Return an explicit failure
    //            AHContext.Fail();
    //        }
    //        return Task.CompletedTask;
    //    }
    //}
}
