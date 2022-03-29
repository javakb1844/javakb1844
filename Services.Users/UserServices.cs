using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.HRMS;
using System.Security.Cryptography;
using Library.Core.Services;
using VM.User;
using System.Web;
using VM.HRMS;
using Services.Look;

namespace Services.Users
{
    public class UserServices
    {
        HRMSWorker hrWorker = new HRMSWorker();
        public Result<bool?> LoginIn(SignInViewModel model)
        {
            var result = new Result<bool?>();
            try
            {
                if (model.Email.IsNotNullOrEmpty())
                {
                    if (model.Password.IsNullOrEmpty())
                    {
                        result.Data = false;
                        result.ResultType = ResultType.Failure;
                        result.Message = "Please enter Password!";
                        return result;
                    }
                    string query = @"SELECT *  FROM [dbo].[Employee]
                                    where [Email] = '" + model.Email + "'  ";
                    string Password = GetHashString(model.Password);
                    var dbUser = hrWorker.Repository.db.Database.SqlQuery<SignUpViewModel>(query).FirstOrDefault();

                    if (dbUser.IsNotNull() && dbUser.IsActive.Equals(true) && (dbUser.PasswordHash.Equals(Password.ToLower()) || dbUser.PasswordHash.Equals(Password)))
                    {
                        result.Data = true;
                        result.ResultType = ResultType.Success;
                        result.Message = "Successfully Logged in";
                        return result;
                    }
                    else
                    {

                        if (dbUser.IsNull())
                        {
                            result.Data = false;
                            result.ResultType = ResultType.Failure;
                            result.Message = "User E-mail does not exist";
                            return result;
                        }

                        if (!(dbUser.PasswordHash.Equals(Password.ToLower()) || dbUser.PasswordHash.Equals(Password)))
                        {
                            result.Data = false;
                            result.ResultType = ResultType.Failure;
                            result.Message = "Wrong Password";
                            return result;
                        }
                        if (!dbUser.IsActive.Equals(true))
                        {
                            result.Data = false;
                            result.ResultType = ResultType.Failure;
                            result.Message = "Not Activated";
                            return result;
                        }
                    }

                    result.Data = false;
                    result.ResultType = ResultType.Success;
                    result.Message = "not matched";
                    return result;
                }
                else
                {
                    result.Data = true;
                    result.ResultType = ResultType.Failure;
                    result.Message = "Please enter your E-mail address ";
                    return result;
                }

            }
            catch (Exception e)
            {
                result.ResultType = ResultType.Exception;
                result.Data = false;
                result.Exception = e;
                result.Message = e.GetOriginalException().Message;
                return result;
            }
        }

        public Result<long> PromoteUser(SignInViewModel newUser)
        {
            var result = new Result<long>();           
            try
            {
                Data.HRMS.User objUser = new User();
                objUser.Email = newUser.Email;
                objUser.PasswordHash = GetHashString(newUser.Password);
                objUser.EmployeeId = newUser.EmployeeId;
                objUser.IsActive = true;
                objUser.RoleId = newUser.RoleId??0;
                hrWorker.Repository.Create(objUser);
                hrWorker.SaveChanges();       
                result.ResultType = ResultType.Success;
                result.Data = objUser.UserId;
            }
            catch (Exception e)
            {
                result.Data = 0;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
         public Result<bool> CreateUserSessionV1(string email)
        {
            var result = new Result<bool>();
            try
            {
                var data = from u in hrWorker.Repository.Read<Employee>()
                           join r in hrWorker.Repository.Read<Role>()
                           on u.RoleId equals r.RoleId
                           where u.Email == email
                           select new { u, r };

                if (data.Any())
                {

                    var userData = data.FirstOrDefault();


                    // HttpContext.Current.Session["UserId"] =  userData.u.EmployeeId;
                    HttpContext.Current.Session["EmployeeId"] = userData.u.EmployeeId;
                    HttpContext.Current.Session["UserEmail"] = userData.u.Email;
                    HttpContext.Current.Session["ManagerId"] = userData.u.ManagerId;
                    HttpContext.Current.Session["BioMetricId"] = userData.u.BioMetricId;
                    HttpContext.Current.Session["UserFirstName"] = userData.u.FirstName;
                    HttpContext.Current.Session["UserLastName"] = userData.u.LastName;
                    HttpContext.Current.Session["IsActive"] = userData.u.IsActive;
                    HttpContext.Current.Session["LookOrganizationId"] = userData.u.LookOrganizationId;
                    HttpContext.Current.Session["SelectedOrganizationId"] = userData.u.LookOrganizationId;
                    HttpContext.Current.Session["ProductSaleProfileId"] = userData.u.ProductSaleProfileId;
                    HttpContext.Current.Session["LookOrganizationIds"] = userData.u.LookOrganizationIds;
                    HttpContext.Current.Session["HasSubOrdinates"] = userData.u.HasSubOrdinates;
                    //HttpContext.Current.Session["UserProfilePic"] = userData.u.i;
                    HttpContext.Current.Session["RoleName"] = userData.r.Name;
                    HttpContext.Current.Session["RoleId"] = userData.r.RoleId;

                    if (userData.u.LookOrganizationId.IsNull() || userData.u.LookOrganizationId == 0)
                    {
                        if (userData.r.RoleId == 0)
                        {
                            var Organization = LookOrganizationService.GetOrganizations(1, 0);
                            if (Organization.IsNotNull() && Organization.Data.CountedPositive())
                            { var uu = Organization.Data.FirstOrDefault();
                                HttpContext.Current.Session["SelectedOrganizationId"] = uu.LookOrganizationId;
                                HttpContext.Current.Session["ProductSaleProfileId"] = uu.ProductSaleProfileId;
                            }
                            else
                            {
                                HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                                HttpContext.Current.Session["LookOrganizationIds"] = "";
                            }
                        }
                       else if (userData.r.RoleId == 1)
                        {                          
                            var Organization = LookOrganizationService.GetOrganizations(1, userData.u.ProductSaleProfileId);
                            if (Organization.IsNotNull() && Organization.Data.CountedPositive())
                                HttpContext.Current.Session["SelectedOrganizationId"] = Organization.Data.FirstOrDefault().LookOrganizationId;
                            else
                            {
                                HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                                HttpContext.Current.Session["LookOrganizationIds"] = "";
                            }
                        }
                        else
                        {
                            HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                            HttpContext.Current.Session["LookOrganizationIds"] = "";
                        }

                    }else if (userData.r.RoleId == 0)
                    {
                        var Organization = LookOrganizationService.GetOrganizations(1, 0);

                        if (Organization.IsNotNull() && Organization.Data.CountedPositive())
                        {
                            var uu = Organization.Data.FirstOrDefault();
                            HttpContext.Current.Session["SelectedOrganizationId"] = uu.LookOrganizationId;
                            HttpContext.Current.Session["ProductSaleProfileId"] = uu.ProductSaleProfileId;
                        }
                        else
                        {
                            HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                            HttpContext.Current.Session["LookOrganizationIds"] = "";
                        }
                    }
                    List<string> atrribute = new List<string>();
                    List<long?> menu = new List<long?>();
                    List<long?> department = new List<long?>();
                    if (userData.r.RoleId == 0)
                    {
                        var permissionsData = from p in hrWorker.Repository.Read<Permission>()
                                             // join p in hrWorker.Repository.Read<Permission>()
                                             // on rp.PermissionId equals p.PermissionId
                                              where  p.IsActive == true 
                                              select p;
                        if (permissionsData.CountedPositive())
                        {
                            var permissions = permissionsData.Distinct().ToListSafely();

                            foreach (var permission in permissions)
                            {
                                menu.Add(permission.MenuId);
                                department.Add(permission.LookDepartmentId);
                                atrribute.Add(permission.Attribute);
                            }
                        }
                    }
                   else if (userData.r.RoleId > 0 && userData.r.RoleId<4)
                    {
                        var permissionsData = from rp in hrWorker.Repository.Read<RolePermission>()
                                              join p in hrWorker.Repository.Read<Permission>()
                                              on rp.PermissionId equals p.PermissionId
                                              where rp.RoleId == userData.r.RoleId && p.IsActive == true && (rp.ProductSaleProfileId==1 || rp.ProductSaleProfileId == userData.u.ProductSaleProfileId)
                                              select p;
                        if (permissionsData.CountedPositive())
                        {
                            var permissions = permissionsData.ToListSafely();

                            foreach (var permission in permissions)
                            {
                                menu.Add(permission.MenuId);
                                department.Add(permission.LookDepartmentId);
                                atrribute.Add(permission.Attribute);
                            }
                        }
                    }
                    else if (userData.r.RoleId > 3)
                    {
                        var permissionsData = from rp in hrWorker.Repository.Read<RolePermission>()
                                              join p in hrWorker.Repository.Read<Permission>()
                                              on rp.PermissionId equals p.PermissionId
                                              where rp.RoleId == userData.r.RoleId && p.IsActive == true && rp.ProductSaleProfileId == userData.u.ProductSaleProfileId
                                              select p;
                        if (permissionsData.CountedPositive())
                        {
                            var permissions = permissionsData.ToListSafely();

                            foreach (var permission in permissions)
                            {
                                menu.Add(permission.MenuId);
                                department.Add(permission.LookDepartmentId);
                                atrribute.Add(permission.Attribute);
                            }
                        }
                    }
                    //////////////////
                string query="";
                    //             query = @"select distinct LeftMenu.* from LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId                         inner
                    //                join RolePermissions on RolePermissions.PermissionId = Permission.PermissionId
                    //                where LeftMenu.IsActive = 1 
                    //                order by LeftMenu.DisplayOrder";
                    //            query = @"select distinct LeftMenu.* from LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId               
                    //                where LeftMenu.IsActive = 1 
                    //                order by LeftMenu.DisplayOrder";
                    //            if (userData.r.RoleId >0)
                    //            {
                    //                int ProductSaleProfileId = userData.u.ProductSaleProfileId;
                    //                if (userData.r.RoleId == 1)
                    //                    ProductSaleProfileId = 1;
                    //                query = @"	  select distinct LeftMenu.* from
                    //LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId             
                    //inner
                    //                join RolePermissions on RolePermissions.PermissionId = Permission.PermissionId						
                    //                where LeftMenu.IsActive = 1 and RolePermissions.RoleId = " + userData.r.RoleId + @" and RolePermissions.ProductSaleProfileId=" + ProductSaleProfileId + @"
                    //                order by LeftMenu.DisplayOrder";
                    //            } 

                    //                List<LeftMenuViewModel> leftmenu = hrWorker.Repository.db.Database.SqlQuery<LeftMenuViewModel>(query)
                    //                     .ToListSafely();

                    //                var tt = leftmenu.Select(x => x.ParentId).Distinct().ToArray();
                    // query = "select LeftMenu.* from LeftMenu where  LeftMenu.IsActive = 1 and LeftMenuId in (" + String.Join(",", tt) + ") order by LeftMenu.DisplayOrder";
                    query = @"EXEC [dbo].getRole
                           @RoleId =" + userData.r.RoleId + @",
                           @ProductSaleProfileId  =" + userData.u.ProductSaleProfileId + @",
                           @OrganizationId  =" + userData.u.LookOrganizationId;
                    List<LeftMenuViewModel> leftmenut = hrWorker.Repository.db.Database.SqlQuery<LeftMenuViewModel>(query).ToListSafely();

                  //  var newList = leftmenut.Concat(leftmenu).OrderBy(x => x.DisplayOrder).ToList(); //.OrderBy(x => x.DisplayOrder);
                                                                                                        //var qq= hrWorker.Repository.Read<LeftMenu>()
                                                                                                        // .Where (x.x=>).FirstOrDefault();
                    var manutemp = menu.Distinct().ToArray();
                    var atrributeTemp = atrribute.Distinct().ToArray();
                    var departmentTemp = department.Distinct().ToArray();
                    /////////////////////
                        HttpContext.Current.Session["LeftMenu"] = leftmenut;
                        HttpContext.Current.Session["Permissions"] = atrribute;
                        HttpContext.Current.Session["RoleMenu"] = manutemp;
                        HttpContext.Current.Session["Departments"] = department;

                        result.Data = true;
                    }
                
                else
                {
                    result.Data = false;
                }
                result.ResultType = ResultType.Success;

            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public Result<bool> CreateUserSession(string email)
        {
            var result = new Result<bool>();
            try
            {
                var data = from u in hrWorker.Repository.Read<Employee>()
                           join r in hrWorker.Repository.Read<Role>()
                           on u.RoleId equals r.RoleId
                           where u.Email == email
                           select new { u, r };

                if (data.Any())
                {

                    var userData = data.FirstOrDefault();


                    // HttpContext.Current.Session["UserId"] =  userData.u.EmployeeId;
                    HttpContext.Current.Session["EmployeeId"] = userData.u.EmployeeId;
                    HttpContext.Current.Session["UserEmail"] = userData.u.Email;
                    HttpContext.Current.Session["ManagerId"] = userData.u.ManagerId;
                    HttpContext.Current.Session["BioMetricId"] = userData.u.BioMetricId;
                    HttpContext.Current.Session["UserFirstName"] = userData.u.FirstName;
                    HttpContext.Current.Session["UserLastName"] = userData.u.LastName;
                    HttpContext.Current.Session["IsActive"] = userData.u.IsActive;
                    HttpContext.Current.Session["LookOrganizationId"] = userData.u.LookOrganizationId;
                    HttpContext.Current.Session["SelectedOrganizationId"] = userData.u.LookOrganizationId;
                    HttpContext.Current.Session["ProductSaleProfileId"] = userData.u.ProductSaleProfileId;
                    HttpContext.Current.Session["LookOrganizationIds"] = userData.u.LookOrganizationIds;
                    HttpContext.Current.Session["HasSubOrdinates"] = userData.u.HasSubOrdinates;
                    //HttpContext.Current.Session["UserProfilePic"] = userData.u.i;
                    HttpContext.Current.Session["RoleName"] = userData.r.Name;
                    HttpContext.Current.Session["RoleId"] = userData.r.RoleId;

                    if (userData.u.LookOrganizationId.IsNull() || userData.u.LookOrganizationId == 0)
                    {
                        if (userData.r.RoleId == 0)
                        {
                            var Organization = LookOrganizationService.GetOrganizations(1, 0);
                            if (Organization.IsNotNull() && Organization.Data.CountedPositive())
                            { var uu = Organization.Data.FirstOrDefault();
                                HttpContext.Current.Session["SelectedOrganizationId"] = uu.LookOrganizationId;
                                HttpContext.Current.Session["ProductSaleProfileId"] = uu.ProductSaleProfileId;
                            }
                            else
                            {
                                HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                                HttpContext.Current.Session["LookOrganizationIds"] = "";
                            }
                        }
                       else if (userData.r.RoleId == 1)
                        {                          
                            var Organization = LookOrganizationService.GetOrganizations(1, userData.u.ProductSaleProfileId);
                            if (Organization.IsNotNull() && Organization.Data.CountedPositive())
                                HttpContext.Current.Session["SelectedOrganizationId"] = Organization.Data.FirstOrDefault().LookOrganizationId;
                            else
                            {
                                HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                                HttpContext.Current.Session["LookOrganizationIds"] = "";
                            }
                        }
                        else
                        {
                            HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                            HttpContext.Current.Session["LookOrganizationIds"] = "";
                        }

                    }else if (userData.r.RoleId == 0)
                    {
                        var Organization = LookOrganizationService.GetOrganizations(1, 0);

                        if (Organization.IsNotNull() && Organization.Data.CountedPositive())
                        {
                            var uu = Organization.Data.FirstOrDefault();
                            HttpContext.Current.Session["SelectedOrganizationId"] = uu.LookOrganizationId;
                            HttpContext.Current.Session["ProductSaleProfileId"] = uu.ProductSaleProfileId;
                        }
                        else
                        {
                            HttpContext.Current.Session["SelectedOrganizationId"] = -2;
                            HttpContext.Current.Session["LookOrganizationIds"] = "";
                        }
                    }
                    List<string> atrribute = new List<string>();
                    List<long?> menu = new List<long?>();
                    List<long?> department = new List<long?>();
                    if (userData.r.RoleId == 0)
                    {
                        var permissionsData = from p in hrWorker.Repository.Read<Permission>()
                                             // join p in hrWorker.Repository.Read<Permission>()
                                             // on rp.PermissionId equals p.PermissionId
                                              where  p.IsActive == true 
                                              select p;
                        if (permissionsData.CountedPositive())
                        {
                            var permissions = permissionsData.Distinct().ToListSafely();

                            foreach (var permission in permissions)
                            {
                                menu.Add(permission.MenuId);
                                department.Add(permission.LookDepartmentId);
                                atrribute.Add(permission.Attribute);
                            }
                        }
                    }
                   else if (userData.r.RoleId > 0 && userData.r.RoleId<4)
                    {
                        var permissionsData = from rp in hrWorker.Repository.Read<RolePermission>()
                                              join p in hrWorker.Repository.Read<Permission>()
                                              on rp.PermissionId equals p.PermissionId
                                              where rp.RoleId == userData.r.RoleId && p.IsActive == true && (rp.ProductSaleProfileId==1 || rp.ProductSaleProfileId == userData.u.ProductSaleProfileId)
                                              select p;
                        if (permissionsData.CountedPositive())
                        {
                            var permissions = permissionsData.ToListSafely();

                            foreach (var permission in permissions)
                            {
                                menu.Add(permission.MenuId);
                                department.Add(permission.LookDepartmentId);
                                atrribute.Add(permission.Attribute);
                            }
                        }
                    }
                    else if (userData.r.RoleId > 3)
                    {
                        var permissionsData = from rp in hrWorker.Repository.Read<RolePermission>()
                                              join p in hrWorker.Repository.Read<Permission>()
                                              on rp.PermissionId equals p.PermissionId
                                              where rp.RoleId == userData.r.RoleId && p.IsActive == true && rp.ProductSaleProfileId == userData.u.ProductSaleProfileId
                                              select p;
                        if (permissionsData.CountedPositive())
                        {
                            var permissions = permissionsData.ToListSafely();

                            foreach (var permission in permissions)
                            {
                                menu.Add(permission.MenuId);
                                department.Add(permission.LookDepartmentId);
                                atrribute.Add(permission.Attribute);
                            }
                        }
                    }
                    //////////////////
                    string query = @"select distinct LeftMenu.* from LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId                         inner
                        join RolePermissions on RolePermissions.PermissionId = Permission.PermissionId
                        where LeftMenu.IsActive = 1 
                        order by LeftMenu.DisplayOrder";
                    query = @"select distinct LeftMenu.* from LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId               
                        where LeftMenu.IsActive = 1 
                        order by LeftMenu.DisplayOrder";
                    if (userData.r.RoleId >0)
                    {
                        int ProductSaleProfileId = userData.u.ProductSaleProfileId;
                        if (userData.r.RoleId == 1)
                            ProductSaleProfileId = 1;
                        query = @"	  select distinct LeftMenu.* from
						  LeftMenu inner join Permission on LeftMenu.LeftMenuId = Permission.MenuId             
						  inner
                        join RolePermissions on RolePermissions.PermissionId = Permission.PermissionId						
                        where LeftMenu.IsActive = 1 and RolePermissions.RoleId = " + userData.r.RoleId + @" and RolePermissions.ProductSaleProfileId=" + ProductSaleProfileId + @"
                        order by LeftMenu.DisplayOrder";
                    } 

                        List<LeftMenuViewModel> leftmenu = hrWorker.Repository.db.Database.SqlQuery<LeftMenuViewModel>(query)
                             .ToListSafely();

                        var tt = leftmenu.Select(x => x.ParentId).Distinct().ToArray();
                        query = "select LeftMenu.* from LeftMenu where  LeftMenu.IsActive = 1 and LeftMenuId in (" + String.Join(",", tt) + ") order by LeftMenu.DisplayOrder";
                        List<LeftMenuViewModel> leftmenut = hrWorker.Repository.db.Database.SqlQuery<LeftMenuViewModel>(query).ToListSafely();
                        var newList = leftmenut.Concat(leftmenu).OrderBy(x => x.DisplayOrder).ToList(); //.OrderBy(x => x.DisplayOrder);
                                                                                                        //var qq= hrWorker.Repository.Read<LeftMenu>()
                                                                                                        // .Where (x.x=>).FirstOrDefault();
                    var manutemp = menu.Distinct().ToArray();
                    var atrributeTemp = atrribute.Distinct().ToArray();
                    var departmentTemp = department.Distinct().ToArray();
                    /////////////////////
                        HttpContext.Current.Session["LeftMenu"] = newList;
                        HttpContext.Current.Session["Permissions"] = atrribute;
                        HttpContext.Current.Session["RoleMenu"] = manutemp;
                        HttpContext.Current.Session["Departments"] = department;

                        result.Data = true;
                    }
                
                else
                {
                    result.Data = false;
                }
                result.ResultType = ResultType.Success;

            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public Result<User> GetUserById(long? id)
        {
            var result = new Result<User>();
            try
            {
                var dbUser = hrWorker.Repository.Read<User>()
                    .Where(x => x.UserId==id).FirstOrDefault();

                result.Data = dbUser;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Success;
                result.Exception = e;
                result.Message = e.GetOriginalException().Message;
            }
            return result;
        }

        public Result<bool> UpdateUser(User user)
        {
            var result = new Result<bool>();
            try
            {
                   var existing = hrWorker.Repository.Read<User>()
                        .Where(x => x.UserId == user.UserId).FirstOrDefault();
                existing.RoleId = user.RoleId;
                hrWorker.Repository.Update(existing);
                hrWorker.SaveChanges();
                result.ResultType = ResultType.Success;
                result.Data = true;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }


        public Result<List<User>> UserList()
        {
            var result = new Result<List<User>>();
            try
            {              
                var dbUser = hrWorker.Repository.Read<User>()
                   .ToListSafely();
                if (dbUser.IsNotNull())
                {
                    result.Data = dbUser;
                }
                else
                {
                    result.Data = null;
                }
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Exception = e;
                result.Message = e.GetOriginalException().Message;
            }
            return result;

        }

        public Result<User> GetUserByEmail(string email)
        {
            var result = new Result<User>();
            try
            {
                var dbUser = hrWorker.Repository.Read<User>()
                    .Where(x => x.Email == email).FirstOrDefault();

                result.Data = dbUser;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Success;
                result.Exception = e;
                result.Message = e.GetOriginalException().Message;
            }
            return result;
        }


    }
}
