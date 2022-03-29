using Data.HRMS;
using Library.Core.Services;
using Library.Lookups;
using Services.HRMS;
using Services.Look;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM.HRMS;

namespace HRMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home fgh
        public ActionResult Index()
        {
            HRMSWorker hrmsWorker = new HRMSWorker();
            string sql = @"select Recruitment.* ,
                             LookDesignation.DesignationName,LookEmployeeType.EmployeeTypeName,
							LookJobType.JobTypeName,EmpApproval.FullName as ForApproval,
							Employee.FullName as Reporting,
							LookGender.LookGenderName,
							EmpcreateBy.FullName as CreatedByName
                            from Recruitment 
                            inner join LookDesignation on LookDesignation.LookDesignationId=Recruitment.LookDesignationId
                            inner join LookEmployeeType on LookEmployeeType.LookEmployeeTypeId=Recruitment.LookEmployeeTypeId
                            inner join LookJobType on LookJobType.LookJobTypeId=Recruitment.LookJobTypeId
                            left join Employee on Employee.EmployeeId=Recruitment.ReportingEmployeeId
							 left join Employee EmpApproval on EmpApproval.EmployeeId=Recruitment.ForApprovalEmployeeId
							 inner join Employee EmpcreateBy on EmpcreateBy.EmployeeId=Recruitment.CreatedBy
                            inner join LookGender on LookGender.LookGenderId=Recruitment.LookGenderId 
                            where Recruitment.LookRecruitmentStatusId=" + (long)EnumLookRecruitmentStatus.Published + @"
                            order by Recruitment.RecruitmentId desc ";
            var dbApplicant = hrmsWorker.Repository.db.Database.SqlQuery<RecruitmentListViewModel>(sql).ToListSafely();
            return View(dbApplicant);
            // else
            //  {
            //  return RedirectToAction("Login", "User");
            // }

        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Create(long? dId)
        {
            //try
            //{
            //    LookQualificationService lookQualificationService = new LookQualificationService();
            //    var qualificationType = lookQualificationService.QualificationTypeList();
            //    if (qualificationType.ResultType.Equals(ResultType.Exception))
            //        return RedirectToAction("No505", "Error");
            //    ViewBag.QualificationType = new SelectList(qualificationType.Data, "LookQualificationTypeId", "QualificationType");
            //    if (id.IsNotNull())
            //    {
            //        DropDownListClass modelDepartment = new DropDownListClass();
            //        ApplicantServices applicantServices = new ApplicantServices();
            //        var applicant = applicantServices.GetApplicantById(id ?? 0);
            //        if (applicant.ResultType.Equals(ResultType.Success))
            //        {
            //            if (applicant.Data.IsNotNull())
            //            {
            //                var dbApplicant = applicant.Data;
            //                return View(applicantServices.ApplicantMaping(dbApplicant).Data);

            //            }
            //            return RedirectToAction("DepartmentList");
            //        }
            //        else
            //        {
            //            return RedirectToAction("No505", "Error");
            //        }
            //    }
            //    else
            //    {
            //        return View(new DropDownListClass());
            //    }
            //}
            //catch
            //{
            //    return RedirectToAction("No505", "Error");
            //}
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            DropDownListClass dropDownListClass = new DropDownListClass();
            var designation = dropDownListClass.GetDesignationById(ProductSaleProfileId,dId);
            if (designation.Data.IsNotNullOrEmpty())
            {
                dropDownListClass.DesignationName = designation.Data.DesignationName;
            }

            return View(dropDownListClass);
        }
        [HttpPost]
        public ActionResult Create(DropDownListClass ddList)
        {
            ApplicantServices applicantServices = new ApplicantServices();
            Applicant model = new Applicant();
            string coveringLetter = string.Empty;
            string resume = string.Empty;
            /////////////////////
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                ddList.CoveringLetterFile = System.Web.HttpContext.Current.Request.Files["CoveringLetter"];
                ddList.ResumeFile = System.Web.HttpContext.Current.Request.Files["Resume"];
                if (ddList.ApplicantId > 0)
                {
                    var Result = applicantServices.GetApplicantById(ddList.ApplicantId);
                    if (Result.ResultType == ResultType.Success)
                    {
                        var fileToDelete = Result.Data.CoveringLetter; //guid
                                                                       // string filePath = HttpContext.Current.Server.MapPath("../../PublicSite/Images/UserProfile/" + fileToDelete);
                                                                       // filePath = filePath.Replace("MobileApi\\", "");
                        string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadApplicant"]) + fileToDelete;
                        if (System.IO.File.Exists(filePath))
                        {
                            try
                            {
                                System.IO.File.Delete(filePath);
                            }
                            catch (Exception e) { }

                        }

                        var ResumeToDelete = Result.Data.Resume; //guid
                                                                 // string filePath = HttpContext.Current.Server.MapPath("../../PublicSite/Images/UserProfile/" + fileToDelete);
                                                                 // filePath = filePath.Replace("MobileApi\\", "");
                        var ResumePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadApplicant"]) + ResumeToDelete;
                        if (ResumeToDelete.IsNotNullOrEmpty() && System.IO.File.Exists(ResumePath))
                        {
                            try
                            {
                                System.IO.File.Delete(ResumePath);
                            }
                            catch (Exception e) { }

                        }
                    }

                }

                try
                {
                    if (ddList.ResumeFile != null && ddList.ResumeFile.FileName.IsNotNullOrEmpty())
                    {
                        var fileName = Path.GetFileName(ddList.ResumeFile.FileName);
                        var fileExtension = Path.GetExtension(ddList.ResumeFile.FileName);
                        resume = Guid.NewGuid().ToString();
                        string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadApplicant"]) + resume + fileExtension;
                        resume = resume + fileExtension;
                        ddList.ResumeFile.SaveAs(filePath);

                    }
                    else
                    {
                        resume = null;
                    }
                }
                catch (Exception e) { resume = null; }
                try
                {
                    if (ddList.CoveringLetterFile != null && ddList.CoveringLetterFile.FileName.IsNotNullOrEmpty())
                    {
                        var fileName = Path.GetFileName(ddList.CoveringLetterFile.FileName);
                        var fileExtension = Path.GetExtension(ddList.CoveringLetterFile.FileName);
                        coveringLetter = Guid.NewGuid().ToString();
                        string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadApplicant"]) + coveringLetter + fileExtension;
                        coveringLetter = coveringLetter + fileExtension;
                        ddList.CoveringLetterFile.SaveAs(filePath);
                        //coveringLetter = System.Configuration.ConfigurationManager.AppSettings["UploadApplicantPath"]+coveringLetter;

                    }
                    else
                    {
                        coveringLetter = null;
                    }
                }
                catch (Exception e) { coveringLetter = null; }

            }
            ////////////////////////////



            var result = applicantServices.ApplicantMaping(ddList);

            if (result.ResultType == ResultType.Success)
            {
                model = result.Data;
                if (coveringLetter.IsNotNullOrEmpty())
                    model.CoveringLetter = coveringLetter;
                if (resume.IsNotNullOrEmpty())
                    model.Resume = resume;
                if (ddList.ApplicantId > 0)
                {
                    model.UpdateDate = DateTime.Now;

                    applicantServices.UpdateApplicant(model);
                }
                else
                {
                    model.CreateDate = DateTime.Now;
                    applicantServices.CreateApplicant(model);
                }
            }

            return RedirectToAction("Index", "Home");
            // return View(new DropDownListClass());
            // return N;
        }

        [HttpPost]
        public JsonResult GetBioMericSyncDateTime()
        {
            HRPolicyService hRPolicyService = new HRPolicyService();
            var ok = hRPolicyService.GetBioDateTimeSync();
            return Json(ok, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        // [CustomAuthorize(Permission = "CreateEmployeeDependent")]
        public JsonResult SelectCompany(long id)
        {
            Result<bool> result = new Result<bool>();
            string ids = Convert.ToString(System.Web.HttpContext.Current.Session["LookOrganizationIds"]);
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            if (roleid == 0 )
            {
                if (id > 0)
                {
                    int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                    var Organization = LookOrganizationService.GetOrganizations(1, 0);
                    var IsExsist = Organization.Data.Where(x => x.LookOrganizationId == id).FirstOrDefault();
                    if (IsExsist.IsNotNull())
                    {
                        System.Web.HttpContext.Current.Session["SelectedOrganizationId"] = id;
                        if (ProductSaleProfileId != IsExsist.ProductSaleProfileId)
                            System.Web.HttpContext.Current.Session["ProductSaleProfileId"] = IsExsist.ProductSaleProfileId;
                    }
                    result.ResultType = ResultType.Success;
                }else
                {
                    result.ResultType = ResultType.Failure;
                }
            }
            else if ( roleid == 1)
            {
                if (id > 0)
                {
                    int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                    var Organization = LookOrganizationService.GetOrganizations(1, ProductSaleProfileId);
                    var IsExsist = Organization.Data.Where(x => x.LookOrganizationId == id).FirstOrDefault();
                    if (IsExsist.IsNotNull())
                        System.Web.HttpContext.Current.Session["SelectedOrganizationId"] = id;
                    result.ResultType = ResultType.Success;
                }
                else
                {
                    result.ResultType = ResultType.Failure;
                }
            }
            else if (id > 0 && ids.Split(',').Contains(id.ToString()))
            {
                System.Web.HttpContext.Current.Session["SelectedOrganizationId"] = id;
                result.ResultType = ResultType.Success;
            }
            else
            {
                result.ResultType = ResultType.Failure;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}