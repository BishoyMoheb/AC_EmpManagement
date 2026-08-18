using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;//To use Claim
using System.Threading.Tasks;
using AC_EmpManagement.Models;//To use CustomAuthorizeAttribute
using AC_EmpManagement.ViewModels;//To use VM_CreateRole
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;//To use IdentityRole, RoleManager
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//To use DbUpdateException
using Microsoft.Extensions.Logging;//To use ILogger

namespace AC_EmpManagement.Controllers
{
    //[CustomAuthorizeAttribute(Roles = "Admin_Role")]
    public class AdminController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly RoleManager<IdentityRole> _roleMan_IDRole;
        private readonly UserManager<IdUserExtension> _userMan_IDUserExt;
        private readonly ILogger<AdminController> _loggerI;
        

        //public AdminController(RoleManager<IdentityRole> RoleMan_IDRole)
        //{
        //    _roleMan_IDRole = RoleMan_IDRole;
        //}

        //public AdminController(RoleManager<IdentityRole> RoleMan_IDRole,
        //                       UserManager<IdUserExtension> UserMan_IDUserExt)
        //{
        //    _roleMan_IDRole = RoleMan_IDRole;
        //    _userMan_IDUserExt = UserMan_IDUserExt;
        //}

        public AdminController(RoleManager<IdentityRole> RoleMan_IDRole,
                               UserManager<IdUserExtension> UserMan_IDUserExt,
                               ILogger<AdminController> LoggerI)
        {
            _roleMan_IDRole = RoleMan_IDRole;
            _userMan_IDUserExt = UserMan_IDUserExt;
            _loggerI = LoggerI;
        }

        //
        // GET: /Admin/CreateRole
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        // POST: /Admin/CreateRole
        [HttpPost]
        public async Task<IActionResult> CreateRole(VM_CreateRole vmCRole)
        {
            if (ModelState.IsValid)
            {
                IdentityRole IDRole = new IdentityRole { Name = vmCRole.RoleName };
                IdentityResult IDResult = await _roleMan_IDRole.CreateAsync(IDRole);
                if (IDResult.Succeeded)
                    //return RedirectToAction("Index", "Home");
                    return RedirectToAction("RolesList", "Admin");
                foreach (IdentityError idError in IDResult.Errors)
                {
                    ModelState.AddModelError("", idError.Description);
                }
            }
            return View(vmCRole);
        }

        //
        // GET: /Admin/RolesList
        [HttpGet]
        public IActionResult RolesList()
        {
            var LRoles = _roleMan_IDRole.Roles;
            return View(LRoles);
        }

        //
        // GET: /Admin/UsersList
        [HttpGet]
        public IActionResult UsersList()
        {
            var LUsers = _userMan_IDUserExt.Users;
            return View(LUsers);
        }

        //
        // GET: /Admin/EditRole
        [HttpGet]
        public async Task<IActionResult> EditRole(string RoleId)
        {
            var IdRole = await _roleMan_IDRole.FindByIdAsync(RoleId);
            if (IdRole == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {RoleId}, can not be found";
                return View("RouteNotFound");
            }
            var vmEditRole = new VM_EditRole
            {
                RoleId = IdRole.Id,
                RoleName = IdRole.Name
            };
            foreach (var IDUExt_item in _userMan_IDUserExt.Users)
            {
                if (await _userMan_IDUserExt.IsInRoleAsync(IDUExt_item, IdRole.Name))
                    vmEditRole.LUsers.Add(IDUExt_item.UserName);
            }
            return View(vmEditRole);
        }

        ////
        //// GET: /Admin/EditRole
        //[HttpGet]
        //// To Authorize only the users who have a certain CLAIM
        //[CustomAuthorize(Policy = "EditPolicy_CLAIM")]
        //public async Task<IActionResult> EditRole(string RoleId)
        //{
        //    var IdRole = await _roleMan_IDRole.FindByIdAsync(RoleId);
        //    if (IdRole == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {RoleId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var vmEditRole = new VM_EditRole
        //    {
        //        RoleId = IdRole.Id,
        //        RoleName = IdRole.Name
        //    };
        //    foreach (var IDUExt_item in _userMan_IDUserExt.Users)
        //    {
        //        if (await _userMan_IDUserExt.IsInRoleAsync(IDUExt_item, IdRole.Name))
        //            vmEditRole.LUsers.Add(IDUExt_item.UserName);
        //    }
        //    return View(vmEditRole);
        //}

        // POST: /Admin/EditRole
        [HttpPost]
        public async Task<IActionResult> EditRole(VM_EditRole vmEditRole)
        {
            var IDRole = await _roleMan_IDRole.FindByIdAsync(vmEditRole.RoleId);
            if (IDRole == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {vmEditRole.RoleId}, can not be found";
                return View("RouteNotFound");
            }
            else
            {
                IDRole.Name = vmEditRole.RoleName;
                var IdResult = await _roleMan_IDRole.UpdateAsync(IDRole);
                if (IdResult.Succeeded)
                {
                    return RedirectToAction("RolesList", "Admin");
                }
                foreach (var IdError in IdResult.Errors)
                {
                    ModelState.AddModelError("", IdError.Description);
                }
                return View(vmEditRole);
            }
        }

        //// POST: /Admin/EditRole
        //[HttpPost]
        //// To Authorize only the users who have a certain CLAIM
        //[CustomAuthorize(Policy = "EditPolicy_CLAIM")]
        //public async Task<IActionResult> EditRole(VM_EditRole vmEditRole)
        //{
        //    var IDRole = await _roleMan_IDRole.FindByIdAsync(vmEditRole.RoleId);
        //    if (IDRole == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {vmEditRole.RoleId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    else
        //    {
        //        IDRole.Name = vmEditRole.RoleName;
        //        var IdResult = await _roleMan_IDRole.UpdateAsync(IDRole);
        //        if (IdResult.Succeeded)
        //        {
        //            return RedirectToAction("RolesList", "Admin");
        //        }
        //        foreach (var IdError in IdResult.Errors)
        //        {
        //            ModelState.AddModelError("", IdError.Description);
        //        }
        //        return View(vmEditRole);
        //    }
        //}

        //
        // GET: /Admin/EditUsers_InRole
        [HttpGet]
        public async Task<IActionResult> EditUsers_InRole(string RoleId)
        {
            ViewBag.roleId = RoleId;
            var IDRole = await _roleMan_IDRole.FindByIdAsync(RoleId);
            if (IDRole == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {RoleId}, can not be found";
                return View("RouteNotFound");
            }
            var L_vmURole = new List<VM_UserRole>();
            foreach (var IdUExt in _userMan_IDUserExt.Users)
            {
                var vmUserRole = new VM_UserRole()
                {
                    UserId = IdUExt.Id,
                    UserName = IdUExt.UserName
                };
                if (await _userMan_IDUserExt.IsInRoleAsync(IdUExt, IDRole.Name))
                    vmUserRole.IsSelected = true;
                else
                    vmUserRole.IsSelected = false;
                L_vmURole.Add(vmUserRole);
            }
            return View(L_vmURole);
        }

        // POST: /Admin/EditUsers_InRole
        [HttpPost]
        public async Task<IActionResult> EditUsers_InRole(List<VM_UserRole> L_vmURole, string RoleId)
        {
            var IDRole = await _roleMan_IDRole.FindByIdAsync(RoleId);
            if (IDRole == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {RoleId}, can not be found";
                return View("RouteNotFound");
            }
            for (int i = 0; i < L_vmURole.Count; i++)
            {
                var IdUExt = await _userMan_IDUserExt.FindByIdAsync(L_vmURole[i].UserId);
                IdentityResult IdResult = null;
                if (L_vmURole[i].IsSelected && !(await _userMan_IDUserExt.IsInRoleAsync(IdUExt, IDRole.Name)))
                    IdResult = await _userMan_IDUserExt.AddToRoleAsync(IdUExt, IDRole.Name);
                else if (!L_vmURole[i].IsSelected && await _userMan_IDUserExt.IsInRoleAsync(IdUExt, IDRole.Name))
                    IdResult = await _userMan_IDUserExt.RemoveFromRoleAsync(IdUExt, IDRole.Name);
                else
                    continue;
                if (IdResult.Succeeded)
                {
                    if (i < L_vmURole.Count - 1)
                        continue;
                    else
                        return RedirectToAction("EditRole", "Admin", new { RoleId = RoleId });
                }
            }
            return RedirectToAction("EditRole", "Admin", new { RoleId = RoleId });
        }

        ////
        //// GET: /Admin/EditUsers
        //[HttpGet]
        //public async Task<IActionResult> EditUsers(string Id)
        //{
        //    var IdUser = await _userMan_IDUserExt.FindByIdAsync(Id);
        //    if (IdUser == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {Id}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var L_UserClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
        //    var L_UserRolesI = await _userMan_IDUserExt.GetRolesAsync(IdUser);
        //    var vmEditUser = new VM_EditUser
        //    {
        //        Id = IdUser.Id,
        //        Email = IdUser.Email,
        //        UserName = IdUser.UserName,
        //        City = IdUser.City,
        //        L_UClaims = L_UserClaimsI.Select(c => c.Value).ToList(),
        //        L_URolesI = L_UserRolesI
        //    };
        //    return View(vmEditUser);
        //}

        //
        // GET: /Admin/EditUsers
        // Dealing with CLAIMS Types and Values
        [HttpGet]
        public async Task<IActionResult> EditUsers(string Id)
        {
            var IdUser = await _userMan_IDUserExt.FindByIdAsync(Id);
            if (IdUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {Id}, can not be found";
                return View("RouteNotFound");
            }
            var L_UserClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
            var L_UserRolesI = await _userMan_IDUserExt.GetRolesAsync(IdUser);
            var vmEditUser = new VM_EditUser
            {
                Id = IdUser.Id,
                Email = IdUser.Email,
                UserName = IdUser.UserName,
                City = IdUser.City,
                L_UClaims = L_UserClaimsI.Where(c => c.Value == "true").Select(c => c.Type).ToList(),
                L_URolesI = L_UserRolesI
            };
            return View(vmEditUser);
        }

        // POST: /Admin/EditUsers
        [HttpPost]
        public async Task<IActionResult> EditUsers(VM_EditUser vmEditUser)
        {
            var IdUser = await _userMan_IDUserExt.FindByIdAsync(vmEditUser.Id);
            if (IdUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {vmEditUser.Id}, can not be found";
                return View("RouteNotFound");
            }
            else
            {
                IdUser.UserName = vmEditUser.UserName;
                IdUser.Email = vmEditUser.Email;
                IdUser.City = vmEditUser.City;
                var IdResult = await _userMan_IDUserExt.UpdateAsync(IdUser);
                if (IdResult.Succeeded)
                    return RedirectToAction("UsersList", "Admin");
                foreach (var IdError in IdResult.Errors)
                {
                    ModelState.AddModelError("", IdError.Description);
                }
                return View(vmEditUser);
            }
        }

        //
        // POST: /Admin/DeleteUser
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var IdUser = await _userMan_IDUserExt.FindByIdAsync(Id);
            if (IdUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {Id}, can not be found";
                return View("RouteNotFound");
            }
            else
            {
                var IdResult = await _userMan_IDUserExt.DeleteAsync(IdUser);
                if (IdResult.Succeeded)
                    return RedirectToAction("UsersList", "Admin");
                foreach (var IdError in IdResult.Errors)
                {
                    ModelState.AddModelError("", IdError.Description);
                }
                return View("UsersList");
            }
        }

        ////
        //// POST: /Admin/DeleteRole
        //[HttpPost]
        //public async Task<IActionResult> DeleteRole(string RoleId)
        //{
        //    var IdRole = await _roleMan_IDRole.FindByIdAsync(RoleId);
        //    if (IdRole == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {RoleId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    else
        //    {
        //        var IdResult = await _roleMan_IDRole.DeleteAsync(IdRole);
        //        if (IdResult.Succeeded)
        //            return RedirectToAction("RolesList", "Admin");
        //        foreach (var IdError in IdResult.Errors)
        //        {
        //            ModelState.AddModelError("", IdError.Description);
        //        }
        //        return View("RolesList");
        //    }
        //}

        ////
        //// POST: /Admin/DeleteRole
        //[HttpPost]
        //public async Task<IActionResult> DeleteRole(string RoleId)
        //{
        //    var IdRole = await _roleMan_IDRole.FindByIdAsync(RoleId);
        //    if (IdRole == null)
        //    {
        //        ViewBag.ErrorMessage = $"Role with Id = {RoleId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var IdResult = await _roleMan_IDRole.DeleteAsync(IdRole);
        //            if (IdResult.Succeeded)
        //                return RedirectToAction("RolesList", "Admin");
        //            foreach (var IdError in IdResult.Errors)
        //            {
        //                ModelState.AddModelError("", IdError.Description);
        //            }
        //            return View("RolesList");
        //        }
        //        catch (DbUpdateException Ex)
        //        {
        //            _loggerI.LogError($"Error deleting role {Ex}");
        //            ViewBag.ErrorTitle = $"{IdRole.Name} role is in use ";
        //            ViewBag.ErrorMessage = $"{IdRole.Name} role can not be deleted " +
        //                                   $"as there are users in the role. To delete " +
        //                                   $"this role you must first remove the users" +
        //                                   $"from this role and then delete the role";
        //            return View("GlobalErrorHandled");
        //        }
        //    }
        //}

        //
        // POST: /Admin/DeleteRole
        [HttpPost]
        // To Authorize only the users who have a certain CLAIM
        [CustomAuthorize(Policy = "DeletePolicy_CLAIM")]
        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            var IdRole = await _roleMan_IDRole.FindByIdAsync(RoleId);
            if (IdRole == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {RoleId}, can not be found";
                return View("RouteNotFound");
            }
            else
            {
                try
                {
                    var IdResult = await _roleMan_IDRole.DeleteAsync(IdRole);
                    if (IdResult.Succeeded)
                        return RedirectToAction("RolesList", "Admin");
                    foreach (var IdError in IdResult.Errors)
                    {
                        ModelState.AddModelError("", IdError.Description);
                    }
                    return View("RolesList");
                }
                catch (DbUpdateException Ex)
                {
                    _loggerI.LogError($"Error deleting role {Ex}");
                    ViewBag.ErrorTitle = $"{IdRole.Name} role is in use ";
                    ViewBag.ErrorMessage = $"{IdRole.Name} role can not be deleted " +
                                           $"as there are users in the role. To delete " +
                                           $"this role you must first remove the users" +
                                           $"from this role and then delete the role";
                    return View("GlobalErrorHandled");
                }
            }
        }

        ////
        //// GET: /Admin/ManageUserRoles
        //[HttpGet]
        //public async Task<IActionResult> ManageUserRoles(string UserId)
        //{
        //    ViewBag.UId = UserId;
        //    var IdUser = await _userMan_IDUserExt.FindByIdAsync(UserId);
        //    if (IdUser == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {UserId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var L_VMUser_WRole = new List<VM_User_WithRoles>();
        //    foreach (var IdRole in _roleMan_IDRole.Roles)
        //    {
        //        var vmUser_WithRole = new VM_User_WithRoles()
        //        {
        //            RoleId = IdRole.Id,
        //            RoleName = IdRole.Name
        //        };
        //        if (await _userMan_IDUserExt.IsInRoleAsync(IdUser, IdRole.Name))
        //            vmUser_WithRole.IsSelected = true;
        //        else
        //            vmUser_WithRole.IsSelected = false;
        //        L_VMUser_WRole.Add(vmUser_WithRole);
        //    }
        //    return View(L_VMUser_WRole);
        //}

        //
        // GET: /Admin/ManageUserRoles
        [HttpGet]
        // Use the custom authoziation requirement and handler
        // Use it here instead of using it with EditRole action
        [CustomAuthorize(Policy = "EditPolicy_CLAIM")]
        public async Task<IActionResult> ManageUserRoles(string UserId)
        {
            ViewBag.UId = UserId;
            var IdUser = await _userMan_IDUserExt.FindByIdAsync(UserId);
            if (IdUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {UserId}, can not be found";
                return View("RouteNotFound");
            }
            var L_VMUser_WRole = new List<VM_User_WithRoles>();
            foreach (var IdRole in _roleMan_IDRole.Roles)
            {
                var vmUser_WithRole = new VM_User_WithRoles()
                {
                    RoleId = IdRole.Id,
                    RoleName = IdRole.Name
                };
                if (await _userMan_IDUserExt.IsInRoleAsync(IdUser, IdRole.Name))
                    vmUser_WithRole.IsSelected = true;
                else
                    vmUser_WithRole.IsSelected = false;
                L_VMUser_WRole.Add(vmUser_WithRole);
            }
            return View(L_VMUser_WRole);
        }

        //// POST: /Admin/ManageUserRoles
        //[HttpPost]
        //public async Task<IActionResult> ManageUserRoles(List<VM_User_WithRoles> L_vmUserWithRoles, string UserId)
        //{
        //    var IdUser = await _userMan_IDUserExt.FindByIdAsync(UserId);
        //    if (IdUser == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {UserId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var L_RolesI = await _userMan_IDUserExt.GetRolesAsync(IdUser);
        //    var IdResult = await _userMan_IDUserExt.RemoveFromRolesAsync(IdUser, L_RolesI);
        //    if(!IdResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Can't remove the specified user from the named ROLES");
        //        return View(L_vmUserWithRoles);
        //    }
        //    IdResult = await _userMan_IDUserExt.AddToRolesAsync(IdUser,
        //                                        L_vmUserWithRoles.Where(x => x.IsSelected)
        //                                                         .Select(y => y.RoleName));
        //    if (!IdResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Can't add the selected ROLES to the user");
        //        return View(L_vmUserWithRoles);
        //    }
        //    return RedirectToAction("EditUsers", new { Id = UserId });
        //}

        // POST: /Admin/ManageUserRoles
        [HttpPost]
        // Use the custom authoziation requirement and handler
        // Use it here instead of using it with EditRole action
        [CustomAuthorize(Policy = "EditPolicy_CLAIM")]
        public async Task<IActionResult> ManageUserRoles(List<VM_User_WithRoles> L_vmUserWithRoles, string UserId)
        {
            var IdUser = await _userMan_IDUserExt.FindByIdAsync(UserId);
            if (IdUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {UserId}, can not be found";
                return View("RouteNotFound");
            }
            var L_RolesI = await _userMan_IDUserExt.GetRolesAsync(IdUser);
            var IdResult = await _userMan_IDUserExt.RemoveFromRolesAsync(IdUser, L_RolesI);
            if (!IdResult.Succeeded)
            {
                ModelState.AddModelError("", "Can't remove the specified user from the named ROLES");
                return View(L_vmUserWithRoles);
            }
            IdResult = await _userMan_IDUserExt.AddToRolesAsync(IdUser,
                                                L_vmUserWithRoles.Where(x => x.IsSelected)
                                                                 .Select(y => y.RoleName));
            if (!IdResult.Succeeded)
            {
                ModelState.AddModelError("", "Can't add the selected ROLES to the user");
                return View(L_vmUserWithRoles);
            }
            return RedirectToAction("EditUsers", new { Id = UserId });
        }

        ////
        //// GET: /Admin/ManageUserClaims
        //[HttpGet]
        //public async Task<IActionResult> ManageUserClaims(string UserId)
        //{
        //    var IdUser = await _userMan_IDUserExt.FindByIdAsync(UserId);
        //    if (IdUser == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {UserId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var L_UserClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
        //    var vmUserClaims = new VM_UserClaims
        //    {
        //        UserId = UserId
        //    };
        //    foreach(Claim cm in ClaimsStore.L_Claims)
        //    {
        //        UserClaim UClaim = new UserClaim()
        //        {
        //            ClaimType = cm.Type,
        //            ClaimValue = cm.Value
        //        };
        //        // To check or uncheck claims checkbox on UI
        //        if (L_UserClaimsI.Any(c => c.Type == cm.Type))
        //            UClaim.IsSelected = true;
        //        vmUserClaims.L_UClaims.Add(UClaim);
        //    }
        //    return View(vmUserClaims);
        //}

        ////
        //// GET: /Admin/ManageUserClaims
        //// Selecting the IsSelected Claims
        //[HttpGet]
        //public async Task<IActionResult> ManageUserClaims(string UserId)
        //{
        //    var IdUser = await _userMan_IDUserExt.FindByIdAsync(UserId);
        //    if (IdUser == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {UserId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var L_UserClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
        //    var vmUserClaims = new VM_UserClaims
        //    {
        //        UserId = UserId
        //    };
        //    foreach (Claim cm in ClaimsStore.L_Claims)
        //    {
        //        UserClaim UClaim = new UserClaim()
        //        {
        //            ClaimType = cm.Type,
        //            ClaimValue = cm.Value
        //        };
        //        // To check or uncheck claims checkbox on UI
        //        if (L_UserClaimsI.Any(c => c.Type == cm.Value && c.Value == "true"))
        //            UClaim.IsSelected = true;
        //        vmUserClaims.L_UClaims.Add(UClaim);
        //    }
        //    return View(vmUserClaims);
        //}

        //
        // GET: /Admin/ManageUserClaims
        [HttpGet]
        // Use the custom authoziation requirement and handler
        // Use it here instead of using it with EditRole action
        [CustomAuthorize(Policy = "EditPolicy_CLAIM")]
        public async Task<IActionResult> ManageUserClaims(string UserId)
        {
            var IdUser = await _userMan_IDUserExt.FindByIdAsync(UserId);
            if (IdUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {UserId}, can not be found";
                return View("RouteNotFound");
            }
            var L_UserClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
            var vmUserClaims = new VM_UserClaims
            {
                UserId = UserId
            };
            foreach (Claim cm in ClaimsStore.L_Claims)
            {
                UserClaim UClaim = new UserClaim()
                {
                    ClaimType = cm.Type,
                    ClaimValue = cm.Value
                };
                // To check or uncheck claims checkbox on UI
                if (L_UserClaimsI.Any(c => c.Type == cm.Value && c.Value == "true"))
                    UClaim.IsSelected = true;
                vmUserClaims.L_UClaims.Add(UClaim);
            }
            return View(vmUserClaims);
        }

        //// POST: /Admin/ManageUserClaims
        //[HttpPost]
        //public async Task<IActionResult> ManageUserClaims(VM_UserClaims vmUserClaims)
        //{
        //    var IdUser = await _userMan_IDUserExt.FindByIdAsync(vmUserClaims.UserId);
        //    if (IdUser == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {vmUserClaims.UserId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var L_ClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
        //    var IdResult = await _userMan_IDUserExt.RemoveClaimsAsync(IdUser, L_ClaimsI);
        //    if (!IdResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Can't remove the specified existing CLAIMS from the user");
        //        return View(vmUserClaims);
        //    }
        //    IdResult = await _userMan_IDUserExt.AddClaimsAsync(IdUser,
        //                        vmUserClaims.L_UClaims
        //                                    .Where(c => c.IsSelected)
        //                                    .Select(c => new Claim(c.ClaimType, c.ClaimValue)));
        //    if (!IdResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Can't add the selected CLAIMS to the user");
        //        return View(vmUserClaims);
        //    }
        //    return RedirectToAction("EditUsers", new { Id = vmUserClaims.UserId });
        //}

        //// POST: /Admin/ManageUserClaims
        //// Adding CLAIMs Type and Value
        //[HttpPost]
        //public async Task<IActionResult> ManageUserClaims(VM_UserClaims vmUserClaims)
        //{
        //    var IdUser = await _userMan_IDUserExt.FindByIdAsync(vmUserClaims.UserId);
        //    if (IdUser == null)
        //    {
        //        ViewBag.ErrorMessage = $"User with Id = {vmUserClaims.UserId}, can not be found";
        //        return View("RouteNotFound");
        //    }
        //    var L_ClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
        //    var IdResult = await _userMan_IDUserExt.RemoveClaimsAsync(IdUser, L_ClaimsI);
        //    if (!IdResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Can't remove the specified existing CLAIMS from the user");
        //        return View(vmUserClaims);
        //    }
        //    IdResult = await _userMan_IDUserExt.AddClaimsAsync(IdUser,
        //                        vmUserClaims.L_UClaims
        //                                    .Select(c => new Claim(c.ClaimValue, c.IsSelected ? "true" : "false")));
        //    if (!IdResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Can't add the selected CLAIMS to the user");
        //        return View(vmUserClaims);
        //    }
        //    return RedirectToAction("EditUsers", new { Id = vmUserClaims.UserId });
        //}

        // POST: /Admin/ManageUserClaims
        [HttpPost]
        // Use the custom authoziation requirement and handler
        // Use it here instead of using it with EditRole action
        [CustomAuthorize(Policy = "EditPolicy_CLAIM")]
        public async Task<IActionResult> ManageUserClaims(VM_UserClaims vmUserClaims)
        {
            var IdUser = await _userMan_IDUserExt.FindByIdAsync(vmUserClaims.UserId);
            if (IdUser == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {vmUserClaims.UserId}, can not be found";
                return View("RouteNotFound");
            }
            var L_ClaimsI = await _userMan_IDUserExt.GetClaimsAsync(IdUser);
            var IdResult = await _userMan_IDUserExt.RemoveClaimsAsync(IdUser, L_ClaimsI);
            if (!IdResult.Succeeded)
            {
                ModelState.AddModelError("", "Can't remove the specified existing CLAIMS from the user");
                return View(vmUserClaims);
            }
            IdResult = await _userMan_IDUserExt.AddClaimsAsync(IdUser,
                                vmUserClaims.L_UClaims
                                            .Select(c => new Claim(c.ClaimValue, c.IsSelected ? "true" : "false")));
            if (!IdResult.Succeeded)
            {
                ModelState.AddModelError("", "Can't add the selected CLAIMS to the user");
                return View(vmUserClaims);
            }
            return RedirectToAction("EditUsers", new { Id = vmUserClaims.UserId });
        }

        //
        // GET: ParthyTech/Admin/Ad_AccessDenied
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Ad_AccessDenied()
        {
            return View();
        }
    }
}
