using Data.HRMS;
using Library.Core.Services;
using Library.Lookups;
using Services.HRMS;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using VM.HRMS;
using VM.Look;

namespace HRM_CRM.Controllers
{
    public class EmployeeLeaveController : Controller
    {
        EmployeeLeaveService employeeLeaveService = new EmployeeLeaveService();
        // GET: Leave test
        [CustomAuthorize(Permission = "ViewCreateLeave")]
        public ActionResult Create(long? id)
        {
            try
            {
                EmployeeAppliedLeaveViewModel employeeAppliedLeaveViewModel = new EmployeeAppliedLeaveViewModel();
                // var result = Session["UserId"] as List<LeftMenuViewModel>;
                var employeeId = Session["EmployeeId"];
                var userEmail = Session["UserEmail"];

                EmployeeServices employeeServices = new EmployeeServices();
                var employeeData = employeeServices.GetEmployeeById((long)employeeId);
                //  EmployeeLeave modelEmployeeLeave = new EmployeeLeave();
                HRPolicyService hRPolicyService = new HRPolicyService();
                // var leavePolicy= hRPolicyService.GetHRPolicyByDesignation(1, 1);
                List<LookPolicyViewModel> employeePolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(employeeData.Data.LookDesignationId, 0, (long)employeeId, true).Data;

                // (long)EnumPolicyType.Leave
                employeeAppliedLeaveViewModel.AvailedLeave = 20;
                employeeAppliedLeaveViewModel.Email = userEmail.ToString();
                employeeAppliedLeaveViewModel.EmployeeNum = employeeData.Data.EmployeeNum;
               
                if (employeePolicy.CountedPositive())
                {
                    ViewBag.LeavePolicy = employeePolicy.Where(x => x.LookPolicyGroupId == (long)EnumPolicyType.Leave);
                    ViewBag.ShiftTimePolicy = employeePolicy.Where(x => x.LookPolicyGroupId == (long)EnumPolicyType.ShiftTiming);
                    ViewBag.WeekWorkingDays = employeePolicy.Where(x => x.LookPolicyGroupId == (long)EnumPolicyType.WeekWorkingDays);
                }
                //HttpContext.Current.Session["UserId"]
                //  ViewBag.Departments = new SelectList(departments.Data, "LookDepartmentId", "DepartmentName");

                /*              var GenderselectList = new SelectList(
                     new List<SelectListItem>
                     {
                          new SelectListItemCustom {Selected = true,Text = "Male", Value = "M"},
                          new SelectListItemCustom {Selected = false,Text = "Female", Value = "F"},
                          new SelectListItemCustom {Selected = false,Text = "Not Prefered", Value = "N"},
                     }, "Value", "Text");



                              var item = new SelectListItemCustom { Selected = true, Value = "123", Text = "Australia", itemsHtmlAttributes = new Dictionary<string, object> { { "countrycode", "au" } } };
                              var items = new List<SelectListItemCustom> { item };

              */
                return View(employeeAppliedLeaveViewModel);

                /*  if (id.IsNotNull())
                  {
                      var employeeLeave = employeeLeaveService.GetEmployeeLeaveById(id ?? 0);
                      if (employeeLeave.ResultType.Equals(ResultType.Success))
                      {
                          if (employeeLeave.Data.IsNotNull())
                          {                           
                              return View(employeeLeave.Data);
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
                  }*/
            }
            catch
            {
                return RedirectToAction("No505", "Error");
            }
        }

        [CustomAuthorize(Permission = "EditEmpCreateloyeeLeave")]
        [HttpPost]
        public ActionResult Create(EmployeeAppliedLeaveViewModel employeeLeave)
        {

            EmployeeLeaveSummary employeeLeaveSummary = new EmployeeLeaveSummary();
            employeeLeaveSummary.EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            employeeLeaveSummary.CreatedBy = Convert.ToInt64(Session["EmployeeId"]);
            employeeLeaveSummary.StartLeaveDate = employeeLeave.LeaveFromDate;
            employeeLeaveSummary.EndLeaveDate = employeeLeave.LeaveToDate;
            employeeLeaveSummary.EmployeePolicyId = employeeLeave.EmployeePolicyId;
            employeeLeaveSummary.ShortLeaves = employeeLeave.CurrentshortLeaves;
            employeeLeaveSummary.HalfLeaves = employeeLeave.CurrentHalfLeaves;
            employeeLeaveSummary.Leaves = employeeLeave.CurrentLeaves;
            employeeLeaveSummary.CreateDate = DateTime.Now;
            employeeLeaveSummary.StatusUpdateDate = DateTime.Now;
            employeeLeaveSummary.LookLeaveStatusId = (long)EnumLeaveStatus.Draft;
            long summaryId = employeeLeaveService.CreateEmployeeLeaveSummary(employeeLeaveSummary).Data;

            var ok = leaveinsertion(employeeLeave, summaryId, true);

            return RedirectToAction("EmployeeLeaveList");
        }

        [CustomAuthorize(Permission = "EmpLeaveCount")]
        [HttpPost]
        public JsonResult LeaveCount(EmployeeAppliedLeaveViewModel employeeLeave)
        {
            return Json(leaveinsertion(employeeLeave, 0, false), JsonRequestBehavior.AllowGet); ;
        }

        [CustomAuthorize(Permission = "SubmitLeave")]
        [HttpPost]
        public JsonResult SubmitLeave(long id)
        {
            var leaveSummary = employeeLeaveService.EmployeeLeaveListById(id).Data;
            long EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            string message = "";
            if (leaveSummary.EmployeeId == EmployeeId)
            {
                bool flag = true;
                HRPolicyService hRPolicyService = new HRPolicyService();
                List<LookPolicyViewModel> leavePolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(1, 0, EmployeeId, true).Data;
                List<EmployeeLeave> availedLeaveList = employeeLeaveService.EmployeeLeaveDetailListById(EmployeeId).Data;
                LookPolicyViewModel tt = leavePolicy.Where(x => x.EmployeePolicyId == leaveSummary.EmployeePolicyId).FirstOrDefault();

                var AvailedShortLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave && x.LookLeaveStatusId > 1).ToListSafely().Count;
                var AvailedHalfLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave && x.LookLeaveStatusId > 1).ToListSafely().Count;

                var PolicyShortLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave).FirstOrDefault().EmployeePolicyValue;
                var PolicyHalfLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave).FirstOrDefault().EmployeePolicyValue;
                if (leaveSummary.ShortLeaves > (Convert.ToInt64(PolicyShortLeave) + AvailedShortLeave) || leaveSummary.HalfLeaves > (Convert.ToInt64(PolicyHalfLeave) + AvailedHalfLeave))
                {
                    flag = false;
                    message = "applied half/short Leave is out of limit ";
                }
                if (!(tt.LookPolicyId == (long)EnumLeavePolicy.ShortLeave || tt.LookPolicyId == (long)EnumLeavePolicy.HalfLeave))
                {
                    var AvailedFullLeave = availedLeaveList.Where(x => x.EmployeePolicyId == leaveSummary.EmployeePolicyId && x.LookLeaveStatusId>1).ToListSafely().Count;
                    var remaining = Convert.ToInt64(tt.EmployeePolicyValue) - AvailedFullLeave;
                    if (leaveSummary.Leaves > remaining)
                    {
                        flag = false;
                        message = message + "\n applied Leave is out of limit ";
                    }
                }

                if (flag == true)
                {
                    employeeLeaveService.UpdateLeaveStatus((long)EnumLeaveStatus.Submitted, id);
                    message = "Status Updated ";
                }
            }
            else
            {

            }

            return Json(message, JsonRequestBehavior.AllowGet);

        }
        //
        [CustomAuthorize(Permission = "DeleteLeave")]
        [HttpPost]
        public JsonResult DeleteLeave(long id)
        {
            var leaveSummary = employeeLeaveService.EmployeeLeaveListById(id).Data;
            long EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            string message = "";
            if (leaveSummary.EmployeeId == EmployeeId)
            {
                    employeeLeaveService.DeleteLeaves(leaveSummary.EmployeeId, id);
                    message = "Leave Deleted ";
            }
            else
            {
                message = "please login again";
            }

            return Json(message, JsonRequestBehavior.AllowGet);

        }

        //makeDraftLeave
        [CustomAuthorize(Permission = "makeDraftLeave")]
        [HttpPost]
        public JsonResult makeDraftLeave(long id)
        {
            var leaveSummary = employeeLeaveService.EmployeeLeaveListById(id).Data;
            long EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            string message = "";
            if (leaveSummary.EmployeeId == EmployeeId)
            {
                // employeeLeaveService.makeDraftLeave(leaveSummary.EmployeeId, id);
                if (leaveSummary.LookLeaveStatusId <= 2)
                {
                    employeeLeaveService.UpdateLeaveStatus((long)EnumLeaveStatus.Draft, id);
                    message = "Status Updated ";
                }
                else
                {
                    message = "Access Denied";
                }
   
            }
            else
            {
                message = "please login again";
            }

            return Json(message, JsonRequestBehavior.AllowGet);

        }
        [CustomAuthorize(Permission = "ViewEmployeeLeaveDetailList")]
        public ActionResult LeaveDetailList(long id, long? empId)
        {
            long? EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            if (empId.IsNotNull())
                EmployeeId = empId;
            var leaveList = employeeLeaveService.EmployeeLeaveDetailListById(EmployeeId, id);
            return View(leaveList.Data);
        }
        [CustomAuthorize(Permission = "ViewLeaveList")]
        public ActionResult EmployeeLeaveList()
        {
            long EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            HRPolicyService hRPolicyService = new HRPolicyService();
            List<LookPolicyViewModel> leavePolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(1, 0, EmployeeId, true).Data;
            ViewBag.employeePolicy = leavePolicy;
            List<EmployeeLeave> availedLeaveList = employeeLeaveService.EmployeeLeaveDetailListById(EmployeeId).Data;
            ViewBag.AvailedShortLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave && x.LookLeaveStatusId > 1).ToListSafely().Count;
            ViewBag.AvailedHalfLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave && x.LookLeaveStatusId > 1).ToListSafely().Count;
            if (leavePolicy.Count > 0)
            {
                var TempPolicyShortLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave).FirstOrDefault();
                if(TempPolicyShortLeave.IsNotNull())
                ViewBag.PolicyShortLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave).FirstOrDefault().EmployeePolicyValue;
                else
                ViewBag.PolicyShortLeave = "0";

                var TempPolicyHalfLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave).FirstOrDefault();
                if (TempPolicyHalfLeave.IsNotNull())
                    ViewBag.PolicyHalfLeave = leavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave).FirstOrDefault().EmployeePolicyValue;
                else
                    ViewBag.PolicyHalfLeave = "0";
            }
            ViewBag.EmployeeLeaves = availedLeaveList;

            // ViewBag.AvailedFullLeave = availedLeaveList.Where(x => x.LookPolicyId == employeeLeave.LookPolicyId).ToListSafely().Count;


            var leaveList = employeeLeaveService.EmployeeLeaveListByEmpId(EmployeeId);
            return View(leaveList.Data);
        }

        private Result<EmployeeLeaveSummaryViewModel> leaveinsertion(EmployeeAppliedLeaveViewModel employeeLeave, long summaryId, bool IsPost)
        {
            var employeeLeaveSummaryViewModelResult = new Result<EmployeeLeaveSummaryViewModel>();
            employeeLeave.EmployeeId = Convert.ToInt64(Session["EmployeeId"]);
            HRPolicyService hRPolicyService = new HRPolicyService();
            EmployeeLeave EmployeeLeave = new EmployeeLeave();
            DateTime newStartLeaveDate;
            DateTime newToLeaveDate;
            EmployeeLeaveSummaryViewModel employeeLeaveSummaryViewModel = new EmployeeLeaveSummaryViewModel();

            PublicHolidaysService publicHolidaysService = new PublicHolidaysService();
            try
            {
                List<LookPolicyViewModel> employeePolicy = hRPolicyService.GetEmployeePolicyPolicyByDesignationAndEmployeeId(1, 0, employeeLeave.EmployeeId, true).Data;

                List<PublicHoliday> holidaysList = publicHolidaysService.GetPublicHolidayBetweenDates(employeeLeave.LeaveFromDate, employeeLeave.LeaveToDate).Data;
                List<DateTime> holidaysArray = holidaysList.Select(x => x.HoIidayDate).ToListSafely();

                List<LookPolicyViewModel> employeeLeavePolicy = employeePolicy.Where(x => x.LookPolicyGroupId == (long)EnumPolicyType.Leave).ToListSafely();
                List<LookPolicyViewModel> employeeShiftTimePolicy = employeePolicy.Where(x => x.LookPolicyGroupId == (long)EnumPolicyType.ShiftTiming).ToListSafely();

                var WeekWorkingDays = employeePolicy.Where(x => x.LookPolicyGroupId == (long)EnumPolicyType.WeekWorkingDays && Convert.ToInt64(x.EmployeePolicyValue) > 0).ToArray();
                var arrayWorkingDays = WeekWorkingDays.Select(x => x.PolicyName).ToListSafely();
                List<string> offDays = Enum.GetNames(typeof(DayOfWeek)).ToListSafely().Where(y => !arrayWorkingDays.Contains(y)).ToListSafely();


                /////////////// start date       
                DateTime StartCheckInDateTimeFormated = employeeLeaveService.AdjustDateWithShiftTiming(employeeLeave.LeaveFromDate.ToShortDateString(), employeeShiftTimePolicy[0].EmployeePolicyValue).Data;
                DateTime startCheckOutDateTimeFormated = employeeLeaveService.AdjustDateWithShiftTiming(employeeLeave.LeaveFromDate.ToShortDateString(), employeeShiftTimePolicy[1].EmployeePolicyValue).Data;
                //////////////////////// to date
                DateTime ToCheckInDateTimeFormated = employeeLeaveService.AdjustDateWithShiftTiming(employeeLeave.LeaveToDate.ToShortDateString(), employeeShiftTimePolicy[0].EmployeePolicyValue).Data;
                DateTime ToCheckOutDateTimeFormated = employeeLeaveService.AdjustDateWithShiftTiming(employeeLeave.LeaveToDate.ToShortDateString(), employeeShiftTimePolicy[1].EmployeePolicyValue).Data;

                //////////////////////////////////////
                List<EmployeeLeave> availedLeaveList = employeeLeaveService.EmployeeLeaveDetailListById(employeeLeave.EmployeeId).Data;
                employeeLeaveSummaryViewModel.AvailedShortLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave && x.LookLeaveStatusId>1).ToListSafely().Count;
                employeeLeaveSummaryViewModel.AvailedHalfLeave = availedLeaveList.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave && x.LookLeaveStatusId > 1).ToListSafely().Count;
                employeeLeaveSummaryViewModel.AvailedFullLeave = availedLeaveList.Where(x => x.LookPolicyId == employeeLeave.LookPolicyId && x.LookLeaveStatusId > 1).ToListSafely().Count;
                var temTotalShortLeave = employeeLeavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.ShortLeave).FirstOrDefault();
                if (temTotalShortLeave.IsNotNull())
                    employeeLeaveSummaryViewModel.TotalShortLeave = Convert.ToInt64(temTotalShortLeave.EmployeePolicyValue);
                else
                    employeeLeaveSummaryViewModel.TotalShortLeave = 0;
                var temTotalHalfLeave = employeeLeavePolicy.Where(x => x.LookPolicyId == (long)EnumLeavePolicy.HalfLeave).FirstOrDefault();
                if (temTotalHalfLeave.IsNotNull())
                    employeeLeaveSummaryViewModel.TotalHalfLeave = Convert.ToInt64(temTotalHalfLeave.EmployeePolicyValue);
                else
                    employeeLeaveSummaryViewModel.TotalHalfLeave = 0;
                if (employeeLeave.LookPolicyId == (long)EnumLeavePolicy.ShortLeave)
                {
                    var isHoliday = holidaysArray.Contains(employeeLeave.LeaveFromDate.Date);
                    var isOffday = offDays.Contains(employeeLeave.LeaveFromDate.Date.DayOfWeek.ToString());

                   // employeeLeaveSummaryViewModel.TotalFullLeave = Convert.ToInt64(employeeLeavePolicy.Where(x => x.EmployeePolicyId == employeeLeave.EmployeePolicyId).FirstOrDefault().PolicyValue);
                    employeeLeaveSummaryViewModel.CurrentFullLeave = 0;// employeeLeaveService.DaysLeft(newStartLeaveDate, newToLeaveDate, true, holidaysArray, offDays);
                    if (isHoliday == false && isOffday == false)
                        employeeLeaveSummaryViewModel.CurrentShortLeave = 1;
                    else
                        employeeLeaveSummaryViewModel.CurrentShortLeave = 0;
                    employeeLeaveSummaryViewModel.CurrentHalfLeave = 0;
                    bool tt = false;
                    if (IsPost == true && isHoliday == false && isOffday == false)
                        tt = employeeLeaveService.CreateEmployeeLeave(employeeLeave, summaryId).Data;
                }
                else if (employeeLeave.LookPolicyId == (long)EnumLeavePolicy.HalfLeave)
                {
                    var isHoliday = holidaysArray.Contains(employeeLeave.LeaveFromDate.Date);
                    var isOffday = offDays.Contains(employeeLeave.LeaveFromDate.Date.DayOfWeek.ToString());

                   // employeeLeaveSummaryViewModel.TotalFullLeave = Convert.ToInt64(employeeLeavePolicy.Where(x => x.EmployeePolicyId == employeeLeave.EmployeePolicyId).FirstOrDefault().PolicyValue);
                    employeeLeaveSummaryViewModel.CurrentFullLeave = 0;// employeeLeaveService.DaysLeft(newStartLeaveDate, newToLeaveDate, true, holidaysArray, offDays);
                    employeeLeaveSummaryViewModel.CurrentShortLeave = 0;
                    if (isHoliday == false && isOffday == false)
                        employeeLeaveSummaryViewModel.CurrentHalfLeave = 1;
                    else
                        employeeLeaveSummaryViewModel.CurrentHalfLeave = 0;
                    bool tt = false;
                    if (IsPost == true && isHoliday == false && isOffday == false)
                        tt = employeeLeaveService.CreateEmployeeLeave(employeeLeave, summaryId).Data;
                }
                else
                {
                    //"From date in between";
                    var result = employeeLeaveService.AdjustLeaveDate(StartCheckInDateTimeFormated, startCheckOutDateTimeFormated, employeeLeave.LeaveFromDate, holidaysArray, offDays, false);
                    newStartLeaveDate = result.Data;

                    if (Convert.ToInt64(result.Message) == 1)
                    {
                        employeeLeaveSummaryViewModel.CurrentShortLeave = 1;
                        bool tt = false;
                        if (IsPost == true)
                            tt = employeeLeaveService.CreateEmployeeLeave(summaryId, employeeLeave.EmployeeId, employeeLeavePolicy, employeeLeave.LeaveFromDate, (long)EnumLeavePolicy.ShortLeave).Data;

                    }
                    else if (Convert.ToInt64(result.Message) == 2)
                    {
                        employeeLeaveSummaryViewModel.CurrentHalfLeave = 1;
                        //////////////////
                        bool tt = false;
                        if (IsPost == true)
                            tt = employeeLeaveService.CreateEmployeeLeave(summaryId, employeeLeave.EmployeeId, employeeLeavePolicy, employeeLeave.LeaveFromDate, (long)EnumLeavePolicy.HalfLeave).Data;

                    }
                    var resultEnd = employeeLeaveService.AdjustLeaveDate(ToCheckInDateTimeFormated, ToCheckOutDateTimeFormated, employeeLeave.LeaveToDate, holidaysArray, offDays, true);
                    newToLeaveDate = resultEnd.Data;
                    if (Convert.ToInt64(resultEnd.Message) == 1)
                    {
                        employeeLeaveSummaryViewModel.CurrentShortLeave = employeeLeaveSummaryViewModel.CurrentShortLeave + 1;
                        bool tt = false;
                        if (IsPost == true)
                            tt = employeeLeaveService.CreateEmployeeLeave(summaryId, employeeLeave.EmployeeId, employeeLeavePolicy, ToCheckInDateTimeFormated, (long)EnumLeavePolicy.ShortLeave).Data;

                    }
                    else if (Convert.ToInt64(resultEnd.Message) == 2)
                    {
                        employeeLeaveSummaryViewModel.CurrentHalfLeave = employeeLeaveSummaryViewModel.CurrentHalfLeave + 1;
                        bool tt = false;
                        if (IsPost == true)
                            tt = employeeLeaveService.CreateEmployeeLeave(summaryId, employeeLeave.EmployeeId, employeeLeavePolicy, ToCheckInDateTimeFormated, (long)EnumLeavePolicy.HalfLeave).Data;

                    }
                    var tempvalue = employeeLeavePolicy.Where(x => x.EmployeePolicyId == employeeLeave.EmployeePolicyId).FirstOrDefault().EmployeePolicyValue;
                    employeeLeaveSummaryViewModel.TotalFullLeave = Convert.ToInt64(tempvalue);
                    if (IsPost == true)
                        employeeLeaveSummaryViewModel.CurrentFullLeave = employeeLeaveService.DaysLeft(newStartLeaveDate, newToLeaveDate, true, holidaysArray, offDays, summaryId, employeeLeave);
                    else
                        employeeLeaveSummaryViewModel.CurrentFullLeave = employeeLeaveService.DaysLeft(newStartLeaveDate, newToLeaveDate, true, holidaysArray, offDays);

                }
                employeeLeaveSummaryViewModelResult.Data = employeeLeaveSummaryViewModel;
                employeeLeaveSummaryViewModelResult.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                employeeLeaveSummaryViewModelResult.Data = null;
                employeeLeaveSummaryViewModelResult.ResultType = ResultType.Exception;
                employeeLeaveSummaryViewModelResult.Exception = e;
                employeeLeaveSummaryViewModelResult.Message = e.GetOriginalException().Message;
            }

            return employeeLeaveSummaryViewModelResult;
        }

    }
}