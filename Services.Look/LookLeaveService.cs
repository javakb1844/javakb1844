using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Look
{
   public class LookLeaveService
    {
        public static Result<List<LookLeaveStatu>> GetActiveLeaveStatus()
        {
            HRMSWorker hWorker = new HRMSWorker();
            var result = new Result<List<LookLeaveStatu>>();
            try
            {
                var dbGrade = hWorker.Repository.Read<LookLeaveStatu>().Where(x=>x.IsActive==true).ToListSafely();
                result.Data = dbGrade;
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
