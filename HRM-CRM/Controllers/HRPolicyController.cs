using Data.HRMS;
using Library.Core.Services;
using Services.HRMS;
using Services.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM.HRMS;

namespace HRM_CRM.Controllers
{
    public class HRPolicyController : Controller
    {
        // GET: HRPolicy
        HRPolicyService hrPolicyService = new HRPolicyService();
        [CustomAuthorize(Permission = "ViewCreateHRPolicy")]
        public ActionResult Create(long? id)
        {
            try
            {
                HRPolicy modelHRPolicy = new HRPolicy();
                LookPolicyService lookPolicyService = new LookPolicyService();
                LookDesignationService lookDesignationService = new LookDesignationService();               
                var lookPolicyList = lookPolicyService.PolicyList();
                ViewBag.LookPolicyList = new SelectList(lookPolicyList.Data, "LookPolicyId", "PolicyName");
                var lookDesignationList = lookDesignationService.GetDesignationList();
                ViewBag.LookDesignationList = new SelectList(lookDesignationList.Data, "LookDesignationId", "DesignationName");
                ViewBag.HrPolicyDetailList = hrPolicyService.PolicyGroupDetailList(id??0).Data;
                if (id.IsNotNull())
                {
                    var hrPolicy = hrPolicyService.GetHRPolicyById(id ?? 0);

                    if (hrPolicy.ResultType.Equals(ResultType.Success))
                    {
                        if (hrPolicy.Data.IsNotNull())
                        {
                           // var dbDepartment = hrPolicy.Data;
                           // modelHRPolicy.DepartmentName = dbDepartment.DepartmentName;
                            return View(hrPolicy.Data);
                        }
                        return RedirectToAction("HRPolicyList");
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
        [CustomAuthorize(Permission = "EditCreateHRPolicy")]
        [HttpPost]
        public ActionResult Create(HRPolicy hrPolicy)
        {
           
            if (hrPolicy.HRPolicyId > 0)
            {
                hrPolicyService.UpdateHRPolicy(hrPolicy);
            }
            else
            {
                hrPolicyService.CreateHRPolicy(hrPolicy);

            }
            return RedirectToAction("HRPolicyList");
        }
        [CustomAuthorize(Permission = "ViewHRPolicyList")]
        public ActionResult HRPolicyList()
        {  
            LookPolicyService lookPolicyService = new LookPolicyService();
            LookDesignationService lookDesignationService = new LookDesignationService();
            HRPolicyViewModel hRPolicyViewModel = new HRPolicyViewModel();
            hRPolicyViewModel.HRPolicyList  = hrPolicyService.HRPolicyList().Data;
            hRPolicyViewModel.LookPolicyList = lookPolicyService.PolicyList().Data;
            hRPolicyViewModel.LookDesignationList = lookDesignationService.GetDesignationList().Data;
            
            /*   HRPolicyViewModel hRPolicyViewModel;
             *  List<HRPolicyViewModel> hRPolicyViewModelList =new List<HRPolicyViewModel>();
             *  var lookPlicyList = lookPolicyService.PolicyList().Data;
            var looDesignationList = lookDesignationService.GetDesignationList().Data;
             * foreach (var hrPolicy in hrPolicyList)
             {
                 hRPolicyViewModel = new HRPolicyViewModel();
                 hRPolicyViewModel.HRPolicyId = hrPolicy.HRPolicyId;
                 hRPolicyViewModel.LookDesignationId = hrPolicy.LookDesignationId;
                 if (lookPlicyList.IsNotNull())
                 {
                     hRPolicyViewModel.DesignationName = looDesignationList.Where(x => x.LookDesignationId == hrPolicy.LookDesignationId).FirstOrDefault().DesignationName;
                 }

                 hRPolicyViewModel.LookPolicyId = hrPolicy.LookPolicyId;
                 if (lookPlicyList.IsNotNull())
                 { 
                     hRPolicyViewModel.PolicyName = lookPlicyList.Where(x => x.LookPolicyId == hrPolicy.HRPolicyId).FirstOrDefault().PolicyName;   
                 }

                 hRPolicyViewModel.PolicyValue = hrPolicy.PolicyValue;

             }*/


            return View(hRPolicyViewModel);
        }

        [CustomAuthorize(Permission = "ViewHRPolicyList")]
        public ActionResult HRPolicyGroupList()
        {
            // LookPolicyService lookPolicyService = new LookPolicyService();
            // LookDesignationService lookDesignationService = new LookDesignationService();
            // HRPolicyViewModel hRPolicyViewModel = new HRPolicyViewModel();
            //hRPolicyViewModel.HRPolicyList = hrPolicyService.HRPolicyGroupList().Data;
            //hRPolicyViewModel.LookPolicyList = lookPolicyService.PolicyList().Data;
            ///hRPolicyViewModel.LookDesignationList = lookDesignationService.GetDesignationList().Data;
            return View(hrPolicyService.HRPolicyGroupList().Data);
        }
    }
}