using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Users;
using Data.HRMS;
using Library.Core.Services;
using VM.HRMS;

namespace HRMS.Controllers
{
    public class MenuController : Controller
    {
        //[CustomAuthorize(Permission = "ViewMenuList")]
        public ActionResult MenuList()
        {
            List<LeftMenuViewModel> leftMenu = new List<LeftMenuViewModel>();
            MenuService menuService = new MenuService();
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            var result = menuService.MenuList(roleid, ProductSaleProfileId);
            ///  if(departmentList.ResultType==ResultType.Success )
            return View(result.Data);
            //  else
            //  return View(departmentList.Data);
        }
        //[CustomAuthorize(Permission = "ViewSubMenuList")]
        public ActionResult SubMenuList(long id)
        {
            List<LeftMenuViewModel> leftMenu = new List<LeftMenuViewModel>();
            MenuService menuService = new MenuService();
            var result = menuService.SubMenuList(id);
            return View(result.Data);
        }
        //[CustomAuthorize(Permission = "ViewSidePar")]
        public ActionResult _LeftSiderBarPartial()
        {
            // List<LeftMenuViewModel> leftMenu = new List<LeftMenuViewModel>();
            List<LeftMenuViewModel> leftMenu; // = new List<LeftMenu>();
            MenuService menuService = new MenuService();
            // var result = menuService.MenuList(1);
           var result = Session["LeftMenu"] as List<LeftMenuViewModel>;
            if (result.IsNotNull())
            {
                long ProfileLeftMenuId = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["ProfileLeftMenuId"]);
                result = result.Where(x => x.ParentId != ProfileLeftMenuId).ToList();
                result = result.Where(x => x.LeftMenuId != ProfileLeftMenuId).ToList();

                var  templeft = result.ToList();
              return PartialView(templeft);
            }
            else
            {
                leftMenu =new List<LeftMenuViewModel>();
                return PartialView(leftMenu);
            }
        }

        public ActionResult _HeaderBarPartial()
        {
           
            List<LeftMenuViewModel> leftMenu; // = new List<LeftMenu>();
            MenuService menuService = new MenuService();
           
            var result = Session["LeftMenu"] as List<LeftMenuViewModel>;
            if (result.IsNotNull())
            {
              long ProfileLeftMenuId= Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["ProfileLeftMenuId"]);
                result = result.Where(x => x.ParentId == ProfileLeftMenuId).ToList();
                var templeft = result.ToList();
                return PartialView(templeft);
            }
            else
            {
                leftMenu = new List<LeftMenuViewModel>();
                return PartialView(leftMenu);
            }
        }

        [CustomAuthorize(Permission = "ViewHolidaysList")]
        [HttpGet]
        public JsonResult PermissionByMnuId(long id)
        {
            // HRMSWorker hrmsWorker = new HRMSWorker();
            PermissionService permissionService = new PermissionService();         
            return Json(permissionService.GetPermissionByMenuId(id), JsonRequestBehavior.AllowGet);
            // return View(dbApplicant);
        }
    }
}