using Data.HRMS;
using Library.Core.Services;
using Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRM_CRM.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        [CustomAuthorize(Permission = "ViewCreatePermission")]
        public ActionResult Create(long? id)
        {
            try
            {
                MenuService menuService = new MenuService();
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                var menuList = menuService.MenuList(roleid, ProductSaleProfileId,1); 
                if (menuList.ResultType.Equals(ResultType.Exception))
                    return RedirectToAction("No505", "Error");
                ViewBag.menuList = new SelectList(menuList.Data, "LeftMenuId", "MenuName");

                Permission modelPermission = new Permission();
                PermissionService permissionService = new PermissionService();
                if (id.IsNotNull())
                {
                    var department = permissionService.GetPermissionById(id ?? 0);
                    if (department.ResultType.Equals(ResultType.Success))
                    {
                        if (department.Data.IsNotNull())
                        {
                            var dbPermission = department.Data;
                           // modelPermission.PermissionName = dbPermission.PermissionName;
                            return View(department.Data);
                        }
                        return RedirectToAction("PermissionList");
                    }
                    else
                    {
                        return RedirectToAction("No505", "Error");
                    }
                }
                else
                {
                    // return RedirectToAction("PermissionList");
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("No505", "Error");
            }
        }
        [CustomAuthorize(Permission = "EditCreatePermission")]
        [HttpPost]
        public ActionResult Create(Permission permission)
        {
            PermissionService permissionService = new PermissionService();
            if (permission.PermissionId > 0)
            {
                permissionService.UpdatePermission(permission);
            }
            else
            {
                permissionService.CreatePermission(permission);

            }
            return RedirectToAction("PermissionList");
        }
        [CustomAuthorize(Permission = "ViewPermissionList")]
        public ActionResult PermissionList()
        {
            PermissionService lookPermissionService = new PermissionService();
            var departmentList = lookPermissionService.PermissionList();
            MenuService menuService = new MenuService();
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            ViewBag.menuList = menuService.MenuList(roleid, ProductSaleProfileId); 
            return View(departmentList.Data);
        }
    }
}