using Data.HRMS;
using Library.Core.Services;
using Services.Look;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class PolicyController : Controller
    {
        // GET: Policy
        LookPolicyService lookPolicyService = new LookPolicyService();

        [CustomAuthorize(Permission ="ViewCreatePolicyGroup")]
        public ActionResult CreatePolicyGroup(long? id)
        {
            try
            {
                LookPolicyGroup modelPolicy = new LookPolicyGroup();

                if (id.IsNotNull())
                {
                    var policy = lookPolicyService.GetPolicyGroupById(id ?? 0);
                    if (policy.ResultType.Equals(ResultType.Success))
                    {
                        if (policy.Data.IsNotNull())
                        {
                            return View(policy.Data);
                        }
                        return RedirectToAction("PolicyGroupList");
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

        [HttpPost]
        [CustomAuthorize(Permission ="EditCreatePolicyGroup")]
        public ActionResult CreatePolicyGroup(LookPolicyGroup lookPolicyGroup)
        {
           
            if (lookPolicyGroup.LookPolicyGroupId > 0)
            {
                lookPolicyService.UpdatePolicyGroup(lookPolicyGroup);
            }
            else
            {
                lookPolicyService.CreatePolicyGroup(lookPolicyGroup);

            }
            return RedirectToAction("PolicyGroupList");
        }
        [CustomAuthorize(Permission ="ViewCreatePolicy")]
        public ActionResult Create(long? id)
        {
            try
            {
                LookPolicy modelPolicy = new LookPolicy();
                var policyGroup = lookPolicyService.PolicyGroupList();
                ViewBag.PolicyGroup = new SelectList(policyGroup.Data, "LookPolicyGroupId", "PolicyGroupName");
                if (id.IsNotNull())
                {
                    var policy = lookPolicyService.GetPolicyById(id ?? 0);
                    if (policy.ResultType.Equals(ResultType.Success))
                    {
                        if (policy.Data.IsNotNull())
                        {
                           
                            return View(policy.Data);
                        }
                        return RedirectToAction("PolicyList");
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
        [HttpPost]
        [CustomAuthorize(Permission ="EditCreatePolicy")]
        public ActionResult Create(LookPolicy lookPolicy)
        {

            if (lookPolicy.LookPolicyId > 0)
            {
                lookPolicyService.UpdatePolicy(lookPolicy);
            }
            else
            {
                lookPolicyService.CreatePolicy(lookPolicy);

            }
            return RedirectToAction("PolicyList");
        }
        [CustomAuthorize(Permission ="ViewPolicyList")]
        public ActionResult PolicyList()
        {
            var policyGroup = lookPolicyService.PolicyGroupList();
            ViewBag.PolicyGroup = policyGroup.Data;
            //ViewBag.QualificationType = new SelectList(policyGroup.Data, "LookPolicyGroupId", "PolicyGroupName");

            return View(lookPolicyService.PolicyList().Data);
        }
        [CustomAuthorize(Permission ="ViewGroupPolicyList")]
        public ActionResult PolicyGroupList()
        {
            return View(lookPolicyService.PolicyGroupList().Data);
           
        }
        
    }
}