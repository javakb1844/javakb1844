using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.HRMS;
using Library.Core.Services;
using Library.Extensions;
using VM.User;

namespace Services.Users
{
    public class RolePermissionService
    {
        HRMSWorker hrWorker = new HRMSWorker();
        public Result<bool> EditRolePermission(List<long> selectedPermissions, long role, int ProductSaleProfileId)
        {
            var result = new Result<bool>();
            try
            {
                if (role == 0)
                    ProductSaleProfileId = 0;
                else if (role == 1)
                    ProductSaleProfileId = 1;
                
                List<RolePermission> exsistingRolePermission;        
                    exsistingRolePermission = hrWorker.Repository.Read<RolePermission>()
                  .Where(x => x.RoleId == role && x.ProductSaleProfileId == ProductSaleProfileId).ToListSafely();
                

                bool isDbChanged = false;
                if (selectedPermissions.IsNotNull())
                {
                    foreach (var permission in selectedPermissions)
                    {
                        var permissionExsistance = exsistingRolePermission.Where(p => p.PermissionId == permission);
                        if (permissionExsistance.CountedZero())
                        {
                            // Determines to add (role) permission if does'nt already exsists, creates newly selected one 
                            RolePermission newRolePermission = new RolePermission();
                            newRolePermission.RoleId = role;
                            newRolePermission.PermissionId = permission;
                           
                            newRolePermission.ProductSaleProfileId = ProductSaleProfileId;
                            hrWorker.Repository.Create(newRolePermission);
                            isDbChanged = true;
                        }
                        else
                        {// Determines non-selected (role) permissions to delete
                            exsistingRolePermission.Remove(exsistingRolePermission.Where(x => x.PermissionId == permission).FirstOrDefault());
                        }
                    }
                }
                if (exsistingRolePermission.CountedPositive())
                {// Deleting non-seleceted exsisting (role) permissions
                    foreach (var permission in exsistingRolePermission)
                    {
                        hrWorker.Repository.Delete(permission);
                    }
                    isDbChanged = true;
                }
                if (isDbChanged)
                {
                    hrWorker.SaveChanges();
                }
                result.Data = true;
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
        public Result<List<PermissionViewModel>> GetRolePermission(long role, long userRoleid)
        {
            var result = new Result<List<PermissionViewModel>>();
            try
            {
                string where = " and PermissionLevel=0";
                string query = $@"SELECT p.PermissionId , p.IsActive  , p.Name ,p.Attribute,rp.RoleId,rp.RolePermissionId, m.ParentId,
								CASE WHEN m.[ParentId] =0 or m.[ParentId] is null  THEN m.LeftMenuId ELSE m.[ParentId] END AS Menu,
								CASE WHEN m.[ParentId] =0 or m.[ParentId] is null  THEN CONVERT(bit, 0)  ELSE CONVERT(bit, 1) END AS IsChild,
								CASE WHEN m.[ParentId] =0 or m.[ParentId] is null  THEN 0 ELSE m.LeftMenuId END as ChildMenu,
                                 CASE WHEN rp.RolePermissionId IS NULL THEN CONVERT(bit, 0)  ELSE CONVERT(bit, 1)  END AS Selected 
                                 FROM [dbo].[Permission] p
                                 LEFT JOIN [RolePermissions] rp
                                 ON p.PermissionId=rp.PermissionId and isnull(rp.RoleId,0)={role}
								 INNER JOIN [LeftMenu] m ON p.MenuId=m.LeftMenuId
                                 where RoleId>0 and IsDeveloperOnly=0) or  (isnull(rp.RoleId,0)={role} )
								 ORDER BY ChildMenu";
                if(userRoleid==0 && role==0)
                    where = "";
                query = $@"SELECT p.PermissionId , p.IsActive  , p.Name ,p.Attribute,rp.RoleId,rp.RolePermissionId, m.ParentId,
								CASE WHEN m.[ParentId] =0 or m.[ParentId] is null  THEN m.LeftMenuId ELSE m.[ParentId] END AS Menu,
								CASE WHEN m.[ParentId] =0 or m.[ParentId] is null  THEN CONVERT(bit, 0)  ELSE CONVERT(bit, 1) END AS IsChild,
								CASE WHEN m.[ParentId] =0 or m.[ParentId] is null  THEN 0 ELSE m.LeftMenuId END as ChildMenu,
                                 CASE WHEN rp.RolePermissionId IS NULL THEN CONVERT(bit, 0)  ELSE CONVERT(bit, 1)  END AS Selected 
                                 FROM [dbo].[Permission] p
                                 LEFT JOIN [RolePermissions] rp 
                                 ON p.PermissionId=rp.PermissionId  and isnull(rp.RoleId,0)={role}
								 INNER JOIN [LeftMenu] m ON p.MenuId=m.LeftMenuId
                                 where 1=1 and p.isactive=1 "+ where + @"
								 ORDER BY ChildMenu";
                var dbRP = hrWorker.Repository.db.Database.
                    SqlQuery<PermissionViewModel>(query).ToListSafely();
                result.Data = dbRP;
                result.ResultType = ResultType.Success;
            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
        public static List<Role> GetRolesList(long roleid, int ProductSaleProfileId)
        {
            HRMSWorker hrWorker = new HRMSWorker();
            if (roleid > 0)
            {
                var roles = hrWorker.Repository.Read<Role>().Where(x => x.IsActive == true && x.RoleId > 1 && (x.ProductSaleProfileId==0 || x.ProductSaleProfileId== ProductSaleProfileId)).ToListSafely();
                return roles;
            }else
            {
                var roles = hrWorker.Repository.Read<Role>().Where(x => x.IsActive == true).ToListSafely();
                return roles;
            }
        }
        public Result<List<MenuViewModel>> GetMenus()
        {
            var result = new Result<List<MenuViewModel>>();
            try
            {
                string query = @"SELECT *
                                  FROM [LeftMenu]   
                                  where [IsActive]=1 order by DisplayOrder";
                var menus = hrWorker.Repository.db.Database.SqlQuery<MenuViewModel>(query).ToListSafely();
                result.Data = menus;
                result.ResultType = ResultType.Success;

            }
            catch (Exception e)
            {
                result.Data = null;
                result.ResultType = ResultType.Exception;
                result.Message = e.GetOriginalException().Message;
                result.Exception = e;
            }
            return result;
        }
    }
}
