using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
  public  class RoleService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<bool> Create(Role role)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                hrmsWorker.Repository.Create(role);
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

        public Result<Role> GetRoleById(long? id)
        {
            var result = new Result<Role>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbRole = hrmsWorker.Repository.Read<Role>()
                    .Where(x => x.RoleId == id).FirstOrDefault();
                if (dbRole.IsNotNull())
                {
                    result.Data = dbRole;
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

        public Result<bool> UpdateRole(Role modelRole)
        {
            var result = new Result<bool>();
            try
            {

                Role dbRole = hrmsWorker.Repository.Read<Role>()
                             .Where(b => b.RoleId == modelRole.RoleId).FirstOrDefault();
                if (dbRole.IsNotNull())
                {
                    dbRole.Name = modelRole.Name;
                    hrmsWorker.Repository.Update(dbRole);
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
        public Result<List<Role>> RoleList(long roleId,int ProductSaleProfileId)
        {
            var result = new Result<List<Role>>();
            try
            {
                if (roleId == 0 && ProductSaleProfileId==0)
                {
                    var dbRole = hrmsWorker.Repository.Read<Role>()
                       .ToListSafely();                    
                        result.Data = dbRole;
                    result.ResultType = ResultType.Success;
                }
                else 
                {
                    var dbRole = hrmsWorker.Repository.Read<Role>().Where(x => x.RoleId > 1 && (x.ProductSaleProfileId == 0 || x.ProductSaleProfileId == ProductSaleProfileId)).ToListSafely();
                      
                    result.Data = dbRole;
                    result.ResultType = ResultType.Success;
                }
                
               
              
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
