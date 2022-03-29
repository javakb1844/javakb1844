using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.HRMS;
using VM.Look;
using Services.Look;
using Library.Core.Services;
using Library.Extensions;


namespace HRM_CRM.Controllers
{
    public class DesignationController : Controller
    {
        LookDesignationService designationService = new LookDesignationService();
        [CustomAuthorize(Permission = "ViewCreateDesignation")]
        [HttpGet]
        public ActionResult Create(long? id)
        {
            LookDepartmentService departmentService = new LookDepartmentService();
            var departments = departmentService.DepartmentList();
            if (departments.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
           ViewBag.Departments = new SelectList(departments.Data, "LookDepartmentId", "DepartmentName");
        
            // var tt = departments.Data.Where(x => x.LookDepartmentId==1);
            if (id.IsNotNull())
            {
                long desgnationId = id ?? 0;
                var designation = designationService.GetDesignation(desgnationId);
                if (designation.ResultType.Equals(ResultType.Exception))
                    return RedirectToAction("No505", "Error");
                if (designation.Data.IsNotNull())
                {
                    return View(designation.Data);
                }
                else
                {
                    return RedirectToAction("List");
                }
            }
            
            return View();
        }
        [CustomAuthorize(Permission = "UpdateCreateDesignation")]
        [HttpPost]
        public ActionResult Create(LookDesignation designation)
        {
            if (designation.LookDesignationId.Equals(0)|| designation.LookDesignationId.Equals(null))
            {
                var insertion= designationService.InsertDesignation(designation);
                if (insertion.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
            }
            else
            {
               var updation= designationService.UpdateDesignation(designation);
                if (updation.ResultType.Equals(ResultType.Exception))
                {

                    return RedirectToAction("No505", "Error");
                }
            }
            return RedirectToAction("DesignationList");
        }
        [CustomAuthorize(Permission = "ViewDesignationList")]
        public ActionResult DesignationList()
        {
           
            var designationList = designationService.GetDesignationList();
            ///  if(departmentList.ResultType==ResultType.Success )
            return View(designationList.Data);
            //  else
            //  return View(departmentList.Data);
        }
        public JsonResult DesignationsList()
        {

            var designationList = designationService.GetDesignationList().Data;
            ///  if(departmentList.ResultType==ResultType.Success )
            return Json(designationList);
            //  else
            //  return View(departmentList.Data);
        }
    }
}