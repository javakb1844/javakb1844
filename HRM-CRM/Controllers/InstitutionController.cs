using Data.HRMS;
using Library.Core.Services;
using Services.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM_CRM.Controllers
{
    public class InstitutionController : Controller
    {
        LookInstitutionService lookInstitutionService = new LookInstitutionService();
        // GET: Institution
        [CustomAuthorize(Permission = "ViewCreateInstitution")]
        public ActionResult Create(long? id)
        {
            try
            {
                LookInstitution modelInstitution = new LookInstitution();
               
                if (id.IsNotNull())
                {
                    var institution = lookInstitutionService.GetInstitutionById(id ?? 0);
                    if (institution.ResultType.Equals(ResultType.Success))
                    {
                        if (institution.Data.IsNotNull())
                        {
                           // var dbDepartment = institution.Data;
                            //modelInstitution.InstitutionName = modelInstitution.InstitutionName;
                            return View(institution.Data);
                        }
                        return RedirectToAction("InstitutionList");
                    }
                    else
                    {
                        return RedirectToAction("No505", "Error");
                    }
                }
                else
                {
                    // return RedirectToAction("DepartmentList");
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("No505", "Error");
            }
        }
        [CustomAuthorize(Permission = "EditCreateInstitution")]
        [HttpPost]
        public ActionResult Create(LookInstitution lookInstitution)
        {
           
            if (lookInstitution.LookInstitutionId > 0)
            {
                lookInstitutionService.UpdateInstitution(lookInstitution);
            }
            else
            {
                lookInstitutionService.CreateInstitution(lookInstitution);

            }
            return RedirectToAction("InstitutionList");
        }
        [CustomAuthorize(Permission = "ViewInstitutionList")]
        public ActionResult InstitutionList()
        {
           
            var institutionList = lookInstitutionService.InstitutionList();
            return View(institutionList.Data);
        }

        [HttpPost]
        public JsonResult CreateInstitution(LookInstitution institution)
        {
            var result = new Result<LookInstitution>();
            try
            {
                LookInstitutionService institutionService = new LookInstitutionService();

                var response = institutionService.CreateInstitution(institution);
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

            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Success;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}