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
    public class DepartmentController : Controller
    {
        [CustomAuthorize(Permission = "ViewCreateDepartment")]
        public ActionResult Create(long? id)
        {
            try
            {
                LookDepartment modelDepartment = new LookDepartment();
                LookDepartmentService lookDepartmentService = new LookDepartmentService();
                if (id.IsNotNull())
                {
                    var department = lookDepartmentService.GetDepartmentById(id ?? 0);
                    if (department.ResultType.Equals(ResultType.Success))
                    {
                        if (department.Data.IsNotNull())
                        {
                            var dbDepartment = department.Data;
                            modelDepartment.DepartmentName = dbDepartment.DepartmentName;
                            return View(department.Data);
                        }
                        return RedirectToAction("DepartmentList");
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
        [CustomAuthorize(Permission = "EditCreateDepartment")]
        [HttpPost]
        public ActionResult Create(LookDepartment lookDepartment)
        {
            LookDepartmentService lookDepartmentService = new LookDepartmentService();
            if (lookDepartment.LookDepartmentId > 0)
            {
                lookDepartmentService.UpdateDepartment(lookDepartment);
            }
            else
            {
                lookDepartmentService.CreateDepartment(lookDepartment);

            }
            return RedirectToAction("DepartmentList");
        }
        [CustomAuthorize(Permission = "ViewDepartmentList")]
        public ActionResult DepartmentList()
        {
            LookDepartmentService lookDepartmentService = new LookDepartmentService();
            var departmentList = lookDepartmentService.DepartmentList();          
            return View(departmentList.Data);          
        }




    }
}