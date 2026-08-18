using AC_EmpManagement.Models;
using AC_EmpManagement.Security;//To use DataProtection_Strings
using AC_EmpManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;//To use Authorize attribute
using Microsoft.AspNetCore.DataProtection;//To use IDataProtector
using Microsoft.AspNetCore.Hosting;//To use IHostingEnvironment
using Microsoft.AspNetCore.Http;//To use IFormFile
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;//To use LogLevel
using System;
using System.Collections.Generic;
using System.IO;//To use Path
using System.Linq;
using System.Threading.Tasks;


namespace AC_EmpManagement.Controller
{
    //public class HomeController

    //// Using route attribute on controller
    //[Route("Home")]

    // Using token replacement
    //[Route("[controller]")]
    //[Route("[controller]/[action]")]
    [Route("ParthyTech/[controller]/[action]")]
    //[CustomAuthorizeAttribute]
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        //public string Index()
        //{
        //    return "Hello from MVC.";
        //}

        //// Using Json
        //public JsonResult Index()
        //{
        //    return Json(new { Id = 8084, Name = "Bishoy" });
        //}

        
        // Dependency Injection with contructor
        //private IEmpRepository _empResI;
        private readonly IEmpRepository _empResI;
        private readonly IHostingEnvironment _hEnvI;
        private readonly ILogger _loggerI;

        // Adding IDataProtector 
        private readonly IDataProtector _dataProtectI;

        //public HomeController(IEmpRepository EmpRepositoryI)
        //{
        //    _empResI = EmpRepositoryI;
        //}

        //public HomeController(IEmpRepository EmpRepositoryI,
        //                      IHostingEnvironment HEnvI)
        //{
        //    _empResI = EmpRepositoryI;
        //    _hEnvI = HEnvI;
        //}

        //public HomeController(IEmpRepository EmpRepositoryI,
        //                      IHostingEnvironment HEnvI,
        //                      ILogger<HomeController> LoggerI)
        //{
        //    _empResI = EmpRepositoryI;
        //    _hEnvI = HEnvI;
        //    _loggerI = LoggerI;
        //}

        public HomeController(IEmpRepository EmpRepositoryI,
                              IHostingEnvironment HEnvI,
                              ILogger<HomeController> LoggerI,
                              IDataProtectionProvider DataPProviderI,
                              DataProtection_Strings DProtector_Strings)
        {
            _empResI = EmpRepositoryI;
            _hEnvI = HEnvI;
            _loggerI = LoggerI;
            _dataProtectI = DataPProviderI.CreateProtector(DProtector_Strings.EmpId_RouteValue);
        }

        //// Using Injection 
        //public string Index()
        //{
        //    return _empResI.GetEmployee(1).Name;
        //    //return _empResI.GetEmployee(1).Name + " " +
        //    //       _empResI.GetEmployee(1).Department + " " +
        //    //       _empResI.GetEmployee(1).Email;
        //}

        //// Using List View
        //public ViewResult Index()
        //{
        //    var All_Emp = _empResI.Get_ALL();
        //    return View(All_Emp);
        //}

        //// Using attribute route
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //public ViewResult Index()
        //{
        //    var All_Emp = _empResI.Get_ALL();
        //    return View(All_Emp);
        //}

        //// Using attribute route on controller
        //[Route("")]
        //[Route("Index")]
        //[Route("/")] /* Equivalent to [Route("~/")] */
        //public ViewResult Index()
        //{
        //    var All_Emp = _empResI.Get_ALL();
        //    return View(All_Emp);
        //}

        ////// Using token replacement
        ////[Route("")]
        ////[Route("[action]")]
        ////[Route("/")] /* Equivalent to [Route("~/")] */
        ////// Setting the default value
        ////[Route("~/Home/Index")]
        ////[Route("~/Home")]
        ////[Route("~/")] /* Equivalent to [Route("/")] */
        //// Adding the company name ParthyTech
        //[Route("~/ParthyTech/Home/Index")]
        //[Route("~/ParthyTech/Home")]
        //[Route("~/ParthyTech")]
        //[Route("~/")] /* Equivalent to [Route("/")] */
        ////[AllowAnonymous]
        //public ViewResult Index()
        //{
        //    var All_Emp = _empResI.Get_ALL();
        //    return View(All_Emp);
        //}

        // Show Id as encrypted
        [Route("~/ParthyTech/Home/Index")]
        [Route("~/ParthyTech/Home")]
        [Route("~/ParthyTech")]
        [Route("~/")] /* Equivalent to [Route("/")] */
        //[AllowAnonymous]
        public ViewResult Index()
        {
            var All_Emp = _empResI.Get_ALL()
                                  .Select(e =>
                                          {
                                              e.EncryptedId = _dataProtectI.Protect(e.Id.ToString());
                                              return e;
                                          });
            return View(All_Emp);
        }


        //public JsonResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    return Json(MEmp);
        //}

        // Respect content negotiation
        //public ObjectResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    return new ObjectResult(MEmp);
        //}

        //// Using View
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    return View(MEmp);
        //}

        //// Specifying special view
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    return View("Test");
        //}

        //// To use specific Absolute path
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    /* Alternatives
        //     * return View("ViewsNew/TestNew.cshtml");
        //     * return View("/ViewsNew/TestNew.cshtml");
        //     */
        //    return View("~/ViewsNew/TestNew.cshtml");
        //}

        //// Relative path
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    //return View("../VRelative/RTest");
        //    // To reach TestNew view using relative path do the following
        //    return View("../../ViewsNew/TestNew");
        //}

        //// Using MVC Conventions
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    return View();
        //}

        //// Using ViewData to pass data
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    ViewData["EmpData"] = MEmp;
        //    ViewData["PageTitle"] = "Employee Details";
        //    return View();
        //}

        //// Using ViewBag to pass data
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    ViewBag.EmpData = MEmp;
        //    ViewBag.PageTitle = "Employee Details";
        //    return View();
        //}

        //// Using Strongly Typed to pass data
        //public ViewResult Details()
        //{
        //    MEmployee MEmp = _empResI.GetEmployee(1);
        //    ViewBag.PageTitle = "Employee Details";
        //    return View(MEmp);
        //}

        //// Using ViewModel "VM_HomeDetails"
        //public ViewResult Details()
        //{
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = _empResI.GetEmployee(1),
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Dynamic IDs
        //public ViewResult Details(int ID)
        //{
        //    if (ID == 0)
        //        ID = 1;
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = _empResI.GetEmployee(ID),
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Using attribute route
        //[Route("Home/Details/{ID?}")]
        //public ViewResult Details(int ID)
        //{
        //    if (ID == 0)
        //        ID = 1;
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = _empResI.GetEmployee(ID),
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Using attribute route
        //// Allowing nullable value
        //[Route("Home/Details/{ID?}")]
        //public ViewResult Details(int? ID)
        //{
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = _empResI.GetEmployee(ID??1),
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Using attribute route on controller
        //[Route("Details/{ID?}")]
        //public ViewResult Details(int? ID)
        //{
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = _empResI.GetEmployee(ID ?? 1),
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Using token replacement
        //[Route("[action]/{ID?}")]
        //[Route("{ID?}")]
        //public ViewResult Details(int? ID)
        //{
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = _empResI.GetEmployee(ID ?? 1),
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Respond to tag helper asp-route-EmpId comming from Index view
        //[Route("[action]/{ID?}")]
        //[Route("{ID?}")]
        //public ViewResult Details(int? EmpId)
        //{
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = _empResI.GetEmployee(EmpId ?? 1),
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Handling 404 Not Found ERRORs
        //[Route("[action]/{ID?}")]
        //[Route("{ID?}")]
        //public ViewResult Details(int? EmpId)
        //{
        //    MEmployee Emp_ToFind = _empResI.GetEmployee(EmpId.Value);
        //    if (Emp_ToFind == null)
        //    {
        //        Response.StatusCode = 404;
        //        return View("EmpNotFound", EmpId.Value);
        //    }
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = Emp_ToFind,
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        //// Unhandled exceptions
        //[Route("[action]/{ID?}")]
        //[Route("{ID?}")]
        //public ViewResult Details(int? EmpId)
        //{
        //    throw new Exception("Error in Details View");
        //}

        //// LogLevel Configuration
        //[Route("[action]/{ID?}")]
        //[Route("{ID?}")]
        ////[AllowAnonymous]
        //public ViewResult Details(int? EmpId)
        //{
        //    _loggerI.LogTrace("Trace Log");
        //    _loggerI.LogDebug("Debug Log");
        //    _loggerI.LogInformation("Information Log");
        //    _loggerI.LogWarning("Warning Log");
        //    _loggerI.LogError("Error Log");
        //    _loggerI.LogCritical("Critical Log");
        //    MEmployee Emp_ToFind = _empResI.GetEmployee(EmpId.Value);
        //    if (Emp_ToFind == null)
        //    {
        //        Response.StatusCode = 404;
        //        return View("EmpNotFound", EmpId.Value);
        //    }
        //    VM_HomeDetails VMHDetails = new VM_HomeDetails()
        //    {
        //        mEmp = Emp_ToFind,
        //        PageTitle = "Employee Details"
        //    };
        //    return View(VMHDetails);
        //}

        // Decrypt the Id to get its value
        [Route("[action]/{ID?}")]
        [Route("{ID?}")]
        //[AllowAnonymous]
        public ViewResult Details(string EmpId)
        {
            _loggerI.LogTrace("Trace Log");
            _loggerI.LogDebug("Debug Log");
            _loggerI.LogInformation("Information Log");
            _loggerI.LogWarning("Warning Log");
            _loggerI.LogError("Error Log");
            _loggerI.LogCritical("Critical Log");
            string DecryptedId = _dataProtectI.Unprotect(EmpId);
            int EmpId_Decrypted = Convert.ToInt32(DecryptedId);
            MEmployee Emp_ToFind = _empResI.GetEmployee(EmpId_Decrypted);
            if (Emp_ToFind == null)
            {
                Response.StatusCode = 404;
                return View("EmpNotFound", EmpId_Decrypted);
            }
            VM_HomeDetails VMHDetails = new VM_HomeDetails()
            {
                mEmp = Emp_ToFind,
                PageTitle = "Employee Details"
            };
            return View(VMHDetails);
        }

        //// Understanding Model Binding
        //[Route("{ID?}")]
        //public string Details(int? ID, string Name)
        //{
        //    return "Id = " + ID.Value.ToString() + ", name = " + Name;
        //}


        //// GET: ~/ParthyTech/Home/Create
        //[HttpGet]
        //public ViewResult Create()
        //{
        //    return View();
        //}

        // GET: ~/ParthyTech/Home/Create
        // Using [Authorize] for special actions
        [HttpGet]
        //[Authorize]
        [CustomAuthorizeAttribute]
        public ViewResult Create()
        {
            return View();
        }

        //// POST: ~/ParthyTech/Home/Create
        //[HttpPost]
        //public RedirectToActionResult Create(MEmployee mEmp)
        //{
        //    MEmployee EmpNew = _empResI.AddEmp(mEmp);
        //    return RedirectToAction("Details", new { ID = EmpNew.Id });
        //}

        //// POST: ~/ParthyTech/Home/Create
        //// Check validation before postting the form request
        //[HttpPost]
        //public IActionResult Create(MEmployee mEmp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MEmployee EmpNew = _empResI.AddEmp(mEmp);
        //        return RedirectToAction("Details", new { ID = EmpNew.Id });
        //    }
        //    return View();
        //}

        //// POST: ~/ParthyTech/Home/Create
        //// Check the singleton
        //[HttpPost]
        //public IActionResult Create(MEmployee mEmp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MEmployee EmpNew = _empResI.AddEmp(mEmp);
        //        //return RedirectToAction("Details", new { ID = EmpNew.Id });
        //    }
        //    return View();
        //}

        //// POST: ~/ParthyTech/Home/Create
        //// Uploading file using IFormFile
        //[HttpPost]
        //public IActionResult Create(VM_MEmployee vmEmp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string Unique_FileName = null;
        //        if (vmEmp.PicPath != null)
        //        {
        //            string Upload_Folder = Path.Combine(_hEnvI.WebRootPath, "Images");
        //            Unique_FileName = Guid.NewGuid().ToString() + "_" + vmEmp.PicPath.FileName;
        //            string FilePath = Path.Combine(Upload_Folder, Unique_FileName);
        //            vmEmp.PicPath.CopyTo(new FileStream(FilePath, FileMode.Create));
        //        }
        //        MEmployee EmpNew = new MEmployee
        //        {
        //            Name = vmEmp.Name,
        //            Email = vmEmp.Email,
        //            Department = vmEmp.Department,
        //            PicPath = Unique_FileName
        //        };
        //        _empResI.AddEmp(EmpNew);
        //        return RedirectToAction("Details", new { EmpId = EmpNew.Id });
        //    }
        //    return View();
        //}

        //// POST: ~/ParthyTech/Home/Create
        //// Refracting the code by removing duplicated code
        //[HttpPost]
        //public IActionResult Create(VM_MEmployee vmEmp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string Unique_FileName = UploadingPics(vmEmp);
        //        MEmployee EmpNew = new MEmployee
        //        {
        //            Name = vmEmp.Name,
        //            Email = vmEmp.Email,
        //            Department = vmEmp.Department,
        //            PicPath = Unique_FileName
        //        };
        //        _empResI.AddEmp(EmpNew);
        //        return RedirectToAction("Details", new { EmpId = EmpNew.Id });
        //    }
        //    return View();
        //}

        // POST: ~/ParthyTech/Home/Create
        // Using [Authorize] for special actions
        [HttpPost]
        //[Authorize]
        [CustomAuthorizeAttribute]
        public IActionResult Create(VM_MEmployee vmEmp)
        {
            if (ModelState.IsValid)
            {
                string Unique_FileName = UploadingPics(vmEmp);
                MEmployee EmpNew = new MEmployee
                {
                    Name = vmEmp.Name,
                    Email = vmEmp.Email,
                    Department = vmEmp.Department,
                    PicPath = Unique_FileName
                };
                _empResI.AddEmp(EmpNew);
                return RedirectToAction("Details", new { EmpId = EmpNew.Id });
            }
            return View();
        }

        //// POST: ~/ParthyTech/Home/Create
        //// Uploading multiple photos using List<IFormFile>
        //[HttpPost]
        //public IActionResult Create(VM_MEmployee vmEmp)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string Unique_FileName = null;
        //        if (vmEmp.Photos_Path != null && vmEmp.Photos_Path.Count > 1)
        //        {
        //            foreach (IFormFile formFileI in vmEmp.Photos_Path)
        //            {
        //                string Upload_Folder = Path.Combine(_hEnvI.WebRootPath, "Images");
        //                Unique_FileName = Guid.NewGuid().ToString() + "_" + formFileI.FileName;
        //                string FilePath = Path.Combine(Upload_Folder, Unique_FileName);
        //                formFileI.CopyTo(new FileStream(FilePath, FileMode.Create));
        //            }
        //        }
        //        MEmployee EmpNew = new MEmployee
        //        {
        //            Name = vmEmp.Name,
        //            Email = vmEmp.Email,
        //            Department = vmEmp.Department,
        //            PicPath = Unique_FileName
        //        };
        //        _empResI.AddEmp(EmpNew);
        //        return RedirectToAction("Details", new { EmpId = EmpNew.Id });
        //    }
        //    return View();
        //}


        //// GET: ~/ParthyTech/Home/Edit
        //[HttpGet]
        //public ViewResult Edit(int EmpId)
        //{
        //    MEmployee Emp = _empResI.GetEmployee(EmpId);
        //    VM_MEmpEDIT Emp_ToEdit = new VM_MEmpEDIT
        //    {
        //        Id = Emp.Id,
        //        Name = Emp.Name,
        //        Email = Emp.Email,
        //        Department = Emp.Department,
        //        Existing_PhotoPath = Emp.PicPath
        //    };
        //    return View(Emp_ToEdit);
        //}

        // GET: ~/ParthyTech/Home/Edit
        // Using [Authorize] for special actions
        [HttpGet]
        //[Authorize]
        [CustomAuthorizeAttribute]
        public ViewResult Edit(int EmpId)
        {
            MEmployee Emp = _empResI.GetEmployee(EmpId);
            VM_MEmpEDIT Emp_ToEdit = new VM_MEmpEDIT
            {
                Id = Emp.Id,
                Name = Emp.Name,
                Email = Emp.Email,
                Department = Emp.Department,
                Existing_PhotoPath = Emp.PicPath
            };
            return View(Emp_ToEdit);
        }

        //// Post: ~/ParthyTech/Home/Edit
        //[HttpPost]
        //public IActionResult Edit(VM_MEmpEDIT vmEmpEdit)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        MEmployee Emp_ToEdit = _empResI.GetEmployee(vmEmpEdit.Id);
        //        Emp_ToEdit.Name = vmEmpEdit.Name;
        //        Emp_ToEdit.Email = vmEmpEdit.Email;
        //        Emp_ToEdit.Department = vmEmpEdit.Department;
        //        if (vmEmpEdit.PicPath != null)
        //        {
        //            if (vmEmpEdit.Existing_PhotoPath != null)
        //            {
        //                string FPath = Path.Combine(_hEnvI.WebRootPath, "Images", vmEmpEdit.Existing_PhotoPath);
        //                System.IO.File.Delete(FPath);
        //            }
        //            Emp_ToEdit.PicPath = UploadingPics(vmEmpEdit);
        //        }
        //        _empResI.UpdateEmp(Emp_ToEdit);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        // Post: ~/ParthyTech/Home/Edit
        // Using [Authorize] for special actions
        [HttpPost]
        //[Authorize]
        [CustomAuthorizeAttribute]
        public IActionResult Edit(VM_MEmpEDIT vmEmpEdit)
        {
            if (ModelState.IsValid)
            {
                MEmployee Emp_ToEdit = _empResI.GetEmployee(vmEmpEdit.Id);
                Emp_ToEdit.Name = vmEmpEdit.Name;
                Emp_ToEdit.Email = vmEmpEdit.Email;
                Emp_ToEdit.Department = vmEmpEdit.Department;
                if (vmEmpEdit.PicPath != null)
                {
                    if (vmEmpEdit.Existing_PhotoPath != null)
                    {
                        string FPath = Path.Combine(_hEnvI.WebRootPath, "Images", vmEmpEdit.Existing_PhotoPath);
                        System.IO.File.Delete(FPath);
                    }
                    Emp_ToEdit.PicPath = UploadingPics(vmEmpEdit);
                }
                _empResI.UpdateEmp(Emp_ToEdit);
                return RedirectToAction("Index");
            }
            return View();
        }

        private string UploadingPics(VM_MEmployee vmEmpEdit)
        {
            string Unique_FileName = null;
            if (vmEmpEdit.PicPath != null)
            {
                string Upload_Folder = Path.Combine(_hEnvI.WebRootPath, "Images");
                Unique_FileName = Guid.NewGuid().ToString() + "_" + vmEmpEdit.PicPath.FileName;
                string FilePath = Path.Combine(Upload_Folder, Unique_FileName);
                using (var FStream = new FileStream(FilePath, FileMode.Create))
                {
                    vmEmpEdit.PicPath.CopyTo(FStream);
                }
            }
            return Unique_FileName;
        }
    }
}
