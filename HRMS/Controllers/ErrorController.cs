using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Core.Services;

namespace HRMS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult No505()
        {
            return View();
        }
        public ActionResult No404()
        {
            return View();
        }
        public ActionResult No401()
        {
            return View();
        }
        public JsonResult NotAuthorized()
        {
            var result = new Result<bool>();
            result.Data = false;
            result.ResultType = ResultType.Failure;
            result.Message = "You are not authorized for this operation.";
           return Json(result, JsonRequestBehavior.AllowGet) ;
        }
        public JsonResult NotLoggedIn()
        {
            var result = new Result<bool>();
            result.Data = false;
            result.ResultType = ResultType.Failure;
            result.Message = "You are not logged in. Please login first than try again";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}