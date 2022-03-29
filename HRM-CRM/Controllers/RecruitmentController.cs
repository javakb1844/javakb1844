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
using System.Web.Script.Serialization;
using VM.HRMS;

namespace HRM_CRM.Controllers
{
    public class RecruitmentController : Controller
    {
        // GET: Recruitment
        Recruitment modelRecruitment = new Recruitment();
        RecruitmentService hrRecruitment = new RecruitmentService();
        [CustomAuthorize(Permission = "ViewCreateHRPolicy")]
        public ActionResult Create(long? id)
        {      
            RecruitmentView recruitmentView = new RecruitmentView();
            HRMSWorker hrmsWorker = new HRMSWorker();
            try
            {                             
                string selectionProcess = string.Empty;
                selectionProcess = @"select *,  convert(bigint,0) as HRRecruitmentProcessId
                                         from LookSelectionProcess";                                         
                if (id.IsNotNull())
                {
                    selectionProcess = @"select *, HRRecruitmentProcess.HRRecruitmentProcessId
                                         from LookSelectionProcess
                                         left join HRRecruitmentProcess on HRRecruitmentProcess.ProcessId = LookSelectionProcess.LookSelectionProcessId and HRRecruitmentProcess.RecruitmentId =" + id;
                }
                    var recruitmentListing = hrmsWorker.Repository.db.Database.SqlQuery<LookSelectionProcessViewModel>(selectionProcess).ToListSafely();
                 if (id.IsNotNull())
                {                  
                    RecruitmentService recruitmentServices = new RecruitmentService();
                    var recruitment = recruitmentServices.GetRecruitmentById(id ?? 0);
                    
                    if (recruitment.ResultType.Equals(ResultType.Success))
                    {
                        if (recruitment.Data.IsNotNull())
                        {
                            var dbRecruitment = recruitment.Data;
                            recruitmentView = recruitmentServices.RecruitmentMaping(dbRecruitment).Data;
                            recruitmentView.LookAppointmentProcess = recruitmentListing.Where(x => x.Type == 0).ToListSafely();
                            recruitmentView.LookSelectionProcess = recruitmentListing.Where(x => x.Type == 1).ToListSafely();
                            return View(recruitmentView);
                        }
                        return RedirectToAction("RecruitmentList");
                    }
                    else
                    {
                        return RedirectToAction("No505", "Error");
                    }
                }
                else
                {
                     recruitmentView.LookAppointmentProcess = recruitmentListing.Where(x => x.Type == 0).ToListSafely();
                    recruitmentView.LookSelectionProcess = recruitmentListing.Where(x => x.Type == 1).ToListSafely();            
                    return View(recruitmentView);
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("No505", "Error");
            }

        }
        [CustomAuthorize(Permission = "UpdateCreateApplicant")]
        [HttpPost]
        public ActionResult Create(RecruitmentView ddList)
        {
            //ApplicantServices applicantServices = new ApplicantServices();
            RecruitmentService recruitmentService = new RecruitmentService();
            Recruitment model = new Recruitment();
            string coveringLetter = string.Empty;
            string resume = string.Empty;
            /////////////////////
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                ddList.PositionDescriptionFile = System.Web.HttpContext.Current.Request.Files["PositionDescription"];
                if (ddList.RecruitmentId > 0)
                {
                    var Result = recruitmentService.GetRecruitmentById(ddList.RecruitmentId);
                    if (Result.ResultType == ResultType.Success)
                    {
                        var fileToDelete = Result.Data.PositionDescription; //guid
                                                                            // string filePath = HttpContext.Current.Server.MapPath("../../PublicSite/Images/UserProfile/" + fileToDelete);
                                                                            // filePath = filePath.Replace("MobileApi\\", "");
                        string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadRecruitment"]) + fileToDelete;
                        if (System.IO.File.Exists(filePath))
                        {
                            try
                            {
                                System.IO.File.Delete(filePath);
                            }
                            catch (Exception e) { }
                        }


                    }

                }


                try
                {
                    if (ddList.PositionDescriptionFile != null && ddList.PositionDescriptionFile.FileName.IsNotNullOrEmpty())
                    {
                        var fileName = Path.GetFileName(ddList.PositionDescriptionFile.FileName);
                        var fileExtension = Path.GetExtension(ddList.PositionDescriptionFile.FileName);
                        coveringLetter = Guid.NewGuid().ToString();
                        string filePath = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadRecruitment"]) + coveringLetter + fileExtension;
                        coveringLetter = coveringLetter + fileExtension;
                        ddList.PositionDescriptionFile.SaveAs(filePath);
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
            model.RecruitmentId = ddList.RecruitmentId;
            model.LookDesignationId = ddList.LookDesignationId;
            model.ForApprovalEmployeeId = ddList.ForApprovalEmployeeId;
            model.LookEmployeeTypeId = ddList.LookEmployeeTypeId;
            model.Preferred_Start_Date = ddList.Preferred_Start_Date;
            model.LookJobTypeId = ddList.LookJobTypeId;
            model.LookDepartmentId = ddList.LookDepartmentId;
            model.ReportingEmployeeId = ddList.ReportingEmployeeId;
            model.Length_Of_Term = ddList.Length_Of_Term;          
            model.NoOfPositions = ddList.NoOfPositions;
            model.LookGenderId = ddList.LookGenderId;
            model.ShortSummary = ddList.ShortSummary;
            model.PositionDescription = coveringLetter;
            model.AdClosingDate = ddList.AdClosingDate;
            ddList.PositionDescription = coveringLetter;
            var result = ddList; //recruitmentService.ApplicantMaping(ddList);
            HRRecruitmentProcessService hRRecruitmentProcessService = new HRRecruitmentProcessService();

            if (ddList.RecruitmentId > 0)
            {
                model.UpdateDate = DateTime.Now;

                model.UpdatedBy = (long)Session["EmployeeId"];
                recruitmentService.UpdateRecruitment(ddList);
                hRRecruitmentProcessService.DeleteProcessByRecruitmentId(ddList.RecruitmentId);
            }
            else
            {
                
                model.CreateDate = DateTime.Now;
                model.CreatedBy = (long)Session["EmployeeId"];
                ddList.RecruitmentId = recruitmentService.CreateRecruitment(model).Data;
               
                
            }

            HRRecruitmentProcess hRRecruitmentProcess;
            foreach (var ProcessId in ddList.LookSelectionProcessList)
            {
                hRRecruitmentProcess = new HRRecruitmentProcess();
                hRRecruitmentProcess.ProcessId = ProcessId;
                hRRecruitmentProcess.RecruitmentId = ddList.RecruitmentId;
                hRRecruitmentProcessService.CreateProcess(hRRecruitmentProcess);
            }
            //  }

            return RedirectToAction("RecruitmentList");
            // return N;
        }
        //[CustomAuthorize(Permission = "EditCreateHRPolicy")]
        //[HttpPost]
        //public ActionResult Create(HRPolicy hrPolicy)
        //{

        //    if (hrPolicy.HRPolicyId > 0)
        //    {
        //        hrPolicyService.UpdateHRPolicy(hrPolicy);
        //    }
        //    else
        //    {
        //        hrPolicyService.CreateHRPolicy(hrPolicy);

        //    }
        //    return RedirectToAction("HRPolicyList");
        //}

        [CustomAuthorize(Permission = "ViewHolidaysList")]
        [HttpPost]
        public JsonResult ApproveRecruitmentStatusById(ApproveRecruitmentStatusViewModel model)
        {
            RecruitmentApprovalService recruitmentApprovalService=new RecruitmentApprovalService();
            RecruitmentApproval recruitmentApproval=new RecruitmentApproval();
            recruitmentApproval.EmployeeId = (long)Session["EmployeeId"];
            if(model.ForwardToEmployeeId > 0)
            recruitmentApproval.ForwardToEmployeeId = model.ForwardToEmployeeId;
            recruitmentApproval.RecruitmentId = model.RecruitmentId;
            recruitmentApproval.LookRecruitmentStatusId = model.LookRecruitmentStatusId;
            recruitmentApproval.Remarks = model.Comments;
            recruitmentApproval.ApprovedDate = DateTime.Now;
            recruitmentApprovalService.Create(recruitmentApproval);
           
            hrRecruitment.UpdateRecruitmentStatus(model.RecruitmentId, model.ForwardToEmployeeId, model.LookRecruitmentStatusId, (long)Session["EmployeeId"]);
           
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(Permission = "ViewHolidaysList")]
        [HttpGet]
        public JsonResult RecruitmentById(long id)
        {
            HRMSWorker hrmsWorker = new HRMSWorker();
            RecruitmentListViewModel recruitmentListViewModel = new RecruitmentListViewModel();
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
                           where Recruitment.RecruitmentId=" + id;
            
           recruitmentListViewModel = hrmsWorker.Repository.db.Database.SqlQuery<RecruitmentListViewModel>(sql).FirstOrDefault();
            IEnumerable<SelectListItem> employeeList = (from m in hrmsWorker.Repository.Read<Employee>() select m).AsEnumerable().Select(m => new SelectListItem() { Text = m.FullName, Value = m.EmployeeId.ToString() });
            recruitmentListViewModel.ForApprovalList= employeeList;

            sql = @"select RecruitmentApproval.*,Employee.FullName as ApproveByName,NextApproval.FullName as FarwardApprovalName 
                    from RecruitmentApproval
                    inner join Employee on Employee.EmployeeId = RecruitmentApproval.EmployeeId
                    left
                    join Employee NextApproval on NextApproval.EmployeeId = RecruitmentApproval.ForwardToEmployeeId
                    where RecruitmentApproval.RecruitmentId="+ id;
            recruitmentListViewModel.RecruitmentApprovalViewModel = hrmsWorker.Repository.db.Database.SqlQuery<RecruitmentApprovalViewModel>(sql).ToListSafely();
            // 

          
         
            return Json(recruitmentListViewModel, JsonRequestBehavior.AllowGet);
            //return testtest
        }

        [CustomAuthorize(Permission = "ViewHolidaysList")]

        public ActionResult RecruitmentList()
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
                             where Recruitment.CreatedBy=" + (long)Session["EmployeeId"] + @" order by Recruitment.RecruitmentId desc " ;

            var dbApplicant = hrmsWorker.Repository.db.Database.SqlQuery<RecruitmentListViewModel>(sql).ToListSafely();
           // var RecruitmentList = hrRecruitment.RecruitmentList();
            return View(dbApplicant);
        }
        public ActionResult ApprovalList()
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
                            order by Recruitment.RecruitmentId desc ";

             ViewBag.enumLookRecruitmentStatus = from EnumLookRecruitmentStatus status in Enum.GetValues(typeof(EnumLookRecruitmentStatus))
                                            select new
                                            {
                                                ID = (long)status,
                                                Name = status.GetStringValue()
                                            };

            var dbApplicant = hrmsWorker.Repository.db.Database.SqlQuery<RecruitmentListViewModel>(sql).ToListSafely();
            // var RecruitmentList = hrRecruitment.RecruitmentList();
            return View(dbApplicant);
        }
    }
}