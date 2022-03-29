using Data.HRMS;
using Library.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Look
{
    public class LookDepartmentService
    {
        public Result<bool> CreateDepartment(LookDepartment lookDepartment)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                hrmsWorker.Repository.Create(lookDepartment);
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

        public Result<LookDepartment> GetDepartmentById(long? id)
        {
            var result = new Result<LookDepartment>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbDepartment = hrmsWorker.Repository.Read<LookDepartment>()
                    .Where(x => x.LookDepartmentId == id).FirstOrDefault();
                if (dbDepartment.IsNotNull())
                {
                    result.Data = dbDepartment;
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

        public Result<bool> UpdateDepartment(LookDepartment modelDepartment)
        {
            var result = new Result<bool>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                LookDepartment dbDepartment = hrmsWorker.Repository.Read<LookDepartment>()
                             .Where(b => b.LookDepartmentId == modelDepartment.LookDepartmentId).FirstOrDefault();
                if (dbDepartment.IsNotNull())
                {
                    dbDepartment.DepartmentName = modelDepartment.DepartmentName;
                    hrmsWorker.Repository.Update(dbDepartment);
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
        public Result<List<LookDepartment>> DepartmentList()
        {
            var result = new Result<List<LookDepartment>>();
            try
            {
                var hrmsWorker = new HRMSWorker();
                var dbDepartment = hrmsWorker.Repository.Read<LookDepartment>()
                   .ToListSafely();
                if (dbDepartment.IsNotNull())
                {
                    result.Data = dbDepartment;
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
        //public Result<List<LookDepartment>> GetDepartmentList()
        //{
        //    var result = new Result<List<LookDepartment>>();
        //    try
        //    {
        //        var hrmsWorker = new HRMSWorker();
        //        var dbDepartment = hrmsWorker.Repository.Read<LookDepartment>().ToListSafely();

        //        if (dbDepartment.IsNotNull())
        //        {
        //            result.Data = dbDepartment;
        //        }
        //        else
        //        {
        //            result.Data = null;
        //        }
        //        result.ResultType = ResultType.Success;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Data = null;
        //        result.ResultType = ResultType.Exception;
        //        result.Exception = e;
        //        result.Message = e.GetOriginalException().Message;
        //    }
        //    return result;

        //}
    }
}
