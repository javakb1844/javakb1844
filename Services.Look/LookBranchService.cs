using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Look
{
 public   class LookBranchService
    {

        HRMSWorker hWorker = new HRMSWorker();

        public Result<List<LookBranch>> GetBranchList()
        {
            var result = new Result<List<LookBranch>>();
            try
            {
                var dbBranchs = hWorker.Repository.Read<LookBranch>().ToListSafely();
                result.Data = dbBranchs;
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
        public Result<List<LookBranchType>> GetBranchTypeList()
        {
            var result = new Result<List<LookBranchType>>();
            try
            {
                var dbBranchs = hWorker.Repository.Read<LookBranchType>().ToListSafely();
                result.Data = dbBranchs;
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
        public Result<LookBranch> GetBranch(long dId)
        {
            var result = new Result<LookBranch>();
            try
            {
                var dbBranch = hWorker.Repository.Read<LookBranch>()
                    .Where(x => x.LookBranchId.Equals(dId)).FirstOrDefault();

                result.Data = dbBranch;
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
        public static Result<string> GetBranchName(long? dId)
        {
            var result = new Result<string>();
            HRMSWorker hWorker = new HRMSWorker();
            try
            {
                var dbBranch = hWorker.Repository.Read<LookBranch>()
                   .Where(x => x.LookBranchId == dId)
                    .FirstOrDefault();

                result.Data = dbBranch.BranchName;
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
        public Result<bool> InsertBranch(LookBranch newBranch)
        {
            var result = new Result<bool>();
            try
            {
                hWorker.Repository.Create(newBranch);
                hWorker.SaveChanges();
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

        public Result<bool> UpdateBranch(LookBranch newBranch)
        {
            var result = new Result<bool>();
            try
            {
                //    var existing = hWorker.Repository.Read<LookBranch>()
                //        .Where(x => x.LookBranchId ==newBranch.LookBranchId);
                hWorker.Repository.Update(newBranch);
                hWorker.SaveChanges();
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
    }
}
