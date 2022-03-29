using Library.Lookups;
using Services.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM_CRM.Controllers
{
    public class LeaveManagementController : Controller
    {
        EmployeeLeaveService employeeLeaveService = new EmployeeLeaveService();
        // GET: LeaveManagement
        [CustomAuthorize(Permission = "HRLeaveList")]
        public ActionResult LeaveList()
        {
            long EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            HRPolicyService hRPolicyService = new HRPolicyService();
            var leaveList = employeeLeaveService.TeamLeaveList(0).Data;
            var gg = leaveList.Where(x => x.LookLeaveStatusId > (long)EnumLeaveStatus.Submitted).ToListSafely();
            return View(gg);
        }
        [CustomAuthorize(Permission = "TeamConfirmLeave")]
        [HttpPost]
        public JsonResult ConfirmLeave(long id)
        {
            var leaveSummary = employeeLeaveService.EmployeeLeaveListById(id).Data;
            string message = "";

            // employeeLeaveService.makeDraftLeave(leaveSummary.EmployeeId, id);
            if (leaveSummary.LookLeaveStatusId == (long)EnumLeaveStatus.Approved)
            {
                employeeLeaveService.UpdateLeaveStatus((long)EnumLeaveStatus.Confirmed, id);
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