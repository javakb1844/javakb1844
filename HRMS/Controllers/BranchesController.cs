using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.HRMS;
using Services.Look;
using Library.Core.Services;

namespace HRMS.Controllers
{
    public class BranchesController : Controller
    {
        private HRMSdb db = new HRMSdb();

        // GET: Branches
        LookBranchService branchService = new LookBranchService();
        [CustomAuthorize(Permission = "ViewCreateBranch")]
        [HttpGet]
        public ActionResult Create(long? id)
        {
         
            var branchtype = branchService.GetBranchTypeList();
            if (branchtype.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
            ViewBag.BranchType = new SelectList(branchtype.Data, "LookBranchTypeId", "BranchType");
            var branch = branchService.GetBranchList();
            if (branch.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
            ViewBag.ParntBranch = new SelectList(branch.Data, "LookBranchId", "BranchName");

            // var tt = departments.Data.Where(x => x.LookDepartmentId==1);
            if (id.IsNotNull())
            {
                long desgnationId = id ?? 0;
                var designation = branchService.GetBranch(desgnationId);
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
        [CustomAuthorize(Permission = "UpdateCreateBranch")]
        [HttpPost]
        public ActionResult Create(LookBranch designation)
        {
            if (designation.LookBranchId.Equals(0) || designation.LookBranchId.Equals(null))
            {
                var insertion = branchService.InsertBranch(designation);
                if (insertion.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
            }
            else
            {
                var updation = branchService.UpdateBranch(designation);
                if (updation.ResultType.Equals(ResultType.Exception))
                {

                    return RedirectToAction("No505", "Error");
                }
            }
            return RedirectToAction("BranchList");
        }
        [CustomAuthorize(Permission = "ViewBranchList")]
        public ActionResult BranchList()
        {

            var designationList = branchService.GetBranchList();
            ///  if(departmentList.ResultType==ResultType.Success )
            return View(designationList.Data);
            //  else
            //  return View(departmentList.Data);
        }
        public JsonResult BranchsList()
        {

            var designationList = branchService.GetBranchList().Data;
            ///  if(departmentList.ResultType==ResultType.Success )
            return Json(designationList);
            //  else
            //  return View(departmentList.Data);
        }

    }
}
