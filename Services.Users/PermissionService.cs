using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
  public  class PermissionService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<bool> CreatePermission(Permission permission)
        {
            var result = new Result<bool>();
            try
            {
                hrmsWorker.Repository.Create(permission);
                hrmsWorker.SaveChanges();
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

        public Result<Permission> GetPermissionById(long? id)
        {
            var result = new Result<Permission>();
            try
            {
               
                var dbPermission = hrmsWorker.Repository.Read<Permission>()
                    .Where(x => x.PermissionId == id).FirstOrDefault();
                if (dbPermission.IsNotNull())
                {
                    result.Data = dbPermission;
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
        public Result<List<Permission>> GetPermissionByMenuId(long id)
        {
            var result = new Result<List<Permission>>();
            try
            {

                var dbPermission = hrmsWorker.Repository.Read<Permission>()
                    .Where(x => x.MenuId == id).ToListSafely();
                if (dbPermission.IsNotNull())
                {
                    result.Data = dbPermission;
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

        public Result<bool> UpdatePermission(Permission permission)
        {
            var result = new Result<bool>();
            try
            {
               
                Permission dbPermission = hrmsWorker.Repository.Read<Permission>()
                             .Where(b => b.PermissionId == permission.PermissionId).FirstOrDefault();
                if (dbPermission.IsNotNull())
                {
                   dbPermission.Attribute = permission.Attribute;
                    dbPermission.IsActive = permission.IsActive;
                    dbPermission.Name = permission.Name;
                    hrmsWorker.Repository.Update(dbPermission);
                    hrmsWorker.SaveChanges();
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
        public Result<List<Permission>> PermissionList()
        {
            var result = new Result<List<Permission>>();
            try
            {
               
                var dbPermission = hrmsWorker.Repository.Read<Permission>()
                   .ToListSafely().OrderBy(x=>x.MenuId).ToListSafely();
                if (dbPermission.IsNotNull())
                {
                    result.Data = dbPermission;
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
    }
}
