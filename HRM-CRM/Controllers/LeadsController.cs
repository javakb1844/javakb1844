using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Core.Services;
using Services.CRM;
using VM.CRM;

namespace HRM_CRM.Controllers
{
    public class LeadsController : Controller
    {
        // GET: Leads
        public ActionResult Leads()
        {
            LeadsService LeadsService = new LeadsService();
            Result<List<ListLeadViewModelXML>> ok = LeadsService.GetEmployeeAttendanceNew();
            return View(ok);
        }
    }
}