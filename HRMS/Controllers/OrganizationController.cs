using Data.HRMS;
using Services.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Core.Services;
using Services.HRMS;


namespace HRMS.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: Organization
        public ActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(LookDepartment lookDepartment)
        {
            LookDepartmentService lookDepartmentService = new LookDepartmentService();
            lookDepartmentService.CreateDepartment(lookDepartment);
            return View();
        }

        public ActionResult GetDepartment()
        {
            return View();
        }
		  [CustomAuthorize(Permission = "ViewCreateOrganization")]
        [HttpGet]
        public ActionResult Organization(long? id)
        {
            LookOrganization organization = new LookOrganization();
            if (!(id.Equals(0) || id.IsNull()))
            {
                LookOrganizationService organizationService = new LookOrganizationService();
                var exsistingOrganization = organizationService.GetOrganization(id ?? 0);
                if (exsistingOrganization.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
                if (exsistingOrganization.Data.IsNull())
                {
                    ViewBag.Alert = "Sorry, Not Found... Why not enter a new one?";
                }
                organization = exsistingOrganization.Data;
            }
            return View(organization);
        }
        [CustomAuthorize(Permission = "EditCreateOrganization")]
        [HttpPost]
        public ActionResult Organization(LookOrganization organization)
        {
            LookOrganizationService organizationService = new LookOrganizationService();
            if (organization.LookOrganizationId.Equals(0) || organization.LookOrganizationId.IsNull())
            {
                var response = organizationService.CreateOrganization(organization);
                if (response.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var response = organizationService.UpdateOrganization(organization);
                if (response.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [CustomAuthorize(Permission = "CreateOrganization")]
        [HttpPost]
        public JsonResult CreateOrganization(LookOrganization organization)
        {
            var result = new Result<LookOrganization>();
            try
            {
                LookOrganizationService organizationService = new LookOrganizationService();
                var response = organizationService.CreateOrganization(organization);
                if (response.ResultType.Equals(ResultType.Exception))
                {
                    result.Data = null;
                    result.ResultType = ResultType.Exception;
                    result.Message = response.Message;
                    result.Exception = response.Exception;
                }
                result.Data = response.Data;
                result.ResultType = ResultType.Success;                
            }

            catch(Exception e) {
                result.Data = null;
                result.ResultType = ResultType.Success;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}