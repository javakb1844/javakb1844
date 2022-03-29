using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Routing;
using Library.Extensions;

namespace HRMS
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute

    {
        public string Permission = null;
        long? userId = null;
        long? roleId = null;
        List<string> userPermissions;
        long DeveloperRoleId;
        bool isAjax = false;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            DeveloperRoleId = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["DeveloperRoleId"]);
            if (filterContext.HttpContext.Session["EmployeeId"].IsNotNullOrEmpty())
            {
                userId = (long)filterContext.HttpContext.Session["EmployeeId"];
            }
            else
            {
                userId = null;
            }
            if (filterContext.HttpContext.Session["Permissions"].IsNotNullOrEmpty())
            {
                userPermissions = filterContext.HttpContext.Session["Permissions"] as List<string>;
            }
            else
            {
                userPermissions = null;
            }

            if (filterContext.HttpContext.Session["RoleId"].IsNotNullOrEmpty())
            {
                roleId = (long)filterContext.HttpContext.Session["RoleId"];
            }
            else
            {
                roleId = null;
            }
            isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            base.OnAuthorization(filterContext);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            if (userId.IsNull())
            {
                if (isAjax)
                {
                    filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary(new { controller = "Error", action = "NotLoggedIn" })
                                    );
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "User", action = "Login" })
                );
                }

            }
            //User is logged in but has no access
            else
            {
                if (isAjax)
                {
                    filterContext.Result = new RedirectToRouteResult(
                                            new RouteValueDictionary(new { controller = "Error", action = "NotAuthorized" })
                                    );
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                            new RouteValueDictionary(new { controller = "Error", action = "No401" })
                    );
                }
            }
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (userId.IsNull())
            {
                return false;
            }
            if (roleId.IsNull())
            {
                return false;
            }
            if (roleId.Equals(DeveloperRoleId))
            {
                return true;
            }
            if (userPermissions.CountedPositive() && Permission.IsNotNull())
            {
                string[] requiredPermissions = Permission.Split(',');
                foreach (string item in userPermissions)
                {
                    if (Permission.Split(',').Length.Equals(1))
                    {
                        if (item.Equals(Permission))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        foreach (var eachPermission in requiredPermissions)
                        {
                            if (eachPermission.Equals(item))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}