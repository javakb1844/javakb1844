using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.HRMS;

namespace Services.Users
{
    public class MenuService
    {
        HRMSWorker hrmsWorker = new HRMSWorker();
        public Result<List<LeftMenuViewModel>> MenuList(long roleId)
        {
            var result = new Result<List<LeftMenuViewModel>>();
            try
            {
                string query = "select LeftMenu.*,RoleMenus.DefaultControllerName,RoleMenus.DefaultActionName, RoleMenus.IsDefault from LeftMenu inner join RoleMenus on LeftMenu.LeftMenuId = RoleMenus.MenuId where LeftMenu.IsActive = 1 and RoleMenus.RoleId = 1 order by LeftMenu.DisplayOrder ";
                var str = hrmsWorker.Repository.db.Database.SqlQuery<LeftMenuViewModel>(query).ToListSafely();
                result.Data = str;
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
        public Result<List<LeftMenuViewModel>> MenuList(long roleid,int ProductSaleProfileId, int isActive=-1)
        {
            var result = new Result<List<LeftMenuViewModel>>();
            try
            {
                string IsActive = "";
                if (isActive == 1)
                    IsActive = " and lm.isActive=1";
                else if (isActive == 0)
                    IsActive = " and lm.isActive=0";

                string query = "select lm.* from LeftMenu lm  where 1=1 "+ IsActive + " order by lm.DisplayOrder ";
                if(roleid==1)
                {
                    ProductSaleProfileId = 1;
                }                
                if (roleid > 0)
                {
                    query = @"select lm.* from LeftMenu lm 
                          inner join RoleMenus rm on lm.LeftMenuId=rm.MenuId
                          where rm.RoleId="+ roleid + IsActive + "  and rm.ProductSaleProfileId=" + ProductSaleProfileId + @"
                          order by lm.DisplayOrder ";
                }
                var str = hrmsWorker.Repository.db.Database.SqlQuery<LeftMenuViewModel>(query).ToListSafely();
                result.Data = str;
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
        public Result<List<LeftMenuViewModel>> SubMenuList(long id)
        {
            var result = new Result<List<LeftMenuViewModel>>();
            try
            {
                string query = "select LeftMenu.* from LeftMenu where LeftMenu.parentId=" + id + " order by LeftMenu.DisplayOrder ";
                var str = hrmsWorker.Repository.db.Database.SqlQuery<LeftMenuViewModel>(query).ToListSafely();
                result.Data = str;
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
