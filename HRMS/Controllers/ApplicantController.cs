using Data.HRMS;
using Library.Core.Services;
using Services.HRMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Look;
using VM.HRMS;
using Library.Lookups;
using Services.Users;
using System.Threading.Tasks;

namespace HRMS.Controllers
{
    public class ApplicantController : Controller
    {

        ApplicantServices applicantServices = new ApplicantServices();
        // GET: Applicant
        [CustomAuthorize(Permission = "ViewApplicantList")]
        public ActionResult ApplicantList()
        {
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            var hrmsWorker = new HRMSWorker();
            var applicantStatus = from EnumLookApplicantStatus status in Enum.GetValues(typeof(EnumLookApplicantStatus))
                                  select new
                                  {
                                      ID = (long)status,
                                      Name = status.GetStringValue()
                                  };
            ViewBag.LookApplicantStatus = new SelectList(applicantStatus, "ID", "Name");
            DropDownListClass dropDownListClass = new DropDownListClass();
            IEnumerable<SelectListItem> designatioList = (from m in hrmsWorker.Repository.Read<LookDesignation>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.DesignationName, Value = m.LookDesignationId.ToString() });
            ViewBag.DesignatioList = designatioList;

            var applicantList = applicantServices.ApplicantList();
            ///  if(departmentList.ResultType==ResultType.Success )
            ViewBag.jobTitleList = dropDownListClass.getDesignation(ProductSaleProfileId);
            return View(applicantList.Data);


        }


        //[CustomAuthorize(Permission = "ViewCreateApplicant")]
        public ActionResult Create(long? id)
        {

            try
            {

                LookQualificationService lookQualificationService = new LookQualificationService();
                var qualificationType = lookQualificationService.QualificationTypeList();
                if (qualificationType.ResultType.Equals(ResultType.Exception))
                    return RedirectToAction("No505", "Error");
                ViewBag.QualificationType = new SelectList(qualificationType.Data, "LookQualificationTypeId", "QualificationType");
                if (id.IsNotNull())
                {
                    DropDownListClass modelDepartment = new DropDownListClass();
                    ApplicantServices applicantServices = new ApplicantServices();
                    var applicant = applicantServices.GetApplicantById(id ?? 0);
                    if (applicant.ResultType.Equals(ResultType.Success))
                    {
                        if (applicant.Data.IsNotNull())
                        {
                            var dbApplicant = applicant.Data;
                            // modelDepartment.DepartmentName = dbDepartment.DepartmentName;
                            // applicantServices.ApplicantMaping(dbApplicant).Data;
                            return View(applicantServices.ApplicantMaping(dbApplicant).Data);

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
                    return View(new DropDownListClass());
                }
            }
            catch
            {
                return RedirectToAction("No505", "Error");
            }





        }
        //[CustomAuthorize(Permission = "UpdateCreateApplicant")]
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

            //return RedirectToAction("ApplicantList");
            return View(new DropDownListClass());
            // return N;
        }
        [CustomAuthorize(Permission = "ViewApplicantList")]
        [HttpPost]
        public ActionResult SearchApplicant(SearchApplicantViewModel model)
        {
            var modell = new EmployeeAttachment();
            HRMSWorker hrmsWorker = new HRMSWorker();
            ApplicantServices applicantServices = new ApplicantServices();

            string resume = string.Empty;

            return Json(applicantServices.ApplicantList(model), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public async Task<JsonResult> ChangeApplicantsStatus(ChangeApplicantsStatusViewModel model)
        {
            var result = new Result<long>();
            try
            {
                if (model.ApplicantId == 0)
                {
                    result.ResultType = ResultType.Failure;
                    result.Message = "No applicants data provided to update status of!";
                }
                    applicantServices.AddStatusChangeComments(model, (long)Session["EmployeeId"]);
                    var operation = applicantServices.UpdateApplicantStatus((EnumLookApplicantStatus)model.LookApplicantStatusId, model.ApplicantId);
                
                    if (operation.ResultType.Equals(ResultType.Exception))
                        return Json(operation);
                    if (model.LookApplicantStatusId.Equals((long)EnumLookApplicantStatus.Offered) || model.LookApplicantStatusId.Equals((long)EnumLookApplicantStatus.Hired))
                    {
                        var applicant = applicantServices.GetApplicantById(model.ApplicantId);
                        if (applicant.ResultType.Equals(ResultType.Exception)) return Json(model.ApplicantId);

                        EmailViewModel email = new EmailViewModel
                        {
                            Recipient = applicant.Data.Email
                        };
                        if (model.LookApplicantStatusId.Equals((long)EnumLookApplicantStatus.Offered))
                        {
                            email.Subject = "Job offer at our company";
                            email.BodyMessage = $@"Dear  { applicant.Data.FullName} 
                            <br/> <br/> It is to inform you that our organization is intrested in hiring you.
                            <br/>Please accept offer : {DateTime.Now.AddDays(15).ToString("dd-MM-yyyy")} .<br/><br/><br/>
                            <br/>Looking forward to positive respond.
                            <br/>Thank you";
                        }
                        else
                        {
                            email.Subject = "Hirig procedure";
                            email.BodyMessage = $@"Dear  { applicant.Data.FullName} 
                            <br/> <br/> It is to inform you that our organization has completed your hiring procedure.
                            <br/> Your joining date will be : {DateTime.Now.AddDays(15).ToString("dd-MM-yyyy")}.<br/><br/><br/>
                            <br/>Looking forward to great beginnings. 
                            <br/>Thank you";
                        }
                        await EmailServices.SendMail(email);
                    }
                result.ResultType = ResultType.Success;
                return Json(result);
            }
            catch (Exception e)
            {
                result.Data = 0;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }

            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> CallApplicantInterview(ApplicantInterview interview)
        {
            var result = new Result<bool>();
            try
            {
                UserServices userService = new UserServices();
                interview.CreateDate = DateTime.Now;
                interview.Status = (long)EnumLookInterviewStatus.Called;
                InterviewServices interviewService = new InterviewServices();
                var interviewCreation = interviewService.CreateInterView(interview);
                if (interviewCreation.ResultType.Equals(ResultType.Exception))
                    return Json(interviewCreation);
                var statusUpdation = applicantServices.UpdateApplicantStatus(EnumLookApplicantStatus.InterviewCalled, interview.ApplicantId ?? 0);
                if (statusUpdation.ResultType.Equals(ResultType.Exception))
                    return Json(statusUpdation);
                var applicant = applicantServices.GetApplicantById(interview.ApplicantId);
                if (applicant.ResultType.Equals(ResultType.Exception)) return Json(statusUpdation);

                EmailViewModel email = new EmailViewModel
                {
                    Recipient = applicant.Data.Email,
                    Subject = "Interview for the position applied",
                    BodyMessage = string.Format($@"Dear  { applicant.Data.FullName} 
                <br/> <br/> It is to inform you that our organization is intrested in interviewing.
                <br/>Please attend the interview dated: {interview.MeetingDate} at {interview.MeetingTime} sharp.<br/><br/><br/><br/>Looking forward to see you. <br/>Thank you")
                };
                await EmailServices.SendMail(email);
                result.Data = true;
                result.ResultType = ResultType.Success;

            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = "Operation Interrurpted due to Internal Error\n" + e.GetOriginalException().Message;
                result.Exception = e;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult MakeEmployee(long applicantId,string joining)
        {
            var result = new Result<bool>();
            try
            {
                var operation = applicantServices.UpdateApplicantStatus(EnumLookApplicantStatus.Joined, applicantId);
                if (operation.ResultType.Equals(ResultType.Exception))
                    return Json(operation);

                var employeeJoining = applicantServices.ApplicantJoining(applicantId,joining);
                if (employeeJoining.ResultType.Equals(ResultType.Exception)) return Json(employeeJoining);
                result.Data = true;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return Json(result);
        }
    }
}