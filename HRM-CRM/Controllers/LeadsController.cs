using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Core.Services;
using Library.Lookups;
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
            Result<List<ListLeadViewModelXML>> ok = LeadsService.GetLeadsList();
            return View(ok);
        }
        public ActionResult _LeadsColumnSettings(int aid)
        {
            LeadsService LeadsService = new LeadsService();
            Result<List<ListLeadListCoulmnSettingViewModel>> ok = LeadsService.GetLeadsColumnSettings(aid);
            return View(ok);
        }
        [HttpPost]
        public ActionResult _LeadsColumnSettings(ListLeadListCoulmnSettingPostViewModel model)
        {
            LeadsService LeadsService = new LeadsService();
            Result<List<ListLeadListCoulmnSettingViewModel>> ok = LeadsService.GetLeadsColumnSettings(model, EnumLookCustTableInfo.LeadListColumnSetting);
            return View(ok);
        }
        [HttpPost]
        public ActionResult _LeadsEditColumnSettings(ListLeadListCoulmnSettingPostViewModel model)
        {
            LeadsService LeadsService = new LeadsService();
            Result<List<ListLeadListCoulmnSettingViewModel>> ok = LeadsService.GetLeadsColumnSettings(model, EnumLookCustTableInfo.LeadEditColumnSetting);
            return View(ok);
        }
        
        //[HttpPost]
        public ActionResult _LeadsColumnDataById()
        {
            LeadSelectionColumnByLidViewModel model = new LeadSelectionColumnByLidViewModel();
            LeadsService LeadsService = new LeadsService();
            Result<List<ListLeadViewModelXML>> ok = LeadsService.GetLeadsListByLid(model);
            ViewBag.Lid = model.LID;
            string hh = "[{\"LeadSourceId\":1,\"LeadSource\":\"Facebook\"},{\"LeadSourceId\":2,\"LeadSource\":\"Website\"},{\"LeadSourceId\":3,\"LeadSource\":\"LIveChat\"}]";
            var okh = hh.Replace("[", "").Replace("]", "").Replace("}","").Split('{');
            var testArray = okh.Where(x => x.IsNotNullOrEmpty()).ToArray();

            return View(ok);
        }
    }
}