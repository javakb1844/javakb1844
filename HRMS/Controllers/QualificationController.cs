using Data.HRMS;
using Library.Core.Services;
using Services.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.HRMS;

namespace HRMS.Controllers
{
    public class QualificationController : Controller
    {
        // GET: Qualification
        LookQualificationService lookQualificationService = new LookQualificationService();
        [CustomAuthorize(Permission = "CreateQualification")]
        public JsonResult New(LookQualification qualification)
        {
            var result = new Result<LookQualification>();
            try
            {
                var insertion = lookQualificationService.InsertQualification(qualification);
                if (insertion.ResultType.Equals(ResultType.Success))
                {                    
                    result.Data = insertion.Data;
                    result.ResultType = ResultType.Success;
                }
                else
                {
                    result.Data = null;
                    result.ResultType = ResultType.Failure;
                }
            }
            catch(Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(Permission = "ViewCreateQualification")]
        public ActionResult Create(long? id)
        {
            var qualificationType = lookQualificationService.QualificationTypeList();
            if (qualificationType.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
              ViewBag.QualificationType = new SelectList(qualificationType.Data, "LookQualificationTypeId", "QualificationType");
            if (id.IsNotNull())
            {
                long desgnationId = id ?? 0;
                var designation = lookQualificationService.GetQualification(desgnationId);
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
        [CustomAuthorize(Permission = "EditCreateQualification")]
        [HttpPost]
        public ActionResult Create(LookQualification qualification)
        {
            if (qualification.LookQualificationId.Equals(0) || qualification.LookQualificationId.Equals(null))
            {
                var insertion = lookQualificationService.InsertQualification(qualification);
                if (insertion.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
            }
            else
            {
                var updation = lookQualificationService.UpdateQualification(qualification);
                if (updation.ResultType.Equals(ResultType.Exception))
                {

                    return RedirectToAction("No505", "Error");
                }
            }
            return RedirectToAction("QualificationList");
        }
        [CustomAuthorize(Permission = "ViewCreateQualification")]
        public ActionResult QualificationTypeCreate(long? id)
        {
            if (id.IsNotNull())
            {
                long desgnationId = id ?? 0;
                var designation = lookQualificationService.GetQualificationType(desgnationId);
                if (designation.ResultType.Equals(ResultType.Exception))
                    return RedirectToAction("No505", "Error");
                if (designation.Data.IsNotNull())
                {
                    return View(designation.Data);
                }
                else
                {
                    return RedirectToAction("QualificationTypeList");
                }
            }

            return View();
        }
        [HttpPost]
        [CustomAuthorize(Permission = "EditCreateQualification")]
        public ActionResult QualificationTypeCreate(LookQualificationType qualification)
        {
            if (qualification.LookQualificationTypeId.Equals(0) || qualification.LookQualificationTypeId.Equals(null))
            {
                var insertion = lookQualificationService.InsertQualification(qualification);
                if (insertion.ResultType.Equals(ResultType.Exception))
                {
                    return RedirectToAction("No505", "Error");
                }
            }
            else
            {
                var updation = lookQualificationService.UpdateQualification(qualification);
                if (updation.ResultType.Equals(ResultType.Exception))
                {

                    return RedirectToAction("No505", "Error");
                }
            }
            return RedirectToAction("QualificationList");
        }
        [CustomAuthorize(Permission = "ViewQualificationList")]
        public ActionResult QualificationList()
        {
            //LookQualification
            
            return View(lookQualificationService.QualificationList().Data);
        }

        [CustomAuthorize(Permission = "ViewQualificationList")]
        public ActionResult QualificationTypeList()
        {
            //LookQualification

            return View(lookQualificationService.QualificationTypeList().Data);
        }
    }
}