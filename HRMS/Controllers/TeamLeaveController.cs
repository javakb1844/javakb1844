using Data.HRMS;
using Library.Lookups;
using Services.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM.HRMS;

namespace HRMS.Controllers
{
    public class TeamLeaveController : Controller
    {
        EmployeeLeaveService employeeLeaveService = new EmployeeLeaveService();
        // GET: TeamLeave
        [CustomAuthorize(Permission = "ViewTeamLeaveList")]
        public ActionResult TeamLeaveList()
        {
            long EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            HRPolicyService hRPolicyService = new HRPolicyService();
            /* List<LookPolicyViewModel> leavePolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(1, 0, EmployeeId, true).Data;
             ViewBag.employeePolicy = leavePolicy;
             List<EmployeeLeave> availedLeaveList = employeeLeaveService.EmployeeLeaveDetailListById(EmployeeId).Data;
             ViewBag.AvailedShortLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave && x.LookLeaveStatusId > 1).ToListSafely().Count;
             ViewBag.AvailedHalfLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave && x.LookLeaveStatusId > 1).ToListSafely().Count;
             if (leavePolicy.Count > 0)
             {
                 ViewBag.PolicyShortLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave).FirstOrDefault().EmployeePolicyValue;
                 ViewBag.PolicyHalfLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave).FirstOrDefault().EmployeePolicyValue;
             }
             ViewBag.EmployeeLeaves = availedLeaveList;
             */
            // ViewBag.AvailedFullLeave = availedLeaveList.Where(x => x.LookPolicyId == employeeLeave.LookPolicyId).ToListSafely().Count;

          /*  EmployeeServices employeeServices = new EmployeeServices();
               List<Employee> empList= employeeServices.EmployeeListByManagerId(EmployeeId).Data;
            ViewBag.Employee = empList;
             long[] teamArray = empList.Select(x => x.EmployeeId).ToArray();*/
            var leaveList = employeeLeaveService.TeamLeaveList(EmployeeId).Data;
            var gg = leaveList.Where(x => x.LookLeaveStatusId > (long)EnumLeaveStatus.Draft).ToListSafely();
            return View(gg);
        }

        [CustomAuthorize(Permission = "TeamLeaveApprove")]
        [HttpPost]
        public JsonResult ApproveLeave(long id)
        {
            var leaveSummary = employeeLeaveService.EmployeeLeaveListById(id).Data;
            string message = "";
           
                // employeeLeaveService.makeDraftLeave(leaveSummary.EmployeeId, id);
                if (leaveSummary.LookLeaveStatusId == (long)EnumLeaveStatus.Submitted)
                {
                    employeeLeaveService.UpdateLeaveStatus((long)EnumLeaveStatus.Approved, id);
                    message = "Status Updated ";
                }
                else
                {
                    message = "Access Denied";
                }

           

            return Json(message, JsonRequestBehavior.AllowGet);

        }

    }
}