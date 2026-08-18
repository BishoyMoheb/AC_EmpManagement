using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;//To use AllowAnonymous
using Microsoft.AspNetCore.Diagnostics;//To use IStatusCodeReExecuteFeature
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;//To use ILogger

namespace AC_EmpManagement.Controllers
{
    public class ErrorController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<ErrorController> _loggerErrorI;

        // Injecting ILogger in the constructor
        public ErrorController(ILogger<ErrorController> LoggerErrorI)
        {
            _loggerErrorI = LoggerErrorI;
        }

        //[Route("Error/{StatusCode}")]
        //public IActionResult HttpStatusCodeHandler(int StatusCode)
        //{
        //    switch (StatusCode)
        //    {
        //        case 404:
        //            ViewBag.ErrorMessage = "The requested resources could not be found";
        //            break;
        //    }
        //    return View("RouteNotFound");
        //}

        //// Use UseStatusCodePagesWithReExecute all properties
        //[Route("Error/{StatusCode}")]
        //public IActionResult HttpStatusCodeHandler(int StatusCode)
        //{
        //    var SCResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        //    switch (StatusCode)
        //    {
        //        case 404:
        //            ViewBag.ErrorMessage = "The requested resources could not be found";
        //            ViewBag.Path = SCResult.OriginalPath;
        //            ViewBag.QString = SCResult.OriginalQueryString;
        //            break;
        //    }
        //    return View("RouteNotFound");
        //}

        // Using ILogger<ErrorController> _loggerErrorI
        [Route("Error/{StatusCode}")]
        public IActionResult HttpStatusCodeHandler(int StatusCode)
        {
            var SCResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (StatusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The requested resources could not be found";
                    _loggerErrorI.LogWarning($"404 Error occured. Path is {SCResult.OriginalPath} " +
                                             $"and QueryString is {SCResult.OriginalQueryString}");
                    break;
            }
            return View("RouteNotFound");
        }

        //// Handling Global Error
        //[Route("Error")]
        //[AllowAnonymous]
        //public IActionResult Handling_GlobalError()
        //{
        //    var Exception_Details = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        //    ViewBag.EPath = Exception_Details.Path;
        //    ViewBag.EMassage = Exception_Details.Error.Message;
        //    ViewBag.STrace = Exception_Details.Error.StackTrace;
        //    return View("GlobalErrorHandled");
        //}

        // Using ILogger<ErrorController> _loggerErrorI
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Handling_GlobalError()
        {
            var Exception_Details = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _loggerErrorI.LogError($"The path {Exception_Details.Path} throw an exception" +
                                   $"{Exception_Details.Error}");
            return View("GlobalErrorHandled");
        }
    }
}
