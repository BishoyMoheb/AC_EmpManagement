using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;//To use ClaimsPrincipal & ClaimTypes
using System.Threading.Tasks;
using AC_EmpManagement.Models;//To use IdUserExtension
using AC_EmpManagement.ViewModels;//To use VM_Register
using Microsoft.AspNetCore.Authorization;//To use AllowAnonymous
using Microsoft.AspNetCore.Identity;//To use IdentityUser, SignInManager, UserManager
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;//To use ILogger

namespace AC_EmpManagement.Controllers
{
    //[AllowAnonymous]
    //public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    //{
    //    private readonly UserManager<IdentityUser> _userM_ID;
    //    private readonly SignInManager<IdentityUser> _signInM_ID;

    //    // Injecting the required identity classes
    //    public AccountController(UserManager<IdentityUser> UserM_ID,
    //                             SignInManager<IdentityUser> SignInM_ID)
    //    {
    //        _userM_ID = UserM_ID;
    //        _signInM_ID = SignInM_ID;
    //    }

    //    //
    //    // GET: ParthyTech/Account/Register
    //    [HttpGet]
    //    public IActionResult Register()
    //    {
    //        return View();
    //    }

    //    // POST: ParthyTech/Account/Register
    //    [HttpPost]
    //    public async Task<IActionResult> Register(VM_Register vmReg)
    //    {
    //        if(ModelState.IsValid)
    //        {
    //            var NewUser = new IdentityUser { UserName = vmReg.Email, Email = vmReg.Email };
    //            var IsCreated = await _userM_ID.CreateAsync(NewUser, vmReg.Password);
    //            if(IsCreated.Succeeded)
    //            {
    //                await _signInM_ID.SignInAsync(NewUser, isPersistent: false);
    //                return RedirectToAction("Index", "Home");
    //            }
    //            foreach(var Error in IsCreated.Errors)
    //            {
    //                ModelState.AddModelError("", Error.Description);
    //            }
    //        }
    //        return View(vmReg);
    //    }

    //    //
    //    // POST: ParthyTech/Account/Logout
    //    [HttpPost]
    //    public async Task<IActionResult> Logout()
    //    {
    //        await _signInM_ID.SignOutAsync();
    //        return RedirectToAction("Index", "Home");
    //    }

    //    //
    //    // GET: ParthyTech/Account/Login
    //    [HttpGet]
    //    public IActionResult Login()
    //    {
    //        return View();
    //    }

    //    //// POST: ParthyTech/Account/Login
    //    //[HttpPost]
    //    //public async Task<IActionResult> Login(VM_Login vmLog)
    //    //{
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
    //    //                                                              vmLog.RememberMe, false);
    //    //        if (IsSignedIN.Succeeded)
    //    //        {
    //    //            return RedirectToAction("Index", "Home");
    //    //        }
    //    //        ModelState.AddModelError(string.Empty, "Invalid login attempts");
    //    //    }
    //    //    return View(vmLog);
    //    //}

    //    //// POST: ParthyTech/Account/Login
    //    //// Redirect the user to the requested page after successful login
    //    //[HttpPost]
    //    //public async Task<IActionResult> Login(VM_Login vmLog, string returnUrl)
    //    //{
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
    //    //                                                              vmLog.RememberMe, false);
    //    //        if (IsSignedIN.Succeeded)
    //    //        {
    //    //            if (!string.IsNullOrEmpty(returnUrl))
    //    //                return Redirect(returnUrl);
    //    //            return RedirectToAction("Index", "Home");
    //    //        }
    //    //        ModelState.AddModelError(string.Empty, "Invalid login attempts");
    //    //    }
    //    //    return View(vmLog);
    //    //}

    //    //// POST: ParthyTech/Account/Login
    //    //// Prevent open redirect vulnerability by throwing exception
    //    //[HttpPost]
    //    //public async Task<IActionResult> Login(VM_Login vmLog, string returnUrl)
    //    //{
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
    //    //                                                              vmLog.RememberMe, false);
    //    //        if (IsSignedIN.Succeeded)
    //    //        {
    //    //            if (!string.IsNullOrEmpty(returnUrl))
    //    //                return LocalRedirect(returnUrl);
    //    //            return RedirectToAction("Index", "Home");
    //    //        }
    //    //        ModelState.AddModelError(string.Empty, "Invalid login attempts");
    //    //    }
    //    //    return View(vmLog);
    //    //}

    //    // POST: ParthyTech/Account/Login
    //    // Prevent open redirect vulnerability using Url.IsLocalUrl
    //    [HttpPost]
    //    public async Task<IActionResult> Login(VM_Login vmLog, string returnUrl)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
    //                                                                  vmLog.RememberMe, false);
    //            if (IsSignedIN.Succeeded)
    //            {
    //                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
    //                    return Redirect(returnUrl);
    //                return RedirectToAction("Index", "Home");
    //            }
    //            ModelState.AddModelError(string.Empty, "Invalid login attempts");
    //        }
    //        return View(vmLog);
    //    }


    //    //[HttpGet]
    //    //[HttpPost]
    //    // Combine them as 
    //    [AcceptVerbs("Get", "Post")]
    //    [AllowAnonymous]
    //    public async Task<IActionResult> IsEmail_Exists(string Email)
    //    {
    //        var Check_Email = await _userM_ID.FindByEmailAsync(Email);
    //        if (Check_Email == null)
    //            return Json(true);
    //        else
    //            return Json($"The Email {Email} is already exists");
    //    }
    //}

    [AllowAnonymous]
    // Extending IdentityUser with IdUserExtension
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        //private readonly UserManager<IdUserExtension> _userM_ID;
        //private readonly SignInManager<IdUserExtension> _signInM_ID;

        //// Injecting the extended IdUserExtension class from IdentityUser classes
        //public AccountController(UserManager<IdUserExtension> UserM_ID,
        //                         SignInManager<IdUserExtension> SignInM_ID)
        //{
        //    _userM_ID = UserM_ID;
        //    _signInM_ID = SignInM_ID;
        //}

        private readonly UserManager<IdUserExtension> _userM_ID;
        private readonly SignInManager<IdUserExtension> _signInM_ID;
        private readonly ILogger<AccountController> _logI_Account;

        // Injecting the built-in ILogger service
        public AccountController(UserManager<IdUserExtension> UserM_ID,
                                 SignInManager<IdUserExtension> SignInM_ID,
                                 ILogger<AccountController> LogI_Account)
        {
            _userM_ID = UserM_ID;
            _signInM_ID = SignInM_ID;
            _logI_Account = LogI_Account;
        }

        //
        // GET: ParthyTech/Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //// POST: ParthyTech/Account/Register
        //[HttpPost]
        //// Extending IdUserExtension class from IdentityUser classes
        //public async Task<IActionResult> Register(VM_Register vmReg)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var NewUser = new IdUserExtension
        //        {
        //            UserName = vmReg.Email,
        //            Email = vmReg.Email,
        //            City = vmReg.City
        //        };
        //        var IsCreated = await _userM_ID.CreateAsync(NewUser, vmReg.Password);
        //        if(IsCreated.Succeeded)
        //        {
        //            await _signInM_ID.SignInAsync(NewUser, isPersistent: false);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        foreach(var Error in IsCreated.Errors)
        //        {
        //            ModelState.AddModelError("", Error.Description);
        //        }
        //    }
        //    return View(vmReg);
        //}

        //// POST: ParthyTech/Account/Register
        //[HttpPost]
        //// Admin Users can register another users and still looged in
        //public async Task<IActionResult> Register(VM_Register vmReg)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var NewUser = new IdUserExtension
        //        {
        //            UserName = vmReg.Email,
        //            Email = vmReg.Email,
        //            City = vmReg.City
        //        };
        //        var IsCreated = await _userM_ID.CreateAsync(NewUser, vmReg.Password);
        //        if (IsCreated.Succeeded)
        //        {
        //            // Making the admin user still logged in after registering new user
        //            if (_signInM_ID.IsSignedIn(User) && User.IsInRole("Admin_Role"))
        //                return RedirectToAction("UsersList", "Admin");
        //            await _signInM_ID.SignInAsync(NewUser, isPersistent: false);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        foreach (var Error in IsCreated.Errors)
        //        {
        //            ModelState.AddModelError("", Error.Description);
        //        }
        //    }
        //    return View(vmReg);
        //}

        // POST: ParthyTech/Account/Register
        [HttpPost]
        // Registeration and Email Confirmation
        public async Task<IActionResult> Register(VM_Register vmReg)
        {
            if (ModelState.IsValid)
            {
                var NewUser = new IdUserExtension
                {
                    UserName = vmReg.Email,
                    Email = vmReg.Email,
                    City = vmReg.City
                };
                var IsCreated = await _userM_ID.CreateAsync(NewUser, vmReg.Password);
                if (IsCreated.Succeeded)
                {
                    // Getting Email Confirmation properties and log the ConfirmationLink to a file 
                    var EmailConf_token = await _userM_ID.GenerateEmailConfirmationTokenAsync(NewUser);
                    var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
                                         new { UserId = NewUser.Id, ECToken = EmailConf_token }, Request.Scheme);
                    _logI_Account.Log(LogLevel.Warning, ConfirmationLink);
                    if (_signInM_ID.IsSignedIn(User) && User.IsInRole("Admin_Role"))
                        return RedirectToAction("UsersList", "Admin");
                    ViewBag.ErrorTitle = "Registration Successful";
                    ViewBag.ErrorMessage = "First, confirm your Email by clicking on the confirmation link " +
                        "that was emailed to you and then login";
                    return View("GlobalErrorHandled");
                }
                foreach (var Error in IsCreated.Errors)
                {
                    ModelState.AddModelError("", Error.Description);
                }
            }
            return View(vmReg);
        }


        //
        // GET: ParthyTech/Account/ConfirmEmail?UserId=&ECToken=
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string UserId, string ECToken)
        {
            if (UserId == null || ECToken == null)
                return RedirectToAction("Index", "Home");
            var IdUExt = await _userM_ID.FindByIdAsync(UserId);
            if(IdUExt==null)
            {
                ViewBag.ErrorMessage = $"The user ID {UserId} is invalid";
                return View("RouteNotFound");
            }
            var IdResult = await _userM_ID.ConfirmEmailAsync(IdUExt, ECToken);
            if (!IdResult.Succeeded)
            {
                ViewBag.ErrorTitle = "Email Confirmation Error";
                ViewBag.ErrorMessage = "The given Email can not be confirmed";
                return View("GlobalErrorHandled");
            }
            return View();
        }

        //
        // POST: ParthyTech/Account/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInM_ID.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        ////
        //// GET: ParthyTech/Account/Login
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //
        // GET: ParthyTech/Account/Login
        // For External LogIN
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            VM_Login vmLog = new VM_Login()
            {
                ReturnURL = returnUrl,
                L_AS_ExternalLoginI = (await _signInM_ID.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(vmLog);
        }

        //// POST: ParthyTech/Account/Login
        //// Prevent open redirect vulnerability using Url.IsLocalUrl
        //[HttpPost]
        //public async Task<IActionResult> Login(VM_Login vmLog, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
        //                                                              vmLog.RememberMe, false);
        //        if (IsSignedIN.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //                return Redirect(returnUrl);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError(string.Empty, "Invalid login attempts");
        //    }
        //    return View(vmLog);
        //}

        //// POST: ParthyTech/Account/Login
        //[HttpPost]
        //// Fixing the null exception for vmLog.L_AS_ExternalLoginI
        //public async Task<IActionResult> Login(VM_Login vmLog, string returnUrl)
        //{

        //    vmLog.L_AS_ExternalLoginI = (await _signInM_ID.GetExternalAuthenticationSchemesAsync()).ToList();
        //    if (ModelState.IsValid)
        //    {
        //        // Check the Email is confirmed or not
        //        // Check that the provided username and password combination is correct
        //        var IdUExt = await _userM_ID.FindByEmailAsync(vmLog.Email);
        //        if (IdUExt != null && !IdUExt.EmailConfirmed
        //           && (await _userM_ID.CheckPasswordAsync(IdUExt, vmLog.Password)))
        //        {
        //            ModelState.AddModelError(string.Empty, "The Email is not confirmed");
        //            return View(vmLog);
        //        }
        //        var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
        //                                                              vmLog.RememberMe, false);
        //        if (IsSignedIN.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //                return Redirect(returnUrl);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError(string.Empty, "Invalid login attempts");
        //    }
        //    return View(vmLog);
        //}

        //// POST: ParthyTech/Account/Login
        //[HttpPost]
        //// Confirm the Email before login process
        //public async Task<IActionResult> Login(VM_Login vmLog, string returnUrl)
        //{

        //    vmLog.L_AS_ExternalLoginI = (await _signInM_ID.GetExternalAuthenticationSchemesAsync()).ToList();
        //    if (ModelState.IsValid)
        //    {
        //        // Check the Email is confirmed or not
        //        // Check that the provided username and password combination is correct
        //        var IdUExt = await _userM_ID.FindByEmailAsync(vmLog.Email);
        //        if (IdUExt != null && !IdUExt.EmailConfirmed
        //           && (await _userM_ID.CheckPasswordAsync(IdUExt, vmLog.Password)))
        //        {
        //            ModelState.AddModelError(string.Empty, "The Email is not confirmed");
        //            return View(vmLog);
        //        }
        //        var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
        //                                                              vmLog.RememberMe, false);
        //        if (IsSignedIN.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //                return Redirect(returnUrl);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        ModelState.AddModelError(string.Empty, "Invalid login attempts");
        //    }
        //    return View(vmLog);
        //}

        // POST: ParthyTech/Account/Login
        [HttpPost]
        // Setting lockoutOnFailure to true
        public async Task<IActionResult> Login(VM_Login vmLog, string returnUrl)
        {

            vmLog.L_AS_ExternalLoginI = (await _signInM_ID.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // Check the Email is confirmed or not
                // Check that the provided username and password combination is correct
                var IdUExt = await _userM_ID.FindByEmailAsync(vmLog.Email);
                if (IdUExt != null && !IdUExt.EmailConfirmed
                   && (await _userM_ID.CheckPasswordAsync(IdUExt, vmLog.Password)))
                {
                    ModelState.AddModelError(string.Empty, "The Email is not confirmed");
                    return View(vmLog);
                }
                var IsSignedIN = await _signInM_ID.PasswordSignInAsync(vmLog.Email, vmLog.Password,
                                                                      vmLog.RememberMe, true);
                if (IsSignedIN.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                if (IsSignedIN.IsLockedOut)
                    return View("AccountLockedOut");
                ModelState.AddModelError(string.Empty, "Invalid login attempts");
            }
            return View(vmLog);
        }


        //[HttpGet]
        //[HttpPost]
        // Combine them as 
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmail_Exists(string Email)
        {
            var Check_Email = await _userM_ID.FindByEmailAsync(Email);
            if (Check_Email == null)
                return Json(true);
            else
                return Json($"The Email {Email} is already exists");
        }

        //
        // GET: ParthyTech/Account/AccessDenied
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string Email)
        {
            return View();
        }

        //
        // GET: ParthyTech/Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: ParthyTech/Account/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(VM_ForgotPassword vmFPassword)
        {
            if (ModelState.IsValid)
            {
                var IdUserEx = await _userM_ID.FindByEmailAsync(vmFPassword.Email);
                if (IdUserEx != null && await _userM_ID.IsEmailConfirmedAsync(IdUserEx))
                {
                    var PResetToken = await _userM_ID.GeneratePasswordResetTokenAsync(IdUserEx);
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                                                new { UserEmail = vmFPassword.Email, PRToken = PResetToken });
                    _logI_Account.Log(LogLevel.Warning, passwordResetLink);
                    return View("ForgotPConfirmation");
                }
                return View("ForgotPConfirmation");
            }
            return View(vmFPassword);
        }


        //
        // GET: ParthyTech/Account/ForgotPConfirmation
        [HttpGet]
        public IActionResult ForgotPConfirmation()
        {
            return View();
        }


        //
        // GET: ParthyTech/Account/ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string UserEmail, string PRToken)
        {
            if (UserEmail == null || PRToken == null)
                ModelState.AddModelError("", "Invalid password reset token.");
            return View();
        }

        //// POST: ParthyTech/Account/ResetPassword
        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(VM_ResetPassword vmRPassword)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var IdUserEx = await _userM_ID.FindByEmailAsync(vmRPassword.UserEmail);
        //        if (IdUserEx != null)
        //        {
        //            var IdResult = await _userM_ID.ResetPasswordAsync(IdUserEx, vmRPassword.PRToken, vmRPassword.Password);
        //            if (IdResult.Succeeded)
        //                return View("ResetPasswordConfirmed");
        //            foreach (var IdError in IdResult.Errors)
        //            {
        //                ModelState.AddModelError("", IdError.Description);
        //            }
        //            return View(vmRPassword);
        //        }
        //        // The following line are more suitable for case IdUserEx is null 
        //        ModelState.AddModelError("", "Password reset token is invalid.");
        //        return View(vmRPassword); //return View("ResetPasswordConfirmed");
        //    }
        //    return View(vmRPassword);
        //}

        // POST: ParthyTech/Account/ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(VM_ResetPassword vmRPassword)
        {
            if (ModelState.IsValid)
            {
                var IdUserEx = await _userM_ID.FindByEmailAsync(vmRPassword.UserEmail);
                if (IdUserEx != null)
                {
                    var IdResult = await _userM_ID.ResetPasswordAsync(IdUserEx, vmRPassword.PRToken, vmRPassword.Password);
                    if (IdResult.Succeeded)
                    {
                        // Case resetting the password for a locked out user
                        if(await _userM_ID.IsLockedOutAsync(IdUserEx))
                            await _userM_ID.SetLockoutEndDateAsync(IdUserEx, DateTimeOffset.UtcNow);
                        return View("ResetPasswordConfirmed");
                    }
                    foreach (var IdError in IdResult.Errors)
                    {
                        ModelState.AddModelError("", IdError.Description);
                    }
                    return View(vmRPassword);
                }
                // The following line are more suitable for case IdUserEx is null 
                ModelState.AddModelError("", "Password reset token is invalid.");
                return View(vmRPassword); //return View("ResetPasswordConfirmed");
            }
            return View(vmRPassword);
        }


        //
        // GET: ParthyTech/Account/ResetPasswordConfirmed
        [HttpGet]
        public IActionResult ResetPasswordConfirmed()
        {
            return View();
        }


        ////
        //// GET: ParthyTech/Account/ChangePassword
        //[HttpGet]
        //[CustomAuthorize]
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}

        //
        // GET: ParthyTech/Account/ChangePassword
        [HttpGet]
        [CustomAuthorize]
        public async Task<IActionResult> ChangePassword()
        {
            var IdUserEx = await _userM_ID.GetUserAsync(User);
            var userHasPassword = await _userM_ID.HasPasswordAsync(IdUserEx);
            if (!userHasPassword)
                return RedirectToAction("AddPassLocally_ExLogin");
            return View();
        }

        // POST: ParthyTech/Account/ChangePassword
        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> ChangePassword(VM_ChangePassword vmChangePassword)
        {
            if (ModelState.IsValid)
            {
                var IdUserEx = await _userM_ID.GetUserAsync(User);
                if (IdUserEx == null)
                    return RedirectToAction("Login");
                var IdResult = await _userM_ID.ChangePasswordAsync(IdUserEx,
                                               vmChangePassword.CurrentPassword,
                                               vmChangePassword.NewPassword);
                if (!IdResult.Succeeded)
                {
                    foreach (var IdError in IdResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, IdError.Description);
                    }
                    return View();
                }
                await _signInM_ID.RefreshSignInAsync(IdUserEx);
                return View("ChangePasswordConfirmed");
            }
            return View(vmChangePassword);
        }


        //
        // GET: ParthyTech/Account/AddPassLocally_ExLogin
        [HttpGet]
        [CustomAuthorize]
        public async Task< IActionResult> AddPassLocally_ExLogin()
        {
            var IdUserEx = await _userM_ID.GetUserAsync(User);
            var userHasPassword = await _userM_ID.HasPasswordAsync(IdUserEx);
            if (userHasPassword)
                return RedirectToAction("ChangePassword");
            return View();
        }

        // POST: ParthyTech/Account/AddPassLocally_ExLogin
        [HttpPost]
        [CustomAuthorize]
        public async Task<IActionResult> AddPassLocally_ExLogin(VM_AddPassLocally_ExLogin vmAddPass)
        {
            var IdUserEx = await _userM_ID.GetUserAsync(User);
            var IdResult = await _userM_ID.AddPasswordAsync(IdUserEx, vmAddPass.NewPassword);
            if(!IdResult.Succeeded)
            {
                foreach (var IdError in IdResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, IdError.Description);
                }
                return View();
            }
            await _signInM_ID.RefreshSignInAsync(IdUserEx);
            return View("AddPL_ExLoginConfirmed");
        }


        //
        // GET: ParthyTech/Account/ChangePasswordConfirmed
        [HttpGet]
        public IActionResult ChangePasswordConfirmed()
        {
            return View();
        }


        //
        // GET: ParthyTech/Account/AddPL_ExLoginConfirmed
        [HttpGet]
        public IActionResult AddPL_ExLoginConfirmed()
        {
            return View();
        }


        //
        // GET: ParthyTech/Account/AccountLockedOut
        [HttpGet]
        public IActionResult AccountLockedOut()
        {
            return View();
        }

        //
        // POST: ParthyTech/Account/External_LogIN
        // External LogIN (Google, Facebook, ect..)
        [HttpPost]
        public IActionResult External_LogIN(string loginProvider, string returnUrl)
        {
            var redirect_URL = Url.Action("After_ExternalLogIN", "Account", new { ReturnUrl = returnUrl });
            var AuthProperties = _signInM_ID.ConfigureExternalAuthenticationProperties(loginProvider, redirect_URL);
            return new ChallengeResult(loginProvider, AuthProperties);
        }


        ////
        //// GET: ParthyTech/Account/After_ExternalLogIN
        //[HttpGet]
        //public async Task<IActionResult> After_ExternalLogIN(string returnUrl=null, string remoteERROR=null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");
        //    VM_Login vmLogin = new VM_Login
        //    {
        //        ReturnURL = returnUrl,
        //        L_AS_ExternalLoginI = (await _signInM_ID.GetExternalAuthenticationSchemesAsync()).ToList()
        //    };
        //    if (remoteERROR != null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error from external provider : {remoteERROR}");
        //        return View("Login", vmLogin);
        //    }
        //    var ExLogInfo = await _signInM_ID.GetExternalLoginInfoAsync();
        //    if (ExLogInfo == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Error loading external Login information");
        //        return View("Login", vmLogin);
        //    }
        //    var SInResult = await _signInM_ID.ExternalLoginSignInAsync(ExLogInfo.LoginProvider,
        //                            ExLogInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //    if (SInResult.Succeeded)
        //        return LocalRedirect(returnUrl);
        //    else
        //    {
        //        var EmailResult = ExLogInfo.Principal.FindFirstValue(ClaimTypes.Email);
        //        if (EmailResult != null)
        //        {
        //            var IDUserEx_Result = await _userM_ID.FindByEmailAsync(EmailResult);
        //            if (IDUserEx_Result == null)
        //            {
        //                IDUserEx_Result = new IdUserExtension
        //                {
        //                    UserName = ExLogInfo.Principal.FindFirstValue(ClaimTypes.Email),
        //                    Email = ExLogInfo.Principal.FindFirstValue(ClaimTypes.Email)
        //                };
        //                await _userM_ID.CreateAsync(IDUserEx_Result);
        //            }
        //            await _userM_ID.AddLoginAsync(IDUserEx_Result, ExLogInfo);
        //            await _signInM_ID.SignInAsync(IDUserEx_Result, isPersistent: false);
        //            return LocalRedirect(returnUrl);
        //        }
        //    }
        //    ViewBag.ErrorTitle = $"Eamil claim not recieved from : {ExLogInfo.LoginProvider}";
        //    ViewBag.ErrorMessage = "Please contact for support on IT@parthy.com";
        //    return View("GlobalErrorHandled");
        //}

        //
        // GET: ParthyTech/Account/After_ExternalLogIN
        [HttpGet]
        // Confirm the Email with External LogIN
        public async Task<IActionResult> After_ExternalLogIN(string returnUrl = null, string remoteERROR = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            VM_Login vmLogin = new VM_Login
            {
                ReturnURL = returnUrl,
                L_AS_ExternalLoginI = (await _signInM_ID.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteERROR != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider : {remoteERROR}");
                return View("Login", vmLogin);
            }
            var ExLogInfo = await _signInM_ID.GetExternalLoginInfoAsync();
            if (ExLogInfo == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external Login information");
                return View("Login", vmLogin);
            }
            var EmailResult = ExLogInfo.Principal.FindFirstValue(ClaimTypes.Email);
            IdUserExtension IDUserEx_Result = null;
            if (EmailResult != null)
            {
                IDUserEx_Result = await _userM_ID.FindByEmailAsync(EmailResult);
                if (IDUserEx_Result != null && !IDUserEx_Result.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "The Email is not confirmed");
                    return View("Login", vmLogin);
                }
            }
            var SInResult = await _signInM_ID.ExternalLoginSignInAsync(ExLogInfo.LoginProvider,
                                    ExLogInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (SInResult.Succeeded)
                return LocalRedirect(returnUrl);
            else
            {
                if (EmailResult != null)
                {
                    if (IDUserEx_Result == null)
                    {
                        IDUserEx_Result = new IdUserExtension
                        {
                            UserName = ExLogInfo.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = ExLogInfo.Principal.FindFirstValue(ClaimTypes.Email)
                        };
                        await _userM_ID.CreateAsync(IDUserEx_Result);
                        // EmailConfirmed value changes here
                        var EmailConf_token = await _userM_ID.GenerateEmailConfirmationTokenAsync(IDUserEx_Result);
                        var ConfirmationLink = Url.Action("ConfirmEmail", "Account",
                                             new { UserId = IDUserEx_Result.Id, ECToken = EmailConf_token }, Request.Scheme);
                        _logI_Account.Log(LogLevel.Warning, ConfirmationLink);
                        ViewBag.ErrorTitle = "Registration Successful";
                        ViewBag.ErrorMessage = "First, confirm your Email by clicking on the confirmation link " +
                            "that was emailed to you and then login";
                        return View("GlobalErrorHandled");
                    }
                    await _userM_ID.AddLoginAsync(IDUserEx_Result, ExLogInfo);
                    await _signInM_ID.SignInAsync(IDUserEx_Result, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
            }
            ViewBag.ErrorTitle = $"Eamil claim not recieved from : {ExLogInfo.LoginProvider}";
            ViewBag.ErrorMessage = "Please contact for support on IT@parthy.com";
            return View("GlobalErrorHandled");
        }
    }
}
