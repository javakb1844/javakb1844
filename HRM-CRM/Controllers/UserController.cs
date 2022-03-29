using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VM.User;
using Services.Users;
using Library.Core.Services;
using Data.HRMS;
using Services.HRMS;

namespace HRM_CRM.Controllers
{
    public class UserController : Controller
    {
        UserServices userService = new UserServices();
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(SignInViewModel user)
        {
            try
            {
              
                var response = userService.LoginIn(user);
                if (response.ResultType.Equals(ResultType.Success))
                {
                    var resp= userService.CreateUserSessionV1(user.Email);
                    if (resp.ResultType.Equals(ResultType.Exception))
                    {
                        return RedirectToAction("No505", "Error");
                    }
                    return RedirectToAction("Dashboard", "Home");
                  
                }
                else
                {
                    if (response.ResultType.Equals(ResultType.Exception))
                    {
                        return RedirectToAction("No505", "Error");
                    }
                    else { 
                        ViewBag.Alert = response.Message;
                    return View(user);
                    }
                }
            }
            catch
            {
                return RedirectToAction("No505", "Error");
            }
        }

        [HttpGet]
        public ActionResult SignssUp()
        {
            return View();
        }


        [HttpPost]

        public JsonResult SignUp(SignInViewModel user)
        {
            var result = new Result<bool>();
            try
            {
              var IsEmailUsed=  userService.GetUserByEmail(user.Email);
                if(IsEmailUsed.IsNotNull() && IsEmailUsed.ResultType==ResultType.Success && IsEmailUsed.Data.IsNotNull())
                {
                    result.Data = false;
                    result.ResultType = ResultType.Failure;
                    result.Message ="Email Already Used";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                var insertUser = userService.PromoteUser(user);
                if (insertUser.IsNotNull() && insertUser.ResultType.Equals(ResultType.Success))
                {
                    EmployeeServices employeeServices = new EmployeeServices();
                    Employee employee = new Employee();
                    employee.EmployeeId = insertUser.Data;
                    employee.UpdateDate = DateTime.Now;
                    employee.UpdatedBy = (long)Session["EmployeeId"];
                    employee.EmployeeId = user.EmployeeId ?? 0;                   
                    employeeServices.UpdateEmployeeUserId(employee);

                    result.Data = true;
                    result.ResultType = ResultType.Success;
                }
                else
                {
                    result.Data = false;
                    result.ResultType = ResultType.Failure;
                    result.Message = insertUser.IsNotNull()?insertUser.Message:"Try again later";

                }
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public JsonResult IsEmailAvailable(string email)
        {
            var result = new Result<bool>();
            try
            {
                var IsEmailUsed = userService.GetUserByEmail(email);
                if (IsEmailUsed.IsNotNull() && IsEmailUsed.ResultType == ResultType.Success && IsEmailUsed.Data.IsNotNull())
                {
                    result.Data = false;
                    result.ResultType = ResultType.Failure;
                    result.Message = "Email Already Used";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }else if (IsEmailUsed.IsNotNull() && IsEmailUsed.ResultType == ResultType.Success && IsEmailUsed.Data.IsNull())
                {
                    result.Data = false;
                    result.ResultType = ResultType.Success;
                    result.Message = "Email is Free";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(IsEmailUsed, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            
        }


        public RedirectToRouteResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

       
        public ActionResult Create(long? id)
        {

            RoleService roleService =new  RoleService();
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);         
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
           
            var roles = roleService.RoleList(roleid, ProductSaleProfileId);
            if (roles.ResultType.Equals(ResultType.Exception))
                return RedirectToAction("No505", "Error");
            ViewBag.Roles = new SelectList(roles.Data, "RoleId", "Name");
            // var tt = departments.Data.Where(x => x.LookDepartmentId==1);
            if (id.IsNotNull())
            {
                long userId = id ?? 0;
                var designation = userService.GetUserById(userId);
                if (designation.ResultType.Equals(ResultType.Exception))
                    return RedirectToAction("No505", "Error");
                if (designation.Data.IsNotNull())
                {
                    return View(designation.Data);
                }
                else
                {
                    return RedirectToAction("List");
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (user.UserId.Equals(0) || user.UserId.Equals(null))
            {
               
                    return RedirectToAction("No505", "Error");
              
            }
            else
            {
                var updation = userService.UpdateUser(user);
                if (updation.ResultType.Equals(ResultType.Exception))
                {

                    return RedirectToAction("No505", "Error");
                }
            }
            return RedirectToAction("UserList");
        }

 [CustomAuthorize(Permission = "ViewUserList")]
        public ActionResult UserList()
        {
            long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
            int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
            UserServices userService = new UserServices();
            RoleService RoleService = new RoleService();
            ViewBag.Roles=  RoleService.RoleList(roleid, ProductSaleProfileId).Data;
            var departmentList = userService.UserList();
            return View(departmentList.Data);
        }

        [HttpGet]
        public JsonResult GetUserRoles()
        {
            RoleService roleService = new RoleService();
            var result = new Result<List<Role>>();
            try
            {
                long roleid = Convert.ToInt64(System.Web.HttpContext.Current.Session["RoleId"]);
                int ProductSaleProfileId = Convert.ToInt32(System.Web.HttpContext.Current.Session["ProductSaleProfileId"]);
                var roles = roleService.RoleList(roleid, ProductSaleProfileId);
                if (roles.ResultType.Equals(ResultType.Success))
                {
                    result.Data = roles.Data;
                    result.ResultType = ResultType.Success;
                }
                else
                {
                    result.Data = null;
                    result.ResultType = ResultType.Failure;
                }
            }
            catch (Exception e)
            {               
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}