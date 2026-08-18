using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AC_EmpManagement.Models
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext AuthFContext)
        {
            var userAuthInfo = AuthFContext.HttpContext.User.Identity.IsAuthenticated;
            if (!userAuthInfo)
            {
                AuthFContext.Result = new RedirectResult("~/ParthyTech/Account/Login?returnUrl=" + AuthFContext.HttpContext.Request.Path);//.Url.PathAndQuery);
                return;
            }
            // For unauthorized role
            var userAuthRoles = AuthFContext.HttpContext.User.IsInRole("Admin_Role");
            if(!userAuthRoles)
            {
                AuthFContext.Result = new RedirectResult("~/ParthyTech/Account/AccessDenied?returnUrl=" + AuthFContext.HttpContext.Request.Path);
                return;
            }
            // For unauthorized claim
            var userAClaim_Edit = AuthFContext.HttpContext.User.Claims.Any(c => c.Type == "Ability for Edition");
            if (!userAClaim_Edit && AuthFContext.HttpContext.Request.Path.Value.Contains("EditRole"))
            {
                AuthFContext.Result = new RedirectResult("~/ParthyTech/Account/AccessDenied?returnUrl=" + AuthFContext.HttpContext.Request.Path);
                return;
            }
            //var userAClaim_Delete = AuthFContext.HttpContext.User.Claims.Any(c => c.Type == "Ability for Deletion");
            //if (!userAClaim_Edit && AuthFContext.HttpContext.Request.Path.Value.Contains("DeleteRole"))
            //{
            //    AuthFContext.Result = new RedirectResult("~/ParthyTech/Account/AccessDenied?returnUrl=" + AuthFContext.HttpContext.Request.Path);
            //    return;
            //}
        }
    }
}
