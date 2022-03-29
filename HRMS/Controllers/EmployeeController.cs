using Data.HRMS;
using Library.Core.Services;
using Services.HRMS;
using Services.Look;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM.HRMS;
using VM.Look;

namespace HRMS.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmployeeServices employeeServices = new EmployeeServices();
        [CustomAuthorize(Permission = "ViewCreateEmployee")]
        public ActionResult Create(Guid? id)
        {
            try
            {


                var employeeList = employeeServices.EmployeeList();
                ViewBag.EmployeeList = new SelectList(employeeList.Data, "EmployeeId", "FullName");
                DropDownListClass modelEmployee = new DropDownListClass();
                HRPolicyService hRPolicyService = new HRPolicyService();
                if (id.IsNotNull())
                {
                    var employee = employeeServices.GetEmployeeByGuid(id);
                    long? empId = employee.Data.EmployeeId;
                    var education = employeeServices.GetEducationById(empId ?? 0);
                    var experience = employeeServices.GetExperienceById(empId ?? 0);
                    var dependents = employeeServices.GetDependentById(empId ?? 0);
                    var attachments = employeeServices.GetAttachmentById(empId ?? 0);
                    var policies = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(employee.Data.LookDesignationId ?? 0, 0, empId ?? 0, false);
                    //if (education.ResultType.Equals(ResultType.Exception))
                    //{
                    //    return RedirectToAction("No505", "Error");
                    //}
                    modelEmployee.EmployeeEducationList = education.Data;
                    modelEmployee.EmployeeExperienceHistoryList = experience.Data;
                    modelEmployee.EmployeeDependentList = dependents.Data;
                    modelEmployee.EmployeeAttachmentList = attachments.Data;
                    modelEmployee.LookPolicyList = policies.Data;
                    if (employee.ResultType.Equals(ResultType.Success))
                    {
                        if (employee.Data.IsNotNull())
                        {
                            var dbEmployee = employee.Data;
                            modelEmployee.EmployeeId = dbEmployee.EmployeeId;
                            modelEmployee.FirstName = dbEmployee.FirstName;
                            modelEmployee.LastName = dbEmployee.LastName;
                            modelEmployee.FatherName = dbEmployee.FatherName;
                            modelEmployee.HusbandName = dbEmployee.HusbandName;
                            modelEmployee.FullName = dbEmployee.FullName;
                            modelEmployee.Email = dbEmployee.Email;
                            modelEmployee.EmployeeGuid = dbEmployee.EmployeeGuid;
                            modelEmployee.MobileNo = dbEmployee.MobileNo;
                            modelEmployee.ConatctInfo = dbEmployee.ConatctInfo;
                            modelEmployee.CNIC = dbEmployee.CNIC;
                            modelEmployee.MaritalStatus = dbEmployee.MaritalStatus;
                            modelEmployee.PresentAddress = dbEmployee.PresentAddress;
                            modelEmployee.PermanentAddress = dbEmployee.PermanentAddress;
                            modelEmployee.Gender = dbEmployee.Gender;
                            modelEmployee.DateOfBirth = dbEmployee.DateOfBirth;
                            modelEmployee.ShortSummary = dbEmployee.ShortSummary;
                            modelEmployee.ICEContactInfo = dbEmployee.ICEContactInfo;
                            modelEmployee.IsDisable = dbEmployee.IsDisable;
                            modelEmployee.DisableDetail = dbEmployee.DisableDetail;
                            modelEmployee.HRRemarks = dbEmployee.HRRemarks;
                            modelEmployee.NoYearsOfExperience = dbEmployee.NoYearsOfExperience;
                            modelEmployee.StartSalary = dbEmployee.StartSalary;
                            modelEmployee.ReferenceName = dbEmployee.ReferenceName;
                            modelEmployee.ReferenceEmail = dbEmployee.ReferenceEmail;
                            modelEmployee.DateOfJoining = dbEmployee.DateOfJoining;
                            modelEmployee.ManagerId = dbEmployee.ManagerId;
                            modelEmployee.LookEmployeeTypeId = dbEmployee.LookEmployeeTypeId;
                            modelEmployee.WorkingHourPolicyId = dbEmployee.WorkingHourPolicyId;
                            modelEmployee.LookDepartmentId = dbEmployee.LookDepartmentId;
                            modelEmployee.LookDesignationId = dbEmployee.LookDesignationId;
                            modelEmployee.ExitDate = dbEmployee.ExitDate;
                            return View(modelEmployee);
                        }
                        return RedirectToAction("EmployeeList");
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
            catch (Exception ex)
            {
                return RedirectToAction("No505", "Error");
            }

        }

        [HttpPost]
        [CustomAuthorize(Permission = "EditCreateEmployee")]
        public ActionResult Create(DropDownListClass ddList)
        {

            Employee model = new Employee();
            if (ddList.EmployeeId > 0)
            {
                employeeServices.UpdateEmployee(ddList);
                //employeeServices.UpdateEducation(ddList);               
                return RedirectToAction("Create", new { id = ddList.EmployeeGuid });
            }
            else
            {
                model = employeeServices.EmployeeMaping(ddList).Data;
                employeeServices.CreateEmployee(model);
                return RedirectToAction("Create", new { id = model.EmployeeGuid });
            }

        }

        [HttpPost]
        [CustomAuthorize(Permission = "CreateEmployeeEducation")]
        public ActionResult CreateEducation(EmployeeEducationViewModel obj)
        {
            var response = employeeServices.CreateEducation(obj);
            return Json(response.Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize(Permission = "CreateEmployeeExperience")]
        public ActionResult CreateExperience(EmployeeExperienceViewModel obj)
        {
            EmployeeServices employeeServices = new EmployeeServices();
            var response = employeeServices.CreateExperience(obj);
            return Json(response.Message, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [CustomAuthorize(Permission = "CreateEmployeeDependent")]
        public ActionResult CreateDependent(EmployeeDependentViewModel obj)
        {
            EmployeeServices employeeServices = new EmployeeServices();
            var response = employeeServices.CreateDependent(obj);
            return Json(response.Message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateEmployeePolicy(LookPolicyViewModel obj)
        {

            EmployeeServices employeeServices = new EmployeeServices();
            var response = employeeServices.UpdateEmployeePolicy(obj);
            return Json(response.Message, JsonRequestBehavior.AllowGet);
        }


        [CustomAuthorize(Permission = "ViewEmployeeList")]
        public ActionResult EmployeeList()
        {
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            if (roleid == 0 || roleid == 1)
            {
                var employeeList = employeeServices.GetEmployeeList();
                ///  if(departmentList.ResultType==ResultType.Success )
                return View(employeeList);
            }
            else if (roleid == 2 || roleid == 3)
            {
                string LookOrganizationIds = Convert.ToString(System.Web.HttpContext.Current.Session["LookOrganizationIds"]);
                if (LookOrganizationIds.IsNullOrEmpty())
                    LookOrganizationIds = Convert.ToString(System.Web.HttpContext.Current.Session["LookOrganizationId"]);
                var employeeList = employeeServices.GetEmployeeList(LookOrganizationIds);
                ///  if(departmentList.ResultType==ResultType.Success )
                return View(employeeList);
            }
            else
            {
                var employeeList = employeeServices.GetEmployeeList(Convert.ToInt64(System.Web.HttpContext.Current.Session["EmployeeId"]));
                ///  if(departmentList.ResultType==ResultType.Success )
                return View(employeeList);
            }
        }
        public JsonResult EmployeesList(long department, long designation)
        {
            var employeeList = employeeServices.GetEmployeesbyDeignationDepartment(designation, department);
            ///  if(departmentList.ResultType==ResultType.Success )
            return Json(employeeList.Data, JsonRequestBehavior.AllowGet);
        }



        [CustomAuthorize(Permission = "UploadEmployeeAttachment")]
        [HttpPost]
        public ActionResult Upload(long main_id)
        {
            var model = new EmployeeAttachment();
            HRMSWorker hrmsWorker = new HRMSWorker();
            string resume = string.Empty;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                var fileExtension = Path.GetExtension(fileName);
                resume = Guid.NewGuid().ToString();
                // var path = Path.Combine(Server.MapPath("~/uploads/employee"), fileName); 
                // var patht = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadEmployee"]);
                var path = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["UploadEmployee"]) + resume + fileExtension;
                file.SaveAs(path);
                resume = resume + fileExtension;
                model.AttachmentTitle = fileName;
                model.AttachmentPath = resume;
                model.EmployeeId = main_id;
                hrmsWorker.Repository.Create(model);
                hrmsWorker.SaveChanges();
            }
            return Json("Saved", JsonRequestBehavior.AllowGet);

        }

      //  [CustomAuthorize(Permission = "GetTimePolicyEmployee")]
        public ActionResult GetTimePolicyId(int  id)
        {
            LookPolicyService lookPolicyService = new LookPolicyService();
           var ok= lookPolicyService.GetPolicyByHRPolicyCaptionId(id, 3);
            return View(ok);
        }
    }
}