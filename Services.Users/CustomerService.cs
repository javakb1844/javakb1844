using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
   public class CustomerService
    {
        public Result<List<ProductSaleProfile>> ProductSaleProfileList(long roleId)
        {
            HRMSWorker hrmsWorker = new HRMSWorker();
            var result = new Result<List<ProductSaleProfile>>();
            try
            {
                if (roleId == 0 )
                {
                    var dbRole = hrmsWorker.Repository.Read<ProductSaleProfile>().Where(x=>x.ProductSaleProfileId>3)
                       .ToListSafely();
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
