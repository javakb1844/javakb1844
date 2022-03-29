using Data.HRMS;
using Library.Core.Services;
using Services.HRMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM_CRM.Controllers
{
    public class PublicHolidaysController : Controller
    {
        PublicHolidaysService publicHolidaysService = new PublicHolidaysService();
        // GET: PublicHolidays
        [CustomAuthorize(Permission = "ViewCreateHolidays")]
        public ActionResult Create(long? id)
        {
            try
            {  
                if (id.IsNotNull())
                {
                    var publicHoliday = publicHolidaysService.GetPublicHolidayById(id ?? 0);
                    if (publicHoliday.ResultType.Equals(ResultType.Success))
                    {
                        if (publicHoliday.Data.IsNotNull())
                        {
                            return View(publicHoliday.Data);
                        }
                        return RedirectToAction("HolidaysList");
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
        [CustomAuthorize(Permission = "EditHolidays")]
        [HttpPost]
        public ActionResult Create(PublicHoliday publicHoliday)
        {
            //LookDepartmentService lookDepartmentService = new LookDepartmentService();
            if (publicHoliday.PublicHolidayId > 0)
            {
                publicHolidaysService.UpdatePublicHoliday(publicHoliday);
            }
            else
            {
                publicHolidaysService.CreatePublicHoliday(publicHoliday);

            }
            return RedirectToAction("HolidaysList");
        }
        [CustomAuthorize(Permission = "ViewHolidaysList")]
        public ActionResult HolidaysList()
        {  
            var publicHolidaysList = publicHolidaysService.PublicHolidaytList();
            return View(publicHolidaysList.Data);
        }
    }
}