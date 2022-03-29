using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.HRMS;
using Services.Users;
using Library.Core.Services;
using VM.User;

namespace HRM_CRM.Controllers
{
    public class AdminController : Controller
    {


        // GET: UI
        RolePermissionService rolePermissionService = new RolePermissionService();
         RoleService roleService = new RoleService();

	    [CustomAuthorize(Permission="ViewRolePermissions")]
		
        public ActionResult RolePermission(long role=-1)
        {
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            RolePermissionViewModel rolePermissions = new RolePermissionViewModel();
            if (role==0 && roleid>0)
            {
                return View(rolePermissions);
            }
            var rP= rolePermissionService.GetRolePermission(role, roleid);
            if (rP.ResultType.Equals(ResultType.Exception))
            {
                return RedirectToAction("No505", "Error");
            }
            var menus = rolePermissionService.GetMenus();
            if (menus.ResultType.Equals(ResultType.Exception))
            {
                return RedirectToAction("No505", "Error");
            }

            rolePermissions.Role = role;
            rolePermissions.Permissions = rP.Data;
            rolePermissions.Menus = menus.Data;
            return View(rolePermissions);
        }
        [HttpPost]

        [CustomAuthorize(Permission = "EditRolePermissions")]
        public ActionResult RolePermission(RolePermissionViewModel newRP)
        {
            if (newRP.Role == 0 && Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"])!=0)
            {
                return RedirectToAction("No505", "Error");
            }
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            var response= rolePermissionService.EditRolePermission(newRP.SelectedPermissions,newRP.Role, ProductSaleProfileId);
            if (response.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
            return RedirectToAction("RolePermission");
        }

        
  [CustomAuthorize(Permission = "NewCustomerViewList")]
 public ActionResult NewCustomer(long? id)
        {
            try
            {
                Role modelRole = new Role();
                if (id.IsNotNull())
                {

                    var role = roleService.GetRoleById(id ?? 0);
                    if (role.ResultType.Equals(ResultType.Success))
                    {
                        if (role.Data.IsNotNull())
                        {


                            return View(role.Data);
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
                }
            }
            catch
            {
                return RedirectToAction("No505", "Error");
            }
        }
        public ActionResult CreateRole(long? id)
        {
            try
            {
                Role modelRole = new Role();
                if (id.IsNotNull())
                {

                    var role = roleService.GetRoleById(id ?? 0);
                    if (role.ResultType.Equals(ResultType.Success))
                    {
                        if (role.Data.IsNotNull())
                        {
                           

                            return View(role.Data);
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
                }
            }
            catch
            {
                return RedirectToAction("No505", "Error");
            }
        }
        [HttpPost]
        public ActionResult CreateRole(Role role)
        {
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);

            if (role.RoleId > 0)
            {
                roleService.UpdateRole(role);
            }
            else
            {  if (roleid ==0)
                    role.ProductSaleProfileId = 0;               
                else if(roleid > 0)
                    role.ProductSaleProfileId = ProductSaleProfileId;

                roleService.Create(role);

            }
            return RedirectToAction("RoleList");
        }
        public ActionResult RoleList(int roleId=-1)
        {
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            RoleService roleService = new RoleService();
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            //if (roleid > 0 && (roleid == 0 && roleId == roleid))
                return View(roleService.RoleList(roleId, ProductSaleProfileId).Data);
            //else
            //{
            //    Result<List<Role>> result = new Result<List<Role>>();
            //    return View(result.Data);
            //}
        }

        public ActionResult CustomerList(int custId = -1)        {
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            CustomerService roleService = new CustomerService();        
            return View(roleService.ProductSaleProfileList(roleid).Data);
           
        }
    }
}