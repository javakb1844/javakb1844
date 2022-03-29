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

namespace HRMS.Controllers
{
    public class GradeController : Controller
    {
        // GET: Grade
        //public ActionResult Index()
        //{
        //    var designationList = designationService.GetDesignationList();
        //    ///  if(departmentList.ResultType==ResultType.Success )
        //    return View(designationList.Data);
        //}
        LookGradeService gradeService = new LookGradeService();
        //[CustomAuthorize(Permission = "ViewCreateGrade")]
        [HttpGet]
        public ActionResult Create(int? id)
        {
            //LookDepartmentService departmentService = new LookDepartmentService();
            var grades = gradeService.GetGradeList();
            //if (departments.ResultType.Equals(ResultType.Exception))
            //    return RedirectToAction("No505", "Error");
            //ViewBag.Departments = new SelectList(departments.Data, "LookDepartmentId", "DepartmentName");

            //// var tt = departments.Data.Where(x => x.LookDepartmentId==1);
            if (id.IsNotNull())
            {
                int gradeId = id ?? 0;
                var grade = gradeService.GetGrade(gradeId);
                if (grade.ResultType.Equals(ResultType.Exception))
                    return RedirectToAction("No505", "Error");
                if (grade.Data.IsNotNull())
                {
                    return View(grade.Data);
                }
                else
                {
                    return RedirectToAction("List");
                }
            }

            return View();
        }
      //  [CustomAuthorize(Permission = "UpdateCreateGrade")]
        [HttpPost]
        public ActionResult Create(LookGrade grade)
        {
            if (grade.LookGradeId.Equals(0) || grade.LookGradeId.Equals(null))
            {
                var insertion = gradeService.InsertGrade(grade);
                if (insertion.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
            }
            else
            {
                var updation = gradeService.UpdateGrade(grade);
                if (updation.ResultType.Equals(ResultType.Exception))
                {

                    return RedirectToAction("No505", "Error");
                }
            }
            return RedirectToAction("Index");
        }
      //  [CustomAuthorize(Permission = "ViewGradeList")]
        public ActionResult Index()
        {

            var designationList = gradeService.GetGradeList();
            ///  if(departmentList.ResultType==ResultType.Success )
            return View(designationList.Data);
            //  else
            //  return View(departmentList.Data);
        }
        public JsonResult DesignationsList()
        {

            var GradeList = gradeService.GetGradeList().Data;
            ///  if(departmentList.ResultType==ResultType.Success )
            return Json(GradeList);
            //  else
            //  return View(departmentList.Data);
        }
    }
}